using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 充值类
/// </summary>
public class ChongZhi : MonoBehaviour
{
    public static ChongZhi instance;
    
    public bool isInitSDK;
    public SDKEnum SDKType = SDKEnum.telecom;


    private bool isInitSDKPrivate = false;

    /// <summary>
    /// 购买物品的类型
    /// </summary>
    [HideInInspector]
    public int buyType = 0;

    /// <summary>
    /// 计费点
    /// </summary>
    [HideInInspector]
    public string index = "";


    #region 事件
    public delegate void BuySuccee(int buyType);
    public event BuySuccee _buySuccee;
    #endregion

    #region 调试相关
    public static string error = "";
    #endregion


    protected void Start() 
    {
        if (instance == null) 
        {
            instance = this;
        }

        if (isInitSDKPrivate == false) 
        {
            switch (SDKType)
            {
                case SDKEnum.link:

                    break;
                case SDKEnum.alipay:

                    break;
                case SDKEnum.telecom:
                    TelecomSDK.instance.InitSDK();
                    break;
                case SDKEnum.mm:
                    MMSDK.instance.InitSDK();
                    break;
            }

            //防止下次在初始化
            isInitSDKPrivate = true;
        }
    }

    protected void Update() 
    {
#if UNITY_ANDROID
        if (SDKType == SDKEnum.telecom) 
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CmBillingAndroid.Instance.ExitWithUI();
            }
        }
#endif



    }

    protected void OnGUI() 
    {
        
        //GUI.Box(new Rect(0,0,300,300),error);
    }

    public void BuyProp(int buyType) 
    {
        this.buyType = buyType;
        ConvertIndex();



        if (isInitSDK)
        {
            error += "\r\n进入购买方法";

            switch (SDKType)
            {
                case SDKEnum.link:

                    break;
                case SDKEnum.alipay:

                    break;
                case SDKEnum.telecom:
                    TelecomSDK.instance.Buy(index, "ChongZhi", "BuyJudgeCallBack");
                    break;
                case SDKEnum.mm:
                    MMSDK.instance.Buy(index, "ChongZhi", "BuyJudgeCallBack");
                    break;
                default:
                    break;
            }
        }
        else 
        {
            error += "\r\n不用SDK购买";
            //如果不使用SDK就直接购买
            BuySuccess();
        }
    }

    public void ConvertIndex() 
    {

        switch (SDKType)
        {
            case SDKEnum.link:
                break;
            case SDKEnum.alipay:
                break;
            case SDKEnum.telecom:
                index = TelecomSDK.instance.AutoIndex(buyType.ToString());
                break;
            case SDKEnum.mm:
                index = MMSDK.instance.AutoIndex(buyType.ToString());
                break;
        }

        
    }

    public void BuyJudgeCallBack(string data) 
    {
        switch (SDKType)
        {
            case SDKEnum.link:
                break;
            case SDKEnum.alipay:
                break;
            case SDKEnum.telecom:
                if (TelecomSDK.instance.DisposeResult(data))
                {
                    BuySuccess();
                }
                else 
                {
                    //这里还没有写
                }
                break;
            case SDKEnum.mm:
                break;
        }
    }

    public void BuySuccess() 
    {
        switch (buyType)
        {
            case 1:         //新手大礼包
                GameManager.shieldCount++;
                GameManager.heartCount++;
                GameManager.slayCount++;
                GameManager.newHandGift = 1;
                break;
            case 2:         //20000钻石额外赠送20000
                GameManager.gemCount += 20000;
                GameManager.gemCount += 20000;
                break;
            case 3:         //人物翠花
                GameManager.isOpenRole2 = 1;
                break;
            case 4:         //人物熊二
                GameManager.isOpenRole3 = 1;
                break;
            case 5:         //人物光头强
                GameManager.isOpenRole4 = 1;
                break;
            case 6:         //6元森林大礼包
                GameManager.shieldCount += 3;
                GameManager.heartCount += 4;
                GameManager.slayCount += 3;
                break;
            case 7:         //10元森林大礼包
                GameManager.shieldCount += 6;
                GameManager.heartCount += 5;
                GameManager.slayCount += 6;
                break;
            case 8:         //20元森林大礼包
                GameManager.shieldCount += 15;
                GameManager.heartCount += 6;
                GameManager.slayCount += 15;
                break;
            case 9:         //复活
                GameManager.heartCount += 2;
                break;
            case 10:        //护盾购买
                GameManager.shieldCount += 3;
                break;
            case 11:        //大招购买
                GameManager.slayCount += 3;
                break;
        }

        GameManager.GameInfoUpdateLocalhost();


        Debug.Log("购买成功");

        try
        {
            _buySuccee(buyType);
        }
        catch { }

    }

    

}

public enum SDKEnum
{
    link,
    alipay,
    telecom,
    mm
}

#region adnroid SDK
/// <summary>
/// 移动MM
/// </summary>
public class MMSDK
{
    public static string mmAPPID = "";
    public static string mmAPPKEY = "";
    public static MMSDK instance = new MMSDK();

    public void InitSDK()
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("setKey", new object[] { MMSDK.mmAPPID, MMSDK.mmAPPKEY });
#endif
    }

    public void Buy(string index, string gameObject, string backCall)
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        //调用order静态方法,传递四个参数[当前页面,计费号,回调Unity的游戏物体名称,支付回调Unity的方法名]
        activity.Call("pay", new object[] { index, gameObject, backCall });
