@description('The kube config for the target Kubernetes cluster.')
@secure()
param kubeConfig string

@description('Address of the container registry where container resides')
param containerRegistry string

@description('Tag of container to use')
param containerTag string = 'latest'

import 'kubernetes@1.0.0' with {
  kubeConfig: kubeConfig
  namespace: 'default'
}

resource api 'apps/Deployment@v1' = {
  metadata: {
    name: 'api'
    labels: {
      app: 'api'
    }
  }
  spec: {
    replicas: 1
    selector: {
      matchLabels: {
        app: 'api'
      }
    }
    template: {
      metadata: {
        labels: {
          app: 'api'
        }
      }
      spec: {
        containers: [
          {
            name: 'api'
            image: '${containerRegistry}/api:${containerTag}'
            imagePullPolicy: 'Always'
            ports: [
              {
                containerPort: 80
              }
            ]
            env: [
              {
                name: 'ASPNETCORE_ENVIRONMENT'
                value: 'Production'
              }
              {
                name: 'ASPNETCORE_URLS'
                value: 'http://+:80'
              }
              {
                name: 'ConnectionStrings__DefaultConnection'
                value: 'Server=database;Database=api;User Id=sa;Password=Password123!'
              }
            ]
          }
        ]
      }
    }
  }
}

resource apiService 'core/Service@v1' = {
  metadata: {
    name: 'api'
    labels: {
      app: 'api'
    }
  }
  spec: {
    type: 'LoadBalancer'
    ports: [
      {
        port: 80
        targetPort: ''
      }
    ]
    selector: {
      app: 'api'
    }
  }
}
