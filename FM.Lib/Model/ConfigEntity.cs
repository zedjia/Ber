using System;
using System.Configuration;

namespace WC.Lib.Model
{
    public class ConfigEntity
    {
        public ConfigEntity()
        {
            //if (ConfigurationManager.AppSettings["SystemUrl"] == null ||
            //    ConfigurationManager.AppSettings["SocketServerIp"] == null ||
            //    ConfigurationManager.AppSettings["SocketServerPort"] == null ||
            //    ConfigurationManager.AppSettings["SpeakerIndex"] == null ||
            //    ConfigurationManager.AppSettings["AudioPort"] == null ||
            //    ConfigurationManager.AppSettings["MicrophoneIndex"] == null ||
            //    ConfigurationManager.AppSettings["MaxLengthOfUserID"] == null ||
            //    ConfigurationManager.AppSettings["CameraFramerate"] == null ||
            //    ConfigurationManager.AppSettings["DesktopFramerate"] == null ||
            //    ConfigurationManager.AppSettings["AudioQuality"] == null 

            //    )
            //    throw new ArgumentNullException("noneAppArgs", "未在配置文件中配置站点参数");

            if (ConfigurationManager.AppSettings["SystemUrl"] != null)
                SystemUrl = ConfigurationManager.AppSettings["SystemUrl"];
            if (ConfigurationManager.AppSettings["ConnectionString"] != null)
                ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            if (ConfigurationManager.AppSettings["SocketServerIp"] != null)
                SocketServerIp = ConfigurationManager.AppSettings["SocketServerIp"];
            if (ConfigurationManager.AppSettings["SocketServerPort"] != null)
                SocketServerPort = Convert.ToInt32(ConfigurationManager.AppSettings["SocketServerPort"]);
            if (ConfigurationManager.AppSettings["AudioPort"] != null)
                AudioPort = Convert.ToInt32(ConfigurationManager.AppSettings["AudioPort"]);
            if (ConfigurationManager.AppSettings["SpeakerIndex"] != null)
                SpeakerIndex = Convert.ToInt32(ConfigurationManager.AppSettings["SpeakerIndex"]);
            if (ConfigurationManager.AppSettings["MicrophoneIndex"] != null)
                MicrophoneIndex = Convert.ToInt32(ConfigurationManager.AppSettings["MicrophoneIndex"]);
            if (ConfigurationManager.AppSettings["MaxLengthOfUserID"] != null)
                MaxLengthOfUserID = byte.Parse(ConfigurationManager.AppSettings["MaxLengthOfUserID"]);
            if (ConfigurationManager.AppSettings["CameraFramerate"] != null)
                CameraFramerate = Convert.ToInt32(ConfigurationManager.AppSettings["CameraFramerate"]);
            if (ConfigurationManager.AppSettings["DesktopFramerate"] != null)
                DesktopFramerate = Convert.ToInt32(ConfigurationManager.AppSettings["DesktopFramerate"]);
            if (ConfigurationManager.AppSettings["AudioQuality"] != null)
                AudioQuality = ConfigurationManager.AppSettings["AudioQuality"];

        }


        public  string SystemUrl { get;private set; }
        public  string SocketServerIp { get; private set; }
        public  int SocketServerPort { get; private set; }
        public  int AudioPort { get; private set; }
        public  int SpeakerIndex { get; private set; }
        public  int MicrophoneIndex { get; private set; }

        public  byte MaxLengthOfUserID { get; private set; }
        public  int CameraFramerate { get; private set; }
        public  int DesktopFramerate { get; private set; }
        public  string AudioQuality { get; private set; }

        public  string ConnectionString { get; private set; }


    }
}
