#!/bin/bash

CONTEXT_NAME="$1"
MIGRATION_NAME="$2"

dotnet ef migrations add $MIGRATION_NAME --startup-project ./src/Aimrank.Web.App --project ./src/Database/Aimrank.Web.Database.Migrator --context $CONTEXT_NAME
