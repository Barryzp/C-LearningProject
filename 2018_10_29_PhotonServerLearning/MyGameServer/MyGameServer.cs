using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using System.IO;
using ExitGames.Logging;        //Line 16
using ExitGames.Logging.Log4Net;//Step2
using log4net.Config;           //Step3
using MyGameServer.Model;
using MyGameServer.Manager;
using MyGameServer.Handler;
using MyGameServer.Threads;
using Common;

namespace MyGameServer
{
    //所有Server端的 主类都要继承自ApplicationBase
    public class MyGameServer : ApplicationBase
    {
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public Dictionary<OperationCode, BaseHandler> handlerSet = new Dictionary<OperationCode, BaseHandler>();
        public List<ClientPeer> peerList = new List<ClientPeer>();

        private SyncPositionThread syncPositionThread = new SyncPositionThread();

        public new static MyGameServer Instance
        {
            get;
            private set;
        }

        //当一个客户端请求连接的时候
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            log.Info("A client created.");
            ClientPeer peer = new ClientPeer(initRequest);
            peerList.Add(peer);
            return peer;
        }

        //初始化
        protected override void Setup()
        {
            Instance = this;
            log.Info("First message logged.");
            syncPositionThread.Run();

            InitHandler();

            #region 日志的初始化
            //配置log日志所在的目录
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] =Path.Combine(ApplicationRootPath,"log");
            //当然如果要放在别的目录下也可以，反正记住要用Path.Combine(这个是屏蔽不同平台的差异的)来进行连接路径，不要用'/'

            //读取log4net配置文件
            //Step 1:读取Log4Net的配置文件
            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath,"log4net.config"));
            if(configFileInfo.Exists)
            {
                //Step2:设置日志工厂，也就是设置这个Log使用的是哪种日志插件
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                //Step3:让log4net读取配置文件
                XmlConfigurator.ConfigureAndWatch(configFileInfo);
            }
            #endregion
        }

        //Server端关闭的时候
        protected override void TearDown()
        {
            syncPositionThread.Stop();
            log.Info("The server shut down");
        }

        public void InitHandler()
        {
            LogginHandler logginHandler = new LogginHandler();
            handlerSet.Add(logginHandler.opCode, logginHandler);

            RegisterHandler registerHandler = new RegisterHandler();
            handlerSet.Add(registerHandler.opCode, registerHandler);

            SyncPosHandler syncPosHandler = new SyncPosHandler();
            handlerSet.Add(syncPosHandler.opCode, syncPosHandler);

            SyncPlayerHandler syncPlayerHandler = new SyncPlayerHandler();
            handlerSet.Add(syncPlayerHandler.opCode, syncPlayerHandler);

            DefaultHandler defaultHandler = new DefaultHandler();
            handlerSet.Add(defaultHandler.opCode,defaultHandler);
        }
    }
}
