#!/bin/bash

docker build -t mariuszba/aimrank-web:$1 -f Dockerfile .
