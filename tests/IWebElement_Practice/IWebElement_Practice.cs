using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace IWebElement_Practice;

public class IWebElement_Practice
{
    private IWebDriver webDriver;

    private WebDriverWait webDriverWait;

    private string projectDirectoryPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

    private string folderNameForScreenshots = "Screenshots";

    private string screenshotFileType = "png";

    /// <summary>
    /// Страница "Базовая программа".
    /// </summary>
    private string classicMortgageProgramURL = "https://ib.psbank.ru/store/products/classic-mortgage-program";

    private string mortgageObjectSelectXPath = "//label[contains(text(),'Объект ипотеки')]//ancestor::div[contains(@class,'mortgage-calculator-controls-select')]//mat-select";

    private string familyMortgageCardXPath = "//*[contains(text(),'Семейная ипотека')]//ancestor::div[contains(@class,'brands-cards__item')]";

    private string lifeInsuranceSwitchXPath = "//*[contains(text(),'Страхование жизни')]//ancestor::psb-switcher";

    private string loanPeriodInputXPath = "//*[@id='loanPeriod']//input";

    /// <summary>
    /// Страница "Семейная военная ипотека".
    /// </summary>
    private string militaryFamilyMortgageProgramURL = "https://ib.psbank.ru/store/products/military-family-mortgage-program";

    private string fillWithoutGosuslugiErrorXPath = "//*[contains(text(),'Оформление заявки станет доступным после заполнения обязательных полей')]";

    // Общие веб-элементы.

    private string fillWithoutGosuslugiButtonXPath = "//*[contains(text(),'Заполнить без Госуслуг')]//ancestor::button";
    
    private string fillWithGosuslugiButtonXPath = "//*[contains(text(),'Заполнить через Госуслуги')]//ancestor::button";
    private string fillWithGosuslugiButtonWrapperXPath = "//*[contains(text(),'Заполнить через Госуслуги')]//ancestor::rui-wrapper";

    private string continueButtonXPath = "//*[contains(text(),'Продолжить')]//ancestor::button";

    // Страница оформления заявки базовой программы без использования госуслуг.

    private string fillApplicationHeaderXpath =
        "  //h1[contains(text(), '{Оформить заявку}')]" +
        "| //h2[contains(text(), 'Оформить заявку')]" +
        "| //h3[contains(text(), 'Оформить заявку')]" +
        "| //h4[contains(text(), 'Оформить заявку')]" +
        "| //h5[contains(text(), 'Оформить заявку')]" +
        "| //h6[contains(text(), 'Оформить заявку')]";

    private string firstNameFieldXPath = "//input[@name='CardHolderFirstName']";
    private string middleNameFieldXPath = "//input[@name='CardHolderMiddleName']";
    private string lastNameFieldXPath = "//input[@name='CardHolderLastName']";
    private string maleCheckboxXPath = "//input[@id='formly_19_radio_Sex_0-0-input']/ancestor::rui-radio[@name='Sex']";
    private string birthDatFieldeXpath = "//rui-datepicker[@name='BirthDate']//input";
    private string phoneNumberFieldXPath = "//input[@name='Phone']";
    private string emailFieldXPath = "//input[@name='Email']";
    private string citizenshipFieldXPath = "//mat-select[@name='RussianFederationResident']";
    private string employmentStatusFieldXPath = "//mat-select[@name='RussianEmployment']";
    private string addressFieldXPath = "//mat-select[contains(@id,'OfficeId')]";
    private string processingPermissionCheckboxXPath = "//rui-checkbox[contains(@name,'PersonalDataProcessingAgreementConcent')]";
    private string creditBureauRequestPermissionCheckboxXpath = "//rui-checkbox[contains(@name,'BkiRequestAgreementConcent')]";

    private string russianCitizenshipOptionXPath = "//mat-option//*[text()='РФ']";
    private string employedOptionXPath = "//mat-option//*[text()='Есть']";
    private string kitayGorodPloshchadRevolyutsiiOptionXPath = "//mat-option//div[contains(text(),'м. Китай-город, Площадь Революции')]";

    [SetUp]
    public void Setup()
    {
        webDriver = new ChromeDriver();
        webDriverWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
    }

