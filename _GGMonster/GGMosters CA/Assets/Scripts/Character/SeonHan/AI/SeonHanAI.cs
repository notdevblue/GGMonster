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
        Debug.Log(turnPlayed && stat.myturn);
        Debug.Log($"myturn: {stat.myturn}");
        Debug.Log($"turnPlayed: {turnPlayed}");
        //turnPlayed = true;
        if (turnPlayed && stat.myturn)
        {
            turnPlayed = true;
            Debug.Log("turn");
            OnTurn();
        }
        if(stat.isDead)
        {
            Dead();
        }

        if (TurnManager.instance.turn % 2 == (stat.startFirst ? 0 : 1))
        {
            Debug.Log($"AI: Turn: {TurnManager.instance.turn}, boolean: {(TurnManager.instance.turn % 2 == (stat.startFirst ? 0 : 1))}");

            turnPlayed = false;
        }
    }



}
