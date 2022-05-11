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
public class AddRemoveElementsPage
{
    private IWebDriver driver;
    private WebDriverWait wait;
    [FindsBy(How = How.XPath, Using = "//div[@id='content']//button[@onclick='addElement()']")]
    private IWebElement addElement;

    public AddRemoveElementsPage(IWebDriver driver) {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        PageFactory.InitElements(driver, this);
    }

    public Boolean isAddElementButtonAccessible(int count)
    {
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='content']//button[@onclick='addElement()']")));
        if (String.Equals(Constants.ADD_ELEMENT, addElement.Text))
        {
            for (int i = 0; i < count; i++)
            {
                addElement.Click();
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public Boolean isAllAddedButtonsAvailable(int count)
    {
        bool isAvailable = false;
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='elements']/button")));
        IList<IWebElement> deleteButtons = driver.FindElements(By.XPath("//div[@id='elements']/button"));
        int x = deleteButtons.Count();
        if (count == x) {
            for (int i = 0; i < count; i++)
            {
                if (deleteButtons[i] != null && String.Equals(Constants.DELETE_BUTTON, deleteButtons[i].Text)) {
                    isAvailable = true;
                }
                else
                {
                    isAvailable = false;
                    break;
                }
            }
            
        }
        return isAvailable;
    }
}

