targetScope = 'subscription'

@minLength(3)
@maxLength(11)
param resourceGroupName string = 'az_oss_rg'

param location string = deployment().location

resource newResourceGroup 'Microsoft.Resources/resourceGroups@2022-09-01' = {
  name: resourceGroupName
  location: location
}

module infrastructure 'infra.bicep' = {
  scope: newResourceGroup
  name: 'infra'
  params: {
    location: location
  }
}
