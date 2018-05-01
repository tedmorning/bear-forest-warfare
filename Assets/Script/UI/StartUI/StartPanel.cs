using UnityEngine;
using System.Collections;
using System;

public class StartPanel : MonoBehaviour {

    private GameObject startPanel;              //开始界面
    public GameObject aboutPanel;               //游戏关于界面
    public GameObject selectLevelPanel;         //选择关卡页面
    public GameObject storyPanel;               //故事界面
    public GameObject exitGamePanel;            //结束游戏界面
	public GameObject upgradePanel;				//升级面板
    public GameObject exerydayPanel;            //每日签到面板


    public GameObject btnSoundOFF;              //关闭声音按钮
    public GameObject btnSoundON;               //开启声音按钮

    void Start() 
    {
		startPanel = this.gameObject;
        GameManager.LocalhostUpdateGameInfo();          //把游戏数据读取到游戏中
		if (GameManager.showSelectLevel == 1) {
			startPanel.SetActive(false);
			selectLevelPanel.SetActive(true);
            return;
		}else if(GameManager.showSelectLevel == 2){
			startPanel.SetActive(false);
			upgradePanel.SetActive(true);
            return;
        }

        #if UNITY_EDITOR
        //GameManager.firstSignGame = 1;
        //GameManager.previousSignTime = "2015-2-23 13:4:45";
        #endif


        if (GameManager.firstSignGame == 1)
        {
            startPanel.SetActive(false);
            selectLevelPanel.SetActive(true);

            //为了使用协同,必须把脚本挂到一个游戏物体上面
            GameObject obj = new GameObject();
            GameObject a = (GameObject)Instantiate(obj);
            a.AddComponent<ServerTime>();
            a.GetComponent<ServerTime>().GetServerTime(ShowSignIn);
        }
    }



    private void ShowSignIn(string time) {
        var d1 = DateTime.Parse(time);
        var d2 = DateTime.Parse(GameManager.previousSignTime);
        var d3 = d1 - d2;

        //Debug.Log("新时间:" + d1);
        //Debug.Log("上次时间:" + d2);
        //Debug.Log(d3);

        startPanel.SetActive(false);
        selectLevelPanel.SetActive(true);

        if (d3.Days >= 1) 
        {
            GameManager.cacheDateTime = time;
            ShowSignIn();
        }


    }

    public void ShowSignIn()
    {
        exerydayPanel.SetActive(true);
    }

    /// <summary>
    /// 显示关于我们界面
    /// </summary>
    public void ShowAbout() 
    {
        
        aboutPanel.SetActive(true);
    }

    /// <summary>
    /// 关闭关于我们界面
    /// </summary>
    public void CancelAbout()
    {
        aboutPanel.SetActive(false);
    }

    /// <summary>
    /// 关闭声音
    /// </summary>
    public void TurnOffSound() 
    {
        btnSoundOFF.SetActive(true);
        btnSoundON.SetActive(false);
        AudioListener.volume = 0f;
    }

    /// <summary>
    /// 开启声音
    /// </summary>
    public void TurnNOSound()
    {
        btnSoundOFF.SetActive(false);
        btnSoundON.SetActive(true);
        AudioListener.volume = 1f;
    }


    /// <summary>
    /// 显示故事界面
    /// </summary>
    public void ShowStoryPanel() 
    {
        startPanel.SetActive(false);
        storyPanel.SetActive(true);
    }

    /// <summary>
    /// 关闭故事界面
    /// </summary>
    public void ShowStoryAnimationFinish() 
    {
        storyPanel.SetActive(false);
        selectLevelPanel.SetActive(true);
    }

    void Update() 
    {

#if UNITY_EDITOR
        //返回键
        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.F1))
        {
            exitGamePanel.SetActive(true);
        }

        if (Input.GetKey(KeyCode.F2)) 
        {
            GameManager.firstSignGame = 1;
            GameManager.SaveFirstSignGame();
        }

#endif





		if(Input.GetKeyDown(KeyCode.W))
		{
			startPanel.SetActive(false);
			selectLevelPanel.SetActive(true);
		}

    }

    public void ShowStartGamePanel() 
    {
        startPanel.SetActive(false);
        selectLevelPanel.SetActive(true);
    }


}
