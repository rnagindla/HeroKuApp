using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroKuApp.page;
using OpenQA.Selenium.Remote;

namespace HeroKuApp.test;

public class HeroKuAppTest
{
    private IWebDriver driver;
    private WelcomePage welcomePage;
    private AddRemoveElementsPage addRemoveElementsPage;
    private Boolean runLocal;
    [SetUp]
    public void setUp() {
        String value = TestContext.Parameters["runLocal"];
        if (value != null)
        {
            runLocal = Boolean.Parse(value);
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
        Assert.IsTrue(welcomePage.isPageLoaded(),"");
        Assert.IsTrue(welcomePage.isAddRemoveElementsAvailable(),"");
        Assert.IsTrue(welcomePage.isAddRemoveElementsAccessible());
        addRemoveElementsPage = new AddRemoveElementsPage(driver);
        Assert.IsTrue(addRemoveElementsPage.isAddElementButtonAccessible(5));
        Assert.IsTrue(addRemoveElementsPage.isAllAddedButtonsAvailable(5),"");

    }

    [TearDown]
    public void tearDown()
    {
        driver.Quit();
    }
}
