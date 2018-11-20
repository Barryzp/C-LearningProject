using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using Common;
using Common.Tools;
using MyGameServer.Handler;

namespace MyGameServer
{
    public class ClientPeer : Photon.SocketServer.ClientPeer
    {
        public ClientPeer(InitRequest initRequest) : base(initRequest)
        {
        }

        //处理客户端断开连接的之后的事情
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
        }

        //处理客户端的请求
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            BaseHandler handler = DictTool.GetDictValue<OperationCode, BaseHandler>(MyGameServer.Instance.handlerSet, (OperationCode)operationRequest.OperationCode);
            if (handler == null)
            {
                handler = DictTool.GetDictValue<OperationCode, BaseHandler>(MyGameServer.Instance.handlerSet, OperationCode.DefaultOperation);
            }
            handler.OnOpereationRequest(operationRequest, sendParameters, this);
            #region 测试
            /*
            //通过OpCode区分不同的请求，类似于枚举
            switch (operationRequest.OperationCode)
            {
                case 1:
                    MyGameServer.log.Info("Receive a request from client.");

                    object clientValue;
                    operationRequest.Parameters.TryGetValue(1, out clientValue);
                    MyGameServer.log.Info("data from client equals to: "+clientValue.ToString());

                    Dictionary<byte, object> data = new Dictionary<byte, object>();
                    string serverValue = "this data is sent from server.";
                    data.Add(1, serverValue);
                    string eventValue = "this is for testing sentEvent.";
                    data.Add(2, eventValue);
                    OperationResponse operationResponse = new OperationResponse(1,data);
                    //operationResponse.SetParameters(data);

                    EventData ed = new EventData(1, data);
                    SendEvent(ed, sendParameters);
                    //给客户端发起响应
                    SendOperationResponse(operationResponse, sendParameters);
                    break;
                default:
                    break;
            }
            */
            #endregion
        }
    }
}
