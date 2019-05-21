using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace TestBotRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            string requestUrl = "http://localhost:3978/api/messages";
            var showRestoreProcess = File.ReadAllText("../../../Json/ShowRestoreProcess.json");
            //string requestUrl = "http://localhost:4535";
            var requestContent = File.ReadAllText("../../../Json/RestoreFile.json");
            var searchCondition = File.ReadAllText("../../../Json/FileSearchConditionSubmit.json");
            var restoreSpecificFile = File.ReadAllText("../../../Json/RestoreSpecificFile.json");
            var restoreFileVersion = File.ReadAllText("../../../Json/RestoreFileVersion.json");
            

            WebRequest webRequest = new WebRequest();
            //string token=webRequest.GetMSAJWTToken();
            //Restore a file
            //var responseStr= webRequest.PostRequest(requestUrl, requestContent);

            //Restore file submit search condition
            //var searchConditionStr = webRequest.PostRequest(requestUrl, searchCondition);

            //Restore file choose the file before version
            //var restoreSpecificFileResponse=webRequest.PostRequest(requestUrl, restoreSpecificFile);

            //Restore File according to version
            //var restoreFileVersionResponse = webRequest.PostRequest(requestUrl, restoreFileVersion);

            //Show the Restore Process
            //var showRestoreProcessResponse = webRequest.PostRequest(requestUrl,showRestoreProcess);

            //webRequest.RestoreFile();
            webRequest.OrderDrink();



            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
        
    }
}
