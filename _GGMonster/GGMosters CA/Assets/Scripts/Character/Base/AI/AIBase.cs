using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : CharactorBase
{
    protected Stat enemyStat = null;
    protected AIStat aiStat = null;

    // lowHpAmount 위한 상수
    private const float LOW_MUL = 0.2f;

    // 적 생성 후에 돌아가야 함
    // 안그러면 널레퍼런스

    sealed protected override void Init(int[] skillPointArr, int hp, Stat.ClassType myType)
    {
        base.Init(skillPointArr, hp, myType);
        
        // 적 stat 받아옴
        GetEnemyStat();

        // 본인 AIStat 받아오고 값 초기화
        InitAIStat();


    }


    #region Init Functions
    private void GetEnemyStat()
    {
        enemyStat = GameObject.FindGameObjectWithTag("Player").GetComponent<Stat>();
        #region null 체크
#if UNITY_EDITOR
        if(enemyStat == null)
        {
            Debug.LogError("AIBase: enemyStat 을 찾을 수 없습니다.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        #endregion
    }

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

        aiStat.lowHpAmount = (int)(stat.hp * LOW_MUL);
        aiStat.enemyLowHpAmount = (int)(enemyStat.hp * LOW_MUL);

        // 다시 변수 설정해야 함
        //aiStat.lowSpAmount = (int)(stat.sp_a * LOW_MUL);
        //aiStat.enemyLowSpAmount = (int)(enemyStat.sp_a * LOW_MUL);
    }


    #endregion

}
