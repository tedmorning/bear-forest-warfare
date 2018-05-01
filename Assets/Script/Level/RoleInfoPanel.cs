using UnityEngine;
using System.Collections;

public class RoleInfoPanel : MonoBehaviour {


    public GameObject chongZhiSlayPanel;
    public GameObject chongZhiShieldPanel;
    public GameObject gamePausePanel;
    public GameObject gameRevivePanel;
	public GameObject gamePassPlane;			//通关面板
	public GameObject gameNotPassPlane;			//通关失败面板

    public UILabel reviveNumberLabel;           //复活倒计时
	
	//血值相关
    public UISprite[] heartIconList;
    private int heartIconIndex;
    public static bool isIntoRevive;            //进入充值界面


    public void Awake() 
    {
        heartIconIndex = heartIconList.Length - 1;
        SoundController.off();
    }

    public void Start() 
    {
        ChongZhi.instance._buySuccee -= instance__buySuccee;
        ChongZhi.instance._buySuccee += instance__buySuccee;
    }

    /// <summary>
    /// 显示充值护盾页面
    /// </summary>
    public void ShowChongZhiShield() 
    {
        SoundController.PlayFXNGUITools("按键");


        if (GameManager.shieldCount <= 0)
        {
            GameManager.shieldCount = 0;
            chongZhiShieldPanel.SetActive(true);
            GameManager.gameState = BearGame.GameStateEnum.GamePause;           //游戏暂停
        }
        else 
        {
            if (PlayerBody.player.shieldNode.isOpen == false)
            {
                GameManager.shieldCount--;
                PlayerBody.player.OpenShield();     //启动护盾
            }

        }
    }

    /// <summary>
    /// 充值导弹页面
    /// </summary>
    public void ShowChongZhiSlay() 
    {

        SoundController.PlayFXNGUITools("按键");

        if (GameManager.slayCount <= 0)
        {
            GameManager.slayCount = 0;
            chongZhiSlayPanel.SetActive(true);
            GameManager.gameState = BearGame.GameStateEnum.GamePause;
        }
        else 
        {
            GameManager.slayCount--;
            PlayerBody.player.OpenSlay();            //启动导弹

        }

    }

    /// <summary>
    /// 关闭护盾页面
    /// </summary>
    public void CancelChongZhiShield() 
    {
        if(PlayerBody.player.hp > 0)
        {
            GameManager.gameState = BearGame.GameStateEnum.GamePlayer;
        }

        chongZhiShieldPanel.SetActive(false);

    }

    /// <summary>
    /// 关闭导弹页面
    /// </summary>
    public void CancelChongZhiSlay()
    {
        if (PlayerBody.player.hp > 0)
        {
            GameManager.gameState = BearGame.GameStateEnum.GamePlayer;
        }
        
        chongZhiSlayPanel.SetActive(false);
    }

    /// <summary>
    /// 游戏暂停方法
    /// </summary>
    public void GamePause() 
    {
        GameManager.gameState = BearGame.GameStateEnum.GamePause;
        gamePausePanel.SetActive(true);
    }

    /// <summary>
    /// 继续游戏
    /// </summary>
    public void GamePlayer() 
    {
        if(PlayerBody.player.hp > 0)
        {
            GameManager.gameState = BearGame.GameStateEnum.GamePlayer;
        }
        gamePausePanel.SetActive(false);
    }

    /// <summary>
    /// 放弃当前关卡
    /// </summary>
    public void ToGiveUpLevel() 
    {
        GameManager.gameState = BearGame.GameStateEnum.GamePause;
        Application.LoadLevel(0);
    }


    /// <summary>
    /// 显示人物复活界面
    /// </summary>
    public void ShowRoleRevivePanel()
    {

        chongZhiSlayPanel.SetActive(false);
        chongZhiShieldPanel.SetActive(false);
        gamePausePanel.SetActive(false);
        gameRevivePanel.SetActive(false);

        gameRevivePanel.SetActive(true);            //显示人物复活界面
        
        //经过5秒之后显示游戏未通过界面
        StartCoroutine(ShowGameNotPassTimePanel());
    }

