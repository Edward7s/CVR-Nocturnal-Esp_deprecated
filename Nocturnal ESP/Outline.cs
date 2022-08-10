using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ABI_RC.Core.Networking.IO.Social;
namespace Nocturnal
{
    internal class Outline : MonoBehaviour
    {
        public Color _outlineColor { get; set; }
        private GameObject _espGameObject { get; set; }
        void Awake()
        {
            switch (this.gameObject.GetComponent<ABI_RC.Core.Player.PlayerDescriptor>().userRank)
            {
                case "Legend":
                    _outlineColor = Config.Legend;
                    break;
                case "Community Guide":
                    _outlineColor = Config.Guide;
                    break;
                case "Moderator":
                    _outlineColor = Config.Mod;
                    break;
                case "Developer":
                    _outlineColor = Config.Dev;
                    break;
                default:
                    _outlineColor = Config.DefaultColor;
                    break;
            }
            if (ABI_RC.Core.InteractionSystem.ViewManager.Instance.FriendList.FirstOrDefault(x => x.UserId == gameObject.name) != null)
                _outlineColor = Config.FriendsColor;
            float Y = gameObject.GetComponentInChildren<ABI.CCK.Components.CVRAvatar>().viewPosition.y;
            _espGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            _espGameObject.layer = 5;
            _espGameObject.name = "Esp";
            _espGameObject.transform.parent = this.transform;
            _espGameObject.transform.localEulerAngles = Vector3.zero;
            _espGameObject.transform.localPosition = new Vector3(0, Y / 1.6f, 0);
            _espGameObject.transform.localScale = new Vector3(Y / 1.5f, Y / 1.4f, Y / 1.5f);
            _espGameObject.GetComponent<MeshRenderer>().allowOcclusionWhenDynamic = false;
            Component.Destroy(_espGameObject.GetComponent<CapsuleCollider>());
            Material material = new Material(Main.s_materialShader);
            material.EnableKeyword("_falloff");
            material.SetFloat("_falloff", Config.s_js.FallOff * 20);
            material.EnableKeyword("_Color");
            material.SetColor("_Color", _outlineColor);
            material.EnableKeyword("width");
            material.SetFloat("_Width", Config.s_js.Width);
            material.EnableKeyword("_ToggleSizeD");
            material.SetFloat("_ToggleSizeD", 1);
            _espGameObject.GetComponent<Renderer>().material = material;
        }
    }
}
