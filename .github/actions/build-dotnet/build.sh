#!/bin/bash

set -euo pipefail

dotnet build $1 --configuration Release --verbosity minimal --no-restore --source $2