using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeonHan : CharactorBase
{
    #region ����
    [Header("MaxHP")]
    [SerializeField] private int hp = 100;

    [Header("Ÿ��")]
    [SerializeField] private Stat.ClassType myType = Stat.ClassType.NOTYPE;

    #endregion

    private void Awake()
    {
        Init(hp, myType);
    }

    protected override void Init(int hp, Stat.ClassType myType, bool calledByAi = false)
    {
        base.Init(hp, myType, calledByAi);
        base.ApplyTypeBenefit(); // <= TODO : �����ؾ� ��, SeonHanAI �� �� Ŭ������ ��ӹ���
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // <= TODO : �� ����
        {
            //Debug.Log("Player: Turn ended");
            stat.turnEnded = true;
        }
    }




}
