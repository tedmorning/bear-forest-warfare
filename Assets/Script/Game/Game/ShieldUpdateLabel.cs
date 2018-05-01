using UnityEngine;
using System.Collections;

public class ShieldUpdateLabel : MonoBehaviour
{
    public UILabel heartLabel;
    public string prefixChar;


    void Update()
    {
        heartLabel.text = prefixChar + GameManager.shieldCount.ToString();
    }
}
