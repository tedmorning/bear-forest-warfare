using UnityEngine;
using System.Collections;

public class EverydayPanel : MonoBehaviour {

    public PropItems [] items = new PropItems[7];

    void OnEnable() 
    {
        items[GameManager.addeveryday].PropBorder.SetActive(true);
    }



    /// <summary>
    /// 领取每日道具
    /// </summary>
    public void GetProp() 
    {
        switch(GameManager.addeveryday)
        {
            case 1:
                GameManager.slayCount++;
                break;
            case 2:
                GameManager.heartCount++;
                break;
            case 3:
                GameManager.gemCount += 3888;
                break;
            case 4:
                GameManager.slayCount+=2;
                break;
            case 5:
                GameManager.shieldCount += 2;
                break;
            case 6:
                GameManager.gemCount += 8888;
                break;
            case 7:
                GameManager.gemCount += 3;
                break;
        }

        this.gameObject.SetActive(false);
        GameManager.previousSignTime = GameManager.cacheDateTime;
        GameManager.addeveryday++;
        GameManager.addeveryday %= 7;


        //Debug.Log("记录当前时间:" + GameManager.cacheDateTime);
        //Debug.Log("累计签到天数:" + GameManager.addeveryday);

        GameManager.GameInfoUpdateLocalhost();

    }
}
