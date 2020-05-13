/**
 * 
 * googlegis 2020.5.4
 * 
 * 
 * */
using System;
using System.IO;
using System.Net;

namespace pbfDownLoader
{
    class Program
    {
        static void Main(string[] args)
        {
			//https://basemaps.arcgis.com/arcgis/rest/services/World_Basemap_v2/VectorTileServer/resources/fonts/Arial%20Bold/0-255.pbf
			//不是所有的字体都在这个地址，主要取决于地图中使用的字体，然后在chrome 的 devTool 中可以看到引用的pbf地址。
            var regularUrl = "https://static.arcgis.com/fonts/arial-unicode-ms-regular/";
            var boldUrl = "https://static.arcgis.com/fonts/arial-unicode-ms-bold/";

            var regularFolder = @"D:\Fonts\regularFolder\"; //手动建好目录，
            var boldFolder = @"D:\Fonts\boldFolder\";//手动建好目录，

            var NetURL = new string[2] { regularUrl,boldUrl};

            var downFolder = new string[2] { regularFolder,boldFolder};

            for (int u = 0; u < 2; u++)
            {
                var downurl = NetURL[u];

                var downFold = downFolder[u];

                for (int i = 0; i < 257; i++)
                {
                    double si = i * 256;
                    double ei = (i + 1) * 256 - 1;

                    var fileName = si.ToString() + "-" + ei.ToString() + ".pbf";

                    var fileUrl = downurl + fileName;

                    var fileDown = downFold + fileName;

                    HttpDownloadFile(fileUrl, fileDown);
                }

            }

        }

        /// <summary>
        /// Http下载文件
        /// </summary>
        public static void HttpDownloadFile(string url, string path)
        {
            try
            {
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                Stream stream = new FileStream(path, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                request.Abort();
                response.Close();
                responseStream.Close();
                stream.Close();
                Console.WriteLine("File:" + path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("File:" + path);
                Console.WriteLine("Error:" + ex.Message);
            }

            //return path;
        }
    }
}