    /// <summary>
    /// 0. Создание скриншота в случае падения теста.
    /// </summary>
    [TearDown]
    public void Teardown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status.Equals(TestStatus.Failed))
        {
            TakeScreenshot();
        }
        webDriver.Dispose();
        webDriver.Quit();
    }

    private void TakeScreenshot()
    {
        string ScreenshotsFolderFullPath = projectDirectoryPath + Path.DirectorySeparatorChar + folderNameForScreenshots;
        Screenshot screenshot = ((ITakesScreenshot)webDriver).GetScreenshot();
        if (!Directory.Exists(ScreenshotsFolderFullPath))
        {
            Directory.CreateDirectory(ScreenshotsFolderFullPath);
        }
        string screenshotFilename = TestContext.CurrentContext.Test.MethodName + " - " + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss");
        screenshot.SaveAsFile(ScreenshotsFolderFullPath + Path.DirectorySeparatorChar + screenshotFilename + ".png");
    }

    private IWebElement GetWebElementByXPath(string XPath)
    {
        return webDriverWait.Until(driver => driver.FindElement(By.XPath(XPath)));
    }

    [Test(Description = "1. Поиск элементов на странице.")]
    public void FindingElements()
    {
        webDriver.Navigate().GoToUrl(classicMortgageProgramURL);

        Assert.DoesNotThrow(() =>
            webDriverWait.Until(driver => driver.FindElement(By.XPath(mortgageObjectSelectXPath))), "Нет выпадающего списка \"Объект ипотеки\".",
            webDriverWait.Until(driver => driver.FindElement(By.XPath(fillWithGosuslugiButtonXPath))), "Нет кнопки \"Заполнить через госуслуги\".",
            webDriverWait.Until(driver => driver.FindElement(By.XPath(familyMortgageCardXPath))), "Нет карточки с брендом \"Семейная ипотека\".",
            webDriverWait.Until(driver => driver.FindElement(By.XPath(lifeInsuranceSwitchXPath))), "Нет свитчера \"Страхование жизни\".",
            webDriverWait.Until(driver => driver.FindElement(By.XPath(loanPeriodInputXPath))), "Нет поля \"Срок кредита\"."
        );
    }

    [Test(Description = "1. Проверка создания скриншота в случае падения теста.")]
    public void CheckingFailure()
    {
        webDriver.Navigate().GoToUrl(classicMortgageProgramURL);
        string unknownElementsXPath = "/*[@data-testid]";

        Assert.DoesNotThrow(() => webDriverWait.Until(driver => driver.FindElement(By.XPath(unknownElementsXPath))));
    }

    [Test(Description = "2. Атрибуты элементов.")]
    public void CheckingElementsAttributes()
    {
        webDriver.Navigate().GoToUrl(classicMortgageProgramURL);

        Dictionary<IWebElement, string> webElements = new()
        {
            { webDriverWait.Until(driver => driver.FindElement(By.XPath(mortgageObjectSelectXPath))), "Выпадающий список \"Объект ипотеки\"" },
            { webDriverWait.Until(driver => driver.FindElement(By.XPath(fillWithGosuslugiButtonXPath))), "Кнопка \"Заполнить через госуслуги\"" },
            { webDriverWait.Until(driver => driver.FindElement(By.XPath(familyMortgageCardXPath))), "Карточка с брендом \"Семейная ипотека\""},
            { webDriverWait.Until(driver => driver.FindElement(By.XPath(lifeInsuranceSwitchXPath))), "Свитчер \"Страхование жизни\""},
            { webDriverWait.Until(driver => driver.FindElement(By.XPath(loanPeriodInputXPath))), "Поле \"Срок кредита\""}
        };

        foreach (KeyValuePair<IWebElement, string> webElement in webElements)
        {
            // Состояние активности.
            Assert.That(webElement.Key.Enabled, Is.True, "Элемент не активен - " + webElement.Value);
            // Состояние видимости.
            Assert.That(webElement.Key.Displayed, Is.True, "Элемент не отображается - " + webElement.Value);
        }

        // Текст поля выпадающего списка "Объект ипотеки"
        string mortgageObjectSelectText = webDriverWait.Until(driver => driver.FindElement(By.XPath(mortgageObjectSelectXPath))).Text;
        // Текст поля "Срок кредита"
        string loanPeriodInputValue = webDriverWait.Until(driver => driver.FindElement(By.XPath(loanPeriodInputXPath))).GetAttribute("value");
        // Состояние свитчера \Страхование жизни\
        string lifeInsuranceSwitchChecked = webDriverWait.Until(driver => driver.FindElement(By.XPath(lifeInsuranceSwitchXPath))).GetAttribute("checked");
        // Состояние карточки "Страхование жизни"
        bool familyMortgageCardXPathActive = webDriverWait.Until(driver => driver.FindElement(By.XPath(familyMortgageCardXPath))).GetAttribute("class").Contains("_active");
    }

    [Test(Description = "3. Ожидания элементов.")]
    public void ElementsAwaiting()
    {
        webDriver.Navigate().GoToUrl(militaryFamilyMortgageProgramURL);

        webDriverWait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

        IWebElement fillWithoutGosuslugiButton = webDriverWait.Until(driver => driver.FindElement(By.XPath(fillWithoutGosuslugiButtonXPath)));
        fillWithoutGosuslugiButton.Click();

        Assert.DoesNotThrow(() => webDriverWait.Until(driver => driver.FindElement(By.XPath(fillWithoutGosuslugiErrorXPath))));

        Thread.Sleep(5000);

        Assert.Throws<NoSuchElementException>(() => webDriver.FindElement(By.XPath(fillWithoutGosuslugiErrorXPath)));
        Assert.DoesNotThrow(() => webDriver.FindElement(By.XPath(fillWithoutGosuslugiButtonXPath)));
    }

    [Test(Description = "4. Действия с элементами.")]
    public void ActionsWithElements()
    {
        webDriver.Manage().Window.Maximize();

        webDriver.Navigate().GoToUrl(classicMortgageProgramURL);

        IWebElement fillWithGosuslugiButton = webDriverWait.Until(driver => driver.FindElement(By.XPath(fillWithoutGosuslugiButtonXPath)));
        fillWithGosuslugiButton.Click();

        webDriverWait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

        Assert.DoesNotThrow(() => webDriverWait.Until(driver => driver.FindElement(By.XPath(fillApplicationHeaderXpath))));

        IWebElement continueButton = webDriverWait.Until(driver => driver.FindElement(By.XPath(continueButtonXPath)));
        Assert.That(continueButton.Displayed, Is.True);
        Assert.That(continueButton.Enabled, Is.False);

        IWebElement firstNameField = webDriverWait.Until(driver => driver.FindElement(By.XPath(firstNameFieldXPath)));
        firstNameField.Click();
        firstNameField.SendKeys("Александр");

        IWebElement secondNameField = webDriverWait.Until(driver => driver.FindElement(By.XPath(middleNameFieldXPath)));
        secondNameField.Click();
        secondNameField.SendKeys("Сергеевич");

        IWebElement lastNameField = webDriverWait.Until(driver => driver.FindElement(By.XPath(lastNameFieldXPath)));
        lastNameField.Click();
        lastNameField.SendKeys("Пушкин");

        IWebElement birthDatField = webDriverWait.Until(driver => driver.FindElement(By.XPath(birthDatFieldeXpath)));
        birthDatField.Click();
        birthDatField.SendKeys("01012000");

        IWebElement phoneNumberField = webDriverWait.Until(driver => driver.FindElement(By.XPath(phoneNumberFieldXPath)));
        phoneNumberField.Click();
        phoneNumberField.SendKeys("9121231212");

        IWebElement emailField = webDriverWait.Until(driver => driver.FindElement(By.XPath(emailFieldXPath)));
        emailField.Click();
        emailField.SendKeys("123@mail.ru");

        webDriverWait.Until(driver => driver.FindElement(By.XPath(maleCheckboxXPath))).Click();

        IWebElement citizenshipField = webDriverWait.Until(driver => driver.FindElement(By.XPath(citizenshipFieldXPath)));
        citizenshipField.Click();
        webDriverWait.Until(driver => driver.FindElement(By.XPath(russianCitizenshipOptionXPath))).Click();

        IWebElement employmentStatusField = webDriverWait.Until(driver => driver.FindElement(By.XPath(employmentStatusFieldXPath)));
        employmentStatusField.Click();
        webDriverWait.Until(driver => driver.FindElement(By.XPath(employedOptionXPath))).Click();

        ((IJavaScriptExecutor)webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", continueButton);

        IWebElement addressField = webDriverWait.Until(driver => driver.FindElement(By.XPath(addressFieldXPath)));
        addressField.Click();
        IWebElement kitayGorodPloshchadRevolyutsiiOption = webDriverWait.Until(driver => driver.FindElement(By.XPath(kitayGorodPloshchadRevolyutsiiOptionXPath)));
        kitayGorodPloshchadRevolyutsiiOption.Click();

        webDriverWait.Until(driver => driver.FindElement(By.XPath(processingPermissionCheckboxXPath))).Click();

        webDriverWait.Until(driver => driver.FindElement(By.XPath(creditBureauRequestPermissionCheckboxXpath))).Click();

        Assert.That(continueButton.Enabled, Is.True);
    }

    [Test(Description = "5. Actions.")]
    public void CheckingElementsStyles()
    {
        webDriver.Navigate().GoToUrl(classicMortgageProgramURL);

        IWebElement fillWithGosuslugiButton = webDriverWait.Until(driver => driver.FindElement(By.XPath(fillWithGosuslugiButtonXPath)));
        
        Assert.That(fillWithGosuslugiButton.Enabled, Is.True);
        Assert.That(fillWithGosuslugiButton.Displayed, Is.True);

        IWebElement fillWithGosuslugiButtonWrapper = webDriverWait.Until(driver => driver.FindElement(By.XPath(fillWithGosuslugiButtonWrapperXPath)));
        Assert.That(fillWithGosuslugiButtonWrapper.GetCssValue("background-color"), Is.EquivalentTo("rgba(242, 97, 38, 1)"));
        
        Actions actions = new Actions(webDriver);
        actions.MoveToElement(fillWithGosuslugiButton).Perform();

        Assert.That(fillWithGosuslugiButtonWrapper.GetCssValue("background-color"), Is.EquivalentTo("rgba(213, 74, 33, 1)"));
    }
}