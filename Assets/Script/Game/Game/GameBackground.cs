using UnityEngine;
using System.Collections;

public class GameBackground : MonoBehaviour {

    public UITexture texture;
    private float nowoffset = 0;
    public float addSpeed = 0.001f;

    public Texture [] backgroundList;


    void Start() 
    {
        texture.mainTexture = backgroundList[GameManager.selectLevel];
    }

    void Update() 
    {

        nowoffset += addSpeed;

        if (nowoffset >= 1)
        {
            nowoffset = 0;
        }

        texture.uvRect = new Rect(0, nowoffset, 1, 1);

    }

    private void MaterialMove() 
    {
        nowoffset += addSpeed;

        if (nowoffset >= 1)
        {
            nowoffset = 0;
        }

        transform.renderer.material.mainTextureOffset = new Vector2(0, nowoffset);
    }
}
