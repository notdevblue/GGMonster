using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeonHanAI : AIBase
{
    #region 변수
    
    [Header("스킬 포인트")]
    [SerializeField] private int[] skillPointArr = new int[4];

    [Header("HP")]
    [SerializeField] private int hp = 100;

    [Header("타입")]
    [SerializeField] private Stat.ClassType myType = Stat.ClassType.NOTYPE;

    #endregion

    private void Awake()
    {
        Debug.Log("SeonHanAI");
        //Init(skillPointArr, hp, myType); <= AI 라서 먼저 적이 있어야 함
    }


}
