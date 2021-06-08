using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CharactorBase : MonoBehaviour
{
    private   Stat.ClassType[] typeArr; // 있는 모든 타입 가져옴
    protected Stat             stat;


    protected const int SKILLPOINTARRSIZE = 4; // skillPointArr의 크기는 무조건 4 이어야 함

    #region 초기화 함수
    protected void getStat()
    {
        stat = GetComponent<Stat>();
        #region null 체크
#if UNITY_EDITOR
        if (stat == null)
        {
            Debug.LogError("SeonHanAI: Stat 을 찾을 수 없습니다.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        #endregion
    }
    private void InitTypeArr()
    {
        int count;
        for (count = 0; count != (int)Stat.ClassType.NOTYPE; ++count) { }

        typeArr = new Stat.ClassType[count];

        for(int i = 0; i < count; ++i)
        {
            typeArr[i] = (Stat.ClassType)i;
        }
    }
    #endregion

    /// <summary>
    /// 캐릭터 스텟 초기화 함수
    /// </summary>
    /// <param name="skillPointArr">스킬포인트 배열</param>
    /// <param name="hp">HP</param>
    /// <param name="myType">자신의 타입</param>
    protected virtual void Init(int hp, Stat.ClassType myType, bool calledByAi = false)
    {
        // stat GetComponent;
        getStat();

        // 적 스텟 받아옴
        GetEnemyStat(calledByAi);

        // 타입 저장 배열 초기화
        InitTypeArr();

        // HP 초기화
        stat.maxHp    = hp;
        stat.curHp = hp;

        // myType 초기화
        stat.myType     = myType;
        stat.enemyType  = stat.enemyStat.myType;

        #region 잘못된 값 또는 미입력 체크
#if UNITY_EDITOR
        CheckValue();
#endif
        #endregion
    }

    // 상대 타입 따라 데미지 버프 또는 페시브
    protected virtual void ApplyTypeBenefit()
    {
        if(stat.myType == typeArr[((int)stat.enemyType + 1) % typeArr.Length])
        {
            stat.damageBoost = true;
        }
        else if(stat.myType == Stat.ClassType.TEACHER && stat.myType == stat.enemyType) // 뭔가 불편함
        {
            stat.damageBoost = true;
        }
        else
        {
            stat.damageBoost = false;
        }
    }

    private void GetEnemyStat(bool calledByAi = false)
    {
        if(calledByAi)
        {
            stat.enemyStat = GameObject.FindGameObjectWithTag("Player").GetComponent<Stat>();
        }
        else
        {
            stat.enemyStat = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Stat>();
        }
        
        #region null 체크
#if UNITY_EDITOR
        if (stat.enemyStat == null)
        {
            Debug.LogError("AIBase: enemyStat 을 찾을 수 없습니다.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        #endregion
    }

    #region 유니티 에디터에서만 실행됨
#if UNITY_EDITOR

    void CheckValue()
    {
        // SP
        bool[] statEmpty = { false, false, false, false };
        bool stop = false;

        for (int i = 0; i < 4; ++i)
        {
            if (stat.sp_arr[i] < 0)
            {
                statEmpty[i] = true;
                stop = true;
                Debug.LogError(i + " 번째 스킬포인트가 잘못 입력되었거나 입력되지 않았습니다.");
            }
        }
        
        // 스킬 데미지
        bool[] dmgEmpty = { false, false, false, false };

        for (int i = 0; i < 4; ++i)
        {
            if (stat.skillDmg[i] < 0)
            {
                dmgEmpty[i] = true;
                stop = true;
                Debug.LogError(i + " 번째 스킬 데미지가 잘못 입력되었거나 입력되지 않았습니다.");
            }
        }

        // HP
        if (stat.maxHp < 0)
        {
            Debug.LogError("HP 가 잘못 입력되었거나 입력되지 않았습니다.");
            stop = true;
        }

        // myType
        if (stat.myType == Stat.ClassType.NOTYPE)
        {
            Debug.LogError("myType 이 입력되지 않았습니다.");
            stop = true;
        }

        if (stop)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }

    }


#endif

    #endregion
}
