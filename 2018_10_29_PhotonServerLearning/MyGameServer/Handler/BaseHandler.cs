using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Photon.SocketServer;

namespace MyGameServer.Handler
{
    public abstract class BaseHandler
    {
        public OperationCode opCode;
        public abstract void OnOpereationRequest(OperationRequest operationRequest, SendParameters sendParameters,ClientPeer peer);
    }
}
