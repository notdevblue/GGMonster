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
        if(stat.myturn && thinkComplete)
        {
            OnTurn();
        }
    }



}
