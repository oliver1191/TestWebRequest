using Microsoft.Bot.Connector.DirectLine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TestBotRequest
{
    public class WebRequest
    {

        public string requestUrl = "http://localhost:3978/api/messages";

        public string serverUrl = "http://localhost:1389";

        public string logPath = "C:\\Users\\administrator.SPCARTOON\\Desktop\\Log\\TestBot";
        

        public List<string> restoreFileAllJsonPathList = new List<string> { "../../../Json/RestoreFile.json", "../../../Json/FileSearchConditionSubmit.json", "../../../Json/RestoreSpecificFile.json", "../../../Json/RestoreFileVersion.json", "../../../Json/ShowRestoreProcess.json" };

        public List<string> orderDrinkAllJsonPathList = new List<string> { "../../../Json/Order.json" };
        public async Task<string> PostRequest(string requestUrl, string content)
        {
            string responseContent = "";
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, error) => { return true; };
            using (HttpClient client = new HttpClient(httpClientHandler))
            {
                try
                {
                    //client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en-US;q=0.8,en;q=0.6,ru;q=0.4");
                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJjb252ZXJzYXRpb25JZCI6IjlkN2Y5OTIwLTc1NDEtMTFlOS1iY2JiLTM3MjQzYWM3ZjQxMXxsaXZlY2hhdCIsImNvbnZlcnNhdGlvbk1vZGUiOiJsaXZlY2hhdCIsImVuZHBvaW50SWQiOiIxIiwidXNlcklkIjoiMDVkMDA3ZDUtYzdkOC00ZTNiLWI1ZTMtMGZkMTRmODQzNDVjIn0");

                    var requestObj = JsonConvert.DeserializeObject<Activity>(content);
                    StringContent strContent = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
                              
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(requestUrl),
                        Method = HttpMethod.Post,
                        Content = strContent
                    };

                    string token = GetMSAJWTToken("4179aafb-3333-41a6-95e4-40ffc9782ee0", "epdrPTUDC70050%^{rmjJP$");
                    string authorization = "Bearer " + token;
                    //GraphService graphService = new GraphService(token);
                    //AccountInfo currentAccount = await graphService.GetCurrentAccount();                    

                    //client.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJub25jZSI6IkFRQUJBQUFBQUFEQ29NcGpKWHJ4VHE5Vkc5dGUtN0ZYLUFqakctRjVjcHcxNkFVZEQzZGpUaFdPajRWenQ4Ym1YLWh1VHpUbG00YXNoc0loeHoyYzVJdGtBblF4VnBUQUg0WmFJM2ZEQXVnU3Y3V1Z5T0pyaHlBQSIsImFsZyI6IlJTMjU2IiwieDV0IjoiSEJ4bDltQWU2Z3hhdkNrY29PVTJUSHNETmEwIiwia2lkIjoiSEJ4bDltQWU2Z3hhdkNrY29PVTJUSHNETmEwIn0.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC83ZDlhMmEwZi01ZTBjLTRhMmItOGExZC03MWFiNmYxYjhhYjkvIiwiaWF0IjoxNTU3ODE5OTE1LCJuYmYiOjE1NTc4MTk5MTUsImV4cCI6MTU1NzgyMzgxNSwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IjQyWmdZTmg2ekpEZlVsRFAvYk82VWM1bjMrdmYyWTI3cWkwMzg4aE11U0JhcXFHMWNBa0EiLCJhbXIiOlsicHdkIl0sImFwcF9kaXNwbGF5bmFtZSI6IkFWQS1JbnRlcm5hbCIsImFwcGlkIjoiNDE3OWFhZmItMzMzMy00MWE2LTk1ZTQtNDBmZmM5NzgyZWUwIiwiYXBwaWRhY3IiOiIxIiwiZmFtaWx5X25hbWUiOiJaaHUiLCJnaXZlbl9uYW1lIjoiTGlqdW4iLCJpcGFkZHIiOiIxMjMuMTc3LjIyLjI1MSIsIm5hbWUiOiJMaWp1biBaaHUiLCJvaWQiOiI0YTA3OGE0MS02YWVhLTQzNmYtYmQ3NC1lY2I0ZjdhNGJjMzgiLCJwbGF0ZiI6IjMiLCJwdWlkIjoiMTAwMzNGRkZBREVCODZBNyIsInB3ZF9leHAiOiIxMTA1NzAzIiwicHdkX3VybCI6Imh0dHBzOi8vcG9ydGFsLm1pY3Jvc29mdG9ubGluZS5jb20vQ2hhbmdlUGFzc3dvcmQuYXNweCIsInNjcCI6IkFsbFNpdGVzLk1hbmFnZSBNYWlsLlJlYWRXcml0ZS5TaGFyZWQgVXNlci5SZWFkIFVzZXIuUmVhZFdyaXRlIiwic3ViIjoiZzlTR2ZyMHdBZE1nWjgzWTlhbVR2eXZrNnA3WjlUUjhMc0kzcVJYdVRRSSIsInRpZCI6IjdkOWEyYTBmLTVlMGMtNGEyYi04YTFkLTcxYWI2ZjFiOGFiOSIsInVuaXF1ZV9uYW1lIjoiam9saWVAZGFxYTEub25taWNyb3NvZnQuY29tIiwidXBuIjoiam9saWVAZGFxYTEub25taWNyb3NvZnQuY29tIiwidXRpIjoiazBnbW9qSjQzMGFLM0ZJSHBlQVRBQSIsInZlciI6IjEuMCIsIndpZHMiOlsiNjJlOTAzOTQtNjlmNS00MjM3LTkxOTAtMDEyMTc3MTQ1ZTEwIl0sInhtc190Y2R0IjoxNTM1MzM1MjA4fQ.WxmJ6C7JQKJbTcZ68g4ZWDf18LEL-YBCVahgijF4Rqx4RF1SivKlcv5F1gW0yYvx6pZowgxSZCD22E6F9z5Tj0GDoVQTjPPEHQOv6glrSAEG4gmwN6a4ke06sDfOIV4cK4DrCoSj3H4sZ82zSmqwbHoSYVHRLFJgOIIWFLLbspDvJ1_UXkEiGOw68tZGjkrzrhjmXFMdqo2nLhac5S1cFbov1jiFCFys0GPZMQZBICtRWPkpwIevAAaM9mSP7VJWC5ZC-FIjho5EWNaY298fAGo0_grUr6Mz19r5qxB5124Hr2HBkXjcVsTQsymZmdrO3YSHsvXuw02TbTErceBmlA");
                    request.Headers.Add("Authorization", authorization);
                    //request.Headers.Add("Accept", "application/json");
                    //request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64;en-US;)");
                    //request.Headers.Add("X-Requested-With", "XMLHttpRequest"); 
                    //request.Headers.Add("BotCode", "c791e9c6-8788-4f4d-a84a-0f381e1a3154");
                    //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJub25jZSI6IkFRQUJBQUFBQUFEQ29NcGpKWHJ4VHE5Vkc5dGUtN0ZYLUFqakctRjVjcHcxNkFVZEQzZGpUaFdPajRWenQ4Ym1YLWh1VHpUbG00YXNoc0loeHoyYzVJdGtBblF4VnBUQUg0WmFJM2ZEQXVnU3Y3V1Z5T0pyaHlBQSIsImFsZyI6IlJTMjU2IiwieDV0IjoiSEJ4bDltQWU2Z3hhdkNrY29PVTJUSHNETmEwIiwia2lkIjoiSEJ4bDltQWU2Z3hhdkNrY29PVTJUSHNETmEwIn0.eyJhdWQiOiJodHRwczovL2dyYXBoLm1pY3Jvc29mdC5jb20iLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC83ZDlhMmEwZi01ZTBjLTRhMmItOGExZC03MWFiNmYxYjhhYjkvIiwiaWF0IjoxNTU3ODE5OTE1LCJuYmYiOjE1NTc4MTk5MTUsImV4cCI6MTU1NzgyMzgxNSwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IjQyWmdZTmg2ekpEZlVsRFAvYk82VWM1bjMrdmYyWTI3cWkwMzg4aE11U0JhcXFHMWNBa0EiLCJhbXIiOlsicHdkIl0sImFwcF9kaXNwbGF5bmFtZSI6IkFWQS1JbnRlcm5hbCIsImFwcGlkIjoiNDE3OWFhZmItMzMzMy00MWE2LTk1ZTQtNDBmZmM5NzgyZWUwIiwiYXBwaWRhY3IiOiIxIiwiZmFtaWx5X25hbWUiOiJaaHUiLCJnaXZlbl9uYW1lIjoiTGlqdW4iLCJpcGFkZHIiOiIxMjMuMTc3LjIyLjI1MSIsIm5hbWUiOiJMaWp1biBaaHUiLCJvaWQiOiI0YTA3OGE0MS02YWVhLTQzNmYtYmQ3NC1lY2I0ZjdhNGJjMzgiLCJwbGF0ZiI6IjMiLCJwdWlkIjoiMTAwMzNGRkZBREVCODZBNyIsInB3ZF9leHAiOiIxMTA1NzAzIiwicHdkX3VybCI6Imh0dHBzOi8vcG9ydGFsLm1pY3Jvc29mdG9ubGluZS5jb20vQ2hhbmdlUGFzc3dvcmQuYXNweCIsInNjcCI6IkFsbFNpdGVzLk1hbmFnZSBNYWlsLlJlYWRXcml0ZS5TaGFyZWQgVXNlci5SZWFkIFVzZXIuUmVhZFdyaXRlIiwic3ViIjoiZzlTR2ZyMHdBZE1nWjgzWTlhbVR2eXZrNnA3WjlUUjhMc0kzcVJYdVRRSSIsInRpZCI6IjdkOWEyYTBmLTVlMGMtNGEyYi04YTFkLTcxYWI2ZjFiOGFiOSIsInVuaXF1ZV9uYW1lIjoiam9saWVAZGFxYTEub25taWNyb3NvZnQuY29tIiwidXBuIjoiam9saWVAZGFxYTEub25taWNyb3NvZnQuY29tIiwidXRpIjoiazBnbW9qSjQzMGFLM0ZJSHBlQVRBQSIsInZlciI6IjEuMCIsIndpZHMiOlsiNjJlOTAzOTQtNjlmNS00MjM3LTkxOTAtMDEyMTc3MTQ1ZTEwIl0sInhtc190Y2R0IjoxNTM1MzM1MjA4fQ.WxmJ6C7JQKJbTcZ68g4ZWDf18LEL-YBCVahgijF4Rqx4RF1SivKlcv5F1gW0yYvx6pZowgxSZCD22E6F9z5Tj0GDoVQTjPPEHQOv6glrSAEG4gmwN6a4ke06sDfOIV4cK4DrCoSj3H4sZ82zSmqwbHoSYVHRLFJgOIIWFLLbspDvJ1_UXkEiGOw68tZGjkrzrhjmXFMdqo2nLhac5S1cFbov1jiFCFys0GPZMQZBICtRWPkpwIevAAaM9mSP7VJWC5ZC-FIjho5EWNaY298fAGo0_grUr6Mz19r5qxB5124Hr2HBkXjcVsTQsymZmdrO3YSHsvXuw02TbTErceBmlA");
                    HttpResponseMessage response = await client.SendAsync(request);
                    //HttpResponseMessage response = await client.PostAsync(requestUrl,request.Content);
                    if (response.IsSuccessStatusCode)
                    {
                        responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Response: "+ responseContent);
                    }
                    else
                    {
                        Console.WriteLine(response.ReasonPhrase);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return responseContent;
        }

        public string GetMSAJWTToken(string clientId,string clientSecret)
        {
            //var options = HttpContext.RequestServices.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;
            //var credential = options.CredentialProvider as SimpleCredentialProvider;
            var authendpoint = "https://login.microsoftonline.com/botframework.com/oauth2/v2.0/token";

            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["grant_type"] = "client_credentials";
                values["client_id"] = clientId;
                values["client_secret"] = clientSecret;
                values["scope"] = clientId + "/.default";

                var response = client.UploadValues(authendpoint, values);
                var responseString = Encoding.Default.GetString(response);
                var result = JsonConvert.DeserializeObject<MSAResponse>(responseString);
                return result.Access_Token;
                //return string.Format("{0} {1}", result.Token_Type, result.Access_Token);
            }
        }

        public void RestoreFile()
        {            
            WriteLog("Restore File Begin.");            
            try
            {
                foreach (string jsonPath in restoreFileAllJsonPathList)
                {
                    string content = File.ReadAllText(jsonPath);
                    string jsonPathName = jsonPath.Substring(jsonPath.LastIndexOf("/") + 1);
                    HttpResponseMessage response = RestoreFileOneStep(content, jsonPathName).Result;
                    if (response!=null&&response.IsSuccessStatusCode)
                    {
                        string responseContent = response.Content.ReadAsStringAsync().Result ?? "Empty";
                        WriteLog("Response " + jsonPathName + " successful:" + responseContent);                        
                        //System.Threading.Thread.Sleep(3000 * timeCount * 3);
                        Console.WriteLine("Response "+jsonPathName+ " successful:" + responseContent);
                    }
                    else if(response!=null)
                    {                        
                        WriteLog("Response " + jsonPathName + " failed:" + "ReasonPhrase: " + response.ReasonPhrase);
                        Console.WriteLine("Response " + jsonPathName + " failed:" + "ReasonPhrase: "+ response.ReasonPhrase);
                        break;
                    }

                }
                WriteLog("Restore File End.");
            }
            catch (Exception ex)
            {
                WriteLog("Response " + " failed:" + "Exception: " + ex.Message);
            }
        }

        public async Task<HttpResponseMessage> RestoreFileOneStep(string content,string errorJsonPath)
        {
            //string responseContent = "";
            HttpResponseMessage response = null;
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, error) => { return true; };
            using (HttpClient client = new HttpClient(httpClientHandler))
            {
                try
                {                    
                    var requestObj = JsonConvert.DeserializeObject<Activity>(content);
                    StringContent strContent = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");

                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(requestUrl),
                        Method = HttpMethod.Post,
                        Content = strContent
                    };

                    string token = GetMSAJWTToken("4179aafb-3333-41a6-95e4-40ffc9782ee0", "epdrPTUDC70050%^{rmjJP$");
                    string authorization = "Bearer " + token;
                    //GraphService graphService = new GraphService(token);
                    //AccountInfo currentAccount = await graphService.GetCurrentAccount();                                        
                    request.Headers.Add("Authorization", authorization);                    
                    response = await client.SendAsync(request);                                      
                }
                catch (Exception e)
                {
                    string logContent = "RestoreFileOneStep" + "errorJsonPath " + errorJsonPath + "errorMessage: " + e.Message;
                    WriteLog(logContent);                    
                }
            }
            return response;
        }

        public void OrderDrink()
        {
            WriteLog("Order Drink Begin.");
            try
            {
                foreach (string jsonPath in orderDrinkAllJsonPathList)
                {
                    string content = File.ReadAllText(jsonPath);
                    string jsonPathName = jsonPath.Substring(jsonPath.LastIndexOf("/") + 1);
                    HttpResponseMessage response = OrderDrinkOneStep(content, jsonPathName).Result;
                    //HttpResponseMessage response = GetActivity(content, jsonPathName).Result;                    
                    if (response != null && response.IsSuccessStatusCode)
                    {
                        string responseContent = response.Content.ReadAsStringAsync().Result ?? "Empty";
                        WriteLog("Order Drink " + jsonPathName + " successful:" + responseContent);
                        //System.Threading.Thread.Sleep(3000 * timeCount * 3);
                        Console.WriteLine("Order Drink " + jsonPathName + " successful:" + responseContent);
                    }
                    else if (response != null)
                    {
                        WriteLog("Order Drink " + jsonPathName + " failed:" + "ReasonPhrase: " + response.ReasonPhrase);
                        Console.WriteLine("Order Drink " + jsonPathName + " failed:" + "ReasonPhrase: " + response.ReasonPhrase);
                        break;
                    }

                }
                WriteLog("Order Drink End.");
            }
            catch (Exception ex)
            {
                WriteLog("Order Drink " + " failed:" + "Exception: " + ex.Message);
            }
        }

        public async Task<HttpResponseMessage> OrderDrinkOneStep(string content, string errorJsonPath)
        {
            //string responseContent = "";
            HttpResponseMessage response = null;
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, error) => { return true; };
            using (HttpClient client = new HttpClient(httpClientHandler))
            {
                try
                {                    
                    var requestObj = JsonConvert.DeserializeObject<Activity>(content);
                    StringContent strContent = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");                    
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(requestUrl),
                        Method = HttpMethod.Post,
                        Content = strContent
                    };

                    //string token = GetMSAJWTToken("", "");
                    //string authorization = "Bearer " + token;
                    //request.Headers.Add("Authorization", authorization);                    
                    response = await client.SendAsync(request);                                       
                }
                catch (Exception e)
                {
                    string logContent = "RestoreFileOneStep" + "errorJsonPath " + errorJsonPath + "errorMessage: " + e.Message;
                    WriteLog(logContent);
                }
            }
            return response;
        }

        public async Task<HttpResponseMessage> GetActivity(string content,string errorJsonPath)
        {
            //string responseContent = "";
            HttpResponseMessage response = null;
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, error) => { return true; };
            using (HttpClient client = new HttpClient(httpClientHandler))
            {
                try
                {
                    //client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en-US;q=0.8,en;q=0.6,ru;q=0.4");
                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJjb252ZXJzYXRpb25JZCI6IjlkN2Y5OTIwLTc1NDEtMTFlOS1iY2JiLTM3MjQzYWM3ZjQxMXxsaXZlY2hhdCIsImNvbnZlcnNhdGlvbk1vZGUiOiJsaXZlY2hhdCIsImVuZHBvaW50SWQiOiIxIiwidXNlcklkIjoiMDVkMDA3ZDUtYzdkOC00ZTNiLWI1ZTMtMGZkMTRmODQzNDVjIn0");

                    var requestObj = JsonConvert.DeserializeObject<Activity>(content);
                    StringContent strContent = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
                    string url = serverUrl;
                    url = string.Format("{0}/v3/conversations/{1}/attachments", url, requestObj.Conversation.Id, requestObj.Id);
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(url),
                        Method = HttpMethod.Post
                        //Content = strContent
                    };

                    response = await client.SendAsync(request);                    
                }
                catch (Exception e)
                {
                    string logContent = "RestoreFileOneStep" + "errorJsonPath " + errorJsonPath + "errorMessage: " + e.Message;
                    WriteLog(logContent);
                }
            }
            return response;
           
        }

        public void CreatePostActivity(Activity activity)
        {
            Activity replyActivity = activity.CreateReply();
            activity.Text = "红茶";


        }

        public void PostActivity(Activity activity)
        {

        }
        

        public void WriteLog(string logContent)
        {
            //ex.StackTrace.ToString().Substring(e.StackTrace.ToString().LastIndexOf('\\') + 1)是得到异常出现的类和行号
            //ex.Message是异常出现的原因            
            logContent = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss  ") + logContent;
            string sFilePath = logPath;//自己定义一个存储log文件的位置
            string sFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            sFileName = sFilePath + @"\\" + sFileName;
            if (!Directory.Exists(sFilePath))
            {
                Directory.CreateDirectory(sFilePath);
            }
            FileStream fs;
            StreamWriter sw;
            if (System.IO.File.Exists(sFileName))
            {
                fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
            }
            sw = new StreamWriter(fs);
            sw.WriteLine(logContent);
            sw.Close();
            fs.Close();
        }


        //public void Post(string requestUrl)
        //{
        //    var uri = new Uri(requestUrl);

        //    DirectLineClientCredentials creds = new DirectLineClientCredentials(secret);

        //    DirectLineClient client = new DirectLineClient(uri, creds);
        //    Conversations convs = new Conversations(client);

        //    string waterMark;

        //    var conv = convs.NewConversation();
        //    var set = convs.GetMessages(conv.ConversationId);
        //    waterMark = set.Watermark;

        //    Message message = new Message(conversationId: conv.ConversationId, text: "your text");
        //    Console.WriteLine(message.Text);
        //    convs.PostMessage(conv.ConversationId, message);

        //    set = convs.GetMessages(conv.ConversationId, waterMark);
        //    PrintResponse(set);
        //    waterMark = set.Watermark;
        //}
    }
}
