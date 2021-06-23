using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{



    // TODO : �߸��� �Լ� ��ġ
    // ���� Ŭ������ �ľ� �Ѵ�.
    public void Heal(Stat stat)
    {
        Debug.Log("Heal item used");
        if (stat.curHp == stat.maxHp) { Debug.Log("�̹� �ִ� HP"); return; }
        if (stat.healItemCnt < 1) { Debug.Log("������ ����"); return; }

        --stat.healItemCnt;
        stat.curHp = (stat.curHp + stat.healAmout > stat.maxHp) ? stat.maxHp : stat.curHp + stat.healAmout;

        CallMidturnTask();
    }


    public void ResetProvokeCount(Stat stat) // hp - 5 �ϴ� ��� ���� ���¸� ������.
    {
        Debug.Log("Reset provoke item used");
        if (!stat.provoke) { Debug.Log("���� ���°� �ƴ�"); return; }
        if (stat.healItemCnt < 1) { Debug.Log("������ ����"); return; }

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
