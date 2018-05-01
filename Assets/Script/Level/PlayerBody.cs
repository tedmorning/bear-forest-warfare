using UnityEngine;
using System.Collections;
using BearGame;

public class PlayerBody : MonoBehaviour {

    private PlayerMove playerMove;
    public string roleName;             //角色名称
    public int hp;                      //血值
    public EnemyStateEnum state;        //角色的状态
    public BoxCollider roleBox;         //角色被子弹攻击的box区域
    public GameObject deadNode;         //死亡
    public GameObject roleDeadPosition; //英雄死亡后移动到的坐标点

	public GameObject reviveNode;
    private RoleInfoPanel roleInfoPanel;
	public static PlayerBody player;


    //护盾节点
    public PlayerShield shieldNode;
    public GameObject slay;


    //大招出现的位置
    private GameObject leftSlay;
    private GameObject centerSlay;
    private GameObject rightSlay;



	void Awake(){
		player = this;
        hp += GameManager.heartCount;
        
    }

    public void Start() 
    {

        playerMove = this.transform.parent.GetComponent<PlayerMove>();
        roleBox = this.transform.GetComponent<BoxCollider>();
        roleInfoPanel = GameObject.Find("RoleInfoPanel").GetComponent<RoleInfoPanel>();

        leftSlay = PointNode.SlayLeftStatic;
        centerSlay = PointNode.SlayCenterStatic;
        rightSlay = PointNode.SlayRightStatic;

        roleDeadPosition = PointNode.RoleDeadNodeStatic;
    }

    //碰撞
    public void OnTriggerEnter(Collider collider) 
    {
        //如果是敌人
        if (Tags.Enemy == collider.gameObject.tag && GameManager.gameState == GameStateEnum.GamePlayer)
        {
            GameManager.gameState = BearGame.GameStateEnum.GameEnd;
            roleBox.enabled = false;
            StartCoroutine(PlayDeadAnimation());
        }

        //敌人的子弹
        if (Tags.EnemyBullet == collider.gameObject.tag && GameManager.gameState == GameStateEnum.GamePlayer)
        {

            if (shieldNode.gameObject.activeInHierarchy)
            {
                shieldNode.CancelShield();
            }
            else 
            {
                hp--;
                if (hp < 2 && hp >= 0)
                {
                    roleInfoPanel.SubHeart();
                }

                if (hp <= 0)
                {
                    //游戏结束
                    GameManager.gameState = BearGame.GameStateEnum.GameEnd;
                    roleBox.enabled = false;
                    StartCoroutine(PlayDeadAnimation());
                }
            }
            //销毁子弹
            GamePoolManager.GamePool.Despawn(collider.gameObject.transform);
        }

        //宝石
        if(Tags.Gem == collider.gameObject.tag)
        {
            GamePoolManager.GamePool.Despawn(collider.gameObject.transform);
            GameManager.levelGem++;
        }

    }

    public void Update() 
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            GameManager.gameState = GameStateEnum.GamePlayer;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) 
        {
            OpenShield();
        }

        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            OpenSlay();
        }
    }

    /// <summary>
    /// 播放死亡动画
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayDeadAnimation()
    {
		deadNode.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        this.transform.parent.gameObject.SetActive(false);

		//显示游戏暂停界面
		roleInfoPanel.ShowRoleRevivePanel ();
    }

	/// <summary>
	/// 角色复活
	/// </summary>
    public void RoleRevive() 
    {
        hp = 2;
		deadNode.SetActive (false);
		reviveNode.SetActive (true);
        this.transform.parent.gameObject.transform.position = roleDeadPosition.transform.position;
        state = EnemyStateEnum.ComeOnAnimation;
        this.transform.parent.gameObject.SetActive(true);

		Transform obj = null;
		//重置死亡动画播放
		for (int i = 0; i < deadNode.transform.childCount; i++) {
			obj = deadNode.transform.GetChild(i);
			obj.gameObject.SetActive(true);	
			obj.GetComponent<UISprite>().enabled = false;
		}
    }

    /// <summary>
    /// 开启护盾
    /// </summary>
    public void OpenShield() 
    {
        shieldNode.OpenShield();
    }


    /// <summary>
    /// 开启大招
    /// </summary>
    public void OpenSlay() 
    {
        var slay1 = GamePoolManager.GamePool.Spawn(slay);
        var slay2 = GamePoolManager.GamePool.Spawn(slay);
        var slay3 = GamePoolManager.GamePool.Spawn(slay);

        slay1.localScale = new Vector3(1, 1, 1);
        slay2.localScale = new Vector3(1, 1, 1);
        slay3.localScale = new Vector3(1, 1, 1);

        slay1.localPosition = leftSlay.transform.localPosition;
        slay2.localPosition = centerSlay.transform.localPosition;
        slay3.localPosition = rightSlay.transform.localPosition;

    }

}
