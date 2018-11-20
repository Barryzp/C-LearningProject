using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using MyGameServer.Manager;
using Common;
using Common.Tools;

namespace MyGameServer.Handler
{
    public class LogginHandler : BaseHandler
    {
        public LogginHandler()
        {
            opCode = OperationCode.LogginOperation;
        }

        public override void OnOpereationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            string username = DictTool.GetDictValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Username) as string;
            string password = DictTool.GetDictValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Password) as string;

            OperationResponse operationResponse = new OperationResponse(operationRequest.OperationCode);
            bool simpleJudge = UserManager.Instance.VerifyUserAccount(username, password);
            if (simpleJudge)
            {
                operationResponse.ReturnCode = (short)ReturnCode.Success;
            }
            else
            {
                operationResponse.ReturnCode = (short)ReturnCode.Failed;
            }

            peer.SendOperationResponse(operationResponse, sendParameters);
        }
    }
}
