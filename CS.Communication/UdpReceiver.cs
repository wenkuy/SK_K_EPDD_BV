using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CS.Communication
{
    public class UdpReceiver
    {
        public static  Action<byte[]> UDPMessAction ;
        private Queue<CacheMes> CacheMessages = new Queue<CacheMes>();
        private static readonly object _lockobj = new object();

        public UdpReceiver()
        {
           

        }

        public void UdpReceiveRun()
        {
            Task.Run(() => { ReceiveUdpSender(); }); //监听发送端
            Task.Run(() => { AnalyzeDatasFromCache(); }); //处理数据
        }


        /// <summary>
        /// 监听发送端，并接收数据
        /// </summary>
        private void ReceiveUdpSender()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8000);  //该端口的任意IP地址

            // 2. 监听: 该端口的任意IP地址（using自动释放资源）
            using (UdpClient receiver = new UdpClient(localEndPoint))
            {
                while (true)
                {
                    try
                    {
                        // 接收数据
                        IPEndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0); //自动填充“发送端的IP+端口”
                        byte[] dates = receiver.Receive(ref senderEndPoint);

                        // 消息放入队列
                        string ip = senderEndPoint.Address.MapToIPv4().ToString();
                        int port = senderEndPoint.Port;

                        lock (_lockobj)
                        {
                            CacheMessages.Enqueue(new CacheMes() { IP = ip, Port = port, Datas = dates });

                            //队列缓存限制
                            if (CacheMessages.Count > 100)
                            {
                                CacheMessages.Dequeue();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"接收失败：{ex.Message}");
                    }
                }
            }
        }


        /// <summary>
        /// 处理接收数据
        /// </summary>
        private void AnalyzeDatasFromCache()
        {
            byte[] dates = new byte[1];
            while (true)
            {
                lock (_lockobj)
                {
                    if (CacheMessages.Count > 0)
                    {
                        dates = CacheMessages.Dequeue().Datas;
                        //Debug.WriteLine($"手打字节数：{dates.Length}");
                    }
                }
                if (dates.Length > 1)
                {
                    UDPMessAction?.Invoke(dates);
                }
                dates = new byte[1];
            }
        }
    }
}
