using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{

    public static GameController gameController;
    private static float rampageTimer = 0f;
    private static bool isRampageTimer;
    private static bool isCoolingRampage;       //开启暴走
    public float coolingRampageSpeed = 0.0003f;
    public UISprite UICoolingRampage;           //冷却UI

    public AudioSource backgroundAudio;
    public AudioClip[] backgroundMusic;

    /// <summary>
    /// 主角选择器
    /// </summary>
    public GameObject[] roleList;

    /// <summary>
    /// 当前敌人
    /// </summary>
    private GameObject[] currentEnemyList;

    [HideInInspector]
    public GameObject currentRole;

    /// <summary>
    /// 当前一波敌人死亡了几个
    /// </summary>
    private static int waveNum = 0;   
	public RoleInfoPanel roleInfoPanel;
    public Transform EnemyParentNode;
    public LevelEN[] levelENList;

    public void Awake()
    {
        StartCoroutine(StartGame());
        gameController = this;
        currentRole = (GameObject)Instantiate(roleList[GameManager.roleIndex]);
    }


    public void Start() 
    {
        backgroundAudio.loop = true;

        //选择背景音乐播放
        switch(GameManager.selectLevel / 2)
        {
            case 0:
                backgroundAudio.clip = backgroundMusic[0];
                break;
            case 1:
                backgroundAudio.clip = backgroundMusic[1];
                break;
            case 2:
                backgroundAudio.clip = backgroundMusic[2];
                break;
        }
        backgroundAudio.Play();
    }

    public void Update()
    {
        //开启暴走计时
        if (isRampageTimer)
        {
            rampageTimer += Time.deltaTime;

            if (rampageTimer >= 5)
            {
                rampageTimer = 0f;
                isRampageTimer = false;
                GameManager.isRampage = false;
                isCoolingRampage = true;
                UICoolingRampage.enabled = true;
            }
        }

        if (isCoolingRampage) 
        {
            UICoolingRampage.fillAmount -= coolingRampageSpeed;
            Debug.Log("减少暴走" + UICoolingRampage.fillAmount);
            if(UICoolingRampage.fillAmount <=0)
            {
                isCoolingRampage = false;
                UICoolingRampage.enabled = false;
                UICoolingRampage.fillAmount = 1;
            }
        }


#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RoleRevive();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.roleIndex++;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameManager.slayCount++;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameManager.slayCount++;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            GameManager.isRampage = true;
        }
#endif

    }

    public IEnumerator StartGame()
    {
        GameManager.gameState = BearGame.GameStateEnum.GamePlayer;
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < levelENList[GameManager.selectLevel].enemyList[GameManager.wave].count; i++) 
        {
            GameObject temp = (GameObject)Instantiate(levelENList[GameManager.selectLevel].enemyList[GameManager.wave].enemy);
            temp.transform.localPosition = PointNode.EnemyNodeStatic.transform.localPosition;
            temp.transform.parent = EnemyParentNode;
            temp.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public static void CenterRampage()
    {
        if (isCoolingRampage == false) 
        {
            GameManager.isRampage = true;
            isRampageTimer = true;
        }
    }


    /// <summary>
    /// 下一波敌人
    /// </summary>
    public void EnemyDead() 
    {
        waveNum++;
        if(waveNum >= levelENList[GameManager.selectLevel].enemyList[GameManager.wave].count)
        {
            waveNum = 0;
            GameManager.wave++;
            if (GameManager.wave >= levelENList[GameManager.selectLevel].enemyList.Length)
            {
                Debug.Log("游戏通关");
                GameManager.gameState = BearGame.GameStateEnum.GameThrough;
                GameManager.wave = 0;
                StartCoroutine(roleInfoPanel.ShowGamePassPanel());
                return;
            }
            //开启下一波敌人
            StartCoroutine(NextWaveEnemy());
        }
    }


    IEnumerator NextWaveEnemy()
    {
        yield return new WaitForSeconds(2);

        Debug.Log(levelENList[GameManager.selectLevel].enemyList[GameManager.wave].count);
        for (int i = 0; i < levelENList[GameManager.selectLevel].enemyList[GameManager.wave].count; i++)
        {
            GameObject temp = (GameObject)Instantiate(levelENList[GameManager.selectLevel].enemyList[GameManager.wave].enemy);
            temp.transform.localPosition = PointNode.EnemyNodeStatic.transform.localPosition;
            temp.transform.parent = EnemyParentNode;
            temp.transform.localScale = new Vector3(1, 1, 1);
        }

    }

    /// <summary>
    /// 角色复活
    /// </summary>
    public void RoleRevive() 
    {
        GameManager.gameState = BearGame.GameStateEnum.GamePlayer;
        PlayerBody.player.RoleRevive(); //主角复活
    }


    /// <summary>
    /// 产生钻石
    /// </summary>
    public void CreateDiamond(Vector3 position) 
    {
        GamePoolManager.GamePool.Spawn("Gem");
    }

    public void CreateDiamond()
    {
        GamePoolManager.GamePool.Spawn("Gem");
    }

    public void ANewGame() 
    {
        GameManager.wave = 0;
        waveNum = 0;
        Application.LoadLevel("Level1");
    }

}
