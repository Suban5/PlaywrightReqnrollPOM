# PlaywrightReqnrollPOM 🚀  
**A Modern Automated Web Testing Framework with Playwright, Reqnroll & POM in C#**

A robust, scalable, and maintainable **web automation testing framework** that combines the power of:

- **Playwright** – Fast, reliable, and cross-browser automation.
- **Reqnroll** (the next generation of SpecFlow) – Behavior-Driven Development (BDD) for business-readable tests.
- **Page Object Model (POM)** – Clean separation between test logic and UI interactions for maintainability.

---

## ✨ Key Features

- ✅ **Cross-browser testing**: Run tests on Chromium, Firefox, and WebKit using Playwright.
- ✅ **BDD with Gherkin**: Write human-readable scenarios in plain English with Reqnroll.
- ✅ **Page Object Model (POM)**: Maintainable and reusable UI automation code.
- ✅ **Extensible & Scalable**: Easily add new tests or adapt to UI changes.
- ✅ **Rich Reporting**: Integrated with ExtentReports for beautiful HTML test reports.
- ✅ **CI/CD Ready**: Includes GitHub Actions workflow for automated test execution and artifact publishing.

---

## 🛠️ Tech Stack

| Category         | Tools/Libraries                    |
|------------------|-----------------------------------|
| Test Automation  | Playwright (.NET)                 |
| BDD Framework    | Reqnroll                          |
| Language         | C# (.NET 8+)                |
| Design Pattern   | Page Object Model (POM)           |
| Reporting        | ExtentReports                     |
| CI/CD            | GitHub Actions                    |
| Build Tool       | MSBuild / dotnet CLI              |

---

## 🚀 Getting Started

### 1. Prerequisites

- [.NET 8+ SDK](https://dotnet.microsoft.com/download)
- IDE: [VS Code](https://code.visualstudio.com/), [Visual Studio](https://visualstudio.microsoft.com/), or [JetBrains Rider](https://www.jetbrains.com/rider/)

### 2. Clone the Repository

```bash
git clone https://github.com/Suban5/PlaywrightReqnrollPOM.git
cd PlaywrightReqnrollPOM
```

### 3. Install Dependencies & Browsers

```bash
##Note: need to be inside project folder
dotnet restore
dotnet new tool-manifest # if you haven't already
dotnet tool install Microsoft.Playwright.CLI
dotnet tool restore
dotnet playwright install 
```

### 4. Build the Project

```bash
dotnet build
```

### 5. Run Tests

```bash
dotnet test --logger:"nunit;LogFilePath=TestResults/nunit-results.xml"
```

---

## 📂 Project Structure

```
PlaywrightReqnrollFramework/
├── Features/           # Gherkin feature files
├── Pages/              # Page Object Model classes
├── StepDefinitions/    # Step definitions for BDD
├── Config/             # Configuration and settings
├── TestResults/        # Test result outputs (gitignored)
├── appsettings.json    # Default configuration
├── appsettings.CI.json # CI-specific configuration
└── ...
```

---

## 📊 Reporting

- **ExtentReports**: After test execution, HTML reports are generated in `TestResults/ExtentReports/`.
- **Artifacts**: In CI, reports and screenshots are uploaded as build artifacts.

---

## 🤖 Continuous Integration

- **GitHub Actions**: Automated workflow runs on every push and pull request.
- **Artifacts**: Test results and reports are available for download after each run.

---

## 🤝 Contributing

Contributions are welcome! Please open issues or submit pull requests for improvements, bug fixes, or new features.

---

**Happy Testing! 🚦**
