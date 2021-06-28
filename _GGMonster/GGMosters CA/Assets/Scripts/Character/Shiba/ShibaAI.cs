using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO : AI �ڵ� ��ȭ�ϴ°� �����.
//        �ϳ��� ���� ������ �� ����
public class ShibaAI : AIBase
{
    #region ����
    [Header("MaxHP")]
    [SerializeField] private int hp = 100;

    [Header("Ÿ��")]
    [SerializeField] private Stat.ClassType myType = Stat.ClassType.NOTYPE;

    #endregion

    private void Awake()
    {
        Init(hp, myType, true); //<= AI �� ���� ���� �־�� ��
    }

    private void Update()
    {
        //Debug.Log($"stat.myturn: {stat.myturn}, turnPlayed: {turnPlayed}");

        if (turnPlayed && stat.myturn && !stat.isDead)
        {
            turnPlayed = false;
            Debug.Log($"Enemy turn\r\nturn: {TurnManager.instance.turn}");
            OnTurn();
        }
    }

}
