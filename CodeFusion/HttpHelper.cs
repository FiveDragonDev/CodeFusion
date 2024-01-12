using System.Diagnostics;
using System.Net;

namespace CodeFusion
{
    public static class HttpHelper
    {
        public static void Open(string url)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        public static async Task<string> SendGetRequest(string url)
        {
            using HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
        [Obsolete]
        public static string? Get(string url)
        {
            HttpWebRequest? request = (HttpWebRequest)WebRequest.Create(url);
            if (request == null) return null;
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new(response.GetResponseStream());
            string result = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return result;
        }
        [Obsolete]
        public static string? Post(string url, string data)
        {
            HttpWebRequest? request = (HttpWebRequest)WebRequest.Create(url);
            if (request == null) return null;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(data);
            request.ContentLength = bytes.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new(response.GetResponseStream());
            string result = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return result;
        }
        [Obsolete]
        public static byte[] Download(string url) => new WebClient().DownloadData(url);
        [Obsolete]
        public static void Upload(string url, byte[] data) => new WebClient().UploadData(url, data);
        public static string Encode(string data) => System.Web.HttpUtility.UrlEncode(data);
        public static string Decode(string data) => System.Web.HttpUtility.UrlDecode(data);
    }
}
