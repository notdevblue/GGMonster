using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO : AI 코드 변화하는게 없어요.
//        하나로 전부 돌려쓸 수 있음
public class ShibaAI : AIBase
{
    #region 변수
    [Header("MaxHP")]
    [SerializeField] private int hp = 100;

    [Header("타입")]
    [SerializeField] private Stat.ClassType myType = Stat.ClassType.NOTYPE;

    #endregion

    private void Awake()
    {
        Init(hp, myType, true); //<= AI 라서 먼저 적이 있어야 함
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
