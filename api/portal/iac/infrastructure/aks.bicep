@description('Azure region to deploy resources into. Defaults to location of target resource group')
param location string = resourceGroup().location

@description('Name of the Azure Kubernetes Service cluster. Defaults to a unique hash prefixed with "saoviet-"')
param aksClusterName string = 'saoviet'

@description('The size of the Virtual Machine.')
param vmSize string = 'Standard_HB176rs_v4'

@description('The number of nodes for the cluster.')
@minValue(1)
@maxValue(50)
param nodesCount int = 1

resource aksCluster 'Microsoft.ContainerService/managedClusters@2022-05-02-preview' = {
  name: aksClusterName
  location: location
  properties: {
    agentPoolProfiles: [
      {
        name: 'systempool'
        minCount: nodesCount
        maxCount: nodesCount
        osDiskSizeGB: 0
        vnetSubnetID: resourceId('Microsoft.Network/virtualNetworks/subnets', 'aks-vnet', 'aks-subnet')
        count: nodesCount
        vmSize: vmSize
        osType: 'Linux'
        mode: 'System'
        type: 'VirtualMachineScaleSets'
        enableAutoScaling: true
      }
    ]
    dnsPrefix: '${aksClusterName}-dns'
    enableRBAC: true
    addonProfiles: {
      httpApplicationRouting: {
        enabled: true
      }
    }
    workloadAutoScalerProfile: {
      keda: {
        enabled: true
      }
    }
  }
  identity: {
    type: 'SystemAssigned'
  }
}

output aksCluster string = aksCluster.name
