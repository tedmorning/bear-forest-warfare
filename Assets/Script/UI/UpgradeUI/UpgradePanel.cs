using UnityEngine;
using System.Collections;

public class UpgradePanel : MonoBehaviour {

    private GameObject upgradePanel;        //升级页面
    public GameObject GemChongZhiPanel;     //获取宝石页面
    public GameObject SelectLevelPanel;     //选择关卡页面


    //技能升级相关
    public UILabel powerLabel;    
    public UILabel drioOutLabel;
    public UILabel shieldLabel;
    public UILabel slayLabel;
    public UILabel rampageLabel;
    

    public IncreaseSlider powerSlider;
    public IncreaseSlider drioOutSlider;
    public IncreaseSlider shieldSlider;
    public IncreaseSlider slaySlider;
    public IncreaseSlider rampageSlider;


    void Awake() 
    {
        upgradePanel = this.gameObject;
        GameManager.LocalhostUpdateGameInfo();
        UpdateBuyGem();         //更新购买技能的标价
        UpdateZhiSlider();      
    }

    //更新当前玩家技能的等级slider
    private void UpdateZhiSlider() 
    {
        for (int i = 0; i < GameManager.powerLevel; i++)
        {
            powerSlider.AddSprite();
        }
        for (int i = 0; i < GameManager.shieldLevel; i++)
        {
            shieldSlider.AddSprite();
        }
        for (int i = 0; i < GameManager.drioOutLevel; i++)
        {
            drioOutSlider.AddSprite();
        }
        for (int i = 0; i < GameManager.rampageLevel; i++)
        {
            rampageSlider.AddSprite();
        }
        for (int i = 0; i < GameManager.slayLevel; i++)
        {
            slaySlider.AddSprite();
        }
    }

    /// <summary>
    /// 打开充值宝石页面
    /// </summary>
    public void ShowGemChongZhiPanel() 
    {
        GemChongZhiPanel.SetActive(true);
    }

    /// <summary>
    /// 关闭充值宝石页面
    /// </summary>
    public void CancelChongZhiPanel()
    {
        GemChongZhiPanel.SetActive(false);
    }

    /// <summary>
    /// 返回游戏关卡选择页面
    /// </summary>
    public void GoBackSelectLevelPanel() 
    {
        this.gameObject.SetActive(false);
        SelectLevelPanel.SetActive(true);
    }


    #region 升级相关
    /// <summary>
    /// 升级威力
    /// </summary>
    public void UpgradePower() 
    {
        //说明已经到达最高级别,不能在升级了
        if (GameManager.powerLevel == GameManager.powerLevelList.Length) return;

        //能购买
        if (GameManager.gemCount > GameManager.GetPowerLevelGem())
        {
            GameManager.SubGem(GameManager.GetPowerLevelGem());
            GameManager.powerLevel++;
            GameManager.SavePowerLevel();

            //更新下一等级购买所需的数值
            UpdateBuyGem();
            powerSlider.AddSprite();
        }
        else 
        {
            ShowGemChongZhiPanel();
        }
    }

    /// <summary>
    /// 升级护盾
    /// </summary>
    public void UpgradeShield()
    {
        //说明已经到达最高级别,不能在升级了
        if (GameManager.shieldLevel == GameManager.shieldLevelList.Length) return;

        //能购买
        if (GameManager.gemCount > GameManager.GetPowerLevelGem())
        {
            GameManager.SubGem(GameManager.GetShieldLevelGem());
            GameManager.shieldLevel++;
            GameManager.SaveShieldLevel();

            //更新下一等级购买所需的数值
            UpdateBuyGem();

            //添加值
            shieldSlider.AddSprite();
        }
        else
        {
            ShowGemChongZhiPanel();
        }
    }


    /// <summary>
    /// 升级暴走
    /// </summary>
    public void UpgradeRampage()
    {
        //说明已经到达最高级别,不能在升级了
        if (GameManager.rampageLevel == GameManager.rampageLevelList.Length) return;

        //能购买
        if (GameManager.gemCount > GameManager.GetRampageLevelGem())
        {
            GameManager.SubGem(GameManager.GetRampageLevelGem());
            GameManager.rampageLevel++;
            GameManager.SaveRampageLevel();

            //更新下一等级购买所需的数值
            UpdateBuyGem();

            //添加值
            rampageSlider.AddSprite();
        }
        else
        {
            ShowGemChongZhiPanel();
        }
    }

    /// <summary>
    /// 掉率
    /// </summary>
    public void UpgradeDrioOut()
    {
        //说明已经到达最高级别,不能在升级了
        if (GameManager.drioOutLevel == GameManager.drioOutLevelList.Length) return;

        //能购买
        if (GameManager.gemCount > GameManager.GetDrioOutLevelGem())
        {
            GameManager.SubGem(GameManager.GetDrioOutLevelGem());
            GameManager.drioOutLevel++;
            GameManager.SaveDrioOutLevel();

            //更新下一等级购买所需的数值
            UpdateBuyGem();

            //添加值
            drioOutSlider.AddSprite();
        }
        else
        {
            ShowGemChongZhiPanel();
        }
    }

    /// <summary>
    /// 升级必杀
    /// </summary>
    public void UpgradeSlay()
    {
        //说明已经到达最高级别,不能在升级了
        if (GameManager.slayLevel == GameManager.slayLevelList.Length) return;

        //能购买
        if (GameManager.gemCount > GameManager.GetSlayLevelGem())
        {
            GameManager.SubGem(GameManager.GetSlayLevelGem());
            GameManager.slayLevel++;
            GameManager.SaveSlayLevel();

            //更新下一等级购买所需的数值
            UpdateBuyGem();

            //添加值
            slaySlider.AddSprite();
        }
        else
        {
            ShowGemChongZhiPanel();
        }
    }


    #endregion


    private void UpdateBuyGem() 
    {
        powerLabel.text = GameManager.GetPowerLevelGem().ToString();
        drioOutLabel.text = GameManager.GetDrioOutLevelGem().ToString();
        shieldLabel.text = GameManager.GetShieldLevelGem().ToString();
        slayLabel.text = GameManager.GetSlayLevelGem().ToString();
        rampageLabel.text = GameManager.GetRampageLevelGem().ToString();
    }

    private 

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.gemCount += 4000;
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            UpgradePower();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SelectLevelPanel.SetActive(true);
            upgradePanel.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.C)){
            PlayerPrefs.DeleteAll();
        }


    }



}
