using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    private Stat stat;
    private Items item;


    private int btnCount = 3;
    private Button[] btnItemArr;

    private void Awake()
    {
        GetStat(); // TODO : Àû
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
