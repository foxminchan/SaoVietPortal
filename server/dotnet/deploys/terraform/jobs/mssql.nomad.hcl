job "mssql" {
	datacenters = ["dc1"]
  type        = "service"

  group "mssql" {
    count = 1

    task "mssql" {
      driver = "docker"

      config {
        image = "microsoft/mssql-server-linux"
        volumes = ["local/mssql:/var/opt/mssql"]
        port_map {
          db = 1433
        }
      }

      resources {
        cpu    = 500
        memory = 1024
        network {
          mbits = 10
          port  "db"  {}
        }
      }

      service {
        name = "mssql"
        port = "db"
        check {
          name     = "alive"
          type     = "tcp"
          interval = "10s"
          timeout  = "2s"
        }
      }

			template {
				data = <<EOH
				ACCEPT_EULA=Y
				SA_PASSWORD=Password123
				EOH
				destination = "secrets/mssql.env"
			}

			template {
				data = <<EOH
				[program:mssql]
				command=/opt/mssql/bin/sqlservr
				autorestart=true
				redirect_stderr=true
				stdout_logfile=/var/log/supervisor/mssql.log
				environment=ACCEPT_EULA=Y,SA_PASSWORD=Password123
				EOH
				destination = "local/mssql/supervisord.conf"
			}
		}

		volume "local/mssql" {
			type = "host"
			read_only = false
			source = "/var/opt/mssql"
		}

		volume "secrets/mssql.env" {
			type = "host"
			read_only = false
			source = "/var/opt/mssql.env"
		}

		volume "local/mssql/supervisord.conf" {
			type = "host"
			read_only = false
			source = "/var/opt/mssql/supervisord.conf"
		}

	}
}