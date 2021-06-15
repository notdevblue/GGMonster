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



    private void Awake()
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


        // AI�� �ƴҶ��� ����Ǿ�� ��
        // WARN , TODO : ��Ƽ�÷��� ����� ������ �ٲ�� ��
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

    #region ��ư���� ���Ǵ� ��ų �Լ���

    public override void SkillA() // �ڵ� ���� ���ϱ�
    {
        if (!stat.myturn) { return; }
        if (stat.sp_arr[0] < 1) { return; }
        InsultCodeDesign(stat.skillDmg[0], ref stat.sp_arr[0]);
        TurnManager.instance.EndTurn();
    }

    public override void SkillB() // ����ġ�� // �� �����̷� ������ ������. ��밡 �������̸� ���ݷ��� 50% ��ŭ ���� �� ��
    {
        if (!stat.myturn) { return; }
        if (stat.sp_arr[1] < 1) { return; }
        MoneyHeal(stat.skillDmg[1], ref stat.sp_arr[1]);
        TurnManager.instance.EndTurn();
    }

    public override void SkillC() // ������ ��� �ȸ� // n�ۼ�Ʈ�� Ȯ���� ��� ++hp
    {
        if (!stat.myturn) { return; }
        if (stat.sp_arr[2] < 1) { return; }
        PowerfulShoulderMassage(stat.skillDmg[2], ref stat.sp_arr[2]);
        TurnManager.instance.EndTurn();
    }

    public override void SkillD() // ����ȯ
    {
        if (!stat.myturn) { return; }
        if (stat.sp_arr[3] < 1) { return; }
        Naruto(stat.skillDmg[3], ref stat.sp_arr[3]);
        TurnManager.instance.EndTurn();
    }

    #endregion



    #region SP ���� ����� ���

    public override void SkillE() // ������ġ �����ϱ� (SP �� ������ ���)
    {
        Debug.Log("SeonHanAtk: ������ġ �����ϱ�");
        BuySandwich();
    }

    // ������ ��ų����Ʈ ���
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
            Debug.Log("SeonHanAtk: ���� ����");
            if(stat.curHp + salaryHp <= stat.maxHp)
            {
                stat.curHp += salaryHp;
            }
            else
            {
                Debug.Log("SeonHanAtk: �� ������ �зȴ�...");
            }
        }
    }
}