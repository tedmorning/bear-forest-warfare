using UnityEngine;
using System.Collections;

public class PointNode : MonoBehaviour {


    public static GameObject EnemeyLeftNodeStatic;
    public static GameObject EnemeyCenterNodeStatic;
    public static GameObject EnemeyRightNodeStatic;
    
    public static GameObject RoleCenterNodeStatic;
    public static GameObject RoleDeadNodeStatic;

    public static GameObject SlayLeftStatic;
    public static GameObject SlayCenterStatic;
    public static GameObject SlayRightStatic;

    public static GameObject EnemyNodeStatic;

    public GameObject RoleCenterNode;
    public GameObject RoleDeadNode;
    public GameObject EnemeyLeftNode;
    public GameObject EnemeyCenterNode;
    public GameObject EnemeyRightNode;
    public GameObject SlayLeftNode;
    public GameObject SlayCenterNode;
    public GameObject SlayRightNode;
    public GameObject EnemyNode;



    void Awake() 
    {
        EnemeyLeftNodeStatic = EnemeyLeftNode;
        EnemeyCenterNodeStatic = EnemeyCenterNode;
        EnemeyRightNodeStatic = EnemeyRightNode;

        RoleCenterNodeStatic = RoleCenterNode;
        RoleDeadNodeStatic = RoleDeadNode;

        SlayLeftStatic = SlayLeftNode;
        SlayCenterStatic = SlayCenterNode;
        SlayRightStatic = SlayRightNode;

        EnemyNodeStatic = EnemyNode;

    }


}
