#!/bin/bash

set -euo pipefail

pwd
echo $1
echo $2
dotnet restore $1 --packages $2 --no-dependencies