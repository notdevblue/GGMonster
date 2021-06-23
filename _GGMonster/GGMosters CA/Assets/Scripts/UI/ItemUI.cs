using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private InfoUI infoUI;
    private Stat stat;
    private Items item;

    private int btnCount = 3;
    private Button[] btnItemArr;

    private void Awake()
    {
        GetStat(); // TODO : ��
        InitButtons();
    }

    private void Start()
    {
        item = GetComponent<Items>();
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

        btnItemArr[0].onClick.AddListener(CallHeal);
        btnItemArr[1].onClick.AddListener(CallResetProvokeCount);

        btnItemArr[0].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => infoUI.CallItemInfo(new ItemData("�����", "ä���� 10 ȸ�������ݴϴ�.")));
        btnItemArr[1].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => infoUI.CallItemInfo(new ItemData("������ ���� �ѹ�", "ä���� 5 �Ҹ��ϴ� ��� ���� ���¸� �����մϴ�.")));
    }

    private void CallHeal()
    {
        item.Heal(stat);
    }
    private void CallResetProvokeCount()
    {
        item.ResetProvokeCount(stat);
    }

    
}
