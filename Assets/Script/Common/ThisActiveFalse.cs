using UnityEngine;
using System.Collections;

public class ThisActiveFalse  : MonoBehaviour{


    public void Cancel() 
    {
        this.gameObject.SetActive(false);
    }

}
