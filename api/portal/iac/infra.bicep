param location string = resourceGroup().location

@description('Which mode to deploy the infrastructure. Defaults to cloud, which deploys everything. The mode dev only deploys the resources needed for local development.')
@allowed([
  'cloud'
  'dev'
])
param mode string = 'cloud'

module registry 'infrastructure/container-registry.bicep' = {
  name: 'registry'
  params: {
    location: location
  }
}

module aks 'infrastructure/aks.bicep' = if (mode == 'cloud') {
  name: 'aks'
  params: {
    location: location
  }
}
