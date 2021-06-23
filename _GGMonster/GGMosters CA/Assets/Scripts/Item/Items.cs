using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{



    // TODO : 잘못된 함수 위치
    // 따로 클래스를 파야 한다.
    public void Heal(Stat stat)
    {
        Debug.Log("Heal item used");
        if (stat.curHp == stat.maxHp) { Debug.Log("이미 최대 HP"); return; }
        if (stat.healItemCnt < 1) { Debug.Log("아이탬 부족"); return; }

        --stat.healItemCnt;
        stat.curHp = (stat.curHp + stat.healAmout > stat.maxHp) ? stat.maxHp : stat.curHp + stat.healAmout;

        CallMidturnTask();
    }


    public void ResetProvokeCount(Stat stat) // hp - 5 하는 대신 도발 상태를 없엔다.
    {
        Debug.Log("Reset provoke item used");
        if (!stat.provoke) { Debug.Log("도발 상태가 아님"); return; }
        if (stat.healItemCnt < 1) { Debug.Log("아이탬 부족"); return; }

        --stat.provItemCnt;

        stat.curHp -= stat.provUseCost;
        stat.provoke = false;
        stat.provokeCount = 0;

        CallMidturnTask();
    }

    private void CallMidturnTask()
    {
        TurnManager.instance.MidTurn();
    }
}
