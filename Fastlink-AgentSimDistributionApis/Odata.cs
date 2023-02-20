using Fastlink_AgentSimDistributionApis.Models.Odata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Fastlink_AgentSimDistributionApis.Models;
using System.Configuration;

namespace Fastlink_AgentSimDistributionApis
{
    public class Odata
    {
        string baseUrl = ConfigurationManager.AppSettings["baseUrl"];
        string CreatioUser = ConfigurationManager.AppSettings["CreatioUsername"];
        string CreatioPass = ConfigurationManager.AppSettings["CreatioPassword"];

        public void OnLogRequest(Showroom show)
        {
            string authUrl = baseUrl+"/ServiceModel/AuthService.svc/Login?UserName=" + CreatioUser  + "&UserPassword="+ CreatioPass;
            string url = baseUrl+"/0/odata/UsrSimInformation";

            string authfct = CallAuth(authUrl, url, show);
            Console.ReadLine();
        }

        public string CallAuth(string authUrl, string url, Showroom show)
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(new Uri(authUrl));
            webrequest.Method = "POST";
            webrequest.ContentType = "application/json";
            AuthParams auth = new AuthParams();
            auth.UserName = CreatioUser;
            auth.UserPassword = CreatioPass;
            string str = JsonConvert.SerializeObject(auth);
            byte[] databyte = Encoding.UTF8.GetBytes(str);
            webrequest.ContentLength = databyte.Length;
            webrequest.KeepAlive = true;
            webrequest.CookieContainer = new CookieContainer();
            webrequest.Headers.Add("ForceUseSession", "true");
            webrequest.GetRequestStream().Write(databyte, 0, databyte.Length);

            string responseContent = string.Empty;
            string bpmcsrf = "";
            string details = "";
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
                {
                    StreamReader streamReader = new StreamReader(response.GetResponseStream());
                    responseContent = streamReader.ReadToEnd();
                    var cookies = response.Headers.GetValues("Set-Cookie").ToList();

                    // Find the BPMCSRF token in the cookies
                    bpmcsrf = cookies
                      .Select(c => c.Split(';')[0])
                      .FirstOrDefault(c => c.StartsWith("BPMCSRF"));

                    // Extract the token value
                    bpmcsrf = bpmcsrf.Split('=')[1];



                    for (int i = 0; i < show.Agents.Count; i++)
                    {
                        for (int j = 0; j < show.Agents[i].Availability.Count; j++)
                        {
                            details = CallRestMethod(webrequest, url, bpmcsrf, response.Cookies, show.ShowroomCode, show.PurchaseNumber.ToString(), show.Agents[i].Id, show.Agents[i].Availability[j].PRICE, show.Agents[i].Availability[j].ICCID, show.Agents[i].Availability[j].PIN1, show.Agents[i].Availability[j].PIN2, show.Agents[i].Availability[j].PUK1, show.Agents[i].Availability[j].PUK2, show.Agents[i].Availability[j].IMSI,show.Agents[i].Availability[j].MDN, show.Agents[i].Availability[j].QrCode, show.SimType); 
                        }
                    }
                    Console.WriteLine(bpmcsrf);
                    Console.WriteLine(details);
                    streamReader.Close();
                    response.Close();
                }
            }
            catch (Exception e)
            {
                responseContent = e.ToString();
            }
            finally
            {
                webrequest.Abort();
            }

            return bpmcsrf;
        }

        public static string CallRestMethod(HttpWebRequest webrequest, string url, string BPMCSRF, CookieCollection cook, string showC, string purcharsenb, string agentID, string availablePrice, string iccid , string pin1, string pin2,string puk1,string puk2,string imsi,string mdn,string qrCode,string simTypee)
        {
            webrequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
            webrequest.Method = "POST";
            webrequest.KeepAlive = true;
            webrequest.Accept = "application/json;";
            webrequest.ContentType = "application/json";
            webrequest.Host = "10.10.23.141:82";
            webrequest.Headers.Add("BPMCSRF", BPMCSRF);

            string cookie_value = "";

            foreach (Cookie cookie in cook)
            {
                cookie_value = cookie_value + cookie.Name + "=" + cookie.Value + "; ";

            }

            webrequest.Headers.Add("Cookie", cookie_value);

            {
                SimInformation sim = new SimInformation();
                sim.UsrMDN = mdn;
                sim.UsrQrCode = qrCode;
                sim.UsrPurchaseNumber = purcharsenb;
                sim.UsrICCID = iccid;
                sim.UsrPIN1 = pin1;
                sim.UsrPIN2 = pin2;
                sim.UsrPUK1 = puk1;
                sim.UsrPUK2 = puk2;
                sim.UsrIMSI = imsi;
                sim.UsrSimTypeId = simTypee;
                sim.UsrShowroomId = showC;
                sim.UsrOwnerId = agentID;
                sim.UsrPrice = availablePrice;


                string str = JsonConvert.SerializeObject(sim);
                byte[] databyte = Encoding.UTF8.GetBytes(str);
                webrequest.ContentLength = databyte.Length;

                webrequest.GetRequestStream().Write(databyte, 0, databyte.Length);

                string responseContent = string.Empty;
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
                    {
                        StreamReader streamReader = new StreamReader(response.GetResponseStream());
                        responseContent = streamReader.ReadToEnd();
                        streamReader.Close();
                        response.Close();
                    }
                }
                catch (Exception e)
                {
                    responseContent = e.ToString();
                }
                finally
                {
                    webrequest.Abort();
                }

                return responseContent;
            }

        }
    }
}