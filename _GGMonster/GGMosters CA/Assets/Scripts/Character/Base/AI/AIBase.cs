using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : CharactorBase
{
    protected AIStat aiStat     = null;

    // lowHpAmount ���� ���
    private const float LOW_MUL = 0.2f;

    // �� ���� �Ŀ� ���ư��� ��
    // �ȱ׷��� �η��۷���

    protected override void Init(int hp, Stat.ClassType myType)
    {
        base.Init(hp, myType);

        // ���� AIStat �޾ƿ��� �� �ʱ�ȭ
        InitAIStat();

        // Ÿ�� ȿ�� ����
        ApplyTypeBenefit();
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
        aiStat.enemyLowHpAmount = (int)(stat.enemyStat.maxHp * LOW_MUL);

        // stat.sp => �ٽ� ���� �����ؾ� ��
        //aiStat.lowSpAmount = (int)(stat.sp_a * LOW_MUL);
        //aiStat.enemyLowSpAmount = (int)(enemyStat.sp_a * LOW_MUL);
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
            if (stat.myType == stat.enemyType || stat.myType == Stat.ClassType.TEACHER)
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

}
