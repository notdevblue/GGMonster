using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaEunAtk : Skills
{
    [SerializeField] private int salaryTurn = 10; // ��ú� ��
    [SerializeField] private int salaryHp = 5;  // ��ú� �̵�


    // ��� ���� Ŭ������ ���������� ���� �մϴ�.
    private void Start()
    {
        stat = GetComponent<Stat>();
        cvsBattle = GameObject.FindGameObjectWithTag("CVSMain");
        #region null üũ
#if UNITY_EDITOR
        if (stat == null)
        {
            Debug.LogError("HaEunAtk: Stat �� ã�� �� �����ϴ�.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        if (cvsBattle == null)
        {
            Debug.LogError("HaEunAtk: cvsMain �±׸� ���� ���� ������Ʈ�� ã�� �� �����ϴ�.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        #endregion


        InitDictionary();

        // AI�� �ƴҶ��� ����Ǿ�� ��
        // WARN , TODO : ��Ƽ�÷��� ����� ������ �ٲ�� ��
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

        salaryTurn = stat.startFirst ? 12 : 13;
    }


    protected override void InitBtn()
    {
        btnSkillArr[0].onClick.AddListener(SkillA);
        btnSkillArr[1].onClick.AddListener(SkillB);
        btnSkillArr[2].onClick.AddListener(SkillC);
        btnSkillArr[3].onClick.AddListener(SkillD);
    }

    #region ��ư���� ���Ǵ� ��ų �Լ���

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



    #region SP ���� ����� ���

    public override void SkillE() // �޽��ϱ� (SP �� ������ ���)
    {
        NoticeUI.instance.SetMsg("�������� �޽��ϱ�!", RestAtHome);
    }

    // ��ų����Ʈ 1 ���
    private void RestAtHome()
    {
        ++stat.sp_arr[0];
        ++stat.sp_arr[1];
        ++stat.sp_arr[2];
        ++stat.sp_arr[3];

        NoticeUI.instance.SetMsg("��� ��ų�� �ٽ� �ѹ� ����� �� �ִ�!");
        NoticeUI.instance.CallNoticeUI(true, false, false, true, false);
    }

    #endregion

    // TODO : ������ Ŭ������ ��� �������� ��ú���.
    public override void Passive()
    {
        if (TurnManager.instance.turn % salaryTurn == 0)
        {
            NoticeUI.instance.SetMsg("�������� ���� �޴� ��!");
            if (stat.curHp + salaryHp <= stat.maxHp)
            {
                NoticeUI.instance.SetMsg($"{salaryHp} ��ŭ�� HP�� ȸ���ߴ�!", () =>
                {
                    stat.curHp += salaryHp;
                    DamageEffects.instance.HealEffect(transform);
                    DamageEffects.instance.TextEffect(salaryHp, GetComponent<CharactorDamage>().damageText);
                    TurnManager.instance.MidTurn();
                });
            }
            else
            {
                NoticeUI.instance.SetMsg("�� �������� ������ �зȴ�...", () => { Debug.LogWarning($"HaEunATK: {stat.curHp}, {salaryHp}"); });
            }
            
            Invoke(nameof(Wait), 1.0f);
        }
    }

    private void Wait()
    {
        NoticeUI.instance.CallNoticeUI(false, true, false, true, false);
    }
}