#endif
    }

    /// <summary>
    /// 转换计费点
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string AutoIndex(string buyType)
    {
        string index = string.Empty;
        switch (buyType)
        {
            case "1":         //新手大礼包
                index = "001";
                break;
            case "2":         //20000钻石
                index = "002";
                break;
            case "3":         //人物翠花
                index = "003";
                break;
            case "4":         //人物熊二
                index = "004";
                break;
            case "5":         //人物光头强
                index = "005";
                break;
            case "6":         //6元森林大礼包
                index = "006";
                break;
            case "7":         //10元森林大礼包
                index = "007";
                break;
            case "8":         //20元森林大礼包
                index = "008";
                break;
            case "9":         //复活
                index = "009";
                break;
            case "10":        //护盾购买
                index = "010";
                break;
            case "11":        //大招购买
                index = "011";
                break;
        }

        return index;
    }

    public bool DisposeResult(string data)
    {
#if UNITY_ANDROID
        return true;
#endif
    }
}

/// <summary>
/// 和游戏SDK
/// </summary>
public class TelecomSDK
{
    public static TelecomSDK instance = new TelecomSDK();

    /// <summary>
    /// 初始化SDK
    /// </summary>
    public void InitSDK()
    {
#if UNITY_ANDROID
        CmBillingAndroid.Instance.InitializeApp();
#endif
    }

    /// <summary>
    /// 购买商品
    /// </summary>
    /// <param name="index"></param>
    /// <param name="gameObject"></param>
    /// <param name="backCall"></param>
    public void Buy(string index, string gameObject, string backCall)
    {
#if UNITY_ANDROID

        if (!CmBillingAndroid.Instance.GetActivateFlag(index)) 
        {
            ChongZhi.error += "\r\n启动购买商品";
            CmBillingAndroid.Instance.DoBilling(true, true, index, "1122334455667789", gameObject, backCall);
        }

#endif
    }

    public void Buy(string index, string gameObject, string backCall, string orderNo)
    {
#if UNITY_ANDROID
        if (!CmBillingAndroid.Instance.GetActivateFlag(index))
            CmBillingAndroid.Instance.DoBilling(true, true, index, orderNo, gameObject, backCall);
#endif
    }


    /// <summary>
    /// 转换计费点
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string AutoIndex(string buyType)
    {
        string index = "001";
        switch (buyType)
        {
            case "1":         //新手大礼包
                index = "001";
                break;
            case "2":         //20000钻石
                index = "002";
                break;
            case "3":         //人物翠花
                index = "003";
                break;
            case "4":         //人物熊二
                index = "004";
                break;
            case "5":         //人物光头强
                index = "005";
                break;
            case "6":         //6元森林大礼包
                index = "006";
                break;
            case "7":         //10元森林大礼包
                index = "007";
                break;
            case "8":         //20元森林大礼包
                index = "008";
                break;
            case "9":         //复活
                index = "009";
                break;
            case "10":        //护盾购买
                index = "010";
                break;
            case "11":        //大招购买
                index = "011";
                break;
        }

        ChongZhi.error += "\r\n计费点:" + index;

        return index;
    }


    /// <summary>
    /// 购买的商品处理的结果
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool DisposeResult(string data)
    {
#if UNITY_ANDROID
        if (CmBillingAndroid.BillingResult.SUCCESS == data)
        {
            return true;
        }
        else
        {
            return false;
        }
#endif
    }

}
#endregion


#region IOS SDK

#endregion

/// <summary>
/// TakingData控制类
/// </summary>
public class TakingDataController
{
    public bool useTaklingData;
    public static TakingDataController instance = new TakingDataController();

#if UNITY_ANDROID
    private static TDGAAccount _TDGAAccount;
#endif




    public void OnChargeRequest(string order, string name, double price, string moneyType, double rmb, string paymentType)
    {

#if UNITY_ANDROID
        if (this.useTaklingData)
        {
            TDGAVirtualCurrency.OnChargeRequest(order, name, price, moneyType, rmb, paymentType);
        }
#endif

        

    }

    public void OnChargeSuccess(string order)
    {
#if UNITY_ANDROID
        if (this.useTaklingData)
        {
            TDGAVirtualCurrency.OnChargeSuccess(order);
        }
#endif
    }
    
    public void MissionBegin(string levelName)
    {
#if UNITY_ANDROID
        if (this.useTaklingData)
        {
            TDGAMission.OnBegin(levelName);
        }
#endif
    }

    public void MissionCompleted(string levelName, bool success, string cause = "")
    {
#if UNITY_ANDROID
        if (this.useTaklingData)
        {
            if (success)
                TDGAMission.OnCompleted(levelName);
            else
                TDGAMission.OnFailed(levelName, cause);
        }
#endif
    }

    public void InitTakingData(string key, string changel)
    {
 #if UNITY_ANDROID
        if (instance.useTaklingData)
        {
            TalkingDataGA.OnStart(key, changel);
            _TDGAAccount = TDGAAccount.SetAccount(TalkingDataGA.GetDeviceId());
            _TDGAAccount.SetAccountType(AccountType.ANONYMOUS);
            _TDGAAccount.SetLevel(1);
        }
#endif
    }
}

