using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeonHanAI : AIBase
{
    #region 변수
    [Header("MaxHP")]
    [SerializeField] private int hp = 100;

    [Header("타입")]
    [SerializeField] private Stat.ClassType myType = Stat.ClassType.NOTYPE;

    #endregion

    private void Start()
    {
        Init(hp, myType, true); //<= AI 라서 먼저 적이 있어야 함
    }

    private void Update()
    {
        //Debug.Log($"stat.myturn: {stat.myturn}, turnPlayed: {turnPlayed}");

        if (turnPlayed && stat.myturn)
        {
            turnPlayed = false;
            Debug.Log("turn");
            OnTurn();
        }
        if(stat.isDead)
        {
            Dead();
        }


    }



}
