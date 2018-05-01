using UnityEngine;
using System.Collections;
using BearGame;
using System;

/// <summary>
/// 角色信息类 
/// </summary>
public class GameManager{
    
    /// <summary>
    /// 威力等级
    /// </summary>
    public static int powerLevel;           
    
    /// <summary>
    /// 护盾等级
    /// </summary>
    public static int shieldLevel;         
 
    /// <summary>
    /// 暴走等级
    /// </summary>
    public static int rampageLevel;      
  
    /// <summary>
    /// 必杀等级
    /// </summary>
    public static int slayLevel;           

    /// <summary>
    /// 掉落等级
    /// </summary>
    public static int drioOutLevel;        

    /// <summary>
    /// 宝石数量
    /// </summary>
    public static int gemCount;

    /// <summary>
    /// 必杀导弹数量
    /// </summary>
    public static int slayCount;

    /// <summary>
    /// 护盾数量
    /// </summary>
    public static int shieldCount;


    /// <summary>
    /// 桃心加成
    /// </summary>
    public static int heartCount;

    /// <summary>
    /// 当前打开的关卡锁
    /// </summary>
    public static int level;

    /// <summary>
    /// 上次签到的时间
    /// </summary>
    public static string previousSignTime;

    /// <summary>
    /// 是否第一次进入游戏
    /// 0新用户
    /// 1老用户
    /// </summary>
    public static int firstSignGame = 1;

    /// <summary>
    /// 游戏的状态
    /// </summary>
    public static GameStateEnum gameState = GameStateEnum.GamePause;

    /// <summary>
    /// 累计签到了几天
    /// </summary>
    public static int addeveryday;

    /// <summary>
    /// 缓存的服务器时间
    /// </summary>
    public static string cacheDateTime;

    /// <summary>
    /// 表示是否拥有新手礼包0没有购买,1表示购买
    /// </summary>
    public static int newHandGift;


    /// <summary>
    /// 表示玩家是否拥有该角色
    /// </summary>
    public static int isOpenRole2;
    public static int isOpenRole3;
    public static int isOpenRole4;
    
    //购买技能所需要的钻石
    public static int[] powerLevelList = { 3000, 4000, 5000, 6000, 7000, 8000, 9000, 9999 };
    public static int[] shieldLevelList = { 4000, 5000, 6000, 7000, 8000 };
    public static int[] rampageLevelList = { 3000, 3500, 4000, 4500, 5000 };
    public static int[] slayLevelList = { 2000, 2000, 2000, 2000, 2000 };
    public static int[] drioOutLevelList = {1000, 1000, 1000, 1000, 1000 };

    //技能升级的效果
    public static float[] singleBulletList = { 1, 1.5f, 2, 2.5f, 3, 3.5f, 4, 4.5f, 5 };     //子弹的威力
    public static float[] shieldTimerList = { 10, 15, 20, 25, 30, 35 };                     //护盾持续的时间
    public static int[] slayCountList = { 1, 2, 3, 4, 5, 6 };                               //导弹的数量
    public static float[] rampageList = { 1, 1.25f, 1.5f, 1.75f, 2 };


    public static int[] levelRewardGem = { 1000, 2000, 3000, 4000, 5000, 6000 };


    public static int GetPowerLevelGem() 
    {
        if (powerLevel >= powerLevelList.Length) 
        {
            return powerLevelList[powerLevelList.Length - 1];
        }
        return powerLevelList[powerLevel];
    }

    public static int GetShieldLevelGem()
    {
        if (shieldLevel >= shieldLevelList.Length) 
        {
            return shieldLevelList[shieldLevelList.Length - 1];
        }
        return shieldLevelList[shieldLevel];
    }

    public static int GetRampageLevelGem()
    {
        if (rampageLevel >= rampageLevelList.Length) 
        {
            return rampageLevelList[rampageLevelList.Length - 1];
        }
        return rampageLevelList[rampageLevel];
    }

    public static  int GetSlayLevelGem()
    {
        if (slayLevel >= slayLevelList.Length) 
        {
            return slayLevelList[slayLevelList.Length - 1];
        }
        return slayLevelList[slayLevel];
    }

    public static int GetDrioOutLevelGem()
    {
        if (drioOutLevel >= drioOutLevelList.Length) 
        {
            return drioOutLevelList[drioOutLevelList.Length - 1];
        }
        return drioOutLevelList[drioOutLevel];
    }

