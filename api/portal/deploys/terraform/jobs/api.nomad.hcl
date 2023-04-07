job "saoviet-portal" {
	datacenters = ["dc1"]

  group "saoviet-portal" {
		network {
			mode = "bridge"
			port "http" { to = 8080 }
			port "app" { to = 8081 }
		}

		service {
			name = "saoviet-portal"
			port = "http"
			tags = ["saoviet-portal"]
			check {
				name = "alive"
				type = "http"
				path = "/health"
				interval = "10s"
				timeout = "2s"
			}

			connect {
				sidecar_service {}
			}

			task "saoviet-portal" {
				driver = "docker"
				config {
					image = "saoviet/portal:latest"
					port_map {
						http = 8080
						app = 8081
					}
				}
				resources {
					cpu = 500
					memory = 1024
					network {
						mbits = 10
						port "http" {}
						port "app" {}
					}
				}
			}
		}
  }
}