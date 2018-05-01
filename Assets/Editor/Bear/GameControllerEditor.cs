using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor {


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameController edit = (GameController)target;

        //if (GUILayout.Button("添加", GUILayoutOption))
        //{
        //    GUILayout.Label("我在编辑Scene视图中");
        //}

      

        
        //GUILayout.EndArea();


    }


    public void OnGUI() 
    {
        GUILayout.Label("我在编辑Scene视图中");
    }

}
