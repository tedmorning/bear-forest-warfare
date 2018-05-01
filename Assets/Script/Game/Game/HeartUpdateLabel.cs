using UnityEngine;
using System.Collections;

public class HeartUpdateLabel : MonoBehaviour
{
    public UILabel heartLabel;
    public string prefixChar;

    void Update() 
    {
        heartLabel.text = prefixChar + GameManager.heartCount.ToString();
    }



}
