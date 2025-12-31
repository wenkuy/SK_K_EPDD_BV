using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF
{
    public class ProtocolParsing
    {
        /// <summary>
        /// 类对象转字节数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static byte[] Serialize<T>(T t)
        {
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, t);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// 字节数组转类对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T Deserialize<T>(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                return Serializer.Deserialize<T>(stream);

            }
        }



    }
}
