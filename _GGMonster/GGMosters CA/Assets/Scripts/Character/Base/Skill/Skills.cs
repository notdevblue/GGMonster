using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillListEnum
{
    // �� �ȿ� �̳� ����?
}

abstract public class Skills : SkillBase
{
    public delegate void SkillExample(int damage, ref int skillPoint);
    public Dictionary<SkillListEnum, SkillExample> skillDictionary = new Dictionary<SkillListEnum, SkillExample>();


    #region ���ѽ�

    //delegate void SeonHanAtkDelegate(int damage, ref int skillPoint);

    public void InsultCodeDesign(int damage, ref int skillPoint) // �ڵ� ���� ���ϱ�, ���ѽ� �� �ڵ�
    {
        --skillPoint;
        Debug.Log("�ڵ� ���� ���ϱ�");
        if (!SkillSuccess())
        {
            Debug.Log("�ڵ� ���� ���ϱ� ����");
            return;
        }

        switch (stat.enemyType)
        {
            case Stat.ClassType.PROGRAMMER:
                stat.enemyStat.curHp -= (int)(damage * stat.dmgBoostAmt); // TODO : <= ������ ����ϰ� ������ ��ƾ� �� (�Ƹ���)
                break;

            case Stat.ClassType.TEACHER:
                stat.enemyStat.curHp -= (int)(damage * stat.dmgDecAmt);
                break;

            default:
                stat.enemyStat.curHp -= damage;
                break;
        }

        
    }

    public void MoneyHeal(int damage, ref int skillPoint) // ����ġ�� // �� �����̷� ������ ������. ��밡 �������̸� ���ݷ��� 50% ��ŭ ���� �� ��
    {
        --skillPoint;
        Debug.Log("����ġ��");
        if (!SkillSuccess())
        {
            Debug.Log("����ġ�� ����");
            return;
        }

        if (stat.enemyType == Stat.ClassType.TEACHER)
        {
            if((stat.enemyStat.curHp += damage / 2) > 100)
            {
                stat.enemyStat.curHp = 100;
            }
            else
            {
                stat.enemyStat.curHp += damage / 2;
            }
            Debug.Log("�������� �ູ�� �󱼷� ���� �����̴�...");
            return;
        }

        if(stat.damageBoost)
        {
            stat.enemyStat.curHp -= (int)(damage * stat.dmgBoostAmt);
        }
        else
        {
            stat.enemyStat.curHp -= damage;
        }
    }

    public void PowerfulShoulderMassage(int damage, ref int skillPoint) // ������ ��� �ȸ� // n�ۼ�Ʈ�� Ȯ���� ��� ++hp, ���ѽ� �� �ڵ�
    {
        --skillPoint;
        Debug.Log("������ ��� �ȸ�");
        if (!SkillSuccess())
        {
            Debug.Log("����");
            return;
        }

        // �
        int rand = Random.Range(1, 100);
        if(rand > 95)
        {
            stat.enemyStat.curHp += (int)(damage * 0.2f);
            Debug.Log("��밡 �ȸ��� �����ϰ� �޾Ƶ�ȴ�...");
            return;
        }

        // ����
        switch (stat.enemyType == Stat.ClassType.TEACHER)
        {
            case true:
                stat.enemyStat.curHp -= (int)(damage * 0.8f);
                break;

            case false:
                stat.enemyStat.curHp -= damage;
                break;
        }
    }

    public void Naruto(int damage, ref int skillPoint) // ����ȯ, ���ѽ� �� �ڵ�
    {
        --skillPoint;
        Debug.Log("����ȯ");
        if (!SkillSuccess())
        {
            Debug.Log("����");
            return;
        }
        stat.enemyStat.curHp -= damage;
    }

    public void Tsundere(int damage, ref int skillPoint) // �����Ÿ���, ���ѽ� �� �ڵ�, ���߱�
    {
        --skillPoint;
        Debug.Log("�����Ÿ���");
        if(!SkillSuccess())
        {
            Debug.Log("����");
            return;
        }

        stat.enemyStat.provoke = true;
        ++stat.enemyStat.provokeCount;
    }

    #endregion

    #region ���� ��ų

    public void WaterAttack(int damage, ref int skillPoint) // ������, ��ǻ�Ϳ� ���� �K�´�, ���ӵ�
    {
        --skillPoint;
        Debug.Log("������");
        if(!SkillSuccess())
        {
            Debug.Log("����");
            return;
        }

    }

    #endregion


    // �̽� ����
    private bool SkillSuccess()
    {
        int rand = Random.Range(0, 100);

        switch (stat.provoke)
        {
            case true:
                if (rand > stat.provokeCount * stat.ProvMissRate)
                {
                    Debug.Log("Skill success");
                    return true;
                }
                break;

            case false:
                if (rand > stat.missRate)
                {
                    Debug.Log("Skill success");
                    return true;
                }
                break;
        }
        return false;
    }


    abstract protected void InitBtn();
}
