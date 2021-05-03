#!/bin/bash

MODULE_NAME="$1"
CONTEXT_NAME="$1Context"
MIGRATION_NAME="$2"

STARTUP_PROJECT="./src/Aimrank.Web.App"
TARGET_PROJECT="./src/Modules/$MODULE_NAME/Aimrank.Web.Modules.$MODULE_NAME.Infrastructure"

dotnet ef migrations add $MIGRATION_NAME --startup-project $STARTUP_PROJECT --project $TARGET_PROJECT --context $CONTEXT_NAME
