# Inventory Management System

A .NET 8.0 inventory management system with microservices architecture, featuring an Inventory Service and Notification Service with RabbitMQ messaging.

## How to Connect Visual Studio Code to Git

This guide will help you set up Visual Studio Code (VS Code) to work seamlessly with Git for this project.

### Prerequisites

Before you begin, ensure you have the following installed:

1. **Git** - Download from [git-scm.com](https://git-scm.com/)
2. **Visual Studio Code** - Download from [code.visualstudio.com](https://code.visualstudio.com/)
3. **.NET 8.0 SDK** - Download from [dotnet.microsoft.com](https://dotnet.microsoft.com/download)

### Step 1: Install Git

#### Windows
```bash
# Using winget
winget install Git.Git

# Or download installer from git-scm.com
```

#### macOS
```bash
# Using Homebrew
brew install git

# Or download installer from git-scm.com
```

#### Linux (Ubuntu/Debian)
```bash
sudo apt update
sudo apt install git
```

### Step 2: Configure Git

Open a terminal and configure your Git identity:

```bash
git config --global user.name "Your Name"
git config --global user.email "your.email@example.com"
```

### Step 3: Clone or Open Repository in VS Code

#### Option A: Clone Repository
```bash
# Clone the repository
git clone https://github.com/malkucha/InventoryChallenge.git

# Open in VS Code
cd InventoryChallenge
code .
```

#### Option B: Open Existing Repository
If you already have the repository:
```bash
# Navigate to project directory
cd path/to/InventoryChallenge

# Open in VS Code
code .
```

#### Option C: Using VS Code GUI
1. Open VS Code
2. Press `Ctrl+Shift+P` (Windows/Linux) or `Cmd+Shift+P` (macOS)
3. Type "Git: Clone" and select it
4. Enter repository URL: `https://github.com/malkucha/InventoryChallenge.git`
5. Choose a local folder to clone into

### Step 4: Install Essential VS Code Extensions

VS Code will prompt you to install recommended extensions. You can also install them manually:

#### Git and Version Control Extensions:
- **GitLens** - Supercharges Git in VS Code
- **Git Graph** - View git log in a visual graph
- **Git History** - View and search git log

#### .NET Development Extensions:
- **C# Dev Kit** - Official C# extension pack
- **NuGet Gallery** - Browse and install NuGet packages
- **REST Client** - Test APIs directly in VS Code

Install extensions by:
1. Press `Ctrl+Shift+X` to open Extensions view
2. Search for extension name
3. Click "Install"

### Step 5: VS Code Git Integration Features

#### Built-in Git Features:

1. **Source Control Panel**
   - Press `Ctrl+Shift+G` to open Source Control
   - View file changes, stage/unstage files
   - Commit changes with messages

2. **Git Status in Status Bar**
   - Current branch shown in bottom-left corner
   - Click to switch branches or create new ones

3. **File Decorations**
   - Modified files show "M" in Explorer
   - New files show "U" (untracked)
   - Green = added, red = deleted, yellow = modified

#### Common Git Operations in VS Code:

##### Viewing Changes
- **View file diff**: Click on modified file in Source Control panel
- **View all changes**: `Ctrl+Shift+G` → expand "Changes" section

##### Staging and Committing
```bash
# In VS Code Source Control panel:
# 1. Click "+" next to files to stage them
# 2. Type commit message in text box
# 3. Click "✓" to commit or press Ctrl+Enter
```

##### Branch Management
```bash
# Click branch name in status bar or use Command Palette:
# Ctrl+Shift+P → "Git: Create Branch"
# Ctrl+Shift+P → "Git: Checkout to..."
```

##### Syncing Changes
```bash
# Click sync icon in status bar or:
# Ctrl+Shift+P → "Git: Pull"
# Ctrl+Shift+P → "Git: Push"
```

### Step 6: Working with Remote Repositories

#### Setting up Authentication

##### For HTTPS (recommended):
1. Use personal access token instead of password
2. VS Code will prompt for credentials automatically
3. Or use Git Credential Manager (installed with Git)

##### For SSH:
```bash
# Generate SSH key
ssh-keygen -t ed25519 -C "your.email@example.com"

# Add to ssh-agent
ssh-add ~/.ssh/id_ed25519

# Copy public key to GitHub
cat ~/.ssh/id_ed25519.pub
# Paste in GitHub Settings → SSH and GPG keys
```

#### Common Remote Operations:
```bash
# Fetch latest changes
git fetch

# Pull changes from remote
git pull

# Push local commits
git push

# Push new branch
git push -u origin branch-name
```

### Step 7: Project-Specific Setup

#### Build and Run the Project:
```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the Inventory Service
cd InventoryService
dotnet run

# Run the Notification Service (in another terminal)
cd NotificationService
dotnet run
```

#### Using VS Code Tasks:
The project includes VS Code tasks for common operations:
- `Ctrl+Shift+P` → "Tasks: Run Task"
- Available tasks: Build, Clean, Restore, Test

#### Debugging:
1. Open a `.cs` file
2. Set breakpoints by clicking in the gutter
3. Press `F5` to start debugging
4. Use Debug Console for interactive debugging

### Step 8: Git Workflow Best Practices

#### Recommended Workflow:
1. **Create feature branch**: `git checkout -b feature/new-feature`
2. **Make changes** and **commit frequently** with descriptive messages
3. **Push branch**: `git push -u origin feature/new-feature`
4. **Create Pull Request** on GitHub
5. **Merge after review**
6. **Delete feature branch** after merge

#### Commit Message Conventions:
```
feat: add new inventory item validation
fix: resolve null reference in product service
docs: update README with setup instructions
refactor: simplify product mapping logic
test: add unit tests for inventory controller
```

### Troubleshooting

#### Common Issues:

1. **Git not recognized**
   - Restart VS Code after installing Git
   - Add Git to PATH environment variable

2. **Authentication failures**
   - Use personal access token instead of password
   - Set up Git Credential Manager

3. **Line ending issues (Windows)**
   ```bash
   git config --global core.autocrlf true
   ```

4. **VS Code not detecting Git repository**
   - Ensure you opened the root folder containing `.git`
   - Check that Git is properly installed

#### Useful Commands:
```bash
# Check Git status
git status

# View commit history
git log --oneline

# Check Git configuration
git config --list

# Check remote repositories
git remote -v
```

### Additional Resources

- [VS Code Git Documentation](https://code.visualstudio.com/docs/editor/versioncontrol)
- [GitLens Extension Documentation](https://gitlens.amod.io/)
- [Git Official Documentation](https://git-scm.com/doc)
- [GitHub Git Handbook](https://guides.github.com/introduction/git-handbook/)

---

## Project Architecture

This inventory management system consists of:

- **InventoryService**: Web API for managing inventory items
- **NotificationService**: Background service for processing notifications
- **Business**: Business logic layer
- **DataAccess**: Data access layer with Entity Framework
- **Domain**: Domain models and entities

## Technology Stack

- .NET 8.0
- Entity Framework Core 8.0
- RabbitMQ for messaging
- SQLite database
- AutoMapper
- FluentValidation
- Swagger/OpenAPI

## Getting Started

1. Clone the repository
2. Install .NET 8.0 SDK
3. Run `dotnet restore`
4. Run `dotnet build`
5. Start the services with `dotnet run`

For detailed VS Code setup, see the "How to Connect Visual Studio Code to Git" section above.