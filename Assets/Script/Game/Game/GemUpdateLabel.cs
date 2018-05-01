using UnityEngine;
using System.Collections;

public class GemUpdateLabel : MonoBehaviour {

    private UILabel gemLabel;

	// Use this for initialization
	void Start () {
	    gemLabel = GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
        gemLabel.text = GameManager.gemCount.ToString();
	}
}
