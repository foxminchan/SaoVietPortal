#!/usr/bin/env pwsh
docker rmi $(docker images -a -q)
docker rm $(docker ps -a -q)