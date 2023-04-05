@description('The kube config for the target Kubernetes cluster.')
@secure()
param kubeConfig string

import 'kubernetes@1.0.0' with {
  kubeConfig: kubeConfig
  namespace: 'default'
}

resource mssqlDeployment 'apps/Deployment@v1' = {
  metadata: {
    name: 'mssql'
    labels: {
      app: 'mssql'
    }
  }
  spec: {
    replicas: 1
    selector: {
      matchLabels: {
        app: 'mssql'
      }
    }
    template: {
      metadata: {
        labels: {
          app: 'mssql'
        }
      }
      spec: {
        containers: [
          {
            name: 'mssql'
            image: 'mcr.microsoft.com/mssql/server:2019-latest'
            ports: [
              {
                containerPort: 1433
              }
            ]
            env: [
              {
                name: 'ACCEPT_EULA'
                value: 'Y'
              }
              {
                name: 'SA_PASSWORD'
                value: 'P@ssw0rd'
              }
            ]
          }
        ]
      }
    }
  }
}

resource mssqlService 'core/Service@v1' = {
  metadata: {
    name: 'mssql'
    labels: {
      app: 'mssql'
    }
  }
  spec: {
    type: 'ClusterIP'
    ports: [
      {
        port: 1433
        targetPort: ''
        protocol: 'TCP'
      }
    ]
    selector: {
      app: 'mssql'
    }
  }
}
