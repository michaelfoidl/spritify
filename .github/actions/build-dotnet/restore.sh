#!/bin/bash

set -euo pipefail

dotnet restore $1 --packages $2 --no-dependencies