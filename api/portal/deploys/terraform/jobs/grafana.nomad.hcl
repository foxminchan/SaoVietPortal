job "grafana" {
	datacenters = ["dc1"]
  type        = "service"

  group "grafana" {
    count = 1

    task "grafana" {
      driver = "docker"

      config {
        image = "grafana/grafana"

        port_map {
          http = 3000
        }
      }

      resources {
        network {
          port "http" {}
        }
      }

      service {
        name = "grafana"
        port = "http"

        check {
          type     = "http"
          path     = "/"
          interval = "10s"
          timeout  = "2s"
        }
      }
    }
  }
}