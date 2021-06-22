using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject skillPannel;
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private GameObject mainMenuPanel;

    [Header("UI Animation")]
    [SerializeField] private Transform  decPos;
    [SerializeField] private Transform  oriPos;
    [SerializeField] private float      animTime = 0.5f;

    private Button[] exitButtons = new Button[2];

    private void Awake()
    {
        skillPannel.SetActive(true);
        skillPannel.transform.position = decPos.position;
        itemPanel.SetActive(true);
        itemPanel.transform.position = decPos.position;
        mainMenuPanel.SetActive(true);

        exitButtons[0] = skillPannel.transform.GetChild(4).GetComponent<Button>(); // TODO : 이것은 하드 코딩
        exitButtons[1] = itemPanel.transform.GetChild(2).GetComponent<Button>();

        // 메인 선택 화면으로 돌아감
        foreach(Button btn in exitButtons)
        {
            if (btn == null) continue;
            btn.onClick.AddListener(ExitToMainCvs);
        }

        mainMenuPanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(ToSkillPannel);
        mainMenuPanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(ToItemPannel);

    }

    public void ExitToMainCvs()
    {
        CloseCanvas(itemPanel);
        CloseCanvas(skillPannel);
        OpenCanvas(mainMenuPanel);

        //skillPannel.SetActive(false);
        //itemPanel.SetActive(false);
        //mainMenuPanel.SetActive(true);
    }

    public void ToSkillPannel()
    {
        CloseCanvas(mainMenuPanel);
        OpenCanvas(skillPannel);

        //mainMenuPanel.SetActive(false);
        //skillPannel.SetActive(true);
    }

    public void ToItemPannel()
    {
        CloseCanvas(mainMenuPanel);
        OpenCanvas(itemPanel);
        //mainMenuPanel.SetActive(false);
        //itemPanel.SetActive(true);
    }



    private void CloseCanvas(GameObject cvs)
    {
        Debug.Log("Called close");
        cvs.transform.DOMove(decPos.position, animTime).SetEase(Ease.InCubic);
    }

    private void OpenCanvas(GameObject cvs)
    {
        Debug.Log("Called open");
        cvs.transform.DOMove(oriPos.position, animTime).SetEase(Ease.OutCubic);
    }
}
