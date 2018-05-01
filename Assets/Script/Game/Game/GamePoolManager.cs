using UnityEngine;
using System.Collections;
using PathologicalGames;

public class GamePoolManager : MonoBehaviour {

    private static SpawnPool gamePool;

    public static SpawnPool GamePool
    {
        get 
        {
            if (gamePool == null) 
            {
                gamePool = PoolManager.Pools["GameManagerSpawn"];
            }
            return GamePoolManager.gamePool; 
        }
        set { GamePoolManager.gamePool = value; }
    }


}
