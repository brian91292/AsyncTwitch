using System;
using System.IO;
using System.Linq;
using System.Text;
using NLog;
using UnityEngine;
using Logger = NLog.Logger;

namespace AsyncTwitch
{
    [Serializable]
    public struct Config
    {
        public string Username;
        public string ChannelName;
        public string OauthKey;
        private Logger _logger;

        public Config(string username, string channelName, string oauthKey)
        {
            Username = username.ToLower();
            ChannelName = channelName.ToLower();
            OauthKey = oauthKey;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public static void CreateDefaultConfig()
        {
            Config defaultConfig = new Config("", "", "");
            defaultConfig.SaveJson();
            defaultConfig._logger.Info("Creating default config file.");
        }

        public void SaveJson()
        {
            using(FileStream fs = new FileStream("UserData/AsyncTwitchConfig.json", FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = Encoding.ASCII.GetBytes(JsonUtility.ToJson(this, true));
                fs.Write(buffer, 0, buffer.Length);
                _logger.Info("Saving config file.");
            }
        }

        public static Config LoadFromJson()
        {
            Config tempConfig = new Config("", "", "");
            try
            {
                if (File.Exists("UserData/AsyncTwitchConfig.json"))
                {
                    using (FileStream fs = new FileStream("UserData/AsyncTwitchConfig.json", FileMode.Open, FileAccess.Read))
                    {
                        byte[] loadBytes = new byte[fs.Length];
                        fs.Read(loadBytes, 0, (int)fs.Length);
                        tempConfig = JsonUtility.FromJson<Config>(Encoding.ASCII.GetString(loadBytes));

                        if (tempConfig.OauthKey != String.Empty && !tempConfig.OauthKey.ToLower().StartsWith("oauth:"))
                            tempConfig.OauthKey = "oauth:" + tempConfig.OauthKey;
                        tempConfig.ChannelName = tempConfig.ChannelName?.ToLower();
                        tempConfig.Username = tempConfig.Username?.ToLower();

                        return tempConfig;
                    }
                }
            }
            catch (Exception e)
            {
                tempConfig._logger.Info($"Error when trying to read config: {e}");
            }
            CreateDefaultConfig();
            return tempConfig;
        }

        public override string ToString()
        {
            return "Username: " + Username + "\nChannel: " + ChannelName + "\nOauth Key: " + OauthKey;
        }
    }
}
