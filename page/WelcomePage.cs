using HeroKuApp.util;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroKuApp.page;
public class WelcomePage
{
    private IWebDriver driver;
    private WebDriverWait wait;
    [FindsBy(How = How.XPath, Using = ".//*[@href = '/add_remove_elements/']")]
    private IWebElement addRemoveElements;
    [FindsBy(How = How.XPath, Using = ".//*[@class= 'heading']")]
    private IWebElement header;
    [FindsBy(How = How.XPath, Using = "//div[@id='content']/h3")]
    private IWebElement addRemovePageHeader;

    public WelcomePage(IWebDriver driver) {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        PageFactory.InitElements(driver, this);
    }

   public Boolean isPageLoaded()
    {
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(".//*[@class= 'heading']")));
        return String.Equals(Constants.WELCOME_PAGE_HEADER, header.Text);
    }

    public Boolean isAddRemoveElementsAvailable()
    {
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(".//*[@href = '/add_remove_elements/']")));
        return String.Equals(Constants.ADD_REMOVE_ELEMENT, addRemoveElements.Text);
    }

    public Boolean isAddRemoveElementsAccessible()
    {
        addRemoveElements.Click();
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='content']/h3")));
        return String.Equals(Constants.ADD_REMOVE_ELEMENT, addRemovePageHeader.Text);
    }

}

