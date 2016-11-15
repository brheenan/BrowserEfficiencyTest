//--------------------------------------------------------------
//
// Browser Efficiency Test
// Copyright(c) Microsoft Corporation
// All rights reserved.
//
// MIT License
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files(the ""Software""),
// to deal in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS
// OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF
// OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//--------------------------------------------------------------

using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace BrowserEfficiencyTest
{
    internal class HoblWorkload : Scenario
    {
        List<string> backgroundUrls = new List<string>
        {
            "https://www.amazon.com/s/ref=nb_sb_noss_2?url=search-alias%3Daps&field-keywords=surface",
            "https://www.yahoo.com/",
            "https://www.facebook.com/",
        };

        List<string> urls = new List<string>
        {
            "https://www.google.com/?gws_rd=ssl#q=surface",
            "https://www.youtube.com/watch?v=L7AxNcg8RXE&t=1s",
            "https://www.amazon.com/s/ref=nb_sb_noss_2?url=search-alias%3Daps&field-keywords=surface",
            "https://www.yahoo.com/",
            "https://en.wikipedia.org/wiki/Special:Search?search=surfce&go=Go&searchToken=5il6yk3lilmjr8t34bh7dhiz1",
            "http://www.ebay.com/sch/i.html?_from=R40&_trksid=p2050601.m570.l1313.TR12.TRC2.A0.H0.Xsurface.TRS0&_nkw=surface&_sacat=0",
            "https://twitter.com/",
            "https://www.reddit.com/",
            "https://www.netflix.com/browse",
            "http://seattle.craigslist.org/search/sss?sort=rel&query=surface",
            "https://www.linkedin.com/in/satya-nadella-3145136?trk=sushi_topic_professionals_guest_button",
            "https://www.pinterest.com/",
            "https://www.outlook.com",
            "https://www.bing.com/search?q=surface&qs=n&form=QBLH&pq=surface&sc=8-5&sp=-1&sk=&cvid=21E4ED21FA684ABDA3CDE9D93C7CE3BB",
            "http://imgur.com/",
            "http://go.com/",
            "https://www.instagram.com/",
            "http://diply.com/",
            "http://www.cnn.com/",
            "https://www.tumblr.com/dashboard",
            "http://www.msn.com/",
            "http://www.walmart.com/",
            "http://espn.go.com/",
            "http://www.imdb.com/",
            "http://www.zillow.com/homes/98052_rb/?fromHomePage=true&shouldFireSellPageImplicitClaimGA=false",
            "http://www.nytimes.com/",
            "http://www.yelp.com/seattle"
        };

        public HoblWorkload()
        {
            Name = "hobl";
            Duration = 100 * 30 * 2;    // Set a very long duration, as this is a long test
        }

        public override void Run(RemoteWebDriver driver, string browser, CredentialManager credentialManager)
        {
            UserInfo credentials = credentialManager.GetCredentials("facebook.com");

            // First tab
            driver.Navigate().GoToUrl(backgroundUrls[0]);
            driver.Wait(30);

            driver.ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);

            // Second tab
            driver.Navigate().GoToUrl(backgroundUrls[1]);
            driver.Wait(30);

            driver.ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);

            // Third tab (Facebook)
            driver.Navigate().GoToUrl(backgroundUrls[2]);
            driver.Wait(3);

            // ...Log in
            driver.TypeIntoField(driver.FindElement(By.Id("email")), credentials.Username);
            driver.TypeIntoField(driver.FindElement(By.Id("pass")), credentials.Password + Keys.Enter);

            driver.Wait(30);

            driver.ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);

            // Fourth tab | The loop
            for (int i = 0; i < 100; i++)
            {
                foreach (string url in urls)
                {
                    driver.Navigate().GoToUrl(url);
                    driver.Wait(30);
                }
            }
        }
    }
}
