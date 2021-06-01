using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CharactorBase : MonoBehaviour
{
    private   Stat.ClassType[] typeArr;
    protected Stat             stat;

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
    protected virtual void Init(int[] skillPointArr, int hp, Stat.ClassType myType)
    {
        // stat GetComponent;
        getStat();

        #region 배열 길이 잘못 입력 체크
#if UNITY_EDITOR
        if (skillPointArr.Length < 4)
        {
            Debug.LogError("skillPoint 배열 길이가 잘못되었습니다. 배열 길이는 무조건 4 이어야 합니다.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        #endregion

        // 타입 저장 배열 초기화
        InitTypeArr();

        // 스킬포인트 초기화
        stat.sp_a = skillPointArr[0];
        stat.sp_b = skillPointArr[1];
        stat.sp_c = skillPointArr[2];
        stat.sp_d = skillPointArr[3];

        // HP 초기화
        stat.hp    = hp;
        stat.curHp = hp;

        // myType 초기화
        stat.myType = myType;

        #region 잘못된 값 또는 미입력 체크
#if UNITY_EDITOR
        CheckSP();
#endif
        #endregion

        ApplyTypeBenefit();
    }

    // 상대 타입 따라 데미지 버프 또는 페시브
    protected virtual void ApplyTypeBenefit()
    {
        if(stat.myType == typeArr[((int)stat.enemyType + 1) % typeArr.Length])
        {
            stat.damageBoost = true;
        }
        else if(stat.myType == stat.enemyType && stat.myType == Stat.ClassType.TEACHER) // 뭔가 불편함
        {
            stat.damageBoost = true;
        }

        #region Archive: switch comparing
        // TODO : 타입이 늘어날 때 마다 코드를 수정해야 함
        // 반복되는 작업이 많음
        // 불필요해보이는 코드가 많음
        // 줄일수는 있을거같음
        // 타입이 추가될 거 같지는 않지만 흠흠
        // IDEA : 배열?

        //switch (myType)
        //{
        //    case Stat.ClassType.DIRECTOR:
        //        if (stat.enemyType == Stat.ClassType.PROGRAMMER)
        //        {
        //            stat.damageBoost = true;
        //        }
        //        break;

        //    case Stat.ClassType.PROGRAMMER:
        //        if (stat.enemyType == Stat.ClassType.ARTIST)
        //        {
        //            stat.damageBoost = true;
        //        }
        //        break;

        //    case Stat.ClassType.ARTIST:
        //        if (stat.enemyType == Stat.ClassType.DIRECTOR)
        //        {
        //            stat.damageBoost = true;
        //        }
        //        break;

        //    case Stat.ClassType.TEACHER:
        //        if (stat.enemyType == Stat.ClassType.TEACHER)
        //        {
        //            stat.damageBoost = true;
        //        }
        //        break;

        //    default:
        //        Debug.LogError("알 수 없는 타입입니다.");
        //        break;
        //}
        #endregion // Do not use
    }

    #region 유니티 에디터에서만 실행됨
#if UNITY_EDITOR

    void CheckSP()
    {
        // SP
        bool[] statEmpty = { false, false, false, false };

        if (stat.sp_a < 0)
        {
            statEmpty[0] = true;
        }
        if (stat.sp_b < 0)
        {
            statEmpty[1] = true;
        }
        if (stat.sp_c < 0)
        {
            statEmpty[2] = true;
        }
        if (stat.sp_d < 0)
        {
            statEmpty[3] = true;
        }

        bool stop = false;
        for (int i = 0; i < statEmpty.Length; ++i)
        {
            if (statEmpty[i])
            {
                Debug.LogError(i + " 번째 스킬포인트가 잘못 입력되었거나 입력되지 않았습니다.");
                stop = true;
            }
        }

        // HP
        if (stat.hp < 0)
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
