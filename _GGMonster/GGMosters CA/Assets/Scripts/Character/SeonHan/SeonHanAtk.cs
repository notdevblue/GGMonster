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


    // 모든 공격 클래스에 공통적으로 들어가야 합니다.
    private void Start()
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


        InitDictionary();

        // AI가 아닐때만 실행되어야 함
        // WARN , TODO : 멀티플레이 만들면 조건을 바꿔야 함
        if(GetComponent<AIStat>() == null)
        {
            CheckSkillNameList();
            InitBattleCsv();
            InitBtn();
        }
        else
        {
            isAI = true;
        }

        salaryTurn = stat.startFirst ? 12 : 13;

    }


    protected override void InitBtn()
    {
        btnSkillArr[0].onClick.AddListener(SkillA);
        btnSkillArr[1].onClick.AddListener(SkillB);
        btnSkillArr[2].onClick.AddListener(SkillC);
        btnSkillArr[3].onClick.AddListener(SkillD);
    }

    #region 버튼으로 사용되는 스킬 함수들

    public override void SkillA()
    {
        if (!stat.myturn) return;
        Skill(0);
    }

    public override void SkillB()
    {
        if (!stat.myturn) return;
        Skill(1);
    }

    public override void SkillC()
    {
        if (!stat.myturn) return;
        Skill(2);
    }

    public override void SkillD()
    {
        if (!stat.myturn) return;
        Skill(3);
    }

    #endregion



    #region SP 전부 사용한 경우

    public override void SkillE() // 샌드위치 구매하기 (SP 다 떨어진 경우)
    {
        // TODO : SkillE
        NoticeUI.instance.SetMsg("선한쌤의 센드위치 구매하기!", BuySandwich);
    }

    // 렌덤한 스킬포인트 상승
    private void BuySandwich()
    {
        int index = Random.Range(0, 4);
        ++stat.sp_arr[index];

        NoticeUI.instance.SetMsg($"{skillDataDictionary[selectedSkills[index]].name} 스킬을 다시 사용할 수 있다!");
        NoticeUI.instance.CallNoticeUI(true, true, true, false, false);
    }

    #endregion

    public override void Passive()
    {
        if (TurnManager.instance.turn % salaryTurn == 0)
        {
            NoticeUI.instance.SetMsg("선한쌤의 월급 받는 날!");
            if (stat.curHp + salaryHp <= stat.maxHp)
            {
                NoticeUI.instance.SetMsg($"{salaryHp} 만큼의 HP을 회복했다!", () =>
                {
                    stat.curHp += salaryHp;
                    DamageEffects.instance.HealEffect(transform);
                    DamageEffects.instance.TextEffect(salaryHp, GetComponent<CharactorDamage>().damageText);
                    TurnManager.instance.MidTurn();
                });
            }
            else
            {
                NoticeUI.instance.SetMsg("앗 선한샘의 월급이 밀렸다...", () => { Debug.LogWarning($"SeonHanATK: {stat.curHp}, {salaryHp}"); });
            }

            Invoke(nameof(Wait), 1.0f);
        }
    }

    private void Wait()
    {
        NoticeUI.instance.CallNoticeUI(false, true, true, false, true);
    }
}