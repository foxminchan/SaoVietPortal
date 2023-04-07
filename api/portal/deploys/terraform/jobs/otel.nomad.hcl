job "otel-collector" {
	  datacenters = ["dc1"]

    group "otel-collector" {
        count = 1

        task "otel-collector" {
            driver = "docker"

            config {
                image = "otel/opentelemetry-collector-contrib-dev:latest"

                volumes = [
                    "local/config.yaml:/etc/otel/config.yaml",
                    "local/otel-collector.crt:/etc/otel/otel-collector.crt",
                    "local/otel-collector.key:/etc/otel/otel-collector.key",
                ]

                port_map {
                    otlp = 4317
                }

                args = [
                    "--config=/etc/otel/config.yaml",
                ]
            }

            resources {
                cpu    = 500
                memory = 1024

                network {
                    mbits = 10
                    port  "otlp" {}
                }
            }

						service {
								name = "otel-collector"
								port = "otlp"

								check {
										name     = "alive"
										type     = "tcp"
										interval = "10s"
										timeout  = "2s"
								}
						}

						template {
								data = <<EOH
								receivers:
								  otlp:
								    protocols:
								      grpc:
								        endpoint: 0.0.0.0:55678
											http:
												endpoint: 0.0.0.0:4317

								exporters:
									otlp:
										endpoint: "otel-collector.service.consul:4317"
										tls:
											cert_file: /etc/otel/otel-collector.crt
											key_file: /etc/otel/otel-collector.key
											insecure: true

								logging:
									level: debug

								service:
									pipelines:
										traces:
											receivers: [otlp]
											exporters: [otlp]
								EOH

								destination = "local/config.yaml"
						}
				}
		}
}