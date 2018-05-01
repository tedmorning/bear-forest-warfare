using UnityEngine;
using System.Collections;

public class SlayUpdateLabel : MonoBehaviour
{
    public UILabel heartLabel;
    public string prefixChar;


    void Update()
    {
        heartLabel.text = prefixChar + GameManager.slayCount.ToString();
    }

}
