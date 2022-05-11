using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroKuApp.page;

namespace HeroKuApp.test;

public class HeroKuAppTest
{
    private IWebDriver driver;
    private WelcomePage welcomePage;
    private AddRemoveElementsPage addRemoveElementsPage;
    [SetUp]
    public void setUp() {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    [Test]
    public void addElementTest()
    {
        driver.Url = "https://the-internet.herokuapp.com/";
        welcomePage = new WelcomePage(driver);
        Assert.IsTrue(welcomePage.isPageLoaded());
        Assert.IsTrue(welcomePage.isAddRemoveElementsAvailable());
        Assert.IsTrue(welcomePage.isAddRemoveElementsAccessible());
        addRemoveElementsPage = new AddRemoveElementsPage(driver);
        Assert.IsTrue(addRemoveElementsPage.isAddElementButtonAccessible(5));
        Assert.IsTrue(addRemoveElementsPage.isAllAddedButtonsAvailable(5));

    }

    [TearDown]
    public void tearDown()
    {
        driver.Quit();
    }
}
