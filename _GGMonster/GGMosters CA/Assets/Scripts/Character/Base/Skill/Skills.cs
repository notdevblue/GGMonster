using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillListEnum
{
    InsultCodeDesign = 0,
    MoneyHeal,
    PowerfulSholderMassage,
    Naruto,
    Tsundere,
    SEONHANEND,
    WaterAttack,
    DEFAULTEND
}

abstract public class Skills : SkillBase
{
    public delegate void SkillExample(ref int skillPoint);
    public Dictionary<SkillListEnum, SkillExample> seonHanSKills = new Dictionary<SkillListEnum, SkillExample>();
    public Dictionary<SkillListEnum, SkillExample> defaultSkills = new Dictionary<SkillListEnum, SkillExample>();

    private IDamageable damageable;

    protected void InitDictionary() // �Լ����� �̷��� �ѵ� ���⼭ IDamageable ã�Ƽ� �ʱ�ȭ �����.
    {
        InitSeonHanDictionary();
        InitDefaultDictionary();

        InitInterface();
    }

    void InitInterface()
    {
        damageable = stat.enemyStat.GetComponent<IDamageable>();

        #region null check
#if UNITY_EDITOR
        if(damageable == null)
        {
            Debug.LogError("Skills: Cannot find IDamgeable From enemy");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        #endregion
    }

    #region Dictionary Init

    private void InitSeonHanDictionary()
    {
        seonHanSKills.Add(SkillListEnum.InsultCodeDesign, InsultCodeDesign);
        seonHanSKills.Add(SkillListEnum.MoneyHeal, MoneyHeal);
        seonHanSKills.Add(SkillListEnum.PowerfulSholderMassage, PowerfulShoulderMassage);
        seonHanSKills.Add(SkillListEnum.Naruto, Naruto);
        seonHanSKills.Add(SkillListEnum.Tsundere, Tsundere);
    }

    private void InitDefaultDictionary()
    {
        defaultSkills.Add(SkillListEnum.WaterAttack, WaterAttack);
    }

    #endregion

    #region ���ѽ�

    //delegate void SeonHanAtkDelegate(int damage, ref int skillPoint);

    public void InsultCodeDesign(ref int skillPoint) // �ڵ� ���� ���ϱ�, ���ѽ� �� �ڵ�
    {
        int damage = 25;

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
                damageable.OnDamage((int)(damage * stat.dmgBoostAmt));
                break;

            case Stat.ClassType.TEACHER:
                damageable.OnDamage((int)(damage * stat.dmgDecAmt));
                break;

            default:
                damageable.OnDamage(damage);
                break;
        }

        
    }

    public void MoneyHeal(ref int skillPoint) // ����ġ�� // �� �����̷� ������ ������. ��밡 �������̸� ���ݷ��� 50% ��ŭ ���� �� ��
    {
        int damage = 17;

        --skillPoint;
        Debug.Log("����ġ��");
        if (!SkillSuccess())
        {
            Debug.Log("����ġ�� ����");
            return;
        }

        if (stat.enemyType == Stat.ClassType.TEACHER)
        {
            damageable.OnDamage(damage / 2, true);
            Debug.Log("�������� �ູ�� �󱼷� ���� �����̴�...");
            return;
        }
        damageable.OnDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage));
    }

    public void PowerfulShoulderMassage(ref int skillPoint) // ������ ��� �ȸ� // n�ۼ�Ʈ�� Ȯ���� ��� ++hp, ���ѽ� �� �ڵ�
    {
        int damage = 20;

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
            damageable.OnDamage((int)(damage * 0.2f), true);
            Debug.Log("��밡 �ȸ��� �����ϰ� �޾Ƶ�ȴ�...");
            return;
        }

        damageable.OnDamage((int)(stat.enemyType == Stat.ClassType.TEACHER ? damage * 0.8f : damage));
    }

    public void Naruto(ref int skillPoint) // ����ȯ, ���ѽ� �� �ڵ�
    {
        int damage = 15;

        --skillPoint;
        Debug.Log("����ȯ");
        if (!SkillSuccess())
        {
            Debug.Log("����");
            return;
        }

        damageable.OnDamage(damage);
    }

    public void Tsundere(ref int skillPoint) // �����Ÿ���, ���ѽ� �� �ڵ�, ���߱�
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

    // TODO : ���ӵ� ī��Ʈ �ʿ���
    public void WaterAttack(ref int skillPoint) // ������, ��ǻ�Ϳ� ���� �K�´�, ���ӵ�
    {
        int damage = 15;

        --skillPoint;
        Debug.Log("������");
        if(!SkillSuccess())
        {
            Debug.Log("����");
            return;
        }

        damageable.OnDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage));
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
        if (stat.provokeCount > 0) stat.provokeCount = 0;
        return false;
    }


    abstract protected void InitBtn();
}
