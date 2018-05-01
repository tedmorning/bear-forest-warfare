using UnityEngine;
using System.Collections;

namespace PlateFace
{
	/// <summary>
	/// 盘子脸类
	/// </summary>
	public class SendMessageTo : MonoBehaviour {

        public enum MesType
        {
            @default,
            @int,
            @string,
            @object
        }

        public GameObject tager;
        public string functionName;
        public MesType MessageType = MesType.@default;

        public int @int;
        public string @string;
        public GameObject @object;


        void OnClick()
        {
            if (tager != null && functionName != "")
            {
                switch (MessageType)
                {
                    case MesType.@default:
                        tager.SendMessage(functionName);
                        break;
                    case MesType.@int:
                        tager.SendMessage(functionName, @int);
                        break;
                    case MesType.@string:
                        tager.SendMessage(functionName, @string);
                        break;
                    case MesType.@object:
                        tager.SendMessage(functionName, @object);
                        break;
                    default:
                        break;
                }
            }
        }
		
	}
}

