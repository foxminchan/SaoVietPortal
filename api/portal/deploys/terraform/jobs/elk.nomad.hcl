job "elk" {
	datacenters = ["dc1"]
  type        = "service"

  group "elk" {
    count = 1

    task "elk" {
      driver = "docker"

      config {
        image = "sebp/elk"

        port_map {
          kibana = 5601
        }
      }

      resources {
        cpu    = 500
        memory = 1024

        network {
          mbits = 10
          port  "kibana" {}
        }
      }
    }
  }
}