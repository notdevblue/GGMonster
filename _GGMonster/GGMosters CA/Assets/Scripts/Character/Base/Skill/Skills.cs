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
    ReTest,
    SEONHANEND,
    WaterAttack,
    DEFAULTEND
}



abstract public class Skills : SkillBase
{
    private IDamageable damageable;
 


    protected void InitDictionary() // �Լ����� �̷��� �ѵ� ���⼭ IDamageable ã�Ƽ� �ʱ�ȭ �����.
    {
        InitSkillDictionary();
        InitInterface();
    }

    void InitInterface()
    {
        damageable = stat.enemyStat.gameObject.GetComponent<IDamageable>();

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

    private void InitSkillDictionary()
    {
        Debug.Log(this.name);
        skillDataDictionary.Add(SkillListEnum.InsultCodeDesign,       
            new SkillData("�ڵ� ���� ���ϱ�", InsultCodeDesign, new SkillInfo("������ �ڵ� ���踦 �����մϴ�", 25, Stat.ClassType.PROGRAMMER, Stat.ClassType.TEACHER)));

        skillDataDictionary.Add(SkillListEnum.MoneyHeal,
            new SkillData("����ġ��", MoneyHeal, new SkillInfo("���濡�� �������� ����ġ�Ḧ �ϴ� ô �ϸ鼭 ���ڸ� �����ϴ�..", 17, Stat.ClassType.NOTYPE, Stat.ClassType.TEACHER)));
        
        skillDataDictionary.Add(SkillListEnum.PowerfulSholderMassage, 
            new SkillData("������ ��� �ȸ�", PowerfulShoulderMassage, new SkillInfo("������ ����� �����ϰ� �ȸ��մϴ�.", 20, Stat.ClassType.NOTYPE, Stat.ClassType.TEACHER)));
        
        skillDataDictionary.Add(SkillListEnum.Naruto,                 
            new SkillData("����ȯ", Naruto, new SkillInfo("���濡�� ������ ����ȯ�� �߻��մϴ�.", 15)));
        
        skillDataDictionary.Add(SkillListEnum.Tsundere,               
            new SkillData("�����Ÿ���", Tsundere, new SkillInfo("���濡�� �����Ÿ��ϴ�.", 0)));
        
        skillDataDictionary.Add(SkillListEnum.ReTest,                 
            new SkillData("�����", ReTest, new SkillInfo("������ ������ ���غ��� ���� ������� ġ���� �մϴ�.", 15, Stat.ClassType.PROGRAMMER, Stat.ClassType.TEACHER, true, 3)));
        
        skillDataDictionary.Add(SkillListEnum.WaterAttack,            
            new SkillData("������", WaterAttack, new SkillInfo("���濡 ��Ʈ�� Ű���忡 ���� �׽��ϴ�.", 15, Stat.ClassType.PROGRAMMER, Stat.ClassType.NOTYPE, true, 3)));
    }

    #endregion

    #region ���ѽ�

    public void InsultCodeDesign(ref int skillPoint) // �ڵ� ���� ���ϱ�, ���ѽ� �� �ڵ�
    {
        int damage = skillDataDictionary[SkillListEnum.InsultCodeDesign].info.damage;

        --skillPoint;
        Debug.Log("�ڵ� ���� ���ϱ�");
        if (!SkillSuccess())
        {
            SkillFailedRoutine();
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


    public void PowerfulShoulderMassage(ref int skillPoint) // ������ ��� �ȸ� // n�ۼ�Ʈ�� Ȯ���� ��� ++hp, ���ѽ� �� �ڵ�
    {
        int damage = skillDataDictionary[SkillListEnum.PowerfulSholderMassage].info.damage;

        --skillPoint;
        Debug.Log("������ ��� �ȸ�");
        if (!SkillSuccess())
        {
            SkillFailedRoutine();
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
        int damage = skillDataDictionary[SkillListEnum.Naruto].info.damage;

        --skillPoint;
        Debug.Log("����ȯ");
        if (!SkillSuccess())
        {
            SkillFailedRoutine();
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
            SkillFailedRoutine();
            return;
        }

        stat.enemyStat.provoke = true;
        ++stat.enemyStat.provokeCount;
    }

    public void ReTest(ref int skillPoint)
    {
        int damageCount = skillDataDictionary[SkillListEnum.ReTest].info.continuesCount;
        int damage = skillDataDictionary[SkillListEnum.ReTest].info.damage / damageCount;

        --skillPoint;
        Debug.Log("�����");
        if (!SkillSuccess())
        {
            SkillFailedRoutine();
            return;
        }

        stat.enemyStat.SetTickDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage), damageCount);

    }

    #endregion

    #region ���� ��ų

    public void WaterAttack(ref int skillPoint) // ������, ��ǻ�Ϳ� ���� �K�´�, ���ӵ�
    {
        int damageCount = skillDataDictionary[SkillListEnum.WaterAttack].info.continuesCount;
        int damage = skillDataDictionary[SkillListEnum.WaterAttack].info.damage / damageCount;

        --skillPoint;
        Debug.Log("������");
        if(!SkillSuccess())
        {
            SkillFailedRoutine();
            return;
        }
        stat.enemyStat.SetTickDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage), damageCount);
    }

    public void MoneyHeal(ref int skillPoint) // ����ġ�� // �� �����̷� ������ ������. ��밡 �������̸� ���ݷ��� 50% ��ŭ ���� �� ��
    {
        int damage = skillDataDictionary[SkillListEnum.MoneyHeal].info.damage;

        --skillPoint;
        Debug.Log("����ġ��");
        if (!SkillSuccess())
        {
            SkillFailedRoutine();
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

    #endregion

    #region ������

    private void Foo(ref int skillPoint)
    {

    }

    #endregion

    private void SkillFailedRoutine()
    {
        Debug.Log("��ų ���� ����");
        TurnManager.instance.EndTurn();
    }

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
