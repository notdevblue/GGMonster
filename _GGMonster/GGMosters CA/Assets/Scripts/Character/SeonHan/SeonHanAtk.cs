using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 그저 생각 : 모든 스킬을 SkillBase 에 만들어버린 다음에 그냥 그걸 상속하게 하면 스킬 돌려쓸수잇지않을까

public class SeonHanAtk : Skills
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
        cvsBattle = GameObject.FindGameObjectWithTag("CVSMain");
        #region null 체크
        #if UNITY_EDITOR
        if (stat == null)
        {
            Debug.LogError("SeonHanAtk: Stat 을 찾을 수 없습니다.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        if(cvsBattle == null)
        {
            Debug.LogError("SeonHanAtk: cvsMain 태그를 가진 게임 오브젝트를 찾을 수 없습니다.");
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
        else
        {
            isAI = true;
        }
    }


    protected override void InitBtn()
    {
        btnSkillArr[0].onClick.AddListener(SkillA);
        btnSkillArr[1].onClick.AddListener(SkillB);
        btnSkillArr[2].onClick.AddListener(SkillC);
        btnSkillArr[3].onClick.AddListener(SkillD);
    }

    #region 버튼으로 사용되는 스킬 함수들

    public override void SkillA() // 코드 설계 욕하기
    {
        if (!stat.myturn) { return; }
        if (stat.sp_arr[0] < 1) { return; }
        InsultCodeDesign(stat.skillDmg[0], ref stat.sp_arr[0]);
        TurnManager.instance.EndTurn();
    }

    public override void SkillB() // 금융치료 // 돈 뭉텅이로 던저서 딜입힘. 상대가 선생님이면 공격력의 50% 만큼 힐을 해 줌
    {
        if (!stat.myturn) { return; }
        if (stat.sp_arr[1] < 1) { return; }
        MoneyHeal(stat.skillDmg[1], ref stat.sp_arr[1]);
        TurnManager.instance.EndTurn();
    }

    public override void SkillC() // 강력한 어깨 안마 // n퍼센트의 확률로 상대 ++hp
    {
        if (!stat.myturn) { return; }
        if (stat.sp_arr[2] < 1) { return; }
        PowerfulShoulderMassage(stat.skillDmg[2], ref stat.sp_arr[2]);
        TurnManager.instance.EndTurn();
    }

    public override void SkillD() // 나선환
    {
        if (!stat.myturn) { return; }
        if (stat.sp_arr[3] < 1) { return; }
        Naruto(stat.skillDmg[3], ref stat.sp_arr[3]);
        TurnManager.instance.EndTurn();
    }

    #endregion



    #region SP 전부 사용한 경우

    public override void SkillE() // 샌드위치 구매하기 (SP 다 떨어진 경우)
    {
        Debug.Log("SeonHanAtk: 샌드위치 구매하기");
        BuySandwich();
    }

    // 렌덤한 스킬포인트 상승
    private void BuySandwich()
    {
        int index = Random.Range(0, 4);
        ++stat.sp_arr[index];
    }

    #endregion

    public override void Passive()
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
}