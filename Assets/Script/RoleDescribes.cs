using UnityEngine;
using System.Collections;

namespace PlateFace
{
	/// <summary>
	/// 角色Label描述类
	/// </summary>
	public class RoleDescribes : MonoBehaviour {

        public string[] describes = 
        {
            "[99ff00]森林吃货 熊二[-]\n座驾:飞天激光\n满级开启奇幻\n激光弹幕增加\n攻击力100%",
            "[99ff00]空战精英 翠花[-]\n座驾:粉系流光\n满级开启流光\n击破弹幕,增\n加攻击力200%",
            "[99ff00]暴走大哥 熊大[-]\n座驾:无敌战车\n满级开启星星\n连弹弹幕可消\n增加攻击力300%",
            "[99ff00]万人迷 光头强[-]\n座驾:钢铁之躯\n购买即开启激\n光射线,满级\n增加攻击力\n400%"
        };

        private UILabel label;




		void Start()
        {
              
        }

		void Awake()
        {
            label = GetComponent<UILabel>();  
        }
		void Update(){}
		

        public void ChangeRoleLabel()
        {
            label.text = describes[GameManager.roleIndex];
        }

	}
}

