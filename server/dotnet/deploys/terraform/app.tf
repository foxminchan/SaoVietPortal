resource "nomad_job" "api" {
	jobspec = file("${path.module}/jobs/api.nomad.hcl")
}