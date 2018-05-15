#!/bin/bash
cd src/ExamApp.Api
dotnet restore
dotnet build
cd ../ExamApp.Core
dotnet restore
dotnet build
cd ../ExamApp.Infrastructure
dotnet restore
dotnet build