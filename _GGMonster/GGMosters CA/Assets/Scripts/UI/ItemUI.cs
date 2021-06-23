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
        GetStat(); // TODO : 적
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

        btnItemArr[0].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => infoUI.CallItemInfo(new ItemData("밥버거", "채력을 10 회복시켜줍니다.")));
        btnItemArr[1].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => infoUI.CallItemInfo(new ItemData("강력한 엘보 한방", "채력을 5 소모하는 대신 도발 상태를 해재합니다.")));
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
