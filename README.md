
# Text Analyzer

ASP.NET MVC app that counts letters in submitted text and shows each letter’s percentage of the total.

## Installation

1. Install the .NET 10 SDK.
2. Clone or unzip the project.
3. Restore and build:
   ```bash
   dotnet restore
   dotnet build

## Running Tests
dotnet test

## Assumptions

Only letters are counted. Numbers special characters and whitespace are ignored.
Matching is case-insensitive.