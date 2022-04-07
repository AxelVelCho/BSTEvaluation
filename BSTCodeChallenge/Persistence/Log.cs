using System.Text;

namespace BSTCodeChallenge.Controllers
{
    public class Log
    {
        public static void RecordLog(TimeSpan ts)
        {
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            string path = @".\Log\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Response Time {DateTime.Now.ToString()}: {elapsedTime}");
            File.AppendAllText($"{path}ResponseTimelog.txt", sb.ToString());
            sb.Clear();
        }
    }
}
