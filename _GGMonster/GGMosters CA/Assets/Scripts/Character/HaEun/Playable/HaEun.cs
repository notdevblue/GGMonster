using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaEun : CharactorBase
{
    #region 변수
    [Header("MaxHP")]
    [SerializeField] private int hp = 100;

    [Header("타입")]
    [SerializeField] private Stat.ClassType myType = Stat.ClassType.NOTYPE;

    #endregion

    // TODO : 스킬 선택 후 해야 함
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
