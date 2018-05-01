using UnityEngine;
using UnityEditor;
using System.Collections;

namespace PlateFace
{

    [CustomEditor(typeof(SendMessageTo))]
    public class SendMessageToEditor : Editor
    {   
        public override void OnInspectorGUI()
        {
            SendMessageTo item = target as SendMessageTo;
            serializedObject.Update();                                  // 序列化更新，不解释。  
            
            item.tager = EditorGUILayout.ObjectField("目标:", item.tager, typeof(GameObject)) as GameObject;
            item.functionName = EditorGUILayout.TextField("方法名:", item.functionName).ToString();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("MessageType"));


            switch (item.MessageType)
            {
                case SendMessageTo.MesType.@default:
                    break;
                case SendMessageTo.MesType.@int:
                    item.@int = EditorGUILayout.IntField("参数(int)", item.@int);
                    break;
                case SendMessageTo.MesType.@string:
                    item.@string = EditorGUILayout.TextField("参数(string)", item.@string).ToString();
                    break;
                case SendMessageTo.MesType.@object:
                    item.@object = EditorGUILayout.ObjectField("参数(object)", item.@object, typeof(GameObject)) as GameObject;
                    break;
            }

            // 更新编辑后的数据。         
            serializedObject.ApplyModifiedProperties();
        }  
	}
}

