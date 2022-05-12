using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroKuApp.page;
using OpenQA.Selenium.Remote;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace HeroKuApp.test;

public class HeroKuAppTest
{
    private IWebDriver driver;
    private WelcomePage welcomePage;
    private AddRemoveElementsPage addRemoveElementsPage;
    private bool runLocal;
    private ExtentReports extentReport;
    private ExtentTest extentTest;

    [OneTimeSetUp]
    public void initReport()
    {
        string workingDirectory = Environment.CurrentDirectory;
        string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        String reportPath = projectDirectory + "//automationreport.html";

        extentReport = new ExtentReports();
        extentReport.AttachReporter(new ExtentSparkReporter(reportPath));

    }

    [SetUp]
    public void setUp() {

        extentTest = extentReport.CreateTest(TestContext.CurrentContext.Test.Name);

        String value = TestContext.Parameters["runLocal"];
        if (value != null)
        {
            runLocal = bool.Parse(value);
        }
        if (runLocal)
        {
            driver = new ChromeDriver();
        }
        else
        {
           
        var sauceOptions = new Dictionary<string, object>
        {
             ["name"] = TestContext.CurrentContext.Test.Name
        };

        var chromeOptions = new ChromeOptions
        {
             BrowserVersion = "latest",
             PlatformName = "Windows 10"
        };
             chromeOptions.AddAdditionalOption("sauce:options", sauceOptions);
             driver = new RemoteWebDriver(new Uri("https://oauth-ramesh.be231-1b284:df659765-3082-4b4a-9c30-1f100ae5fad7@ondemand.us-west-1.saucelabs.com:443/wd/hub"), chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(30));  
        }
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    [Test]
    public void addElementTest()
    {
        driver.Url = "https://the-internet.herokuapp.com/";
        welcomePage = new WelcomePage(driver);
        Assert.IsTrue(welcomePage.isPageLoaded(), "Welcome to the-internet page not loaded");
        extentTest.Log(Status.Info, "Welcome to the-internet page", getScreenShot());
        Assert.IsTrue(welcomePage.isAddRemoveElementsAvailable(), "Add/Remove Elements link not available");
        Assert.IsTrue(welcomePage.isAddRemoveElementsAccessible(), "Unable to access Add/Remove Elements page");
        extentTest.Log(Status.Pass, "Add/Remove Elements page", getScreenShot());
        addRemoveElementsPage = new AddRemoveElementsPage(driver);
        Assert.IsTrue(addRemoveElementsPage.isAddElementButtonAccessible(5),"Unable to add new Elements");
        extentTest.Log(Status.Pass, "Add/Remove Elements page: Adding new Elements", getScreenShot());
        Assert.IsTrue(addRemoveElementsPage.isAllAddedButtonsAvailable(5), "Unable to validate new Elements availability");
        extentTest.Log(Status.Pass, "New elements availability check");

    }

    public AventStack.ExtentReports.Model.Media getScreenShot()
    {
        DateTime time = DateTime.Now;
        string fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
        ITakesScreenshot ts = (ITakesScreenshot)driver;
        var shot = ts.GetScreenshot().AsBase64EncodedString;
        return MediaEntityBuilder.CreateScreenCaptureFromBase64String(shot, fileName).Build();
       
    }

    [TearDown]
    public void cleanUp()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;
        if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
        {
            extentTest.Pass("Test execution: Passed", getScreenShot());

        }
        else if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            extentTest.Pass("Test execution: Failed", getScreenShot());
        }
        else
        {
            extentTest.Skip("Test execution: Skipped");
        }
        extentReport.Flush();
        driver.Quit();
    }
}
