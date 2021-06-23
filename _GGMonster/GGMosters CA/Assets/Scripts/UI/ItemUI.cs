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
        GetStat(); // TODO : 적
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

        btnInfo[0].onClick.AddListener(() => infoUI.CallItemInfo(new ItemData("밥버거", "채력을 10 회복시켜줍니다.")));
        btnInfo[1].onClick.AddListener(() => infoUI.CallItemInfo(new ItemData("강력한 엘보 한방", "채력을 5 소모하는 대신 도발 상태를 해재합니다.")));
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
