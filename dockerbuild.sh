#!/bin/bash

sudo docker build -t arcade-blazor-client -f docker/arcade-blazor-client.Dockerfile .
sudo docker tag arcade-blazor-client 10.101.153.109:5000/arcade-blazor-client
sudo docker push 10.101.153.109:5000/arcade-blazor-client
