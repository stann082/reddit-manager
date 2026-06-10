# Reddit Comment Manager

A .NET-based application for searching, caching, and managing Reddit comments. This project provides both a command-line interface (CLI) and a web-based UI for interacting with Reddit data using custom OAuth2 authentication and MongoDB for persistent storage.

## 📋 Table of Contents

- [Features](#features)
- [Architecture](#architecture)
- [Prerequisites](#prerequisites)
- [Setup](#setup)
  - [Reddit OAuth Configuration](#reddit-oauth-configuration)
  - [MongoDB Setup](#mongodb-setup)
  - [CLI Setup](#cli-setup)
  - [Web UI Setup](#web-ui-setup)
- [Usage](#usage)
  - [CLI Commands](#cli-commands)
  - [Web Interface](#web-interface)
- [Development](#development)
- [Deployment](#deployment)
- [Project Structure](#project-structure)
- [Build & Test Status](#build--test-status)
- [Troubleshooting](#troubleshooting)
- [Advanced: SonarQube Analysis](#advanced-sonarqube-analysis)

## ✨ Features

### Core Functionality
- **Custom OAuth2 Reddit API Client** - Direct HTTP communication with Reddit API (no third-party libraries)
- **Comment Search** - Search Reddit comments with configurable sorting and time filters
- **Saved Comments** - Store and retrieve comments in MongoDB
- **Bulk Import** - Cache comments from archived Pushshift data
- **Web Dashboard** - Blazor-based UI for searching and managing comments
- **CLI Tools** - Command-line commands for automation and scripting

### Technical Highlights
- **OAuth2 Token Management** - Automatic token refresh and expiration tracking
- **Reddit Markdown Parser** - Parse and format Reddit comments with quotes and responses
- **Configurable Search** - Sort by relevance/new/top, time ranges, pagination
- **Database Integration** - MongoDB for persistent storage and querying
- **Structured Logging** - Serilog for comprehensive application logging
- **Security** - All vulnerabilities resolved, security scanning enabled

## 🏗️ Architecture

```
┌─────────────┐
│   CLI App   │  ┌─────────────┐
│  (search    │  │   Web UI    │
│   saved,    │  │  (Blazor)   │
│   cache)    │  └─────────────┘
└──────┬──────┘         │
       │                │
       └────────┬───────┘
                │
        ┌───────▼────────┐
        │   lib.dll      │
        │ (Services)     │
        └────────┬───────┘
                │
      ┌─────────┴──────────┐
      │                    │
  ┌───▼────────┐    ┌──────▼──────┐
  │ RedditAPI  │    │  MongoDB    │
  │ (OAuth2)   │    │  Storage    │
  └────────────┘    └─────────────┘
```

### Components

- **lib.dll** - Core library containing services and models
  - `RedditClient` - OAuth2 API communication
  - `RedditTokenProvider` - Token management
  - `SearchService` - Reddit API search
  - `SavedService` - MongoDB operations
  - `CacheService` - Bulk import functionality
  - `CommentParser` - Markdown parsing

- **cli.dll** - Command-line application
  - `SearchCommand` - Search Reddit comments
  - `SavedCommand` - Query saved comments
  - `CacheCommand` - Import archived data

- **web.dll** - Blazor web application
  - Pages: Index, Saved, Error
  - Components: SearchForm, AdvancedFilter, ResultDisplay, CommentCard

## 📦 Prerequisites

- **.NET 10.0** or later
- **MongoDB 5.0+** (local or remote instance)
- **Reddit API Credentials** (OAuth App)
- **PowerShell 5.0+** (for build scripts on Windows)

## 🔧 Setup

### Reddit OAuth Configuration

1. **Create a Reddit Application**
   - Navigate to https://www.reddit.com/prefs/apps
   - Click "Create Application"
   - Name: Any name (e.g., "Reddit Comment Manager")
   - Type: Select "script"
   - Description: (optional)
   - Redirect URI: `http://localhost` (required but not used in this implementation)
   - Click "Create app"

2. **Note Your Credentials**
   - **Client ID**: Under your app name (below the app icon)
   - **Client Secret**: Visible when you click "edit"

3. **Generate Refresh Token**
   - For the first-time setup, you need a refresh token
   - Use this script (requires `requests` library):

   ```python
   import requests
   from requests.auth import HTTPBasicAuth
   import webbrowser

   client_id = "YOUR_CLIENT_ID"
   client_secret = "YOUR_CLIENT_SECRET"
   redirect_uri = "http://localhost"

   # Step 1: Get authorization code
   auth_url = f"https://www.reddit.com/api/v1/authorize?client_id={client_id}&response_type=code&state=random&redirect_uri={redirect_uri}&scope=read"
   print(f"Visit: {auth_url}")
   webbrowser.open(auth_url)
   
   # Step 2: Copy the code from redirect URL
   auth_code = input("Paste the 'code' parameter from the redirect URL: ")

   # Step 3: Exchange for refresh token
   token_url = "https://www.reddit.com/api/v1/access_token"
   response = requests.post(
       token_url,
       auth=HTTPBasicAuth(client_id, client_secret),
       data={
           "grant_type": "authorization_code",
           "code": auth_code,
           "redirect_uri": redirect_uri
       },
       headers={"User-Agent": "MyApp/1.0 by username"}
   )
   
   print("Refresh Token:", response.json()["refresh_token"])
   ```

### MongoDB Setup

#### Option 1: Local MongoDB (Windows)
```powershell
# Download from: https://www.mongodb.com/try/download/community

# After installation, MongoDB runs as a Windows Service on port 27017
# Verify connection:
$mongoClient = [MongoDB.Driver.MongoClient]::new("mongodb://localhost:27017")
```

#### Option 2: Docker (Recommended)
```powershell
# Start MongoDB container
docker run -d -p 27017:27017 --name reddit-mongo mongo:latest

# Verify connection
docker exec reddit-mongo mongosh
```

#### Option 3: MongoDB Atlas (Cloud)
- Create account at https://www.mongodb.com/cloud/atlas
- Create a cluster and get connection string
- Update connection string in configuration

### CLI Setup

1. **Create Configuration File**
   - Location: `%APPDATA%\redditapi\config.json`
   - Contents:
   ```json
   {
     "app_id": "YOUR_CLIENT_ID",
     "refresh_token": "YOUR_REFRESH_TOKEN"
   }
   ```

2. **Build the CLI**
   ```powershell
   dotnet build src/cli/cli.csproj
   ```

3. **Add to PATH (Optional)**
   ```powershell
   $env:PATH += ";C:\Users\YourUsername\workspace\reddit\src\cli\bin\Debug\net10.0"
   ```

### Web UI Setup

1. **MongoDB Connection**
   - Edit `src/web/appsettings.json`:
   ```json
   {
     "Mongo": {
       "ConnectionString": "mongodb://localhost:27017"
     }
   }
   ```

2. **Build and Run**
   ```powershell
   dotnet run --project src/web/web.csproj
   ```

3. **Access the Application**
   - Open browser to: https://localhost:7260 (or displayed in console)

## 📖 Usage

### CLI Commands

#### Search Reddit Comments
```powershell
# Basic search
cli search --subreddit politics --query "election"

# Advanced search with options
cli search --subreddit AskAnAmerican --query "healthcare" --sort top --time month --limit 50

# Options:
# --subreddit, -s   : Subreddit to search (required)
# --query, -q        : Search query (required)
# --sort             : relevance (default), new, top, comments
# --time             : all (default), day, week, month, year
# --limit, -l        : Number of results (default: 25)
# --after            : Pagination cursor for next page
```

#### Query Saved Comments
```powershell
# List all saved comments
cli saved --subreddit AskAnAmerican

# Search with filter
cli saved --subreddit politics --query "voting"

# Options:
# --subreddit, -s   : Subreddit to filter (optional)
# --query, -q        : Search query (optional)
```

#### Import Cached Comments
```powershell
# Import Pushshift archive data
cli cache --path ".\data\user_comments\author\AskAnAmerican"

# Options:
# --path, -p        : Path to cached JSON files (required)
```

### Web Interface

1. **Home Page** (`/`)
   - Search Reddit in real-time
   - Configure search parameters
   - View comment details

2. **Saved Page** (`/saved`)
   - Browse saved comments
   - Filter by subreddit
   - Search within saved comments

3. **Advanced Filtering**
   - Time range selection
   - Sort options (relevance, newest, top)
   - Comment preview

## 👨‍💻 Development

### Project Structure
```
reddit/
├── src/
│   ├── cli/              # Command-line application
│   │   ├── App.cs
│   │   └── Program.cs
│   ├── lib/              # Core library
│   │   ├── commands/     # CLI commands
│   │   ├── options/      # CLI options
│   │   └── *.cs          # Services and models
│   └── web/              # Blazor web application
│       ├── Pages/        # Razor pages
│       ├── Components/   # Blazor components
│       └── Program.cs
├── tests/
│   └── lib.test/         # Unit tests
├── data/                 # Cached/archived comments
├── Dockerfile            # Docker image definition
├── deployment.yaml       # Kubernetes deployment
└── build.ps1            # PowerShell build script
```

### Building the Project

```powershell
# Build all projects
dotnet build reddit.slnx

# Build specific project
dotnet build src/lib/lib.csproj

# Release build
dotnet build -c Release
```

### Running Tests

```powershell
# Run all tests
dotnet test

# Run specific test file
dotnet test tests/lib.test/lib.test.csproj

# With coverage
dotnet test /p:CollectCoverage=true
```

### Code Style

- C# 12 conventions
- Async/await for I/O operations
- Dependency injection for services
- XML documentation for public APIs
- Interface-based design

### Adding New Commands

1. Create a new `*Options` class in `src/lib/options/`
2. Create a new `*Command` class in `src/lib/commands/`
3. Add parser configuration in `src/cli/App.cs`
4. Implement command logic using injected services

## 🚀 Deployment

### Docker

```powershell
# Build image
docker build -t reddit-app:latest .

# Run container
docker run -p 8080:80 -e "ASPNETCORE_ENVIRONMENT=Production" reddit-app:latest
```

### Kubernetes

```powershell
# Apply deployment
kubectl apply -f deployment.yaml

# Check status
kubectl get pods -l app=redditapp

# View logs
kubectl logs -l app=redditapp

# Access via port-forward
kubectl port-forward svc/redditapp 8080:80
```

### Environment Variables

- `ASPNETCORE_ENVIRONMENT` - Development/Production
- `PUSHSHIFT_PATH` - Path to cached comment data
- MongoDB connection string in `appsettings.json`

## 📁 Project Structure

| Directory | Purpose |
|-----------|---------|
| `src/lib` | Core business logic and services |
| `src/cli` | Command-line interface |
| `src/web` | Blazor web application |
| `tests` | Unit and integration tests |
| `data` | Cached/archived Reddit data |
| `.github/workflows` | CI/CD pipelines |

## ✅ Build & Test Status

- ✅ **Build**: Clean, no errors or warnings
- ✅ **Tests**: All 5 unit tests passing
- ✅ **Security**: No vulnerabilities detected
- ✅ **Code Quality**: SonarCloud integration enabled
- ✅ **Net Framework**: .NET 10.0

## 🔍 Troubleshooting

### MongoDB Connection Errors
```
Error: "No servers available in ServerSelectionException"
```
**Solution**: Ensure MongoDB is running on `localhost:27017`. Check:
- Docker container is running: `docker ps`
- Windows Service is running: Check Services app
- Connection string is correct in appsettings.json

### Reddit API Errors
```
Error: "401 Unauthorized"
```
**Solution**: Check credentials:
- Verify app_id and refresh_token in config.json
- Ensure no extra whitespace or special characters
- Regenerate refresh token if expired (6 months default)

### CLI Not Found
```
Error: "'cli' is not recognized as an internal or external command"
```
**Solution**: 
- Add CLI to PATH: `$env:PATH += ";C:\path\to\reddit\src\cli\bin\Debug\net10.0"`
- Or use full path: `C:\path\to\reddit\src\cli\bin\Debug\net10.0\cli.exe`

### Port Already in Use (Web)
```
Error: "Address already in use"
```
**Solution**: 
- Change port in `src/web/Properties/launchSettings.json`
- Or kill existing process: `netstat -ano | findstr :7260`

## 🔬 Advanced: SonarQube Analysis

### Local Setup

1. **Start SonarQube Container**
   ```powershell
   docker-compose -f .\etc\docker-compose.sonarqube.yml up -d
   ```

2. **Configure Project**
   - Navigate to http://localhost:9000/dashboard
   - Login: admin/admin
   - Create project with key "reddit-manager"
   - Generate authentication token

3. **Install Scanner**
   ```powershell
   dotnet tool install --global dotnet-sonarscanner
   ```

4. **Run Analysis**
   ```powershell
   dotnet-sonarscanner begin /k:"reddit-manager" /d:sonar.login="YOUR_TOKEN"
   dotnet build
   dotnet-sonarscanner end /d:sonar.login="YOUR_TOKEN"
   ```

### Cloud Analysis (SonarCloud)

The project is configured for automatic SonarCloud analysis on GitHub Actions:
- Triggers on: push to main, pull requests, manual workflow_dispatch
- Coverage reports: dotCover HTML format
- Results visible at: https://sonarcloud.io

## 📝 License

This project is provided as-is for educational and personal use.

## 🤝 Contributing

1. Create a feature branch
2. Make changes and add tests
3. Ensure all tests pass: `dotnet test`
4. Build succeeds: `dotnet build`
5. Submit pull request

## 📞 Support

For issues or questions:
1. Check the Troubleshooting section above
2. Review GitHub Issues
3. Check application logs in: `%APPDATA%\redditapi\logs\` 
