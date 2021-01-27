#!/bin/bash

MIGRATION_NAME="$1"

dotnet ef --startup-project ./src/Aimrank.Web --project ./src/Database/Aimrank.Database.Migrator migrations add $MIGRATION_NAME
