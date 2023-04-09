#!/usr/bin/env pwsh
cd ../k8s

kuberctl cluster-info

Write-Host "Deploying to Kubernetes..."
helm install SAOVIET-PORTAL /helm

Write-Host "Getting pods..."
kubectl get pods

Write-Host "Getting services..."
kubectl get services