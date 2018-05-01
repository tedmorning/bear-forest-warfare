using UnityEngine;
using System.Collections;
using PlateFace;

public class SelectRolePanel : MonoBehaviour
{

    private GameObject selectRolePanel;
    public GameObject forestGiftPanel;
    public GameObject selectLevelPanel;


    //切换角色相关
    public GameObject[] roleList;          //角色列表
    public GameObject[] origin;             //角色列表原点
    private float moveSpeed;                //切换角色移动的速度
    private GameObject currentRole;         //当前角色
    private GameObject newRole;             //新出现的角色
    private bool isChangeRole;              //切换角色锁,防止连续点击

    private Transform tempPositon;
    private bool isNewRoleMove;             //新角色移动
    private bool isCurrentRoleMove;         //当前角色移动

    public Transform leftRolePosition;      //左边角色目标点
    public Transform rightRolePosition;     //右边角色目标点

    public RoleDescribes roleDescribes;

    public GameObject btnStartGame;
    public GameObject btnGet;


    void Awake()
    {
        moveSpeed = 0.1f;
        GameManager.roleIndex = 0;
        currentRole = roleList[0].gameObject;
    }


    void Start() 
    {
        selectRolePanel = this.gameObject;
        roleDescribes.ChangeRoleLabel();            //显示角色描述

        ChongZhi.instance._buySuccee -= instance__buySuccee;
        ChongZhi.instance._buySuccee += instance__buySuccee;
    }



    void instance__buySuccee(int buyType)
    {
        if (buyType == 3 || buyType == 5 || buyType == 4) 
        {
            btnStartGame.SetActive(true);
            btnGet.SetActive(false);
        }
    }

    /// <summary>
    /// 打开森林大礼包
    /// </summary>
    public void OpenForestGift()
    {
        forestGiftPanel.SetActive(true);
    }

    /// <summary>
    /// 关闭森林大礼包
    /// </summary>
    public void CancelForestGift()
    {
        forestGiftPanel.SetActive(false);
    }

    /// <summary>
    /// 显示选择关卡页面
    /// </summary>
    public void ShowSelectLevelPanel()
    {
        selectRolePanel.SetActive(false);
        selectLevelPanel.SetActive(true);

    }

    /// <summary>
    /// 上一个角色
    /// </summary>
    public void PreviousRole()
    {
        if (isChangeRole) return;
        isChangeRole = true;

        GameManager.roleIndex--;
        if (GameManager.roleIndex < 0) GameManager.roleIndex = roleList.Length - 1;
        roleList[GameManager.roleIndex].transform.position = rightRolePosition.transform.position;
        roleList[GameManager.roleIndex].SetActive(true);
        newRole = roleList[GameManager.roleIndex];

        tempPositon = leftRolePosition;
        isCurrentRoleMove = true;
        isNewRoleMove = true;

        roleDescribes.ChangeRoleLabel();

        LockRole();
    }

    /// <summary>
    /// 下一个角色
    /// </summary>
    public void NextRole()
    {
        if (isChangeRole) return;
        isChangeRole = true;


        GameManager.roleIndex++;
        if (GameManager.roleIndex > roleList.Length - 1) GameManager.roleIndex = 0;
        roleList[GameManager.roleIndex].transform.position = leftRolePosition.transform.position;
        roleList[GameManager.roleIndex].SetActive(true);
        newRole = roleList[GameManager.roleIndex];

        tempPositon = rightRolePosition;
        isCurrentRoleMove = true;
        isNewRoleMove = true;

        roleDescribes.ChangeRoleLabel();

        LockRole();

    }


    /// <summary>
    /// 是否开启角色选择
    /// </summary>
    private void LockRole() 
    {
        btnStartGame.SetActive(true);
        btnGet.SetActive(false);

        switch (GameManager.roleIndex)
        {
            case 1:
                if (GameManager.isOpenRole2 == 0) 
                {
                    btnGet.SetActive(true);
                    btnStartGame.SetActive(false);
                }
                break;
            case 2:
                if (GameManager.isOpenRole3 == 0)
                {
                    btnGet.SetActive(true);
                    btnStartGame.SetActive(false);
                }
                break;
            case 3:
                if (GameManager.isOpenRole4 == 0)
                {
                    btnGet.SetActive(true);
                    btnStartGame.SetActive(false);
                }
                break;
        }
    }


    public void GetRoleChongZhi()
    {
        switch (GameManager.roleIndex)
        {
            case 1:
                ChongZhi.instance.SendMessage("BuyProp", 3);
                break;
            case 2:
                ChongZhi.instance.SendMessage("BuyProp", 4);
                break;
            case 3:
                ChongZhi.instance.SendMessage("BuyProp", 5);
                break;
        }
    }


    void Update()
    {
        //当前显示的角色,向左右移动
        if (isCurrentRoleMove)
        {
            currentRole.transform.localPosition = Vector3.Lerp(currentRole.transform.localPosition
                , tempPositon.transform.localPosition, moveSpeed);


            if (Mathf.Abs(currentRole.transform.localPosition.x - tempPositon.transform.localPosition.x) < 2f)
            {
                currentRole.SetActive(false);
                isCurrentRoleMove = false;
            }
        }

        //新出来的角色想中心店移动
        if (isNewRoleMove)
        {
            newRole.transform.localPosition = Vector3.Lerp(newRole.transform.localPosition
                                 , Vector3.zero, moveSpeed);

            if (Mathf.Abs(newRole.transform.localPosition.x) < 2f)
            {
                newRole.transform.localPosition = Vector3.zero;

                isNewRoleMove = false;
                currentRole = newRole;
                isChangeRole = false;
            }
        }


        if(Input.GetKeyDown(KeyCode.Escape))
        {
            selectLevelPanel.SetActive(true);
            selectRolePanel.SetActive(false);
        }

    }

    public void EnterGame() 
    {
        Application.LoadLevel("Level1");
    }

}
