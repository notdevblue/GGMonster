using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemData
{
    public string name;
    public string info;
    public string stat;
    public ItemData(string name, string info, string stat) { this.name = name; this.info = info; this.stat = stat; }
}


public class Items : MonoBehaviour
{
    [SerializeField] private Text playerDamageText;
    [SerializeField] private Text enemyDamageText;

    public void Heal(Stat stat)
    {
        if (stat.curHp == stat.maxHp) { return; }
        if (stat.healItemCnt < 1) { Debug.Log("아이탬 부족"); return; } // TODO : 부족도 시각화 해야함

        --stat.healItemCnt;
        stat.curHp = (stat.curHp + stat.healAmout > stat.maxHp) ? stat.maxHp : stat.curHp + stat.healAmout;

        DamageEffects.instance.HealEffect(stat.transform);
        DamageEffects.instance.TextEffect((stat.curHp + stat.healAmout > stat.maxHp) ? (stat.curHp + stat.healAmout - stat.maxHp) : stat.healAmout,
                                           stat.CompareTag("Player") ? playerDamageText : enemyDamageText,
                                           true);
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
