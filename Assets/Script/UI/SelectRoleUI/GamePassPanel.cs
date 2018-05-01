using UnityEngine;
using System.Collections;

public class GamePassPanel : MonoBehaviour {

    public UILabel gemLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gemLabel.text = GameManager.levelGem + "";
	}
}
