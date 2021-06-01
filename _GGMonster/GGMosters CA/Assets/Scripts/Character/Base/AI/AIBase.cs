using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : CharactorBase
{
    protected Stat enemyStat = null;
    protected AIStat aiStat = null;

    // lowHpAmount ���� ���
    private const float LOW_MUL = 0.2f;

    // �� ���� �Ŀ� ���ư��� ��
    // �ȱ׷��� �η��۷���

    sealed protected override void Init(int[] skillPointArr, int hp, Stat.ClassType myType)
    {
        base.Init(skillPointArr, hp, myType);
        
        // �� stat �޾ƿ�
        GetEnemyStat();

        // ���� AIStat �޾ƿ��� �� �ʱ�ȭ
        InitAIStat();


    }


    #region Init Functions
    private void GetEnemyStat()
    {
        enemyStat = GameObject.FindGameObjectWithTag("Player").GetComponent<Stat>();
        #region null üũ
#if UNITY_EDITOR
        if(enemyStat == null)
        {
            Debug.LogError("AIBase: enemyStat �� ã�� �� �����ϴ�.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        #endregion
    }

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

        aiStat.lowHpAmount = (int)(stat.hp * LOW_MUL);
        aiStat.enemyLowHpAmount = (int)(enemyStat.hp * LOW_MUL);

        // �ٽ� ���� �����ؾ� ��
        //aiStat.lowSpAmount = (int)(stat.sp_a * LOW_MUL);
        //aiStat.enemyLowSpAmount = (int)(enemyStat.sp_a * LOW_MUL);
    }


    #endregion

}
