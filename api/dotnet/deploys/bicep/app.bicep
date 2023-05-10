@description('Name of container registry. Default value is unique hashed ID with "SaoVietPortal" prefix.')
param registryName string = 'SaoVietPortal${uniqueString(resourceGroup().id)}'

@description('Name of Azure Kubernetes Service. Default value is unique hashed ID with "SaoVietPortal" prefix.')
param aksName string = 'saoviet'

resource containerRegistry 'Microsoft.ContainerRegistry/registries@2019-05-01' existing = {
  name: registryName
}

resource aksCluster 'Microsoft.ContainerService/managedClusters@2019-02-01' existing = {
  name: aksName
}

module mssql 'app/mssql.bicep' = {
  name: 'mssql'
  params: {
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}

module saovietportal 'app/api.bicep' = {
  name: 'api'
  params: {
    containerRegistry: containerRegistry.properties.loginServer
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}

module ingress 'app/ingress.bicep' = {
  name: 'ingress'
  params: {
    HTTPApplicationRoutingZoneName: aksCluster.properties.addonProfiles.httpApplicationRouting.config.HTTPApplicationRoutingZoneName
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}

module redis 'app/redis.bicep' = {
  name: 'redis'
  params: {
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}

module elk 'app/elk.bicep' = {
  name: 'elk'
  params: {
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}
