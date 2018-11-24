using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using System.Xml.Serialization;
using System.IO;
using ExitGames.Logging;

namespace MyGameServer.Handler
{
    public class SyncPlayerHandler : BaseHandler
    {
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public SyncPlayerHandler()
        {
            opCode = OperationCode.SyncPlayerOp;
        }

        public override void OnOpereationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            List<string> usernameList = new List<string>();
            foreach (var item in MyGameServer.Instance.peerList)
            {
                if (string.IsNullOrEmpty(item.username)==false&& item!=peer)
                {
                    usernameList.Add(item.username);
                }
            }

            #region 序列化
            StringWriter stringWriter = new StringWriter();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<string>));
            xmlSerializer.Serialize(stringWriter, usernameList);
            stringWriter.Close();
            string xmlString = stringWriter.ToString();
            #endregion

            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add((byte)ParameterCode.UsernameList, xmlString);
            OperationResponse operationResponse = new OperationResponse(operationRequest.OperationCode);
            operationResponse.Parameters = data;

            peer.SendOperationResponse(operationResponse, sendParameters);

            //We need to notify other clients that there is a new client connected!
            foreach (var item in MyGameServer.Instance.peerList)
            {
                if (string.IsNullOrEmpty(item.username)==false && item != peer)
                {
                    EventData eventData = new EventData((byte)EventCode.NewPlayer);
                    Dictionary<byte, object> singleEd = new Dictionary<byte, object>();
                    singleEd.Add((byte)ParameterCode.Username, item.username);
                    eventData.Parameters = singleEd;

                    log.Info(item.username);
                    
                    //调用的是item而不是peer，因为要的是item分别向其它客户端发起event
                    item.SendEvent(eventData, sendParameters);
                }
            }
        }
    }
}
