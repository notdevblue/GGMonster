using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject skillPannel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject itemPanel;

    private Button[] exitButtons = new Button[2];
    private Button btnToSkill;
    private Button btnToItem;



    private void Awake()
    {
        skillPannel.SetActive(false);
        //itemPanel.SetActive(false);
        mainMenuPanel.SetActive(true);

        exitButtons[0] = skillPannel.transform.GetChild(4).GetComponent<Button>(); // TODO : 이것은 하드 코딩
        //exitButtons[1] = itemPanel.transform.GetChild().GetComponent<Button>();

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
        skillPannel.SetActive(false);
        //itemPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void ToSkillPannel()
    {
        mainMenuPanel.SetActive(false);
        skillPannel.SetActive(true);
    }

    public void ToItemPannel()
    {
        mainMenuPanel.SetActive(false);
        itemPanel.SetActive(true);
    }
}
