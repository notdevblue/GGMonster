using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private InfoUI infoUI;
    [SerializeField] private Items item;
    private Stat stat;

    [SerializeField] private Button[] btnItem = new Button[2];
    [SerializeField] private Button[] btnInfo = new Button[2];

    private void Start()
    {
        GetStat(); // TODO : 적
        InitButtons();
    }

    private void GetStat()
    {
        stat = GameObject.FindGameObjectWithTag("Player").GetComponent<Stat>();
    }

    private void InitButtons()
    {
        btnItem[0].onClick.AddListener(CallHeal);
        btnItem[1].onClick.AddListener(CallResetProvokeCount);

        InitDictionary();

        btnInfo[0].onClick.AddListener(() => infoUI.CallItemInfo(item.itemDictionary[ItemEnum.RiceBurger]));
        btnInfo[1].onClick.AddListener(() => infoUI.CallItemInfo(item.itemDictionary[ItemEnum.StrongElbow]));
    }

    private void InitDictionary()
    {
        item.itemDictionary.Add(ItemEnum.RiceBurger, new ItemData("밥버거", "채력을 10 회복시켜줍니다.", $"{stat.healItemCnt} 개 남았습니다."));
        item.itemDictionary.Add(ItemEnum.StrongElbow, new ItemData("강력한 엘보 한방", "채력을 5 소모하는 대신 도발 상태를 해재합니다.", $"{stat.provItemCnt} 개 남았습니다."));
    }

    private void CallHeal()
    {
        if (!stat.myturn) return;
        item.Heal(stat);
        RemainBBG();
    }
    private void CallResetProvokeCount()
    {
        if (!stat.myturn) return;
        item.ResetProvokeCount(stat);
        RemainElbow();
    }


    [SerializeField] private GameObject[] bbgs = new GameObject[3];
    [SerializeField] private GameObject   elbow;
    private void RemainBBG()
    {
        switch(stat.healItemCnt)
        {
            case 3:
                bbgs[0].SetActive(true);
                bbgs[1].SetActive(true);
                bbgs[2].SetActive(true);
                break;

            case 2:
                bbgs[0].SetActive(true);
                bbgs[1].SetActive(true);
                bbgs[2].SetActive(false);
                break;

            case 1:
                bbgs[0].SetActive(true);
                bbgs[1].SetActive(false);
                bbgs[2].SetActive(false);
                break;

            default:
                bbgs[0].SetActive(false);
                bbgs[1].SetActive(false);
                bbgs[2].SetActive(false);
                break;
        }
    }
    
    private void RemainElbow()
    {
        switch(stat.provItemCnt)
        {
            case 1:
                elbow.SetActive(true);
                break;

            default:
                elbow.SetActive(false);
                break;
        }
    }
}
