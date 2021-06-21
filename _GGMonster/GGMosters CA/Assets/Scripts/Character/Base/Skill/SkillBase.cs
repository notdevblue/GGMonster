using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillData
{
    public delegate void SkillExample(ref int skillPoint);

    public int damage;
    public string name = "Default";
    public SkillExample skill;

    public SkillData(int d, string n, SkillExample s) { damage = d; name = n; skill = s; }
}

abstract public class SkillBase : MonoBehaviour, ISkill
{
    protected Stat stat = null;

    protected GameObject    cvsBattle       = null;
    protected Text[]        txtSkillnameArr = new Text[4];
    protected Button[]      btnSkillArr     = new Button[4];

    protected bool   isAI = false;
    protected bool[] noSP = new bool[4]; // SP üũ��

    [HideInInspector] public Dictionary<SkillListEnum, SkillData> skillDataDictionary = new Dictionary<SkillListEnum, SkillData>(); // ��ų ������ ����ִ� ��ųʸ�


    [SerializeField] protected List<SkillListEnum> selectedSkills = new List<SkillListEnum>(); // ������ ��ų


    protected void InitBattleCsv()
    {
        for (int index = 0; index < 4; ++index)
        {
            Transform temp          = cvsBattle.transform.GetChild(0).GetChild(index);
            btnSkillArr[index]      = temp.GetComponent<Button>();
            txtSkillnameArr[index]  = temp.GetChild(0).GetComponent<Text>();
        }

        for (int i = 0; i < 4; ++i)
        {
            txtSkillnameArr[i].text = skillDataDictionary[selectedSkills[i]].name;
        }
    }

    abstract public void SkillA();
    abstract public void SkillB();
    abstract public void SkillC();
    abstract public void SkillD();

    abstract public void SkillE();
    abstract public void Passive();

    protected void CheckSkillNameList()
    {
        #region List check
#if UNITY_EDITOR
        bool bStop = false;

        if (selectedSkills.Count != 4)
        {
            Debug.LogError("SkillBase: Wrong size");
            bStop = true;
        }
        if(selectedSkills.Contains(SkillListEnum.DEFAULTEND) || selectedSkills.Contains(SkillListEnum.SEONHANEND))
        {
            Debug.LogError("SkillBase: Wrong enum");
            bStop = true;
        }

        if(bStop) { UnityEditor.EditorApplication.isPlaying = false; }
#endif

        // TODO : O(n + n - 1) ����
        /*
        i = 0, j = X
        i = 1, j = 0
        i = 2, j = 0, 1
        i = 3, j = 0, 1, 2
        */
        for (int i = 0; i < 4; ++i)
        {
            for(int j = 0; j < i; ++j)
            {
                if(selectedSkills[i] == selectedSkills[j])
                {
                    Debug.LogError("SkillBase: You cannot use same skill more than once");
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Checks remain skillpoint
    /// </summary>
    /// <returns>false when all skillpoint is zero</returns>
    public bool CheckSP()
    {
        for (int i = 0; i < 4; ++i)
        {
            noSP[i] = false;
        }

        for (int sp = 0; sp < 4; ++sp)
        {
            if (stat.sp_arr[sp] <= 0)
            {
                noSP[sp] = true;
            }
        }


        foreach (bool sp in noSP)
        {
            if (!sp)
            {
                return true; // ��� �ϳ��� ��ų����Ʈ�� �����ִ� ���
            }
        }

        return false;
    }
}
