using UnityEngine;
using System.Collections;

public class LevelNode : MonoBehaviour {
    public GameObject roadSign;     //路标
    public GameObject notOpen;      //未打开节点
    public UISprite levelSprite;
    public int level;               //当前第几关

    private SelectLevelPanel selectLevelPanel;

    void Awake() 
    {
        selectLevelPanel = GameObject.Find("SelectLevelPanel").GetComponent<SelectLevelPanel>();

        //关卡
        if (level == 1)
        {
            selectLevelPanel.SetLevelNode(this.gameObject);
        }

    }

    /// <summary>
    /// 选择关卡被单击
    /// </summary>
    public void ChangeLevel() 
    {

        Debug.Log("当前解锁关卡:" + GameManager.level + " - " + this.level);

        if(GameManager.level >= this.level - 1)
        {
            //关闭之前的关卡
            selectLevelPanel.CancelRoadSign();
            selectLevelPanel.level = this.level;
            selectLevelPanel.SetLevelNode(this.gameObject);

            //显示当前关卡路标
            notOpen.SetActive(false);
            roadSign.SetActive(true);
            levelSprite.enabled = true;

            GameManager.selectLevel = this.level;
        }
    }


    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            GameManager.level++;
        }    
        
    }
}
