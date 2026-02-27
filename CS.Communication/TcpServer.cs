using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CS.Communication
{
    public class TcpServer
    {
        private int _numOfMaxLinks = 500;
        private ConcurrentQueue<TcpClient> _clientsQueue = new ConcurrentQueue<TcpClient>(); //这个是线程安全的队列，适合多线程环境下使用，避免了锁的使用，提高了性能


        public void TcpServerRun()
        {
            TcpListener server = null;
            try
            {
                // 监听此端口
                server = new TcpListener(IPAddress.Parse("127.0.0.1"), 6000);
                // 启动服务器
                server.Start();


                //TASK:专接收连接服务器的客户端
                Task.Run(() =>
                {
                    AcceptClients(server);
                });

                //TASK: 专门处理已连接客户端请求
                while (true)
                {
                    if (_clientsQueue.TryDequeue(out TcpClient client))
                    {
                        Task.Run(() => { GetClientMes(client); });
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        private void AcceptClients(TcpListener server)
        {
            // 无限循环监听客户端
            while (true)
            {
                // 阻塞调用：等待客户端连接（也可以用 server.AcceptSocket() 替代）
                //using TcpClient client = server.AcceptTcpClient(); 用了using就完蛋，直接释放
                TcpClient client = server.AcceptTcpClient();
                _clientsQueue.Enqueue(client);
            }
        }


        private void GetClientMes(TcpClient client)
        {
            Byte[] bytes = new Byte[256];
            String data = null;


            // 获取用于读写数据的流对象
            using NetworkStream stream = client.GetStream();
            int i;

            try
            {
                // 循环接收客户端发送的所有数据
                while ((i = stream.Read(bytes, 0, bytes.Length)) > 0)
                {
                    // 提取远程客户端的ip地址
                    IPEndPoint clientEndPoint = (IPEndPoint)client.Client.RemoteEndPoint; //抽象协议转具体协议实例
                    string ip = clientEndPoint.Address.MapToIPv4().ToString();

                    // 将字节数据转换为UTF8字符串
                    data = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                    Console.WriteLine("收到数据: {0}", data);

                    // 处理客户端数据：转为大写
                    data = data.ToUpper();

                    // 将处理后的字符串编码为字节数组
                    byte[] msg = System.Text.Encoding.UTF8.GetBytes($"From Server : Send To {ip}{data} \r\n");

                    // 向客户端发送响应
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("发送数据: {0}", data);
                }
            }
            catch (Exception ex)
            {
                client.Dispose();
                client = null;
                throw ;
                //throw ex;  听说这种会丢失原始异常的堆栈信息，不推荐使用，直接 throw 就行了，能保留原始异常的堆栈信息，更有利于调试和定位问题。

            }



        }

    }
}