    public static void SavePowerLevel() 
    {
        PlayerPrefs.SetInt("powerLevel", powerLevel);
        PlayerPrefs.SetInt("gemCount", gemCount);
    }
    public static void SaveRampageLevel()
    {
        PlayerPrefs.SetInt("rampageLevel", rampageLevel);
        PlayerPrefs.SetInt("gemCount", gemCount);
    }
    public static void SaveShieldLevel()
    {
        PlayerPrefs.SetInt("shieldLevel", shieldLevel);
        PlayerPrefs.SetInt("gemCount", gemCount);
    }
    public static void SaveDrioOutLevel()
    {
        PlayerPrefs.SetInt("drioOutLevel", drioOutLevel);
        PlayerPrefs.SetInt("gemCount", gemCount);
    }
    public static void SaveSlayLevel()
    {
        PlayerPrefs.SetInt("slayLevel", slayLevel);
        PlayerPrefs.SetInt("gemCount", gemCount);
    }
    public static void SaveFirstSignGame() 
    {
        PlayerPrefs.SetInt("firstSignGame", firstSignGame);
    }

    public static void SavePreviousSignTime() 
    {
        PlayerPrefs.SetString("previousSignTime", previousSignTime);
    }

    /// <summary>
    /// 减去宝石
    /// </summary>
    /// <param name="gem"></param>
    public static void SubGem(int gem) 
    {
        gemCount -= gem;
        PlayerPrefs.SetInt("gemCount",gemCount);
    }

    public static void AddGem(int gem) 
    {
        gemCount += gem;
        PlayerPrefs.SetInt("gemCount", gemCount);
    }


    /// <summary>
    /// 从本地更新到游戏中
    /// </summary>
    public static void LocalhostUpdateGameInfo() 
    {
        powerLevel = PlayerPrefs.GetInt("powerLevel");
        rampageLevel = PlayerPrefs.GetInt("rampageLevel");
        shieldLevel = PlayerPrefs.GetInt("shieldLevel");
        drioOutLevel = PlayerPrefs.GetInt("drioOutLevel");
        slayLevel = PlayerPrefs.GetInt("slayLevel");
        gemCount = PlayerPrefs.GetInt("gemCount");

        slayCount = PlayerPrefs.GetInt("slayCount");
        shieldCount = PlayerPrefs.GetInt("shieldCount");

        previousSignTime = PlayerPrefs.GetString("previousSignTime");
        firstSignGame = PlayerPrefs.GetInt("firstSignGame");
        addeveryday = PlayerPrefs.GetInt("Addeveryday");

        level = PlayerPrefs.GetInt("level");


        //是否开启其他角色
        isOpenRole2 = PlayerPrefs.GetInt("isOpenRole2");
        isOpenRole3 = PlayerPrefs.GetInt("isOpenRole3");
        isOpenRole4 = PlayerPrefs.GetInt("isOpenRole4");

        newHandGift = PlayerPrefs.GetInt("newHandGift");

        heartCount = PlayerPrefs.GetInt("heartCount");
    }


    /// <summary>
    /// 从游戏更新到本地
    /// </summary>
    public static void GameInfoUpdateLocalhost()
    {
        PlayerPrefs.SetInt("powerLevel", powerLevel);
        PlayerPrefs.SetInt("rampageLevel", rampageLevel);
        PlayerPrefs.SetInt("shieldLevel", shieldLevel);
        PlayerPrefs.SetInt("drioOutLevel", drioOutLevel);
        PlayerPrefs.SetInt("slayLevel", slayLevel);
        PlayerPrefs.SetInt("gemCount", gemCount);

        PlayerPrefs.SetInt("slayCount", slayCount);
        PlayerPrefs.SetInt("shieldCount", shieldCount);

        PlayerPrefs.SetString("previousSignTime", previousSignTime);
        PlayerPrefs.SetInt("firstSignGame", firstSignGame);
        PlayerPrefs.SetInt("Addeveryday",addeveryday);

        PlayerPrefs.SetInt("level", level);

        //是否开启其他角色
        PlayerPrefs.SetInt("isOpenRole2", isOpenRole2);
        PlayerPrefs.SetInt("isOpenRole3", isOpenRole3);
        PlayerPrefs.SetInt("isOpenRole4", isOpenRole4);

        PlayerPrefs.SetInt("newHandGift", newHandGift);

        PlayerPrefs.SetInt("heartCount",heartCount);

    }


    //===========================不需要保存的数据
    /// <summary>
    /// 当前第几波
    /// </summary>
    public static int wave = 0;

    /// <summary>
    /// 当前选择的关卡
    /// </summary>
    public static int selectLevel = 0;

    /// <summary>
    /// 是否暴走
    /// </summary>
    public static bool isRampage;

    /// <summary>
    /// 当前选中的角色
    /// </summary>
    public static int roleIndex;

	/// <summary>
	/// 显示选择关卡面板
	/// </summary>
	public static int showSelectLevel;

    /// <summary>
    /// 当前关卡获得的宝石
    /// </summary>
    public static int levelGem;

}
