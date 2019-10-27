using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _2019_10_20_SocketServer
{
    public class ClientState {
        public Socket socket;
        public byte[] readBuff=new byte[1024];
    }

    class Program
    {
        static Dictionary<Socket, ClientState> clients = new Dictionary<Socket, ClientState>();
        static Socket listenfd;
        static void Main(string[] args)
        {
            syncServerImproving();
        }

        #region 异步服务器(无阻塞)
        static void asyncServer() {
            Console.WriteLine("异步Server启动~");

            listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 8888);
            listenfd.Bind(iPEndPoint);

            //Listen
            listenfd.Listen(0);
            Console.WriteLine("异步Server启动成功~");
            listenfd.BeginAccept(acceptCallback, listenfd);
            Console.ReadLine();
        }

        private static void acceptCallback(IAsyncResult ar)
        {
            try{
                Console.WriteLine("Server Accept.");
                Socket listenfd = (Socket)ar.AsyncState;
                Socket clientfd = listenfd.EndAccept(ar);
                ClientState state = new ClientState();
                state.socket = clientfd;
                clients.Add(clientfd, state);

                clientfd.BeginReceive(state.readBuff, 0, 1024, 0, recvCallback, state);
                //继续Accept
                listenfd.BeginAccept(acceptCallback, listenfd);

            }
            catch (SocketException se) {
                Console.WriteLine("Socket accept failed!"+se.ToString());
            }
        }

        private static void recvCallback(IAsyncResult ar)
        {
            try{
                ClientState state = (ClientState)ar.AsyncState;
                Socket clientfd = state.socket;
                int count = clientfd.EndReceive(ar);
                if (count == 0) {
                    clientfd.Close();
                    clients.Remove(clientfd);
                    Console.WriteLine("Client close!");
                    return;
                }

                string recvStr = System.Text.Encoding.UTF8.GetString(state.readBuff, 0, count);
                string sendStr = clientfd.RemoteEndPoint.ToString()+":"+recvStr;
                Console.WriteLine("服务器发送的数据："+sendStr);
                byte[] sendClientBytes = System.Text.Encoding.UTF8.GetBytes(sendStr);
                foreach (var skt in clients.Values) {
                    skt.socket.Send(sendClientBytes);
                }
                clientfd.BeginReceive(state.readBuff, 0, 1024, 0, recvCallback, state);
            }
            catch (SocketException se) {
                Console.WriteLine("Recv failed:"+se.ToString());
            }

        }

        #endregion
        #region 同步服务器(有阻塞)
        static void syncServer()
        {
            Console.WriteLine("同步Server启动~");
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //绑定地址和端口号
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 8888);//形成一个网络节点
            socket.Bind(ipEndPoint);
            socket.Listen(0);//表示指定队列中最多可接受的连接数，0代表不受限制

            //开始监听来自客户端的请求
            while (true)
            {
                //接收请求
                Socket aConnect = socket.Accept();
                Console.WriteLine("服务器收到一个连接.");

                //接收数据
                byte[] recvBytes = new byte[1024];
                int clientSendBytesCount = aConnect.Receive(recvBytes);
                string clientStr = System.Text.Encoding.UTF8.GetString(recvBytes);
                Console.WriteLine("服务器收到来自客户端的数据：" + clientStr);

                //给客户端的东西
                byte[] toClientBytes = System.Text.Encoding.UTF8.GetBytes("服务器发送的" + clientStr);
                aConnect.Send(toClientBytes);
            }
        }
        #endregion

        #region 同步服务器(改善1)
        static void syncServerImproving()
        {
            Console.WriteLine("同步Server启动~");
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //绑定地址和端口号
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 8888);//形成一个网络节点
            socket.Bind(ipEndPoint);
            socket.Listen(0);//表示指定队列中最多可接受的连接数，0代表不受限制
            Console.WriteLine("同步Server启动成功~");
            //开始监听来自客户端的请求
            while (true)
            {
                if (socket == null) return;
                if (socket.Poll(0, SelectMode.SelectRead)){
                    readListenfd(socket);
                }
                foreach (var s in clients.Values){
                    Socket clientfd = s.socket;
                    if (clientfd.Poll(0, SelectMode.SelectRead)){
                        if (!readClientfd(clientfd)) break;
                    }
                }
                //防止死循环
                System.Threading.Thread.Sleep(1);
            }
        }

        //读取来自客户端的socket
        public static void readListenfd(Socket listenfd) {
            Console.WriteLine("Accept!");

            Socket clientfd = listenfd.Accept();
            ClientState state = new ClientState();
            state.socket = clientfd;
            clients.Add(clientfd, state);
        }

        public static bool readClientfd(Socket clientfd) {
            ClientState state = clients[clientfd];

            //处理接收
            int count = 0;
            try
            {
                count = clientfd.Receive(state.readBuff);
            }
            catch (SocketException se) {
                clientfd.Close();
                clients.Remove(clientfd);
                Console.WriteLine("Socket recv failed!info: "+se.ToString());
                return false;
            }

            //表示客户端关闭
            if (count == 0) {
                clientfd.Close();
                clients.Remove(clientfd);
                Console.WriteLine("Socket close." );
                return false;
            }
            string recvStr = System.Text.Encoding.UTF8.GetString(state.readBuff,0,count);
            Console.WriteLine("Recv "+recvStr);
            string sendStr = clientfd.RemoteEndPoint.ToString() + recvStr;
            byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(sendStr);

            foreach (var skt in clients.Values) {
                skt.socket.Send(sendBytes);
            }
            return true;
        }
        #endregion

    }
}
