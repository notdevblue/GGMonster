using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : CharactorBase
{
    protected AIStat aiStat     = null;

    // lowHpAmount 위한 상수
    private const float LOW_MUL = 0.2f;

    // 적 생성 후에 돌아가야 함
    // 안그러면 널레퍼런스

    protected override void Init(int hp, Stat.ClassType myType)
    {
        base.Init(hp, myType);

        // 본인 AIStat 받아오고 값 초기화
        InitAIStat();

        // 타입 효과 적용
        ApplyTypeBenefit();
    }


    #region Init Functions


    private void InitAIStat()
    {
        aiStat = GetComponent<AIStat>();
        #region null 체크
#if UNITY_EDITOR
        if(aiStat == null)
        {
            Debug.LogError("AIBase: aiStat 을 찾을 수 없습니다.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        #endregion

        aiStat.lowHpAmount = (int)(stat.maxHp * LOW_MUL);
        aiStat.enemyLowHpAmount = (int)(stat.enemyStat.maxHp * LOW_MUL);

        // stat.sp => 다시 변수 설정해야 함
        //aiStat.lowSpAmount = (int)(stat.sp_a * LOW_MUL);
        //aiStat.enemyLowSpAmount = (int)(enemyStat.sp_a * LOW_MUL);
    }

    protected override void ApplyTypeBenefit()
    {
        base.ApplyTypeBenefit();

        #region 적 타입 미입력 체크
        #if UNITY_EDITOR
        if(stat.enemyStat.myType == Stat.ClassType.NOTYPE)
        {
            Debug.LogError("AI: Enemy has no type, quitting");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #endif
        #endregion

        Debug.Log($"AI: My type is {stat.myType}, enemy type is {stat.enemyType}");


        // 유 불리 체크
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
