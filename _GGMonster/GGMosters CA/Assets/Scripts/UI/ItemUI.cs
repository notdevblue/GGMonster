using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private InfoUI infoUI;
    private Stat stat;
    private Items item;

    [SerializeField] private Button[] btnItem = new Button[2];
    [SerializeField] private Button[] btnInfo = new Button[2];

    private void Awake()
    {
        GetStat(); // TODO : ��
        InitButtons();
        item = GetComponent<Items>();
    }

    private void GetStat()
    {
        stat = GameObject.FindGameObjectWithTag("Player").GetComponent<Stat>();
    }

    private void InitButtons()
    {
        btnItem[0].onClick.AddListener(CallHeal);
        btnItem[1].onClick.AddListener(CallResetProvokeCount);

        btnInfo[0].onClick.AddListener(() => infoUI.CallItemInfo(new ItemData("�����", "ä���� 10 ȸ�������ݴϴ�.", $"{stat.healItemCnt} �� ���ҽ��ϴ�.")));
        btnInfo[1].onClick.AddListener(() => infoUI.CallItemInfo(new ItemData("������ ���� �ѹ�", "ä���� 5 �Ҹ��ϴ� ��� ���� ���¸� �����մϴ�.", $"{stat.provItemCnt} �� ���ҽ��ϴ�.")));
    }

    private void CallHeal()
    {
        if (!stat.myturn) return;
        item.Heal(stat);
    }
    private void CallResetProvokeCount()
    {
        if (!stat.myturn) return;
        item.ResetProvokeCount(stat);
    }

    
}
