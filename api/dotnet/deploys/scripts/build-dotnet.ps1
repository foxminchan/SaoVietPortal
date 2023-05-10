#!/usr/bin/env pwsh
$TAG = $(git rev-parse --short HEAD)
Write-Host "Building image with tag: $TAG"

$NAMESPACE = "saovietportal"
Write-Host "Using namespace: $NAMESPACE"

Write-Host "Building image..."
docker build -f ../src/Portal.Api/Dockerfile -t $NAMESPACE/portal.api:$TAG ./src/Portal.Api

Write-Host "Composing docker-compose.yml..."
docker compose up -d -f ./docker/docker-compose.yml
