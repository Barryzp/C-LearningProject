using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Xml.Serialization;
using Common;
using Photon.SocketServer;

namespace MyGameServer.Threads
{
    public class SyncPositionThread
    {
        private Thread thread;

        private void UpdatePosition()
        {
            Thread.Sleep(1000);

            while (true)
            {
                Thread.Sleep(50);
                SendPosition();
            }
        }

        private void SendPosition()
        {
            List<PlayerData> playerDataList = new List<PlayerData>();
            foreach (var item in MyGameServer.Instance.peerList)
            {
                if (string.IsNullOrEmpty(item.username))
                {
                    continue;
                }
                PlayerData playerData = new PlayerData();
                var username = item.username;
                playerData.Username = username;
                playerData.Position = new Vector3Data() { X = item.x, Y = item.y, Z = item.z };
                playerDataList.Add(playerData);
            }

            StringWriter stringWriter = new StringWriter();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PlayerData>));
            xmlSerializer.Serialize(stringWriter, playerDataList);
            stringWriter.Close();
            string playerDataListString = stringWriter.ToString();

            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add((byte)ParameterCode.PlayerDataList, playerDataListString);

            foreach (var item in MyGameServer.Instance.peerList)
            {
                if (string.IsNullOrEmpty(item.username))
                {
                    continue;
                }

                EventData eventData = new EventData((byte)EventCode.SyncPlayerPosition);
                eventData.Parameters = data;
                item.SendEvent(eventData, new SendParameters());
            }
        }

        public void Run()
        {
            thread = new Thread(UpdatePosition);
            //使其在后台运行
            thread.IsBackground = true;
            thread.Start();
        }

        public void Stop()
        {
            thread.Abort();
        }
    }
}
