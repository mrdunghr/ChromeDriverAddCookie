using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ChromeDriverAddCookie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--window-size=1455,947");
            options.AddArgument("--window-position=188,29");
            //options.AddArgument("app=https://fb.com/");

            ChromeDriver chrome = new ChromeDriver(service, options);

            chrome.Url = "https://fb.com/";
            
            string cookie = "datr=xQAyZvImIBJZ2mo2ZDw8lqUE; sb=xQAyZlZiK_2B-G_mJAMN7l6z; ps_n=1; ps_l=1; locale=vi_VN; c_user=100004208802353; xs=2%3AvONyLsVueSyTmg%3A2%3A1714553265%3A-1%3A6394%3A%3AAcWIeSZV2JOw3v1aIwpIygVeaWoUFF6JuouPJW3Ssg; fr=1sAXJ443Sryg0Sr07.AWUFCI0ZnanCQ8sMJ3Hidh9kc04.BmMgvO..AAA.0.0.BmMgvO.AWUT_8k64D0; presence=C%7B%22t3%22%3A%5B%5D%2C%22utc3%22%3A1714555858486%2C%22v%22%3A1%7D; wd=1321x956";

            List<Cookie> cookies = GetCookies(cookie);

            // Thêm mỗi cookie vào ChromeDriver
            foreach (Cookie cookieResult in cookies)
            {
                chrome.Manage().Cookies.AddCookie(new Cookie(cookieResult.Name, cookieResult.Value, ".facebook.com", "/", DateTime.Now.AddDays(1)));
            }

            chrome.Navigate().Refresh();

        }


        public static List<Cookie> GetCookies(string textCookie)
        {
            List<Cookie> cookies = new List<Cookie>();

            // b1 tách chuỗi cookie thành các cookie riêng biệt
            //var cookiePart = textCookie.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries); // cũng là tách mảng nhưng nhiều option hơn
            string[] cookiePart = textCookie.Split(';'); // cách hay sử dụng

            foreach (string part in cookiePart)
            {
                // tách kv cho cookie
                //var kv = part.Split(new[] { '=' }, 2); // cũng là tách mảng nhưng nhiều option hơn
                string[] kv = part.Split('='); // cách hay sử dụng
                if (kv.Length == 2)
                {
                    //Tạo đối tượng Cookie từ key và value
                    //var cookie = new Cookie(kv[0].Trim(), kv[1].Trim()); // cũng là tách mảng nhưng nhiều option hơn
                    Cookie cookieResult = new Cookie(kv[0].Trim(), kv[1].Trim()); // cũng là tách mảng nhưng nhiều option hơn
                    cookies.Add(cookieResult);
                }
            }


            return cookies;
        }
    }
}
