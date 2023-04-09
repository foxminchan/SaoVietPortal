@description('The kube config for the target Kubernetes cluster.')
@secure()
param kubeConfig string

import 'kubernetes@1.0.0' with {
  kubeConfig: kubeConfig
  namespace: 'default'
}

resource elkDeployment 'apps/Deployment@v1' = {
  metadata: {
    name: 'elk'
    labels: {
      app: 'elk'
    }
  }

  spec: {
    replicas: 1
    selector: {
      matchLabels: {
        app: 'portlal'
        service: 'elk'
      }
    }
    template: {
      metadata: {
        labels: {
          app: 'portlal'
          service: 'elk'
        }
      }
      spec: {
        containers: [
          {
            name: 'elk'
            image: 'sebp/elk:latest'
            ports: [
              {
                name: 'kibana'
                containerPort: 5601
                protocol: 'TCP'
              }
              {
                name: 'elasticsearch'
                containerPort: 9200
                protocol: 'TCP'
              }
              {
                name: 'logstash'
                containerPort: 5044
                protocol: 'TCP'
              }
            ]
            env: [
              {
                name: 'TZ'
                value: 'Asia/Ho_Chi_Minh'
              }
              {
                name: 'ES_JAVA_OPTS'
                value: '-Xms512m -Xmx512m'
              }
              {
                name: 'LOGSTASH_START'
                value: '0'
              }
              {
                name: 'LOGSTASH_HEAP_SIZE'
                value: '512m'
              }
              {
                name: 'KIBANA_START'
                value: '0'
              }
              {
                name: 'KIBANA_HEAP_SIZE'
                value: '512m'
              }
            ]
            resources: {
              requests: {
                memory: '1Gi'
                cpu: '500m'
              }
              limits: {
                memory: '1Gi'
                cpu: '500m'
              }
            }
            volumeMounts: [
              {
                name: 'elk-data'
                mountPath: '/var/lib/elasticsearch'
              }
              {
                name: 'elk-log'
                mountPath: '/var/log/elasticsearch'
              }
              {
                name: 'elk-logstash'
                mountPath: '/var/log/logstash'
              }
              {
                name: 'elk-kibana'
                mountPath: '/var/log/kibana'
              }
            ]
          }
        ]
        volumes: [
          {
            name: 'elk-data'
            emptyDir: {}
          }
          {
            name: 'elk-log'
            emptyDir: {}
          }
          {
            name: 'elk-logstash'
            emptyDir: {}
          }
          {
            name: 'elk-kibana'
            emptyDir: {}
          }
        ]
      }

    }
  }
}

resource elkService 'core/Service@v1' = {
  metadata: {
    name: 'elk'
    labels: {
      app: 'elk'
    }
  }
  spec: {
    type: 'NodePort'
    ports: [
      {
        name: 'kibana'
        port: 5601
        targetPort: ''
        protocol: 'TCP'
      }
      {
        name: 'elasticsearch'
        port: 9200
        targetPort: ''
        protocol: 'TCP'
      }
      {
        name: 'logstash'
        port: 5044
        targetPort: ''
        protocol: 'TCP'
      }
    ]
    selector: {
      app: 'portlal'
      service: 'elk'
    }
  }
}
