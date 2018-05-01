using UnityEngine;
using System.Collections;
using PathologicalGames;
using Bullet;

public class Emission : MonoBehaviour {

    public GameObject bulletPreset;       //子弹预设
    public float speedMS;                 //多少秒发射一次
    public int shot;                      //与初始化的个数

    private BulletModel buttelt;

    public BulletModel Buttelt
    {
        get { return buttelt; }
        set { buttelt = value; }
    }


    public void Awake() 
    {
        buttelt = bulletPreset.GetComponent<BulletModel>();

        if (!GamePoolManager.GamePool.IsSpawned(bulletPreset.transform))
        {
            var temp = new PrefabPool(bulletPreset.transform);
            temp.preloadAmount = shot;
            temp.limitFIFO = false;
            temp.cullDespawned = true;
            GamePoolManager.GamePool._perPrefabPoolOptions.Add(temp);
        }
    }


    public void Start() 
    {

    }

    /// <summary>
    /// 发射子弹
    /// </summary>
    public void EmissionBullet() 
    {
        Transform tempBullet = GamePoolManager.GamePool.Spawn(bulletPreset.transform);
        tempBullet.position = this.transform.position;
        tempBullet.localScale = new Vector3(1, 1, 1);
        tempBullet.gameObject.SetActive(true);
        
    }

}
