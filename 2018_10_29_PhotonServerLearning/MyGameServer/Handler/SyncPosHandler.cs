using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using Common.Tools;
using ExitGames.Logging;

namespace MyGameServer.Handler
{
    public class SyncPosHandler : BaseHandler
    {
        public SyncPosHandler()
        {
            opCode = OperationCode.SyncPositionOp;
        }

        public override void OnOpereationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            float x =  (float)DictTool.GetDictValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.X);
            float y = (float)DictTool.GetDictValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Y);
            float z = (float)DictTool.GetDictValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Z);

            peer.x = x;
            peer.y = y;
            peer.z = z;
        }
    }
}
