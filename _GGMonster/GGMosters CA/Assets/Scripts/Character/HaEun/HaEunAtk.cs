using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaEunAtk : Skills
{
    [SerializeField] private int salaryTurn = 10; // 페시브 턴
    [SerializeField] private int salaryHp = 5;  // 페시브 이득


    // 모든 공격 클래스에 공통적으로 들어가야 합니다.
    private void Start()
    {
        stat = GetComponent<Stat>();
        cvsBattle = GameObject.FindGameObjectWithTag("CVSMain");
        #region null 체크
#if UNITY_EDITOR
        if (stat == null)
        {
            Debug.LogError("HaEunAtk: Stat 을 찾을 수 없습니다.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        if (cvsBattle == null)
        {
            Debug.LogError("HaEunAtk: cvsMain 태그를 가진 게임 오브젝트를 찾을 수 없습니다.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        #endregion


        InitDictionary();

        // AI가 아닐때만 실행되어야 함
        // WARN , TODO : 멀티플레이 만들면 조건을 바꿔야 함
        if (GetComponent<AIStat>() == null)
        {
            CheckSkillNameList();
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

    public override void SkillE() // 휴식하기 (SP 다 떨어진 경우)
    {
        Debug.Log("HaEunAtk: 휴식하기");
        RestAtHome();
    }

    // 스킬포인트 1 상승
    private void RestAtHome()
    {
        ++stat.sp_arr[0];
        ++stat.sp_arr[1];
        ++stat.sp_arr[2];
        ++stat.sp_arr[3];
    }

    #endregion

    // TODO : 선생님 클레스인 경우 공통적인 페시브임.
    public override void Passive()
    {
        if (TurnManager.instance.turn % salaryTurn == 0)
        {
            NoticeUI.instance.SetMsg("선생님의 월급 받는 날!");
            if (stat.curHp + salaryHp <= stat.maxHp)
            {
                stat.curHp += salaryHp;
            }
            else
            {
                NoticeUI.instance.SetMsg("앗 하은쌤의 월급이 밀렸다...");
            }

            NoticeUI.instance.CallNoticeUI(false, true, false, true, false);
        }
    }
}
