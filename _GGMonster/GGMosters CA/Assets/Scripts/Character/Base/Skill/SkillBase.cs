using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 이 아레 클레스와 다른게 없는거 같긴 한데 흠흠
public class SkillData
{
    public delegate void SkillExample(ref int skillPoint);

    public readonly string name = "Default";

    public readonly SkillInfo info;
    public readonly SkillExample skill;

    public SkillData(string n, SkillExample s, SkillInfo inf) { name = n; skill = s; info = inf; }
}

// 스킬 정보가 담겨져 있는 클레스
public class SkillInfo
{
    public readonly string info = "Default Info";                            // 스킬 정보
    public readonly bool isContinues = false;                                // 지속댐 여부
    public readonly int continuesCount = 0;                                  // 지속댐 반복
    public readonly Stat.ClassType effectiveClass = Stat.ClassType.NOTYPE;   // 효과적인 타입
    public readonly Stat.ClassType uneffectiveClass = Stat.ClassType.NOTYPE; // 효과적이지 못한 타입
    public readonly int damage = -1;                                         // 데미지

    /// <summary>
    /// 
    /// </summary>
    /// <param name="i">info</param>
    /// <param name="d">Damage, if c is true: d = oneShotDamage * cc</param>
    /// <param name="c">isContinues</param>
    /// <param name="cc">continuesCount</param>
    /// <param name="ec">EffectiveClass</param>
    /// <param name="uec">UnEffectiveClass</param>
    public SkillInfo(string i, int d, Stat.ClassType ec = Stat.ClassType.NOTYPE, Stat.ClassType uec = Stat.ClassType.NOTYPE, bool c = false, int cc = 0)
    {
        info = i;
        isContinues = c;
        continuesCount = cc;
        effectiveClass = ec;
        uneffectiveClass = uec;
        damage = d;
    }
}


abstract public class SkillBase : MonoBehaviour, ISkill
{
    [SerializeField] private InfoUI infoUI = null;

    protected Stat stat = null;

    protected GameObject    cvsBattle       = null;
    protected Text[]        txtSkillnameArr = new Text[4];
    protected Button[]      btnSkillArr     = new Button[4];
    protected Button[]      btnInfoArr      = new Button[4]; // 스킬 설명을 담은 버튼

    protected bool   isAI = false;
    protected bool[] noSP = new bool[4]; // SP 체크용

    [HideInInspector] public Dictionary<SkillListEnum, SkillData> skillDataDictionary = new Dictionary<SkillListEnum, SkillData>(); // 스킬 정보가 들어있는 딕셔너리


    [SerializeField] protected List<SkillListEnum> selectedSkills = new List<SkillListEnum>(); // 선택한 스킬


    protected void InitBattleCsv()
    {
        for (int index = 0; index < 4; ++index)
        {
            Transform temp          = cvsBattle.transform.GetChild(0).GetChild(index);
            btnSkillArr[index]      = temp.GetComponent<Button>();
            txtSkillnameArr[index]  = temp.GetChild(0).GetComponent<Text>();
            btnInfoArr[index]       = temp.GetChild(1).GetComponent<Button>();
        }

        // 버튼 이름 텍스트
        for (int i = 0; i < 4; ++i)
        {
            txtSkillnameArr[i].text = skillDataDictionary[selectedSkills[i]].name;
        }

        for(int i = 0; i < 4; ++i)
        {
            int num = i;
            btnInfoArr[i].onClick.AddListener(() => infoUI.CallSkillInfo(skillDataDictionary[selectedSkills[num]]));
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

        // TODO : O(n + n - 1) 비교임
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
                return true; // 어디 하나라도 스킬포인트가 남아있는 경우
            }
        }

        return false;
    }
}
