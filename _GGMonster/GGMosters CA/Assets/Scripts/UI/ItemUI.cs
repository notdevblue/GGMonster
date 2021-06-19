using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    private Stat stat;

    private int btnCount = 3;
    private Button[] btnItemArr;

    private void Awake()
    {
        GetStat();
        InitButtons();
    }

    private void GetStat()
    {
        stat = GameObject.FindGameObjectWithTag("Player").GetComponent<Stat>();
    }

    private void InitButtons()
    {
        btnCount   = transform.childCount;
        btnItemArr = new Button[btnCount - 1];

        for (int i = 0; i < btnCount - 1; ++i)
        {
            btnItemArr[i] = transform.GetChild(i).GetComponent<Button>();
        }

        btnItemArr[0].onClick.AddListener(Heal);
        btnItemArr[1].onClick.AddListener(ResetProvokeCount);
    }

    private void Heal()
    {
        Debug.Log("Heal item used");
        if(stat.curHp == stat.maxHp) { Debug.Log("�̹� �ִ� HP"); return; }
        if(stat.healItemCnt < 1) { Debug.Log("������ ����"); return; }

        --stat.healItemCnt;
        stat.curHp = (stat.curHp + stat.healAmout > stat.maxHp) ? stat.maxHp: stat.curHp + stat.healAmout;

        CallMidturnTask();
    }

    private void ResetProvokeCount() // hp - 5 �ϴ� ��� ���� ���¸� ������.
    {
        Debug.Log("Reset provoke item used");
        if(!stat.provoke) { Debug.Log("���� ���°� �ƴ�"); return; }
        if(stat.healItemCnt < 1) { Debug.Log("������ ����"); return; }

        --stat.provItemCnt;

        stat.curHp       -= stat.provUseCost;
        stat.provoke      = false;
        stat.provokeCount = 0;

        CallMidturnTask();
    }

    private void CallMidturnTask()
    {
        TurnManager.instance.MidTurn();
    }
}
