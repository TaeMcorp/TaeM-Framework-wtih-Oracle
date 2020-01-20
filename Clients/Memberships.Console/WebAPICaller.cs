using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Threading;

using Memberships.Entity;

namespace Memberships.Console
{
    public class WebAPICaller
    {
        public static Member CallCreateMember(string uri, Member member)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ConfigurationManager.AppSettings["HeaderType"]));

            HttpResponseMessage response = client.PostAsJsonAsync(
                "Memberships/CreateMember",
                member).Result;

            if (response.IsSuccessStatusCode)
            {
                Member ret = response.Content.ReadAsAsync<Member>().Result;
                return ret;
            }
            else
            {
                System.Console.WriteLine("{0} : {1} ({2})", "Call CreateMember API method", (int)response.StatusCode, response.ReasonPhrase);
            }

            return null;
        }

        public static Member CallGetMember(string uri, int memberID)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ConfigurationManager.AppSettings["HeaderType"]));

            HttpResponseMessage response = client.GetAsync(
                string.Format("Memberships/GetMember?memberID={0}", memberID)
                ).Result;

            if (response.IsSuccessStatusCode)
            {
                Member ret = response.Content.ReadAsAsync<Member>().Result;
                return ret;
            }
            else
            {
                System.Console.WriteLine("{0} : {1} ({2})", "Call GetMember API method", (int)response.StatusCode, response.ReasonPhrase);
            }

            return null;
        }

        public static List<Member> CallGetMembers(string uri, string memberName)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ConfigurationManager.AppSettings["HeaderType"]));

            HttpResponseMessage response = client.PostAsJsonAsync(
                "Memberships/GetMembers",
                memberName).Result;

            if (response.IsSuccessStatusCode)
            {
                List<Member> ret = response.Content.ReadAsAsync<List<Member>>().Result;
                return ret;
            }
            else
            {
                System.Console.WriteLine("{0} : {1} ({2})", "Call GetMembers API method", (int)response.StatusCode, response.ReasonPhrase);
            }

            return null;
        }

        public static int CallGetGetNumsOfMembers(string uri, string memberName)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ConfigurationManager.AppSettings["HeaderType"]));

            HttpResponseMessage response = client.PostAsJsonAsync(
                "Memberships/GetNumsOfMembers",
                memberName).Result;

            if (response.IsSuccessStatusCode)
            {
                int ret = response.Content.ReadAsAsync<int>().Result;
                return ret;
            }
            else
            {
                System.Console.WriteLine("{0} : {1} ({2})", "Call GetNumsOfMembers API method", (int)response.StatusCode, response.ReasonPhrase);
            }

            return -1;
        }

        public static bool CallSetMember(string uri, Member member)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ConfigurationManager.AppSettings["HeaderType"]));

            HttpResponseMessage response = client.PutAsJsonAsync(
                "Memberships/SetMember",
                member).Result;

            if (response.IsSuccessStatusCode)
            {
                bool ret = response.Content.ReadAsAsync<bool>().Result;
                return ret;
            }
            else
            {
                System.Console.WriteLine("{0} : {1} ({2})", "Call SetMember API method", (int)response.StatusCode, response.ReasonPhrase);
            }

            return false;
        }

        public static bool CallRemoveMember(string uri, int memberID)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ConfigurationManager.AppSettings["HeaderType"]));

            HttpResponseMessage response = client.PutAsJsonAsync(
                "Memberships/RemoveMember",
                memberID).Result;

            if (response.IsSuccessStatusCode)
            {
                bool ret = response.Content.ReadAsAsync<bool>().Result;
                return ret;
            }
            else
            {
                System.Console.WriteLine("{0} : {1} ({2})", "Call SetMember API method", (int)response.StatusCode, response.ReasonPhrase);
            }

            return false;
        }
    }
}