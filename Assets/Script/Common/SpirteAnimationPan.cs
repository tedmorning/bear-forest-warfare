using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpirteAnimationPan : MonoBehaviour {

    /// <summary>
    /// 要切换的图片名称,图片名称保证在当前atlas中
    /// </summary>
    public string[] cutSpriteName;
    public UISprite sprite;

    /// <summary>
    /// 一次播放动画的间隔时间
    /// </summary>
    private float intervalAnimation;

    public bool isOneDestroy;
    public bool isOneEnable;
    public bool isOneStop;          

    public float startPlay;
    private bool isStartPlay;
    private float startPlayTimer;
    /// <summary>
    /// 切换每一张图片的间隔时间
    /// </summary>
    public float cutSprteAnimation;
    private float intervalAnimationTimer;
    private float cutSprteAnimationTimer;
    private bool isIntervalAnimation;
    private int spriteIndex;

    void Awake() 
    {
        List<string> tempList = new List<string>();
        if (cutSpriteName == null || cutSpriteName.Length == 0) 
        {
            foreach (var i in sprite.atlas.spriteList)
            {
                tempList.Add(i.name);
            }
            cutSpriteName = tempList.ToArray();
        }
    }

    void Update() 
    {
        if (cutSpriteName != null && cutSpriteName.Length > 0)
        {
            //第一次经过几秒开始播放动画
            if (isStartPlay == false) 
            {
                startPlayTimer += Time.deltaTime;

                if (startPlayTimer >= startPlay)
                {
                    startPlayTimer = 0f;
                    isStartPlay = true;
                    sprite.enabled = true;
                }
                return;
            }


            //每次一次完整动画的间隔时间
            if (isIntervalAnimation == false)
            {
                intervalAnimationTimer += Time.deltaTime;
            }

            //可以进行一次动画播放!
            if (intervalAnimationTimer >= intervalAnimation)
            {
                isIntervalAnimation = true;

                cutSprteAnimationTimer += Time.deltaTime;
                if (cutSprteAnimationTimer >= cutSprteAnimation)
                {


                    sprite.spriteName = cutSpriteName[spriteIndex];
                    cutSprteAnimationTimer = 0f;
                    spriteIndex++;


                    //一次动画播放完毕
                    if (spriteIndex >= cutSpriteName.Length)
                    {
                        spriteIndex = 0;
                        intervalAnimationTimer = 0;
                        isIntervalAnimation = false;

                        if (isOneEnable) 
                        {
							EnableSpirteAnimationPan();
                        }

                        if (isOneDestroy) 
                        {
                            GamePoolManager.GamePool.Despawn(this.transform);
                        }
                    }
                }
            }
        }

    }


    public void DestorySpirteAnimationPan() 
    {
    
    }

    public void EnableSpirteAnimationPan()
    {
		spriteIndex = 0;
        isStartPlay = false;
        this.gameObject.SetActive(false);

		
		transform.localScale = new Vector3 (1,1,1);

		TweenScale scale = transform.GetComponent<TweenScale>();


		if(scale != null)
		{
			scale.ResetToBeginning();
			//scale.Play();
			scale.enabled = true;
		}
    }


}
