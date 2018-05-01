using UnityEngine;
using System.Collections;
using BearGame;

public class PlayerShield : MonoBehaviour {

    private TweenScale scale;
    public bool isOpen;

    void Awake() 
    {
        scale = GetComponent<TweenScale>();
    }

    /// <summary>
    /// 开启护盾
    /// </summary>
    public void OpenShield() 
    {
        isOpen = true;
        this.gameObject.SetActive(true);
        this.gameObject.transform.localScale = new Vector3(1,1,1);
        scale.enabled = false;
    }

    public void OnTriggerEnter(Collider collider) 
    {
        if (collider.gameObject.tag == Tags.EnemyBullet) 
        {
            //销毁子弹
            GamePoolManager.GamePool.Despawn(collider.gameObject.transform);
        
            CancelShield();
        }

        
    }


    /// <summary>
    /// 关闭护盾
    /// </summary>
    public void CancelShield() 
    {
        isOpen = false;
        if (scale.enabled) return;      //说明护盾已经在播放关闭动画了
        scale.enabled = true;
        scale.ResetToBeginning();
    }

    public void CancelShieldAnimationCall() 
    {
        scale.enabled = false;
        this.gameObject.SetActive(false);
    }

}
