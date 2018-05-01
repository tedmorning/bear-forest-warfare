using UnityEngine;
using System.Collections;

public class ChangRoleHead : MonoBehaviour {

    private UISprite head;
    public UISprite headBorder;

	void Awake(){
		head = this.GetComponent<UISprite>();
	}

    public void Update() 
    {
        switch(GameManager.roleIndex)
        {
            case 0://熊大
                head.spriteName = "XCM_Effect_XDFace_Armature0_0";
                head.MakePixelPerfect();
                if (headBorder != null) headBorder.spriteName = "XCM_UI3_8";
                break;
            case 2://翠花
				head.spriteName = "XCM_Effect_XEFace_Armature0_5";
                if (headBorder != null) headBorder.spriteName = "XCM_UI3_10";
                head.MakePixelPerfect();
                break;
            case 1://熊二
                head.spriteName = "XCM_Effect_CHFace_Armature0_4";
                if (headBorder != null) headBorder.spriteName = "XCM_UI3_9";
                head.MakePixelPerfect();
                break;
            case 3://翠花
                head.spriteName = "XCM_Effect_GTQFace_Armature0_0";
                if (headBorder != null) headBorder.spriteName = "XCM_UI4_2";
                head.MakePixelPerfect();
                break;
        }
        
    }

}
