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

        #region null üũ
        #if UNITY_EDITOR
        if (stat == null)
        {
            Debug.LogError("SeonHanAtk: Stat �� ã�� �� �����ϴ�.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #endif
        #endregion
    }



    public void SkillA() // �ڵ� ���� ���ϱ�
    {
        Debug.Log("SeonHanAtk: �ڵ� ���� ���ϱ�");
        if (!SkillSuccess()) // TODO : �ߺ���
        {
            Debug.Log("SeonHanAtk: ����");
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
            Debug.Log("SeonHanAtk: ����");
        }


    }

    public void SkillC() // ������ ��� �ȸ�
    {
        Debug.Log("SeonHanAtk: ������ ��� �ȸ�");
        if(!SkillSuccess())
        {
            Debug.Log("SeonHanAtk: ����");
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
            Debug.Log("SeonHanAtk: ����");
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

        Debug.Log("SeonHanAtk: Skill failed");
        return false;
    }
}