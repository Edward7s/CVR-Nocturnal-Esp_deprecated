using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Reflection;
using ABI_RC.Core.Savior;

namespace Nocturnal
{

    public class Json
    {
        public int[] DefaultColor { get; set; }
        public int[] FriendsColor { get; set; }
        public int[] Legend { get; set; }
        public int[] Guide { get; set; }
        public int[] Mod { get; set; }
        public int[] Dev { get; set; }

        public float Width { get; set; }
        public float FallOff { get; set; }

    }

    internal class Config
    {
        public static Color DefaultColor { get; set; }
        public static Color FriendsColor { get; set; }
        public static Color Legend { get; set; }
        public static Color Guide { get; set; }
        public static Color Mod { get; set; }
        public static Color Dev { get; set; }
        internal static Json s_js { get; set; }
        private static int[] s_fArr { get; set; }

        public Config()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "//Nocturnal"))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "//Nocturnal");

            if (!File.Exists(Directory.GetCurrentDirectory() + "//Nocturnal//ESP_Config1.1.Json"))
                File.WriteAllText(Directory.GetCurrentDirectory() + "//Nocturnal//ESP_Config1.1.Json", JsonConvert.SerializeObject(new Json()
                {
                    DefaultColor = new int[] { 255, 8, 90 },
                    FriendsColor = new int[] { 255, 251, 0 },
                    Legend = new int[] { 227, 129, 0 },
                    Guide = new int[] { 0, 199, 7 },
                    Mod = new int[] { 158, 0, 29 },
                    Dev = new int[] { 77, 0, 14 },        
                    FallOff = 0.2f,
                    Width = 0.5f
                }));

            s_js = JsonConvert.DeserializeObject<Json>(File.ReadAllText(Directory.GetCurrentDirectory() + "//Nocturnal//ESP_Config1.1.Json"));
            PropertyInfo[] ConfigProps = typeof(Config).GetProperties(BindingFlags.Public | BindingFlags.Static);
            PropertyInfo[] JsonProps =  s_js.GetType().GetProperties();
            for (int i = 0; i < ConfigProps.Length; i++)
            {
                s_fArr = (int[])JsonProps.FirstOrDefault(x => x.Name == ConfigProps[i].Name).GetValue(s_js);
                ConfigProps[i].SetValue(typeof(Config),(Color)new Color32(byte.Parse(s_fArr[0].ToString()), byte.Parse(s_fArr[1].ToString()), byte.Parse(s_fArr[2].ToString()),255));
            }
        }

    }
}
