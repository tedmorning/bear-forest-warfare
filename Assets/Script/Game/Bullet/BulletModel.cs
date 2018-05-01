using UnityEngine;
using System.Collections;
using BearGame;

namespace Bullet 
{

    public class BulletModel : MonoBehaviour
    {
        /// <summary>
        /// 子弹预设
        /// </summary>
        private GameObject bullet;

        /// <summary>
        /// 子弹的名称
        /// </summary>
        private string bulletName;

        /// <summary>
        /// 子弹爆炸特效
        /// </summary>
        public Transform bulletEffect;

        /// <summary>
        /// 子弹的伤害
        /// </summary>
        public int power;

        public bool isSlay;

        /// <summary>
        /// 向左右移动
        /// </summary>
        public BulletHorizontal bulletHorizontal;

        /// <summary>
        /// 向前后移动
        /// </summary>
        public BulletVertical bulletForce;

        /// <summary>
        /// 围绕某一个点旋转
        /// </summary>
        public BulletRotate bulletRotateARound;

        /// <summary>
        /// 围绕自身旋转
        /// </summary>
        public BulletRotate bulletRotateOneself;

        #region 属性
        public GameObject Bullet
        {
            get
            {
                if (bullet == null)
                {
                    bullet = this.gameObject;
                }
                return bullet;
            }
            set { bullet = value; }
        }
        public string BulletName
        {
            get
            {
                if (string.IsNullOrEmpty(bulletName))
                {
                    bulletName = this.gameObject.name;
                }
                return bulletName;
            }
            set { bulletName = value; }
        }
        #endregion


        public void TrunOFFALLScript() 
        {
            if (bulletRotateOneself != null) bulletRotateOneself.enabled = false;
            if (bulletRotateARound != null) bulletRotateARound.enabled = false;
            if (bulletForce != null) bulletForce.enabled = false;
            if (bulletHorizontal != null) bulletHorizontal.enabled = false;
                
        }

        public void TrunOpenALLScript()
        {
            if (bulletRotateOneself != null) bulletRotateOneself.enabled = true;
            if (bulletRotateARound != null) bulletRotateARound.enabled = true;
            if (bulletForce != null) bulletForce.enabled = true;
            if (bulletHorizontal != null) bulletHorizontal.enabled = true;
        }
        




    }


}

