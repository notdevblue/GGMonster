using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class SkillBase : MonoBehaviour
{
    protected Stat stat = null;

    protected GameObject    cvsBattle       = null;
    protected Text[]        txtSkillnameArr = new Text[4];
    protected Button[]      btnSkillArr     = new Button[4];

    [SerializeField] protected string[] skillnameArr = new string[4];

    protected void InitBattleCsv()
    {
        int childCount = cvsBattle.transform.childCount;

        for (int index = 0; index < childCount; ++index)
        {
            Transform temp          = cvsBattle.transform.GetChild(index);
            btnSkillArr[index]      = temp.GetComponent<Button>();
            txtSkillnameArr[index]  = temp.GetChild(0).GetComponent<Text>();
        }

        for (int i = 0; i < 4; ++i)
        {
            txtSkillnameArr[i].text = skillnameArr[i];
        }
    }

    // btn.onClick.AddEventListener();
    abstract protected void InitBtn();
}
