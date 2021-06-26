using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemData
{
    public string name;
    public string info;
    public string stat;
    public ItemData(string name, string info, string stat) { this.name = name; this.info = info; this.stat = stat; }
}


public class Items : MonoBehaviour
{
    public void Heal(Stat stat)
    {
        Debug.Log("Heal item used");
        if (stat.curHp == stat.maxHp) { Debug.Log("�̹� �ִ� HP"); return; }
        if (stat.healItemCnt < 1) { Debug.Log("������ ����"); return; }

        --stat.healItemCnt;
        stat.curHp = (stat.curHp + stat.healAmout > stat.maxHp) ? stat.maxHp : stat.curHp + stat.healAmout;

        DamageEffects.instance.HealEffect(stat.transform);

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
