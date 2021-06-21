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
        if (!stat.myturn || stat.sp_arr[0] < 1) { return; }
        skillDataDictionary[selectedSkills[0]].skill(ref stat.sp_arr[0]);
        TurnManager.instance.EndTurn();
    }

    public override void SkillB() // ����ġ�� // �� �����̷� ������ ������. ��밡 �������̸� ���ݷ��� 50% ��ŭ ���� �� ��
    {
        if (!stat.myturn || stat.sp_arr[1] < 1) { return; }
        skillDataDictionary[selectedSkills[1]].skill(ref stat.sp_arr[1]);
        TurnManager.instance.EndTurn();
    }

    public override void SkillC() // ������ ��� �ȸ� // n�ۼ�Ʈ�� Ȯ���� ��� ++hp
    {
        if (!stat.myturn || stat.sp_arr[2] < 1) { return; }
        skillDataDictionary[selectedSkills[2]].skill(ref stat.sp_arr[2]);
        TurnManager.instance.EndTurn();
    }

    public override void SkillD() // ����ȯ
    {
        if (!stat.myturn || stat.sp_arr[3] < 1) { return; }
        skillDataDictionary[selectedSkills[3]].skill(ref stat.sp_arr[3]);
        TurnManager.instance.EndTurn();
    }

    #endregion



    #region SP ���� ����� ���

    public override void SkillE() // ������ġ �����ϱ� (SP �� ������ ���)
    {
        Debug.Log("HaEunAtk: �޽��ϱ�");
        RestAtHome();
    }

    // ������ ��ų����Ʈ ���
    private void RestAtHome()
    {
        int index = Random.Range(0, 4);
        ++stat.sp_arr[index];
    }

    #endregion

    public override void Passive()
    {
        if (TurnManager.instance.turn % salaryTurn == 0)
        {
            Debug.Log("HaEunAtk: ���� ����");
            if (stat.curHp + salaryHp <= stat.maxHp)
            {
                stat.curHp += salaryHp;
            }
            else
            {
                Debug.Log("HaEunAtk: �� ������ �зȴ�...");
            }
        }
    }
}
