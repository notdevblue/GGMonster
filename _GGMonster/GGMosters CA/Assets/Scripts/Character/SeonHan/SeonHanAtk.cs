using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeonHanAtk : MonoBehaviour, ISKill
{
    private Stat stat = null;
    [SerializeField] private int  salaryTurn   = 10;
    [SerializeField] private int  salaryHp     = 5;

    private void Awake()
    {
        stat = GetComponent<Stat>();

        #region null 체크
        #if UNITY_EDITOR
        if (stat == null)
        {
            Debug.LogError("SeonHanAtk: Stat 을 찾을 수 없습니다.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #endif
        #endregion
    }



    public void SkillA() // 코드 설계 욕하기
    {
        Debug.Log("SeonHanAtk: 코드 설계 욕하기");
        if (!SkillSuccess()) // TODO : 중복됨
        {
            Debug.Log("SeonHanAtk: 실패");
        }

        switch (stat.enemyType)
        {
            case Stat.ClassType.PROGRAMMER:
                stat.enemyStat.curHp -= (int)(stat.skillDmg[0] * stat.dmgBoostAmt);
                break;

            case Stat.ClassType.TEACHER:
                stat.enemyStat.curHp -= (int)(stat.skillDmg[0] * stat.dmgDecAmt);
                break;

            default:
                stat.enemyStat.curHp -= stat.skillDmg[0];
                break;
        }
    }

    public void SkillB() // 
    {
        if (!SkillSuccess())
        {
            Debug.Log("SeonHanAtk: 실패");
        }


    }

    public void SkillC() // 강력한 어깨 안마
    {
        Debug.Log("SeonHanAtk: 강력한 어깨 안마");
        if(!SkillSuccess())
        {
            Debug.Log("SeonHanAtk: 실패");
        }

        // 공격
        switch (stat.enemyType == Stat.ClassType.TEACHER)
        {
            case true:
                stat.enemyStat.curHp -= (int)(stat.skillDmg[3] * 0.8f);
                break;

            case false:
                stat.enemyStat.curHp -= stat.skillDmg[3];
                break;
        }
    }

    public void SkillD() // 나선환
    {
        if (!SkillSuccess())
        {
            Debug.Log("SeonHanAtk: 실패");
        }


    }

    public void SkillE() // 샌드위치 구매하기 (SP 다 떨어진 경우)
    {
        Debug.Log("SeonHanAtk: 샌드위치 구매하기");
        BuySandwich();
    }
    
    public void Passive()
    {
        if(TurnManager.instance.turn % salaryTurn == 0)
        {
            Debug.Log("SeonHanAtk: 월급 수령");
            if(stat.curHp + salaryHp <= stat.maxHp)
            {
                stat.curHp += salaryHp;
            }
            else
            {
                Debug.Log("SeonHanAtk: 앗 월급이 밀렸다...");
            }
        }

    }

    // 렌덤한 스킬포인트 상승
    private void BuySandwich()
    {
        int index = Random.Range(0, 4);
        ++stat.sp_arr[index];
    }

    // 미스 여부
    private bool SkillSuccess()
    {
        int rand = Random.Range(0, 100);
        
        switch(stat.provoke)
        {
            case true:
                if (rand > stat.provokeCount * stat.ProvMissRate)
                {
                    Debug.Log("SeonHanAtk: Skill success");
                    return true;
                }
                break;
            
            case false:
                if(rand > stat.missRate)
                {
                    Debug.Log("SeonHanAtk: Skill success");
                    return true;
                }
                break;
        }

        Debug.Log("SeonHanAtk: Skill failed");
        return false;
    }
}