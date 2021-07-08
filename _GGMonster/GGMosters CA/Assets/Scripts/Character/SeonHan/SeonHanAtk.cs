using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���� ���� : ��� ��ų�� SkillBase �� �������� ������ �׳� �װ� ����ϰ� �ϸ� ��ų ������������������

public class SeonHanAtk : Skills
{
    // �ڵ� ���� ���ϱ�
    // ����ġ��
    // ������ � �ȸ�
    // ����ȯ

    [SerializeField] private int  salaryTurn   = 10; // ��ú� ��
    [SerializeField] private int  salaryHp     = 5;  // ��ú� �̵�


    // ��� ���� Ŭ������ ���������� ���� �մϴ�.
    private void Start()
    {
        stat = GetComponent<Stat>();
        cvsBattle = GameObject.FindGameObjectWithTag("CVSMain");
        #region null üũ
        #if UNITY_EDITOR
        if (stat == null)
        {
            Debug.LogError("SeonHanAtk: Stat �� ã�� �� �����ϴ�.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        if(cvsBattle == null)
        {
            Debug.LogError("SeonHanAtk: cvsMain �±׸� ���� ���� ������Ʈ�� ã�� �� �����ϴ�.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        #endregion


        InitDictionary();

        // AI�� �ƴҶ��� ����Ǿ�� ��
        // WARN , TODO : ��Ƽ�÷��� ����� ������ �ٲ�� ��
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

    public override void SkillE() // ������ġ �����ϱ� (SP �� ������ ���)
    {
        // TODO : SkillE
        NoticeUI.instance.SetMsg("���ѽ��� ������ġ �����ϱ�!", BuySandwich);
    }

    // ������ ��ų����Ʈ ���
    private void BuySandwich()
    {
        int index = Random.Range(0, 4);
        ++stat.sp_arr[index];

        NoticeUI.instance.SetMsg($"{skillDataDictionary[selectedSkills[index]].name} ��ų�� �ٽ� ����� �� �ִ�!");
        NoticeUI.instance.CallNoticeUI(true, true, true, false, false);
    }

    #endregion

    public override void Passive()
    {
        if (TurnManager.instance.turn % salaryTurn == 0)
        {
            NoticeUI.instance.SetMsg("���ѽ��� ���� �޴� ��!");
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
                NoticeUI.instance.SetMsg("�� ���ѻ��� ������ �зȴ�...", () => { Debug.LogWarning($"SeonHanATK: {stat.curHp}, {salaryHp}"); });
            }

            Invoke(nameof(Wait), 1.0f);
        }
    }

    private void Wait()
    {
        NoticeUI.instance.CallNoticeUI(false, true, true, false, true);
    }
}