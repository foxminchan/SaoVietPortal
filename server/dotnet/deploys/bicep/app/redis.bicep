@description('The kube config for the target Kubernetes cluster.')
@secure()
param kubeConfig string

import 'kubernetes@1.0.0' with {
  kubeConfig: kubeConfig
  namespace: 'default'
}

resource redisDeployment 'apps/Deployment@v1' = {
  metadata: {
    name: 'redis'
    labels: {
      app: 'redis'
    }
  }

  spec: {
    replicas: 1
    selector: {
      matchLabels: {
        app: 'redis'
      }
    }

    template: {
      metadata: {
        labels: {
          app: 'redis'
        }
      }
      spec: {
        containers: [
          {
            name: 'redis'
            image: 'redis:alpine'
            ports: [
              {
                containerPort: 6379
              }
            ]
          }
        ]
      }
    }
  }
}

resource redisService 'core/Service@v1' = {
  metadata: {
    name: 'redis'
    labels: {
      app: 'redis'
    }
  }

  spec: {
    ports: [
      {
        port: 6379
        targetPort: ''
        protocol: 'TCP'
      }
    ]
    selector: {
      app: 'redis'
    }
  }
}
