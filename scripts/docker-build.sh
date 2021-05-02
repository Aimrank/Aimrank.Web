#!/bin/bash

docker build -t ghcr.io/aimrank/aimrank-web:$1 -f Dockerfile .
