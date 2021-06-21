using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaEun : CharactorBase
{
    #region ����
    [Header("MaxHP")]
    [SerializeField] private int hp = 100;

    [Header("Ÿ��")]
    [SerializeField] private Stat.ClassType myType = Stat.ClassType.NOTYPE;

    #endregion

    // TODO : ��ų ���� �� �ؾ� ��
    private void Start()
    {
        Init(hp, myType);
    }

    protected override void Init(int hp, Stat.ClassType myType, bool calledByAi = false)
    {
        base.Init(hp, myType, calledByAi);
        base.ApplyTypeBenefit();
    }

    private void Update()
    {
        if (stat.isDead)
        {
            Dead();
        }
    }
}
