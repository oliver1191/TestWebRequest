using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TestBotRequest
{
    class GraphService
    {       

        private readonly string token;

        public GraphService(string token)
        {
            this.token = token;
        }

        //public async Task<string> GetCurrentAccountName()
        //{
        //    try
        //    {
        //        var graphClient = GetAuthenticatedClient();
        //        User me = await graphClient.Me.Request().Select("DisplayName").GetAsync();
        //        return me.DisplayName;
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error(e.ToString());
        //    }
        //    return "";
        //}

        public async Task<AccountInfo> GetCurrentAccount()
        {
            try
            {
                var graphClient = GetAuthenticatedClient();
                User me = await graphClient.Me.Request().Select("DisplayName,UserPrincipalName,JobTitle,Country").GetAsync();
                //var orgs = await graphClient.Organization.Request().Select("DisplayName").GetAsync();
                //List<string> organizations = new List<string>();
                //foreach (var org in orgs)
                //{
                //    organizations.Add(org.DisplayName);
                //}               
                AccountInfo accountInfo = new AccountInfo
                {
                    Name = me.DisplayName,
                    Email = me.UserPrincipalName,
                    JobTitle = me.JobTitle,
                    Country = me.Country,
                    //Organization = string.Join(";", organizations)
                };
                return accountInfo;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetCurrentAccount Failed.Reason:"+ e.Message);
            }
            return new AccountInfo();
        }

        //public async Task<string> GetMySiteUrl()
        //{
        //    string mySiteUrl = "";
        //    try
        //    {
        //        var graphClient = GetAuthenticatedClient();
        //        var me = await graphClient.Me.Request().Select("mysite").GetAsync();
        //        mySiteUrl = me.MySite;
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error("An error occurred when getting mysite by User. Try to get with OneDrive property. Exception: {0}.", e.ToString());
        //    }
        //    if (string.IsNullOrEmpty(mySiteUrl))
        //    {
        //        try
        //        {
        //            var graphClient = GetAuthenticatedClient();
        //            var drive = await graphClient.Me.Drive.Request().Select("webUrl").GetAsync();
        //            string webUrl = drive.WebUrl;
        //            if (!string.IsNullOrEmpty(webUrl))
        //            {
        //                int index = webUrl.LastIndexOf('/');
        //                if (index > 0)
        //                {
        //                    mySiteUrl = webUrl.Substring(0, index);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error("an error occurred when get mysite by Drive. Exception: {0}.", ex.ToString());
        //        }
        //    }
        //    return mySiteUrl;
        //}

        //public async Task<List<MailInfo>> GetAllDeletedMails(string filter = null)
        //{
        //    List<MailInfo> mails = new List<MailInfo>();

        //    try
        //    {
        //        int startIndex = 0;
        //        List<MailInfo> mailInfos = await GetDeletedMails(startIndex, 100, filter);
        //        mails.AddRange(mailInfos);
        //        while (mailInfos.Count > 0)
        //        {
        //            startIndex += 100;
        //            mailInfos = await GetDeletedMails(startIndex, 100, filter);
        //            mails.AddRange(mailInfos);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error(e.ToString());
        //    }
        //    return mails;
        //}

        //private async Task<List<MailInfo>> GetDeletedMails(int startIndex, int count, string filter = null)
        //{
        //    List<MailInfo> mails = new List<MailInfo>();
        //    try
        //    {
        //        var graphClient = GetAuthenticatedClient();
        //        IMailFolderMessagesCollectionPage deletedItems;
        //        //Othter Properties: CreatedDateTime,SentDateTime,ReceivedDateTime,From,ToRecipients,WebLink,BodyPreview,Importance,HasAttachments,IsRead,IsDraft
        //        if (string.IsNullOrEmpty(filter))
        //        {
        //            deletedItems = await graphClient.Me.MailFolders.DeletedItems.Messages.Request().Select("Id,Subject,From,ToRecipients,SentDateTime,WebLink").Skip(startIndex).Top(count).GetAsync();
        //        }
        //        else
        //        {
        //            deletedItems = await graphClient.Me.MailFolders.DeletedItems.Messages.Request().Select("Id,Subject,From,ToRecipients,SentDateTime,WebLink").Filter(filter).Skip(startIndex).Top(count).GetAsync();
        //        }

        //        foreach (var item in deletedItems)
        //        {
        //            var mail = new MailInfo
        //            {
        //                Id = item.Id,
        //                Name = item.Subject,
        //                From = item.From?.EmailAddress?.Address ?? "",
        //                FullPath = item.WebLink,
        //                SentDateTime = item.SentDateTime
        //            };

        //            mail.ToRecipients = new List<string>();
        //            foreach (var recipient in item.ToRecipients)
        //            {
        //                if (recipient.EmailAddress != null && !string.IsNullOrEmpty(recipient.EmailAddress.Address))
        //                {
        //                    mail.ToRecipients.Add(recipient.EmailAddress.Address);
        //                }
        //            }
        //            mails.Add(mail);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error(e.ToString());
        //    }
        //    return mails;
        //}

        //public async Task<List<MailInfo>> GetTopDeletedMails(int top = 5)
        //{
        //    List<MailInfo> mails = new List<MailInfo>();
        //    try
        //    {
        //        var graphClient = GetAuthenticatedClient();
        //        //Othter Properties: SentDateTime,ReceivedDateTime,From,ToRecipients,WebLink
        //        var deletedItems = await graphClient.Me.MailFolders.DeletedItems.Messages.Request().Select("Id,Subject,From,Sender,ToRecipients,SentDateTime,ReceivedDateTime,WebLink").Top(top).GetAsync();
        //        foreach (var item in deletedItems)
        //        {
        //            var mail = new MailInfo
        //            {
        //                Id = item.Id,
        //                Name = item.Subject,
        //                From = item.From.EmailAddress.Address,
        //                FullPath = item.WebLink
        //            };
        //            mails.Add(mail);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error(e.ToString());
        //    }
        //    return mails;
        //}

        //public async Task<MailFolderInfo> GetMailInboxFolder()
        //{
        //    MailFolderInfo mailFolderInfo = null;
        //    try
        //    {
        //        var graphClient = GetAuthenticatedClient();
        //        //Othter Properties: SentDateTime,ReceivedDateTime,From,ToRecipients,WebLink
        //        var mailFolder = await graphClient.Me.MailFolders.Inbox.Request().Select("Id,DisplayName,ParentFolderId,ChildFolderCount").GetAsync();
        //        mailFolderInfo = new MailFolderInfo();
        //        mailFolderInfo.Id = mailFolder.Id;
        //        mailFolderInfo.DisplayName = mailFolder.DisplayName;
        //        mailFolderInfo.ParentFolderId = mailFolder.ParentFolderId;
        //        mailFolderInfo.ChildFolderCount = mailFolder.ChildFolderCount.HasValue ? mailFolder.ChildFolderCount.Value : 0;
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error(e.ToString());
        //    }
        //    return mailFolderInfo;
        //}

        //public async Task<MailFolderInfo> GetMailFolderByName(string folderName)
        //{
        //    MailFolderInfo mailFolderInfo = null;
        //    try
        //    {
        //        string filterQuery = string.Format("DisplayName eq '{0}'", folderName);
        //        var graphClient = GetAuthenticatedClient();
        //        var mailFolders = await graphClient.Me.MailFolders.Request().Select("Id,DisplayName,ParentFolderId,ChildFolderCount").Filter(filterQuery).GetAsync();
        //        var mailFolder = mailFolders.Count > 0 ? mailFolders[0] : null;
        //        if (mailFolder != null)
        //        {
        //            mailFolderInfo = new MailFolderInfo();
        //            mailFolderInfo.Id = mailFolder.Id;
        //            mailFolderInfo.DisplayName = mailFolder.DisplayName;
        //            mailFolderInfo.ParentFolderId = mailFolder.ParentFolderId;
        //            mailFolderInfo.ChildFolderCount = mailFolder.ChildFolderCount.HasValue ? mailFolder.ChildFolderCount.Value : 0;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error(e.ToString());
        //    }
        //    return mailFolderInfo;
        //}

        //public async Task<MailInfo> MoveEmail(MailInfo email, MailFolderInfo targetFolder)
        //{
        //    MailInfo mailInfo = null;
        //    try
        //    {
        //        var graphClient = GetAuthenticatedClient();
        //        var newEmail = await graphClient.Me.MailFolders.DeletedItems.Messages[email.Id].Move(targetFolder.Id).Request().PostAsync();
        //        mailInfo = new MailInfo();
        //        mailInfo.Id = newEmail.Id;
        //        mailInfo.Name = newEmail.Subject;
        //        mailInfo.From = newEmail.From.EmailAddress.Address;
        //        mailInfo.FullPath = newEmail.WebLink;
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error(e.ToString());
        //    }
        //    return mailInfo;
        //}

        private GraphServiceClient GetAuthenticatedClient()
        {
            GraphServiceClient graphClient = new GraphServiceClient(
                new DelegateAuthenticationProvider(
                    async (requestMessage) =>
                    {
                        string accessToken = token;

                        // Append the access token to the request.
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                        // Get event times in the current time zone.
                        requestMessage.Headers.Add("Prefer", "outlook.timezone=\"" + TimeZoneInfo.Local.Id + "\"");
                    }));
            return graphClient;
        }

        //public async Task GetMyTeams()
        //{
        //    var requestUrl = "https://graph.microsoft.com/v1.0/me/joinedTeams";
        //    var content = await GetBetaResponseContentString(requestUrl);
        //    var teams = JsonConvert.DeserializeObject(content);
        //}

        //private async Task<string> GetBetaResponseContentString(string requestUrl)
        //{
        //    string contentStr = "";
        //    using (HttpClient client = new HttpClient())
        //    {
        //        var request = new HttpRequestMessage()
        //        {
        //            RequestUri = new Uri(requestUrl),
        //            Method = HttpMethod.Get
        //        };
        //        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

        //        var response = await client.SendAsync(request);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            contentStr = await response.Content.ReadAsStringAsync();
        //        }
        //        else
        //        {
        //            logger.Warn("Failed to getting the response of get request. StatusCode:{0}, Reason:{1}, URL: {2}", response.StatusCode, response.ReasonPhrase, requestUrl);
        //        }
        //    }
        //    return contentStr;
        //}
    }
}
