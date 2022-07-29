using System;
using System.Collections;
using System.Linq;
using MelonLoader;
using UnityEngine;
using Harmony;
using System.Diagnostics;
using System.Reflection;
using ABI_RC.Core.Player;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.InteropServices;

namespace Nocturnal
{
    public class Main : MelonMod
    {
      
        
        private static Harmony.HarmonyInstance _instance = new Harmony.HarmonyInstance(Guid.NewGuid().ToString());
        private static GameObject _espGameObject { get; set; }
        private static float s_yValue { get; set; }
        internal static Shader s_materialShader { get; set; }
        public override void OnApplicationStart()
        {    
            Hook();
            MelonCoroutines.Start(WaitForUi());
            new Config();
        }

        private static IEnumerator WaitForUi()
        {
            while (GameObject.Find("/Cohtml") == null)
                yield return null;
            using (WebClient wc = new WebClient())
            {
                AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromMemory(wc.DownloadData("https://nocturnal-client.xyz/Resources/outline"));
                GameObject ShaderGameObject = myLoadedAssetBundle.LoadAsset<GameObject>("shdaers");
                myLoadedAssetBundle.Unload(false);
                GameObject NewObject = GameObject.Instantiate(ShaderGameObject, GameObject.Find("/Cohtml").transform);
                s_materialShader = NewObject.transform.Find("Capsule").gameObject.GetComponent<MeshRenderer>().materials.FirstOrDefault<Material>().shader;
                NewObject.SetActive(false);
                wc.Dispose();
            }
            yield break;
        }
        private static unsafe void Hook() => _instance.Patch(typeof(ABI_RC.Core.Player.PuppetMaster).GetMethod(nameof(ABI_RC.Core.Player.PuppetMaster.AvatarInstantiated)), null, typeof(Main).GetMethod(nameof(OnAvatarChanged), System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).ToNewHarmonyMethod());

        private static void OnAvatarChanged(ABI_RC.Core.Player.PuppetMaster __instance)
        {
            s_yValue = __instance.gameObject.GetComponentInChildren<ABI.CCK.Components.CVRAvatar>().viewPosition.y;
            if (__instance.gameObject.name == "_PLAYERLOCAL") return;
            if (__instance.transform.Find("Esp") != null)
            {
                __instance.transform.Find("Esp").transform.localPosition = new Vector3(0, s_yValue / 1.6f, 0);
                __instance.transform.Find("Esp").transform.localScale = new Vector3(s_yValue / 1.6f, s_yValue / 1.4f, s_yValue / 1.5f);
                return;
            }
            __instance.gameObject.AddComponent<Outline>();
        }
      
    }



}
