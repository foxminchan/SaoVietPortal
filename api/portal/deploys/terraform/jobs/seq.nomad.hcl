job "seq" {
	datacenters = ["dc1"]
  type        = "batch"

  group "seq" {
    count = 1

    task "seq" {
      driver = "raw_exec"

      config {
        command = "seq"
        args    = ["1", "10000000"]
      }

      resources {
        cpu    = 500
        memory = 256
      }
    }
  }
}