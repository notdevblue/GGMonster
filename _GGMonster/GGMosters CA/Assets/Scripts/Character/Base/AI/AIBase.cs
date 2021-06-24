using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : CharactorBase
{
    protected AIStat aiStat     = null;
    protected Skills skillList  = null;
    protected Items  item       = null;
    private   ISkill skill      = null;

    private delegate void   SkillFunction(); 

    // lowHpAmount ���� ���
    private readonly float LOW_MUL = 0.2f;

    [HideInInspector] public static bool turnPlayed;

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
    private readonly int onExtremelyLowHpUseProb  = 0;

    #endregion


    #endregion
    
    
    
    protected override void Init(int hp, Stat.ClassType myType, bool calledByAi = false)
    {
        base.Init(hp, myType, calledByAi);

        skill     = GetComponent<ISkill>();
        skillList = GetComponent<Skills>();
        item      = GameObject.FindGameObjectWithTag("CVSMain").transform.GetChild(1).GetComponent<Items>();

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

        if (stat.startFirst) turnPlayed = true;
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
        Debug.Log("TC");
        NoticeUI.instance.CallNoticeUI(true);
        Debug.Log("Called");
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
        skillList.Skill(SelectSkill());
        
    }

    private void UseItem()
    {
        ProvokeItem();
        RecoveryItem();

        #region Function inside Function

        void ProvokeItem()
        {
            if(!stat.provoke || stat.provItemCnt < 1) { return; }
            int probSave = (aiStat.isHighProvoke ? onHighProvokeItemUseProb : onProvokeItemUseProb);
            int provokeProb = aiStat.lowHp ? probSave * onLowHpProvokeItemUseMul : probSave;


            if (Random.Range(0, 100) > provokeProb)
            {
                item.ResetProvokeCount(stat);
            }
        }

        void RecoveryItem()
        {
            if(stat.healItemCnt < 1) { return; }
            // ����
            int hpProb = ((aiStat.exLowHpAmount >= stat.curHp) ? onExtremelyLowHpUseProb : (aiStat.lowHp ? onLowHpItemUseProb : 154));

            if(Random.Range(0, 100) > hpProb)
            {
                item.Heal(stat);
            }
        }
        #endregion
    }

    private int SelectSkill(int i = -1)
    {
        if(i != -1)
        {
            if (stat.sp_arr[i] < 1)
            {
                return -1;
            }
            else
            {
                return i;
            }
        }

        int idx;

        int r = 0;
        while(true)
        {
            ++r;
            idx = Random.Range(0, 3);
            if(stat.sp_arr[idx] > 1)
            {
                break;
            }

            if(r > 100) {  break; }
        }
        if(r > 100) Debug.LogError("ERR WHILE LOOP");
        return idx;
    }


    #endregion
}
