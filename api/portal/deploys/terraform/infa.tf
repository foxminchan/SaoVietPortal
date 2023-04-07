provider "nomad" {
	address = "http://localhost:4646"
	version = "~> 1.4.0"
}

resource "nomad_job" "redis" {
	jobspec = file("${path.module}/jobs/redis.nomad.hcl")
}

resource "nomad" "mssql" {
	jobspec = file("${path.module}/jobs/mssql.nomad.hcl")
}

resource "nomad" "elk" {
	jobspec = file("${path.module}/jobs/elk.nomad.hcl")
}

resource "nomad" "prometheus" {
	jobspec = file("${path.module}/jobs/prometheus.nomad.hcl")
}

resource "nomad" "grafana" {
	jobspec = file("${path.module}/jobs/grafana.nomad.hcl")
}

resource "nomad" "seq" {
	jobspec = file("${path.module}/jobs/seq.nomad.hcl")
}

resource "nomad" "otel" {
	jobspec = file("${path.module}/jobs/otel.nomad.hcl")
}