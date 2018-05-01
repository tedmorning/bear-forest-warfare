using UnityEngine;
using System.Collections;

public class IncreaseSlider :MonoBehaviour
{

    public GameObject UIZhiSprite;      //值
    public UIAtlas atlas;               //纹理
    private int count = 0;
    
    //添加一个显示值
    public void AddSprite() 
    {
        // var gameObj = (GameObject)Instantiate(UIZhiSprite);
        var gameObj = NGUITools.AddChild<UISprite>(UIZhiSprite,false);
        gameObj.transform.parent = this.transform;

        gameObj.atlas = atlas;
        gameObj.spriteName = "XCM_UI0_10";
        gameObj.SetDimensions(12, 14);
        gameObj.transform.localScale = new Vector3(1, 1, 1);
        gameObj.depth = 2;
        gameObj.name = "zhi";
        gameObj.transform.localPosition = new Vector3(
            gameObj.transform.localPosition.x + 12.2f * count,
            gameObj.transform.localPosition.y,
            gameObj.transform.localPosition.z
            );

        count++;

        //NGUITools.AddChild(gameObj);
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            AddSprite();
        }
    }

}
