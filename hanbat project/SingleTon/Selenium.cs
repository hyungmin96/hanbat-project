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
    public class Selenium
    {

        public IWebDriver driver = null;
        private ChromeDriverService driverS;
        private ChromeOptions driver0;

        #region [ Constructor ]

        public Selenium() { }

        #endregion

        #region [ take a class ]

        public void openChrome(String _cookie, String _url, String _classId)
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
                driver0.AddArguments("--user-data-dir=C:\\Users\\" + GetUserName() + "\\AppData\\Local\\Google\\Chrome\\User Data\\");
                driver = new ChromeDriver(driverS, driver0);
            }

            driver.Navigate().GoToUrl("http://cyber.hanbat.ac.kr/MLesson.do?cmd=viewStudyContentsForm&studyRecordDTO.lessonElementId=" + _url + "&courseDTO.courseId=" + _classId + "");

            driver.Manage().Cookies.AddCookie(new OpenQA.Selenium.Cookie("RSN_JSESSIONID", Regex.Split(Regex.Split(_cookie, "RSN_JSESSIONID=")[1], ";")[0]));

            driver.Navigate().Refresh();

            driver.SwitchTo().Frame("bodyFrame");

            Actions action = new Actions(driver);

            waitElement();

            IWebElement playbtn = driver.FindElement(By.XPath("//*[@id=\"movie_player\"]/div[4]/button"));

            action.MoveToElement(playbtn).Click(playbtn).Perform();

        }

        #endregion

        #region [ Selenium Method ]

        private void inputKeys(IWebElement element, String str, int delay1, int delay2)
        {
            char[] charArray = str.ToCharArray();
            foreach (char word in charArray)
            {
                element.SendKeys(word.ToString());
                DelayOp.Delay(DelayOp.GetRandomNumber(delay1, delay2));
            }
        }

        public Tuple<String, String> waitNavigate(String Url)
        {
            if (driver.Url.ToString() != Url)
            {
                driver.Navigate().GoToUrl(Url);
                waitElement();
            }

            return Tuple.Create(getCookies(), driver.PageSource);
        }

        public String getCookies()
        {
            int index = 0;
            do
            {

                var info = driver.Manage().Cookies.AllCookies;
                StringBuilder cinfo = new StringBuilder();
                for (; index < info.Count - 1; index++)
                {
                    cinfo.Append(info[index].Name);
                    cinfo.Append("=");
                    cinfo.Append(info[index].Value);
                    cinfo.Append("; ");
                }
                return cinfo.ToString();

            } while (true);
        }

        public void waitElement()
        {
            try
            {
                driver.SwitchTo().Alert().Dismiss();
            }
            catch (Exception ex) { }

            WebDriverWait waitForElement = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            waitForElement.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            DelayOp.Delay(3000);

        }

        private String GetUserName()
        {
            String str = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string[] result = str.Split(new string[] { "\\" }, StringSplitOptions.None);
            return result[1];
        }

        #endregion

    }

}