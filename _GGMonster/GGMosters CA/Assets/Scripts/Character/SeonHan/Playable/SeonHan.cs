using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeonHan : CharactorBase
{
    #region 변수
    [Header("MaxHP")]
    [SerializeField] private int hp = 100;

    [Header("타입")]
    [SerializeField] private Stat.ClassType myType = Stat.ClassType.NOTYPE;

    #endregion

    private void Awake()
    {
        Init(hp, myType);
    }

    protected override void Init(int hp, Stat.ClassType myType, bool calledByAi = false)
    {
        base.Init(hp, myType, calledByAi);
        base.ApplyTypeBenefit(); // <= TODO : 수정해야 함, SeonHanAI 가 이 클레스를 상속받음
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // <= TODO : 턴 종료
        {
            //Debug.Log("Player: Turn ended");
            stat.turnEnded = true;
        }
    }




}
