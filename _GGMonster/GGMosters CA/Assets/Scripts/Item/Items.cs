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
            NoticeUI.instance.SetMsg("?????Ÿ? ?Ա⿡?? ä???? ?̹? ?? ???ִ?.");
            NoticeUI.instance.CallNoticeUI(false, true);
            
            return;
        }
        if (stat.healItemCnt < 1)
        {
            NoticeUI.instance.SetMsg("?̹? ?????Ÿ? ?? ?Ծ???!");
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

        // ?? ȿ???? ?????? ???⼭ ?? ??
        NoticeUI.instance.SetMsg($"{stat.charactorName}?? ?????Ÿ? ?????ߴ?!", () => { ResetUsedBoolean(); BattleSoundManager.PlaySound(SoundEnum.Healed); });
        NoticeUI.instance.CallNoticeUI(true, true, TurnManager.instance.enemyTurn, false, false, null, false);
    }


    public void ResetProvokeCount(Stat stat) // hp - 5 ?ϴ? ???? ???? ???¸? ??????.
    {
        if (isUsed) { return; }
        if (stat.tickDamageItemCount < 1)
        {
            NoticeUI.instance.SetMsg("?̹? ?????? ?????? ?????ߴ?!");
            NoticeUI.instance.CallNoticeUI(false, true);
            return;
        }
        if (!stat.isTickDamage)
        {
            NoticeUI.instance.SetMsg("    ?????? ???? ?ѹ???\r\n???? ?????? ?????? ???? ?????? ?? ?ִ?!");
            NoticeUI.instance.CallNoticeUI(false, true);
            return; 
        }
        isUsed = true;

        --stat.tickDamageItemCount;

        // ȿ????
        // ?ٷ? UI ?????? ?̷??? ?ۼ?
        BattleSoundManager.PlaySound(SoundEnum.TickDamaged);

        stat.isTickDamage = false;
        stat.tickDamageCount = 0;

        NoticeUI.instance.SetMsg($"{stat.charactorName}?? ?????? ?????? ?????ߴ?!", ResetUsedBoolean);
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
