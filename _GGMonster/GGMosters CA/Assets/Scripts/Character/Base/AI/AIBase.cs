using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : CharactorBase
{
                     protected AIStat aiStat     = null;
                     protected Skills skillList  = null;
                     private   ISkill skill;
    [SerializeField] protected ItemUI item       = null;

    private delegate void   SkillFunction();
    private SkillFunction[] skills = new SkillFunction[4];

    // lowHpAmount ���� ���
    private readonly float LOW_MUL = 0.2f;

    #region AI �Ǵ� ����
    [Header("�Ǵ� ��� �ð�")]
    [SerializeField] private float thinkTime = 2.0f;

    #region item ����

    //// Provoke
    // ���� ������ �� �������� ����� Ȯ��
    private readonly int onProvokeItemUseProb     = 20;
    private readonly int onHighProvokeItemUseProb = 40;

    // ���� �����̰� HP�� ���� �� ������ ��� Ȯ�� ���
    private readonly int onLowHpProvokeItemUseMul = 2;

    //// HP
    // ���� HP�� �� ������ ��� Ȯ��
    private readonly int onLowHpItemUseProb       = 30;
    // �ſ� ���� HP�� �� ������ ��� Ȯ��
    private readonly int onExtremelyLowHpUseProb  = 100;

    #endregion


    #endregion
    
    
    
    protected override void Init(int hp, Stat.ClassType myType, bool calledByAi = false)
    {
        base.Init(hp, myType, calledByAi);

        skill = GetComponent<ISkill>();
        skillList = GetComponent<Skills>();
        #region null üũ
        if (skill == null)
        {
            Debug.LogError("AIBase: Cannot GetComponent ISkill.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        if (skillList == null)
        {
            Debug.LogError("AIBase: Cannot GetComponent SKills.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        if(item == null)
        {
            Debug.LogError("AIBase: You forgot to add ItemUI to me.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #endregion

        // ���� AIStat �޾ƿ��� �� �ʱ�ȭ
        InitAIStat();

        // Ÿ�� ȿ�� ����
        ApplyTypeBenefit();
        
        // �Լ� ������ �迭 �ʱ�ȭ
        InitDelegate();
    }


    #region Init Functions

    private void InitAIStat()
    {
        aiStat = GetComponent<AIStat>();
        #region null üũ
#if UNITY_EDITOR
        if(aiStat == null)
        {
            Debug.LogError("AIBase: aiStat �� ã�� �� �����ϴ�.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        #endregion

        aiStat.lowHpAmount = (int)(stat.maxHp * LOW_MUL);
        aiStat.exLowHpAmount = (int)(stat.maxHp * (LOW_MUL / 2));
        aiStat.enemyLowHpAmount = (int)(stat.enemyStat.maxHp * LOW_MUL);
    }

    protected override void ApplyTypeBenefit()
    {
        base.ApplyTypeBenefit();

        #region �� Ÿ�� ���Է� üũ
        #if UNITY_EDITOR
        if(stat.enemyStat.myType == Stat.ClassType.NOTYPE)
        {
            Debug.LogError("AI: Enemy has no type, quitting");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #endif
        #endregion

        Debug.Log($"AI: My type is {stat.myType}, enemy type is {stat.enemyType}");


        // �� �Ҹ� üũ
        if (stat.damageBoost)
        {
            if (stat.myType == Stat.ClassType.TEACHER)
            {
                aiStat.advantage = true;
                aiStat.disAdvantage = true;
                Debug.Log("AI: Teacher battle");
            }
            else
            {
                aiStat.advantage = true;
                aiStat.disAdvantage = false;
                Debug.Log("AI: Advantage battle");
            }
        }
        else
        {
            if (stat.myType == stat.enemyType || stat.myType == Stat.ClassType.TEACHER || stat.enemyType ==  Stat.ClassType.TEACHER)
            {
                aiStat.advantage = false;
                aiStat.disAdvantage = false;
                Debug.Log("AI: No benefit battle");
            }
            else
            {
                aiStat.advantage = false;
                aiStat.disAdvantage = true;
                Debug.Log("AI: Disadvantage battle");
            }
        }
    }

    private void InitDelegate()
    {
        skills[0] = skill.SkillA;
        skills[1] = skill.SkillB;
        skills[2] = skill.SkillC;
        skills[3] = skill.SkillD;
    }

    #endregion


    #region AI Base Function

    // ���� ��ų ��������� ���� �ٲ�� ��
    protected void OnTurn()
    {
        Invoke(nameof(ThinkComplete), thinkTime);
    }

    private void ThinkComplete()
    {
        CheckMyStatus();
        UseItem();
        UseSkill();
    }
    
    private void CheckMyStatus()
    {
        // HP
        aiStat.lowHp = stat.curHp < aiStat.enemyLowHpAmount ? true : false;

        // Provoke
        aiStat.isHighProvoke = stat.provokeCount > 1 ? true : false;
    }

    private void UseSkill()
    {
        if(stat.enemyStat.curHp <= aiStat.enemyLowHpAmount)
        {

            return;
        }

        skills[SelectSkill()]();
    }

    private void UseItem()
    {
        ProvokeItem();
        RecoveryItem();

        #region Function inside Function

        void ProvokeItem()
        {
            int probSave = (stat.provokeCount > 1 ? onHighProvokeItemUseProb : onProvokeItemUseProb);
            int provokeProb = aiStat.lowHp ? probSave * onLowHpProvokeItemUseMul : probSave;


            if (Random.Range(0, 100) > provokeProb)
            {
                item.ResetProvokeCount();
            }
        }

        void RecoveryItem()
        {
            // ����
            int hpProb = ((aiStat.exLowHpAmount >= stat.curHp) ? onExtremelyLowHpUseProb : (aiStat.lowHpAmount >= stat.curHp ? onLowHpItemUseProb : 154));

            if(Random.Range(0, 100) > hpProb)
            {
                item.Heal();
            }
        }
        #endregion
    }

    private int SelectStrongesetSkill()
    {
        int idx;

        // TODO :  ������ ��ų ����
        int largestDamage = 0;
        foreach(SkillData data in skillList.skillDataDictionary.Values)
        {
            largestDamage = largestDamage <= data.damage ? data.damage : largestDamage;
        }

        return idx;
    }

    private int SelectSkill()
    {
        int idx;
        while(true)
        {
            idx = Random.Range(0, skills.Length);
            if(stat.sp_arr[idx] > 1)
            {
                break;
            }
        }

        return idx;
    }


    #endregion
}
