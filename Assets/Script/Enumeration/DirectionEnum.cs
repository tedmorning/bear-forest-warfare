using UnityEngine;
using System.Collections;

namespace BearGame 
{


    public enum GameStateEnum
    {
        GamePause,
        GamePlayer,
        GameEnd,
        GameThrough
    }

    public enum EnemyStateEnum
    {
        Move,
        Emission,
        ComeOnAnimation
    }

    public struct Tags
    {
        public static string Bullet = "Bullet";
        public static string EnemyBullet = "EnemyBullet";
        public static string Gem = "Gem";
        public static string Enemy = "Enemy";
        public static string BulletSlay = "BulletSlay";
    }





}



