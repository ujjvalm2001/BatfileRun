# Console App for Running .bat Files

This is a simple .NET console application that runs a specified .bat file.

## Prerequisites

- .NET 9.0 or later

## Building the Project

Run the following command to build the project:

```
dotnet build
```

## Running the Application

To run the application, provide the path to the .bat file as an argument:

```
dotnet run "path/to/your/file.bat"
```

For example:

```
dotnet run "C:\Scripts\myScript.bat"
```

The application will execute the batch file and wait for it to complete.

## Project Structure

- `Program.cs`: The main entry point that handles running the .bat file.
- `consoleAppForBatRun.csproj`: The project file.