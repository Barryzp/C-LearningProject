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
    public class RegisterHandler : BaseHandler
    {
        public RegisterHandler()
        {
            opCode = OperationCode.RegisterOperation;
        }

        public override void OnOpereationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            string username = DictTool.GetDictValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Username) as string;
            string password = DictTool.GetDictValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Password) as string;

            OperationResponse operationResponse = new OperationResponse(operationRequest.OperationCode);

            //Verify is db exist this account
            if (UserManager.Instance.IsDBExistSameUsername(username))
            {
                operationResponse.ReturnCode = (short)ReturnCode.AccountExist;
            }else
            {
                UserManager.Instance.Add(username,password);
                operationResponse.ReturnCode = (short)ReturnCode.RegisterSuccess;
            }

            peer.SendOperationResponse(operationResponse, sendParameters);

        }
    }
}
