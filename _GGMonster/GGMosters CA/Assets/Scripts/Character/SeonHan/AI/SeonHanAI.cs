using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeonHanAI : AIBase
{
    #region ����
    [Header("MaxHP")]
    [SerializeField] private int hp = 100;

    [Header("Ÿ��")]
    [SerializeField] private Stat.ClassType myType = Stat.ClassType.NOTYPE;

    #endregion

    private void Start()
    {
        Init(hp, myType, true); //<= AI �� ���� ���� �־�� ��
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
