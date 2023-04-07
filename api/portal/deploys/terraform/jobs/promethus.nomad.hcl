job "prometheus" {
	datacenters = ["dc1"]
  type        = "service"

  group "prometheus" {
    count = 2

    task "server" {
      driver = "docker"

      config {
        image = "prom/prometheus:latest"
        port_map {
          http = 9090
        }
      }

      service {
        name = "prometheus"
        port = "http"

        check {
          type     = "http"
          path     = "/-/ready"
          interval = "10s"
          timeout  = "2s"
        }
      }

      resources {
        cpu    = 500
        memory = 512

        network {
          mbits = 10
          port  "http"{}
        }
      }
    }
  }
}