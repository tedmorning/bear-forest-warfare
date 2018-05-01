using UnityEngine;
using System.Collections;
using BearGame;

/// <summary>
/// 人物的移动
/// </summary>
public class PlayerMove : MonoBehaviour
{
    private bool isMove;
    private Vector3 recordMouseDownPosition;            //记录鼠标单击的位子
    public float speed = 1;
    public UI2DSprite player;
    public UI2DSpriteAnimation playerAnimation;
    public Sprite playerLeftState;
    public Sprite playerRightState;

    //角色移动的边界
    private float areaWidth;            //边界宽度
    private float areaHeight;           //边界高度
    public PlayerBody body;

    //进场动画相关
    public float enterSpeed;
    private GameObject enterSceneTarget;

    public int power;           //当前的角色的威力

    public static PlayerMove staticPlayer;

    void Awake()
    {
        //根据屏幕大小获取边界的宽度和高度
        areaWidth = Screen.width - player.drawingDimensions.z;
        areaHeight = Screen.height - player.drawingDimensions.w;
        body = GetComponentInChildren<PlayerBody>();
        staticPlayer = this;
    }

    void Start() 
    {
        enterSceneTarget = PointNode.RoleCenterNodeStatic;
    }

    public void PlayerStartMove()
    {
        isMove = true;
        recordMouseDownPosition = Input.mousePosition;
    }


    public void PlayerEndMove()
    {
        isMove = false;
    }

    public void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMove = true;
            recordMouseDownPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMove = false;
        }
    }

    void LateUpdate()
    {
        if (GameManager.gameState == GameStateEnum.GamePlayer)
        {
            switch (body.state)
            {
                case EnemyStateEnum.Move:
                    if (isMove)
                    {
                        RoleMove();
                    }
                    break;
                case EnemyStateEnum.ComeOnAnimation:        //出场动画
                    ComeOnTheStage();
                    break;
            }
        }
    }

    /// <summary>
    /// 出场动画
    /// </summary>
    public void ComeOnTheStage()
    {


        transform.localPosition = Vector3.Lerp(transform.localPosition, enterSceneTarget.transform.localPosition, enterSpeed);



        if (Vector3.Distance(transform.localPosition, enterSceneTarget.transform.localPosition) < 2f)
        {
            body.roleBox.enabled = true;
            body.state = EnemyStateEnum.Move;
        }
    }

    public void RoleMove() 
    {
        Vector3 dir = Input.mousePosition - recordMouseDownPosition;

        //切换左右人物状态
        if (dir.x > 0)
        {
            playerAnimation.enabled = false;
            player.nextSprite = playerRightState;
        }
        else if (dir.x < 0)
        {
            playerAnimation.enabled = false;
            player.nextSprite = playerLeftState;

        }
        else if (dir.x == 0)
        {
            playerAnimation.enabled = true;
        }


        this.transform.localPosition = new Vector3(
                transform.localPosition.x + dir.x * 0.7f,
                transform.localPosition.y + dir.y * 0.7f,
                transform.localPosition.z);

        //防止跑出右边界
        if (transform.localPosition.x > areaWidth && dir.x > 0)
        {
            this.transform.localPosition = new Vector3(
                    areaWidth,
                    transform.localPosition.y,
                    transform.localPosition.z);
        }

        //防止跑出左边界
        if (transform.localPosition.x < -areaWidth && dir.x < 0)
        {
            this.transform.localPosition = new Vector3(
                    -areaWidth,
                    transform.localPosition.y,
                    transform.localPosition.z);
        }

        //防止跑出上边界
        if (transform.localPosition.y > areaHeight && dir.y > 0)
        {
            this.transform.localPosition = new Vector3(
                    transform.localPosition.x,
                    areaHeight,
                    transform.localPosition.z);
        }

        //防止跑出下边界
        if (transform.localPosition.y < -areaHeight && dir.y < 0)
        {
            this.transform.localPosition = new Vector3(
                    transform.localPosition.x,
                    -areaHeight,
                    transform.localPosition.z);
        }

        recordMouseDownPosition = Input.mousePosition;

    }


    public void RoleRestore() 
    {
   
    }

    void OnGUI()
    {
        //GUILayout.Label("鼠标的坐标:" + Input.mousePosition.x);
        //GUILayout.Label("人物的坐标:" + transform.localPosition.x);

        //GUILayout.Label("屏幕宽度:" + Screen.width);
        //GUILayout.Label("屏幕高度:" + Screen.height);

        //GUILayout.Label("width:" + areaWidth);
        //GUILayout.Label("height:" + areaHeight);

        //GUILayout.Label("x:" + transform.localPosition.x);
        //GUILayout.Label("y:" + transform.localPosition.y);
    }

    void OnCollisionEnter() 
    {

    }
}
