using UnityEngine;
using System.Collections;
using BearGame;
using System;
using Bullet;

public class EnemyAI : MonoBehaviour
{

    //当然电脑的基本属性
    public EnemyStateEnum enemyState;            //主角移动的状态
    public float HP;                            //当前血值         
    private float originalHP;                   
    private BoxCollider bodyBox;                 //当前机器人box
    public UISprite HPSprite;                   //血值
    private bool isDeadLock;

    //射击相关
    private EnemyEmission enemyEmission;
    private float [] emissionMSTimer;               //多久射击一次


    //暴走行为相关
    private int rampage = 0;
    private int rampageRandom;
    private float rampageTime = 1f;
    private float rampageTimer;
    private float rampageRunTimer = 5f;
    public int rampageCent;


    //左右移动部分
    private float moveDistance;                 //左右移动的距离
    private Vector3 moveTargetDistance;         //要移动到的目标点

    public float moveTimer = 3f;                //多少秒移动一次
    public float moveSpped = 3;                 //移动的速度
    private float moveTime = 3f;
    private bool rangdomMove = false;           //是否随机过要移动到的目标点位置   
    private float distancePosition = 5f;        //离目标点多近的时候停止移动
    private GameObject leftNode;                 //左边距
    private GameObject rightNode;                //右边距


    //人物进入游戏场景的动作
    private Transform enterSceneTarget;          //移动的场景的那个位置
    public bool isScaleAnimation;               //是否有放大缩小的动画
    public float enterSpeed;

    //游戏控制器
    private GameController gameController;

    void Start()
    {
        enemyEmission = GetComponent<EnemyEmission>();
        bodyBox = GetComponent<BoxCollider>();
        bodyBox.enabled = false;
        leftNode = PointNode.EnemeyLeftNodeStatic;
        rightNode = PointNode.EnemeyRightNodeStatic;
        enterSceneTarget = PointNode.EnemeyCenterNodeStatic.transform;
        gameController = GameController.gameController;
        emissionMSTimer = new float[enemyEmission.emissionNode.Length];
    }

    void OnEnable() 
    {
        HP = 100 * (GameManager.wave + 1) * (10 * (GameManager.selectLevel +1) * (GameManager.selectLevel + 1));
        originalHP = HP;
    }


    void Update()
    {
        if (GameManager.gameState == GameStateEnum.GamePlayer) 
        {
            #region 敌人开始行动的代码
            switch (enemyState)
            {
                case EnemyStateEnum.ComeOnAnimation:            //开始动画
                    ComeOnTheStage();
                    return;
                case EnemyStateEnum.Move:               //移动
                    if (rangdomMove == false)
                    {
                        moveDistance =  UnityEngine.Random.Range(leftNode.transform.localPosition.x, rightNode.transform.localPosition.x);
                        moveTargetDistance = new Vector3(moveDistance, transform.localPosition.y, transform.localPosition.z);
                        rangdomMove = true;
                    }

                    transform.localPosition = Vector3.Lerp(transform.localPosition, moveTargetDistance, moveSpped * Time.deltaTime);


                    if (Mathf.Abs(transform.localPosition.x - moveTargetDistance.x) < distancePosition)
                    {
                        moveTime = 0;
                        rangdomMove = false;
                        enemyState = EnemyStateEnum.Emission;
                    }

                    break;
                case EnemyStateEnum.Emission:        //射击
                    moveTime += Time.deltaTime;     //在射击的时候,移动才计时

                    for (int i = 0; i < enemyEmission.emissionNode.Length; i++)
                    {
                        if (emissionMSTimer[i] >= enemyEmission.emissionNode[i].speedMS)
                        {
                            enemyEmission.emissionNode[i].EmissionBullet();
                            emissionMSTimer[i] = 0;
                        }
                    }
                    break;
            }

            for (int i = 0; i < emissionMSTimer.Length; i++)
            {
                emissionMSTimer[i] +=Time.deltaTime;
            }
            if (moveTime >= moveTimer)
            {
                enemyState = EnemyStateEnum.Move;
            }
            #endregion
        }
    }


    /// <summary>
    /// 出场动画
    /// </summary>
    public void ComeOnTheStage() 
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, enterSceneTarget.localPosition, enterSpeed);

        if (Vector3.Distance(transform.localPosition, enterSceneTarget.localPosition) < 2f)
        {
            bodyBox.enabled = true;
            enemyState = EnemyStateEnum.Emission;
        }
    }


    void OnTriggerEnter(Collider obj)
    {

        if(obj.gameObject.tag == Tags.Bullet)
        {

            BulletModel bulletModel = obj.GetComponent<BulletModel>();

            //判断是否暴走
            if (GameManager.isRampage)
            {
                HP -= PlayerMove.staticPlayer.power * GameManager.singleBulletList[GameManager.powerLevel] * GameManager.rampageList[GameManager.rampageLevel];
            }
            else if (bulletModel.isSlay)
            {
                HP -= bulletModel.power;
            }
            else
            {
                HP -= PlayerMove.staticPlayer.power * GameManager.singleBulletList[GameManager.powerLevel];
            }

            


            HPSprite.fillAmount = HP / originalHP;
            
            //敌人挂掉了
            if (HP <= 0 && isDeadLock == false) 
            {
                isDeadLock = true;
                bodyBox.enabled = false;            
                gameController.EnemyDead();
                Destroy(this.gameObject, 1);
                //创建宝石
                GamePoolManager.GamePool.Spawn("Gem", this.transform.position, Quaternion.identity);
            }

            //创建子弹特效
            GamePoolManager.GamePool.Spawn(obj.GetComponent<BulletModel>().bulletEffect, obj.transform.position, Quaternion.identity);

            //消除子弹
            GamePoolManager.GamePool.Despawn(obj.transform);
        }
    }

}
