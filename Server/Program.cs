using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TestBotRequest;

namespace Server
{
    class server
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
       
        static void Main(string[] args)
        {
            //
            // TODO: 在此处添加代码以启动应用程序
            //
            int recv;//用于表示客户端发送的信息长度
            byte[] data = new byte[1024];//用于缓存客户端所发送的信息,通过socket传递的信息必须为字节数组
            //IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);//本机预使用的IP和端口
            string ipadd = "127.0.0.1";
            //Console.WriteLine();
            //Console.Write("please input the server port:");
            //int port = Convert.ToInt32(Console.ReadLine());
            int port = 7777;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ipadd), port);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipep);//绑定
            newsock.Listen(30);//监听
            Console.WriteLine("waiting for a client");
            Socket client = newsock.Accept();//当有可用的客户端连接尝试时执行，并返回一个新的socket,用于与客户端之间的通信
            IPEndPoint clientip = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("connect with client:" + clientip.Address + " at port:" + clientip.Port);
            //string welcome = "welcome here!";
            //data = Encoding.ASCII.GetBytes(welcome);
            //client.Send(data, data.Length, SocketFlags.None);//发送信息
            try
            {
                while (true)
                {//用死循环来不断的从客户端获取信息
                    data = new byte[client.ReceiveBufferSize];
                    recv = client.Receive(data);
                    if (recv == 0)//当信息长度为0，说明客户端连接断开
                        break;
                    string requestBody = System.Text.Encoding.Default.GetString(data);
                    requestBody = requestBody.Substring(requestBody.IndexOf("{"));
                    Activity activity = JsonConvert.DeserializeObject<Activity>(requestBody);
                    TestBotRequest.WebRequest webRequest = new TestBotRequest.WebRequest();
                    webRequest.CreatePostActivity(activity);

                    
                    //Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
                    //client.Send(data, recv, SocketFlags.None);
                }
            }
            catch (Exception ex)
            {

                WriteLog(ex.Message);
            }
            Console.WriteLine("Disconnected from" + clientip.Address);
            client.Close();
            newsock.Close();
            Console.ReadKey();

        }


        public  static void WriteLog(string logContent)
        {
            //ex.StackTrace.ToString().Substring(e.StackTrace.ToString().LastIndexOf('\\') + 1)是得到异常出现的类和行号
            //ex.Message是异常出现的原因            
            logContent = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss  ") + logContent;
            string logPath = "C:\\Users\\administrator.SPCARTOON\\Desktop\\Log\\TestSocket";
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
    }
}
