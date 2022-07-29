using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
namespace Nocturnal
{

    public class Json
    {
        public int[] DefaultColor { get; set; }

        public int[] FriendsColor { get; set; }

        public float Width { get; set; }
        public float FallOff { get; set; }

    }

    internal class Config
    {
        internal static Color s_defaultColor { get; set; }
        internal static Color s_friendsColor { get; set; }
        internal static Json s_js { get; set; }
        public Config()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "//Nocturnal"))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "//Nocturnal");

            if (!File.Exists(Directory.GetCurrentDirectory() + "//Nocturnal//ESP_Config.Json"))
                File.WriteAllText(Directory.GetCurrentDirectory() + "//Nocturnal//ESP_Config.Json", JsonConvert.SerializeObject(new Json()
                {
                    DefaultColor = new int[] { 255, 8, 90 },
                    FriendsColor = new int[] { 255, 251, 0 },
                    FallOff = 0.2f,
                    Width = 0.5f
                }));

            s_js = JsonConvert.DeserializeObject<Json>(File.ReadAllText(Directory.GetCurrentDirectory() + "//Nocturnal//ESP_Config.Json"));
            s_defaultColor = new Color32(byte.Parse(s_js.DefaultColor[0].ToString()), byte.Parse(s_js.DefaultColor[1].ToString()), byte.Parse(s_js.DefaultColor[2].ToString()),255);
            s_friendsColor = new Color32(byte.Parse(s_js.FriendsColor[0].ToString()), byte.Parse(s_js.FriendsColor[1].ToString()), byte.Parse(s_js.FriendsColor[2].ToString()), 255);
         }

    }
}
