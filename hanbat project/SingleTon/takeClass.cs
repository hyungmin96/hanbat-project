using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using WindowsFormsApp2.Class;

namespace hanbat_project.Class
{
    public class takeClass
    {

        public IWebDriver driver = null;
        private ChromeDriverService driverS;
        private ChromeOptions driver0;

        public takeClass() { }

        public void openChrome(String _url, String _classId)
        {

            int _driverNum;

            try { _driverNum = driver.WindowHandles.Count; } catch (Exception ex) { _driverNum = 0; }

            if (_driverNum < 1)
            {
                driverS = ChromeDriverService.CreateDefaultService();
                driverS.HideCommandPromptWindow = true;
                driver0 = new ChromeOptions();

                driver0.AddArgument("--incognito");
                driver0.AddArgument("--window-position=0,0");
                driver0.AddExcludedArgument("enable-automation");
                driver0.AddAdditionalCapability("useAutomationExtension", false);
                driver0.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.75 Safari/537.36");
                driver0.AddArguments("--window-size=1000,1000");
                driver = new ChromeDriver(driverS, driver0);
            }

            _url = "http://cyber.hanbat.ac.kr/MLesson.do?cmd=viewStudyContentsForm&studyRecordDTO.lessonElementId="
                + _url + "&courseDTO.courseId=" + _classId + "";

            driver.Navigate().GoToUrl(_url);

            driver.Manage().Cookies.AddCookie(new OpenQA.Selenium.Cookie("RSN_JSESSIONID",
                                                Regex.Split(Regex.Split(Singleton.getInstance().getCookie().GetCookieHeader(new Uri(_url)),
                                                "RSN_JSESSIONID=")[1], ";")[0]));

            driver.Navigate().Refresh();

            driver.SwitchTo().Frame("bodyFrame");

            Actions action = new Actions(driver);

            Option.Delay(2000);

            IWebElement playbtn = driver.FindElement(By.XPath("//*[@id=\"movie_player\"]/div[4]/button"));

            action.MoveToElement(playbtn).Click(playbtn).Perform();

        }

    }

}