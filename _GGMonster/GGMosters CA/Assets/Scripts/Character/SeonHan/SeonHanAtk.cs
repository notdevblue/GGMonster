using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeonHanAtk : SkillBase, ISKill
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
        cvsBattle = GameObject.FindGameObjectWithTag("CVSBattle");
        #region null üũ
        #if UNITY_EDITOR
        if (stat == null)
        {
            Debug.LogError("SeonHanAtk: Stat �� ã�� �� �����ϴ�.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        if(cvsBattle == null)
        {
            Debug.LogError("SeonHanAtk: cvsBattle �� ã�� �� �����ϴ�.");
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

    }


    protected override void InitBtn()
    {
        btnSkillArr[0].onClick.AddListener(SkillA);
        btnSkillArr[1].onClick.AddListener(SkillB);
        btnSkillArr[2].onClick.AddListener(SkillC);
        btnSkillArr[3].onClick.AddListener(SkillD);
    }

    public void SkillA() // �ڵ� ���� ���ϱ�
    {
        Debug.Log("SeonHanAtk: �ڵ� ���� ���ϱ�");
        if (!SkillSuccess()) // TODO : �ߺ��� 
        {
            Debug.Log("SeonHanAtk A: ����");
            return;
        }

        switch (stat.enemyType)
        {
            case Stat.ClassType.PROGRAMMER:
                stat.enemyStat.curHp -= (int)(stat.skillDmg[0] * stat.dmgBoostAmt); // TODO : <= ������ ����ϰ� ������ ��ƾ� �� (�Ƹ���)
                break;

            case Stat.ClassType.TEACHER:
                stat.enemyStat.curHp -= (int)(stat.skillDmg[0] * stat.dmgDecAmt);
                break;

            default:
                stat.enemyStat.curHp -= stat.skillDmg[0];
                break;
        }
    }

    public void SkillB() // ����ġ�� // �� �����̷� ������ ������. ��밡 �������̸� �� ��
    {
        Debug.Log("SeonHanAtk: ����ġ��");
        if (!SkillSuccess())
        {
            Debug.Log("SeonHanAtk B: ����");
            return;
        }


    }

    public void SkillC() // ������ ��� �ȸ�
    {
        Debug.Log("SeonHanAtk: ������ ��� �ȸ�");
        if(!SkillSuccess())
        {
            Debug.Log("SeonHanAtk C: ����");
            return;
        }

        // ����
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

    public void SkillD() // ����ȯ
    {
        if (!SkillSuccess())
        {
            Debug.Log("SeonHanAtk D: ����");
            return;
        }


    }

    public void SkillE() // ������ġ �����ϱ� (SP �� ������ ���)
    {
        Debug.Log("SeonHanAtk: ������ġ �����ϱ�");
        BuySandwich();
    }
    
    public void Passive()
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

    // ������ ��ų����Ʈ ���
    private void BuySandwich()
    {
        int index = Random.Range(0, 4);
        ++stat.sp_arr[index];
    }

    // �̽� ����
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