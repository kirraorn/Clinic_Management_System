# TheraHealthMacOS

This repository contains the TheraHealth MAUI app (macOS / mobile) and supporting libraries.

Contents:
- `Maui.TheraHealthOS/` — MAUI front-end app (Views, ViewModels, Platforms)
- `Library.TheraHealth/` — Shared models and services
- `CLI.TheraHealth/` — Command-line utilities

Quick start (local macOS development):

1. Ensure .NET SDK 8.0+ is installed and MAUI workloads set up.
2. From the repository root, build for Mac Catalyst (example):

   dotnet build -f net8.0-maccatalyst -r maccatalyst-arm64 -c Debug

3. To run the CLI project:

   dotnet run --project CLI.TheraHealth/CLI.TheraHealth.csproj

Creating the GitHub repository

- If you have the GitHub CLI (`gh`) installed and authenticated, from the repo root you can run:

  gh repo create <OWNER>/<REPO> --public --source=. --remote=origin --push -y

  Replace `<OWNER>/<REPO>` with your username and desired repo name, or omit to create under your account with the current folder name.

- Alternatively, create a new repo on github.com, then add the remote locally:

  git remote add origin https://github.com/<OWNER>/<REPO>.git
  git push -u origin main

Notes

- This repo uses MAUI and platform-specific targets; running on device or simulator may require additional tooling (Xcode, simulators).
- See `Maui.TheraHealthOS/README.md` for platform-specific setup if added.
