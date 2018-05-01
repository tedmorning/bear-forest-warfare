using UnityEngine;
using System.Collections;

public class EnemyEmission : MonoBehaviour {

    public Emission[] emissionNode; 
	
    /// <summary>
    /// 发射子弹
    /// </summary>
    public void EmissionBullet() 
    {
        foreach (var emission in emissionNode)
        {
            //发射子弹
            emission.EmissionBullet();
        }

    }


}
