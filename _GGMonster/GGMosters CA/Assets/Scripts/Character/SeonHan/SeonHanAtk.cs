using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeonHanAtk : SkillBase, ISKill
{
    // 코드 설계 욕하기
    // 금융치료
    // 강력한 어께 안마
    // 나선환



    [SerializeField] private int  salaryTurn   = 10; // 페시브 턴
    [SerializeField] private int  salaryHp     = 5;  // 페시브 이득

    private void Awake()
    {
        stat = GetComponent<Stat>();
        cvsBattle = GameObject.FindGameObjectWithTag("CVSBattle");
        #region null 체크
        #if UNITY_EDITOR
        if (stat == null)
        {
            Debug.LogError("SeonHanAtk: Stat 을 찾을 수 없습니다.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        if(cvsBattle == null)
        {
            Debug.LogError("SeonHanAtk: cvsBattle 을 찾을 수 없습니다.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        #endregion


        // AI가 아닐때만 실행되어야 함
        // WARN , TODO : 멀티플레이 만들면 조건을 바꿔야 함
        if(GetComponent<AIStat>() == null)
        {
            InitBattleCsv();
            InitBtn();
        }

    }


    protected override void InitBtn()
    {
        btnSkillArr[0].onClick.AddListener(SkillA);
        btnSkillArr[1].onClick.AddListener(SkillB);
        btnSkillArr[2].onClick.AddListener(SkillC);
        btnSkillArr[3].onClick.AddListener(SkillD);
    }

    public void SkillA() // 코드 설계 욕하기
    {
        Debug.Log("SeonHanAtk: 코드 설계 욕하기");
        if (!SkillSuccess()) // TODO : 중복됨 
        {
            Debug.Log("SeonHanAtk A: 실패");
            return;
        }

        switch (stat.enemyType)
        {
            case Stat.ClassType.PROGRAMMER:
                stat.enemyStat.curHp -= (int)(stat.skillDmg[0] * stat.dmgBoostAmt); // TODO : <= 데미지 계산하고 변수에 담아야 함 (아마도)
                break;

            case Stat.ClassType.TEACHER:
                stat.enemyStat.curHp -= (int)(stat.skillDmg[0] * stat.dmgDecAmt);
                break;

            default:
                stat.enemyStat.curHp -= stat.skillDmg[0];
                break;
        }
    }

    public void SkillB() // 금융치료 // 돈 뭉텅이로 던저서 딜입힘. 상대가 선생님이면 힐 줌
    {
        Debug.Log("SeonHanAtk: 금융치료");
        if (!SkillSuccess())
        {
            Debug.Log("SeonHanAtk B: 실패");
            return;
        }


    }

    public void SkillC() // 강력한 어깨 안마
    {
        Debug.Log("SeonHanAtk: 강력한 어깨 안마");
        if(!SkillSuccess())
        {
            Debug.Log("SeonHanAtk C: 실패");
            return;
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
            Debug.Log("SeonHanAtk D: 실패");
            return;
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
        return false;
    }
}