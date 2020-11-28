using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using WindowsFormsApp2.Class;

namespace hanbat_project.Class
{
    public class Selenium
    {

        public IWebDriver driver;
        private ChromeDriverService driverS;
        private ChromeOptions driver0;
        object main;

        #region [ Construct ]
        public Selenium(object main)
        {
            this.main = main;
        }

        #endregion

        #region [ open Chrome ]

        public void openChrome(String _url, String _cookie)
        {
            do
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

                driver.Navigate().GoToUrl("http://cyber.hanbat.ac.kr/Course.do?cmd=viewStudyHome&courseDTO.courseId=H020382002003200502513011&boardInfoDTO.boardInfoGubun=study_home&gubun=study_course");

                driver.Manage().Cookies.AddCookie(new OpenQA.Selenium.Cookie("RSN_JSESSIONID", Regex.Split(_cookie, "RSN_JSESSIONID=")[1]));

                driver.Navigate().Refresh();

                driver.SwitchTo().Window(driver.WindowHandles.First());

                DelayOp.Delay(2500);

                driver.FindElement(By.XPath("//*[@id=\"listBox\"]/table/tbody/tr[3]/td[2]/div[1]/ul[2]/li[2]/a[2]")).Click();

                DelayOp.Delay(2500);

                driver.SwitchTo().Window(driver.WindowHandles.Last());

                DelayOp.Delay(2500);

                driver.SwitchTo().Frame("bodyFrame");

                Actions action = new Actions(driver);

                IWebElement playbtn = driver.FindElement(By.XPath("//*[@id=\"movie_player\"]/div[4]/div"));

                action.MoveToElement(playbtn).Click(playbtn).Perform();

                break;

            } while (true);

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