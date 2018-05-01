using UnityEngine;
using System.Collections;

public class MouseInputTest : MonoBehaviour {

    public Vector3 RecordPosition;

    void OnMouseDown() 
    {
        Debug.Log("落下!");
        RecordPosition = Input.mousePosition;
    }

    void OnMouseUp() 
    {
        Debug.Log("放开!");
    }


    void OnGUI() 
    {
        //var a =  Input.mousePosition - RecordPosition;
        //GUI.Label(new Rect(0, 0, 100, 100), "鼠标的坐标位置:  " + a.x + "-" + a.y + "-" + a.z);
        //RecordPosition = a;

        var b = Input.mousePosition - RecordPosition;
        this.transform.position = new Vector3(b.x,transform.position.y,transform.position.z);

        GUI.Label(new Rect(0, 0, 100, 100), "鼠标的坐标位置:  " + b.ToString());
    }



}
