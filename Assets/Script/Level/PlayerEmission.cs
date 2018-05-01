using UnityEngine;
using System.Collections;
using PathologicalGames;

public class PlayerEmission : MonoBehaviour
{

    /// <summary>
    /// 飞机有多个发射子弹的节点
    /// </summary>
    public Emission [] emissionNode;

    /// <summary>
    /// 子弹发射计时器
    /// </summary>
    private float[] bulletMsTimer;

    /// <summary>
    /// 是否射击
    /// </summary>
    public bool isEmiision;
    
    /// <summary>
    /// 子弹纹理
    /// </summary>
    public UIAtlas bulletAtlas;

    private int addEmissionSpeed = 1;


    private float playMusicTimer = 3f;
    private float playMusicTime = 0;

	void Start () {
        bulletMsTimer = new float[emissionNode.Length];

        foreach (var i in emissionNode)
        {
            if (!GamePoolManager.GamePool.IsSpawned(i.Buttelt.transform))
            {
                var temp = new PrefabPool(i.Buttelt.transform);
                temp.preloadAmount = 20;
                
                GamePoolManager.GamePool._perPrefabPoolOptions.Add(temp);
            }
        }
	}

	
	void Update () {

        
        


        //进入暴走模式,射击速度提高成2倍
        if (GameManager.isRampage)
        {
            addEmissionSpeed = 2;
        }
        else 
        {
            addEmissionSpeed = 1;
        }

        if(GameManager.gameState == BearGame.GameStateEnum.GamePlayer)
        {
            if (isEmiision)
            {
                for (int i = 0; i < bulletMsTimer.Length; i++)
                {
                    bulletMsTimer[i] += Time.deltaTime;
                }

                playMusicTime += Time.deltaTime;
                if (playMusicTime >= playMusicTimer) 
                {
                    playMusicTime = 0f;
                    SoundController.PlayFXNGUITools("攻击");
                }



                //是否可以发射子弹
                for (int i = 0; i < emissionNode.Length; i++)
                {
                    if (bulletMsTimer[i] > (emissionNode[i].speedMS / addEmissionSpeed))
                    {
                        //发射子弹
                        Transform tempBullet = GamePoolManager.GamePool.Spawn(emissionNode[i].Buttelt.transform);
                        

                        if (i == emissionNode.Length - 1) 
                        {
                            
                            UISprite temp = null;
                            temp = tempBullet.GetComponent<UISprite>();
                            temp.atlas = bulletAtlas;
                            temp.spriteName = (GameManager.powerLevel) + "";
                            //让精灵 = 图片的大小
                            temp.MakePixelPerfect();
                        }


                        tempBullet.position = emissionNode[i].transform.position;
                        tempBullet.localScale = new Vector3(1, 1, 1);
                        tempBullet.gameObject.SetActive(true);

                        bulletMsTimer[i] = 0f;

                    }
                }
            }
        }


        //测试方法===============================================================================
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Transform tempBullet = GamePoolManager.GamePool.Spawn("Bullet0001");

            tempBullet.parent = emissionNode[0].transform.parent;
            tempBullet.position = emissionNode[0].transform.position;
            tempBullet.localScale = new Vector3(1, 1, 1);
            tempBullet.gameObject.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.F2))
        {
            GameManager.powerLevel++;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            GameManager.isRampage = !GameManager.isRampage;
        }

	}
}
