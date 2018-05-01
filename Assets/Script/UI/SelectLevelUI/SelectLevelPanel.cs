using UnityEngine;
using System.Collections;
using System;

public class SelectLevelPanel : MonoBehaviour {

    private GameObject selectLevelPanel;
    public GameObject upgradePanel;                 //角色能力升级界面
    public GameObject noviceGiftPanel;              //新手大礼包充值界面
    public GameObject startGamePanel;               //开始面板
    public GameObject selectRolePanel;
    public UISprite levelBackground;                //选择关卡背景
    public int level;                               //当前关卡
    private GameObject levelNode;                   //当前选择的关卡节点
    public GameObject newGift;                 //新手礼包

    void Start() 
    {
        selectLevelPanel = this.gameObject;

        //新用户打开每日签到面板
        if (GameManager.firstSignGame == 0)
        {
            startGamePanel.GetComponent<StartPanel>().ShowSignIn();
            GameManager.previousSignTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            Debug.Log("用户第一次时间:" + GameManager.previousSignTime);

            GameManager.firstSignGame = 1;
            GameManager.SaveFirstSignGame();
        }


        if (GameManager.newHandGift == 0)
        {
            newGift.SetActive(true);
        }
        else 
        {
            newGift.SetActive(false);
        }

        ChongZhi.instance._buySuccee -= instance__buySuccee;
        ChongZhi.instance._buySuccee += instance__buySuccee;

    }

    void instance__buySuccee(int buyType)
    {
        if (buyType == 1) 
        {
            newGift.SetActive(false);
            CancelNoviceGiftPanel();
        }
    }

    /// <summary>
    /// 开始游戏
    /// </summary>
    public void EnterGame() 
    {
        selectLevelPanel.SetActive(false);
        selectRolePanel.SetActive(true);
        //Application.LoadLevel("Level1");
    }


    /// <summary>
    /// 进入角色升级界面
    /// </summary>
    public void ShowUpgradePanel() 
    {
        selectLevelPanel.SetActive(false);
        upgradePanel.SetActive(true);
    }

    /// <summary>
    /// 显示新手大礼包充值
    /// </summary>
    public void ShowNoviceGiftPanel() 
    {
        noviceGiftPanel.SetActive(true);
    }


    /// <summary>
    /// 关闭新手大礼包充值
    /// </summary>
    public void CancelNoviceGiftPanel() 
    {
        noviceGiftPanel.SetActive(false);
    }

    public void SetLevelNode(GameObject levelNode) 
    {
        this.levelNode = levelNode;

        //切换背景
        switch(level)
        {
            case 1:
                levelBackground.spriteName = "selectPassBg_0";
                break;
            case 2:
                levelBackground.spriteName = "selectPassBg_2";
                break;
            case 3:
                levelBackground.spriteName = "selectPassBg_3";
                break;
            case 4:
                levelBackground.spriteName = "selectPassBg_4";
                break;
            case 5:
                levelBackground.spriteName = "selectPassBg_5";
                break;
        }
        
        

    }

    public void CancelRoadSign() 
    {
        LevelNode node =  levelNode.GetComponent<LevelNode>();
        node.notOpen.SetActive(true);
        node.roadSign.SetActive(false);
        node.levelSprite.enabled = false;
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            selectLevelPanel.SetActive(false);
            startGamePanel.SetActive(true);
        }
    }

}
