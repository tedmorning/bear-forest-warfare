using UnityEngine;
using System.Collections;

public class ExitGamePanel : MonoBehaviour {


    public void ExitGame() 
    {
        Application.Quit();
    }


    /// <summary>
    /// 关闭结束游戏面板
    /// </summary>
    public void CancelExitGamePanel() 
    {
        this.gameObject.SetActive(false);
    }


}
