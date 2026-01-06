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
- ✅ **Rich Reporting**: Integrated with Allure Reports for beautiful, interactive HTML test reports with screenshots.
- ✅ **CI/CD Ready**: Includes GitHub Actions workflow for automated build validation.

---

## 🛠️ Tech Stack

| Category         | Tools/Libraries                    |
|------------------|-----------------------------------|
| Test Automation  | Playwright (.NET)                 |
| BDD Framework    | Reqnroll                          |
| Language         | C# (.NET 8+)                      |
| Design Pattern   | Page Object Model (POM)           |
| Reporting        | Allure Reports                    |
| Test Runner      | NUnit 4                           |
| CI/CD            | GitHub Actions                    |
| Build Tool       | dotnet CLI                        |

---

## 🚀 Getting Started

### 1. Prerequisites

- [.NET 8+ SDK](https://dotnet.microsoft.com/download)
- IDE: [VS Code](https://code.visualstudio.com/), [Visual Studio](https://visualstudio.microsoft.com/), or [JetBrains Rider](https://www.jetbrains.com/rider/)
- [Allure CLI](https://allurereport.org/docs/install/) (for generating reports)

### 2. Clone the Repository

```bash
git clone https://github.com/Suban5/PlaywrightReqnrollPOM.git
cd PlaywrightReqnrollPOM
```

### 3. Install Allure CLI

**Option A: Using Homebrew (macOS/Linux)**
```bash
brew install allure
```

**Option B: Using npm**
```bash
npm install -g allure-commandline
```

**Option C: Using Scoop (Windows)**
```powershell
scoop install allure
```

Verify installation:
```bash
allure --version
```

### 4. Install Dependencies & Browsers

```bash
# Note: need to be inside project folder
dotnet restore
dotnet build
pwsh bin/Debug/net8.0/playwright.ps1 install
```

### 5. Run Tests

**Run all tests:**
```bash
dotnet test
```

**Run specific tests by tag:**
```bash
dotnet test --filter "TestCategory=web"
```

### 6. Generate Allure Report

After running tests, generate and view the interactive Allure report:

```bash
# Generate and open report (recommended)
allure serve PlaywrightReqnrollFramework/bin/Debug/net8.0/allure-results/

# OR generate to specific folder
allure generate PlaywrightReqnrollFramework/bin/Debug/net8.0/allure-results/ -o allure-report --clean
allure open allure-report
```

**Using VS Code Tasks (Alternative):**
- Press `Cmd+Shift+P` (macOS) or `Ctrl+Shift+P` (Windows/Linux)
- Select "Tasks: Run Task"
- Choose "Test + Report (Full Flow)"

---

## 📂 Project Structure

```
PlaywrightReqnrollFramework/
├── Features/                  # Gherkin feature files (.feature)
│   ├── Login.feature
│   ├── ItemsCheckout.feature
│   └── Calculator.feature
├── Pages/                     # Page Object Model classes
│   ├── BasePage.cs
│   ├── LoginPage.cs
│   ├── InventoryPage.cs
│   ├── CheckoutPage.cs
│   └── PageFactory.cs
├── StepDefinitions/           # Step definitions for BDD scenarios
│   ├── LoginStepDef.cs
│   ├── InventoryStepDef.cs
│   └── BaseSteps.cs
├── Hook/                      # Test lifecycle hooks
│   ├── Hooks.cs               # Playwright initialization & cleanup
│   └── AllureReportHooks.cs   # Allure reporting hooks (screenshots)
├── Config/                    # Configuration and settings
│   ├── ConfigReader.cs
│   └── TestSettings.cs
├── Driver/                    # Browser driver management
│   └── PlaywrightDriver.cs
├── Helpers/                   # Helper utilities
│   └── AllureReportManager.cs
├── Model/                     # Data models
│   ├── ProductItem.cs
│   └── CheckoutDetails.cs
├── TestLogger/                # Custom test loggers
│   └── AllureReportOpenerLogger.cs
├── bin/Debug/net8.0/
│   └── allure-results/        # Allure test results (JSON files)
├── appsettings.json           # Default test configuration
├── ci.appsettings.json        # CI-specific configuration
├── dev.appsettings.json       # Development configuration
├── allureConfig.json          # Allure report configuration
├── reqnroll.json              # Reqnroll BDD configuration
└── PlaywrightReqnrollFramework.csproj
```

---

## 📊 Allure Reporting

### What's Included:

✅ **Beautiful Dashboard** - Overview with pass/fail statistics and trend graphs  
✅ **Test Suites** - Tests organized by feature files  
✅ **BDD Scenarios** - Given/When/Then steps with execution details  
✅ **Screenshots** - Automatically captured on test failures  
✅ **Timeline** - Visual timeline of test execution  
✅ **Categories** - Failure categorization and analysis  
✅ **History** - Track test trends over multiple runs  
✅ **Environment Info** - Display test environment configuration

### Report Location:

- **Raw Results**: `PlaywrightReqnrollFramework/bin/Debug/net8.0/allure-results/`
- **Generated Report**: Created by Allure CLI in temporary directory or specified output folder
- **CI Artifacts**: Reports and screenshots uploaded as build artifacts

### Screenshot Capture:

Screenshots are automatically captured on step failures and attached to the Allure report. The `AllureReportHooks.cs` handles this automatically using the `[AfterStep]` hook.

---

## 🤖 Continuous Integration

- **GitHub Actions**: Automated build workflow runs on every push and pull request.
- **Build Validation**: Ensures the project compiles successfully on both Ubuntu and Windows environments.
- **Status**: Test execution and reporting are currently disabled in CI/CD pipeline.

---

## 🎯 Writing Tests

### 1. Create a Feature File

Create a `.feature` file in the `Features/` folder:

```gherkin
@web
Feature: Login Functionality
  
  Scenario: Successful login with valid credentials
    Given I navigate to "https://www.saucedemo.com"
    When I login with username "standard_user" and password "secret_sauce"
    Then I should be redirected to the inventory page
```

### 2. Create Page Object

Create a page class in `Pages/` folder:

```csharp
public class LoginPage : BasePage
{
    public LoginPage(IPage page) : base(page) { }
    
    private ILocator UsernameInput => Page.Locator("#user-name");
    private ILocator PasswordInput => Page.Locator("#password");
    private ILocator LoginButton => Page.Locator("#login-button");
    
    public async Task LoginAsync(string username, string password)
    {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
    }
}
```

### 3. Implement Step Definitions

Create step definitions in `StepDefinitions/` folder:

```csharp
[Binding]
public class LoginStepDef : BaseSteps
{
    private readonly LoginPage _loginPage;
    
    public LoginStepDef(ScenarioContext scenarioContext) : base(scenarioContext)
    {
        _loginPage = PageFactory.GetLoginPage(Page);
    }
    
    [When(@"I login with username ""(.*)"" and password ""(.*)""")]
    public async Task WhenILoginWithUsernameAndPassword(string username, string password)
    {
        await _loginPage.LoginAsync(username, password);
    }
}
```

---

## 🧹 Clean Allure Results

To clean previous test results before a new run:

```bash
rm -rf PlaywrightReqnrollFramework/bin/Debug/net8.0/allure-results/*
```

Or use the VS Code task: "Clean Allure Results"

---

## 🤝 Contributing

Contributions are welcome! Please open issues or submit pull requests for improvements, bug fixes, or new features.

---

## 📚 Additional Resources

- [Playwright Documentation](https://playwright.dev/dotnet/)
- [Reqnroll Documentation](https://docs.reqnroll.net/)
- [Allure Report Documentation](https://allurereport.org/docs/)
- [NUnit Documentation](https://docs.nunit.org/)

---

**Happy Testing! 🚦**
