using UnityEngine;
using System.Collections;

public class MainScene : MonoBehaviour {

    public static int width = Screen.width;
    public static int height = Screen.height;

    

    void Start() 
    {
        //this.GetComponent<UISprite>().width = width;   
    }

    void OnGUI() 
    {
        GUILayout.Label("宽度" + width);
        GUILayout.Label("高度" + height);

    }


}