    public IEnumerator ShowGameNotPassTimePanel() 
    {
        yield return new WaitForSeconds(1f);
        reviveNumberLabel.text = "5";
        SoundController.PlayFXNGUITools("倒计时");
        yield return new WaitForSeconds(1f);
        reviveNumberLabel.text = "4";
        SoundController.PlayFXNGUITools("倒计时");
        yield return new WaitForSeconds(1f);
        reviveNumberLabel.text = "3";
        SoundController.PlayFXNGUITools("倒计时");
        yield return new WaitForSeconds(1f);
        reviveNumberLabel.text = "2";
        SoundController.PlayFXNGUITools("倒计时");
        yield return new WaitForSeconds(1f);
        reviveNumberLabel.text = "1";
        yield return new WaitForSeconds(1f);

        if (isIntoRevive == false)
        {
            GameManager.gemCount += GameManager.levelGem;
            GameManager.GameInfoUpdateLocalhost();
            
            //显示游戏结束界面
            ShowGameNotPassPanel();
        }

        isIntoRevive = false;
        
    }




    /// <summary>
    /// 关闭人物复活界面
    /// </summary>
    public void CancelRoleRevivePanel() 
    {
        
    }

    /// <summary>
    /// 显示被击败页面
    /// </summary>
    public void ShowDefeatedPanel() 
    {
   		
    }

    public void CenterRampage() 
    {
        GameController.CenterRampage();
    }

    /// <summary>
    /// 减去桃心
    /// </summary>
    public void SubHeart() 
    {
        heartIconList[heartIconIndex].enabled = false;
        heartIconIndex--;
    }

	/// <summary>
	/// 角色复活
	/// </summary>
	public void RoleRevive(){
        ChongZhi.instance.BuyProp(9);
    }

    void instance__buySuccee(int buyType)
    {
        if (9 == buyType) 
        {
            //关闭英雄复活界面
            gameRevivePanel.SetActive(false);
            gameNotPassPlane.SetActive(false);
            isIntoRevive = true;

            //显示桃心
            heartIconList[0].enabled = true;
            heartIconList[1].enabled = true;
            heartIconIndex = 1;

            GameManager.gameState = BearGame.GameStateEnum.GamePlayer;
            PlayerBody.player.RoleRevive();		//角色复活
        }
    }
    
	
    /// <summary>
    /// 显示通关完毕界面
    /// </summary>
    public IEnumerator ShowGamePassPanel()
	{
        chongZhiSlayPanel.SetActive(false);
        chongZhiShieldPanel.SetActive(false);
        gamePausePanel.SetActive(false);
        gameRevivePanel.SetActive(false);
        
        yield return new  WaitForSeconds(2f);
        GameManager.gameState = BearGame.GameStateEnum.GameEnd;

        gamePassPlane.SetActive (true);
        GamePassPanel gamePassCompoment = gamePassPlane.GetComponent<GamePassPanel>();
        GameManager.levelGem += GameManager.levelRewardGem[GameManager.selectLevel];

        Debug.Log(gamePassCompoment.gemLabel.text);
        GameManager.AddGem(GameManager.levelRewardGem[GameManager.selectLevel]);


        if(GameManager.selectLevel >= GameManager.level)
        {
            GameManager.level = GameManager.selectLevel + 1;
        }

    }

    /// <summary>
    /// 通关游戏
    /// </summary>
    public void PassGame() 
    {
        GameManager.showSelectLevel = 1;
        GameManager.GameInfoUpdateLocalhost();
        Application.LoadLevel(0);
    }

	public void ShowGameNotPassPanel(){
        gameRevivePanel.SetActive(false);
		gameNotPassPlane.SetActive (true);
	}


	public void ShowSelectLevelPanle(){
		GameManager.showSelectLevel = 1;
		Application.LoadLevel (0);
	}

	public void ShowUpgradePanel(){
		GameManager.showSelectLevel = 2;
		Application.LoadLevel (0);
	}

}
