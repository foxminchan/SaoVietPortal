job "redis" {
	datacenters = ["dc1"]
  type        = "service"

  group "cache" {
    count = 3

    task "redis" {
      driver = "docker"

      config {
        image = "redis:3.2"
      }

      resources {
        cpu    = 500
        memory = 256

        network {
          port "db" {}
					mode = "host"
        }

				port "tcp" {
					to = 6379
					static = 6379
				}
      }

      service {
        name = "redis-cache"
        port = "db"

        check {
          type     = "tcp"
          interval = "10s"
          timeout  = "2s"
        }
      }
    }
  }
}