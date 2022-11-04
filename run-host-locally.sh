#!/bin/bash

set -e

dotnet watch \
  --project ./Mumrich.Publisher.Host \
  --no-hot-reload \
  -- run -c Debug
