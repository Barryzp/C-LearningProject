using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;

namespace MyGameServer.Handler
{
    public class DefaultHandler : BaseHandler
    {
        public DefaultHandler()
        {
            opCode = Common.OperationCode.DefaultOperation;
        }

        public override void OnOpereationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
        }
    }
}
