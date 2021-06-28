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

public enum ItemEnum
{
    RiceBurger,
    StrongElbow
}


public class Items : MonoBehaviour
{
    [SerializeField] private Text playerDamageText;
    [SerializeField] private Text enemyDamageText;

    private bool isUsed = false;


    public Dictionary<ItemEnum, ItemData> itemDictionary = new Dictionary<ItemEnum, ItemData>();

    public void Heal(Stat stat)
    {
        if (isUsed) { return; }
        if (stat.curHp == stat.maxHp)
        {
            NoticeUI.instance.SetMsg("밥버거를 먹기에는 채력이 이미 꽉 차있다.");
            NoticeUI.instance.CallNoticeUI(false, true);
            
            return;
        }
        if (stat.healItemCnt < 1)
        {
            NoticeUI.instance.SetMsg("이미 밥버거를 다 먹었다!");
            NoticeUI.instance.CallNoticeUI(false, true);
            return; 
        }
        isUsed = true;

        --stat.healItemCnt;
        stat.curHp = (stat.curHp + stat.healAmout > stat.maxHp) ? stat.maxHp : stat.curHp + stat.healAmout;

        DamageEffects.instance.HealEffect(stat.transform);
        DamageEffects.instance.TextEffect((stat.curHp + stat.healAmout > stat.maxHp) ? (stat.curHp + stat.healAmout - stat.maxHp) : stat.healAmout,
                                           stat.CompareTag("Player") ? playerDamageText : enemyDamageText,
                                           true);
        CallMidturnTask();

        NoticeUI.instance.SetMsg($"{stat.charactorName}은 밥버거를 사용했다!", ResetUsedBoolean);
        NoticeUI.instance.CallNoticeUI(true, true, TurnManager.instance.enemyTurn, false, false, null, false);
    }


    public void ResetProvokeCount(Stat stat) // hp - 5 하는 대신 도발 상태를 없엔다.
    {
        if (isUsed) { return; }
        if (!stat.provoke)
        {
            NoticeUI.instance.SetMsg("강력한 엘보 한방은 도발 상태일때만 사용할 수 있다!");
            NoticeUI.instance.CallNoticeUI(false, true);
            return; 
        }
        if (stat.provItemCnt < 1)
        {
            NoticeUI.instance.SetMsg("이미 강력한 엘보를 사용했다!");
            NoticeUI.instance.CallNoticeUI(false, true);
            return;
        }
        isUsed = true;

        --stat.provItemCnt;

        stat.curHp -= stat.provUseCost; // TODO : 이거는 왜 이걸로 했지
        stat.provoke = false;
        stat.provokeCount = 0;

        NoticeUI.instance.SetMsg($"{stat.charactorName}은 강력한 엘보를 사용했다!", ResetUsedBoolean);
        NoticeUI.instance.CallNoticeUI(true, true, TurnManager.instance.enemyTurn, false, false, null, false);

        CallMidturnTask();
    }


    private void ResetUsedBoolean()
    {
        isUsed = false;
    }

    private void CallMidturnTask()
    {
        TurnManager.instance.MidTurn();
    }
}
