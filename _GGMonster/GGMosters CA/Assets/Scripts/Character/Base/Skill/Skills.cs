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
    ALGOHOMEWORK,
    AMONGUS,
    UNFRIEDMANDU,
    HWANJU,
    GUDIAKGAE,
    HAEUNEND,
    WaterAttack,
    DEFAULTEND
}

// init
abstract public partial class Skills : SkillBase
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
        skillDataDictionary.Add(SkillListEnum.InsultCodeDesign,       
            new SkillData("�ڵ� ���� ���ϱ�", InsultCodeDesign, new SkillInfo("������ �ڵ� ���踦 �����մϴ�", 25, Stat.ClassType.PROGRAMMER, Stat.ClassType.TEACHER)));

        skillDataDictionary.Add(SkillListEnum.MoneyHeal,
            new SkillData("����ġ��", MoneyHeal, new SkillInfo("���濡�� �������� ����ġ�Ḧ �ϴ� ô �ϸ鼭 ���ڸ� �����ϴ�..", 17, Stat.ClassType.NOTYPE, Stat.ClassType.TEACHER)));
        
        skillDataDictionary.Add(SkillListEnum.PowerfulSholderMassage, 
            new SkillData("������ ��� �ȸ�", PowerfulShoulderMassage, new SkillInfo("������ ����� �����ϰ� �ȸ��մϴ�.", 20, Stat.ClassType.NOTYPE, Stat.ClassType.TEACHER)));
        
        skillDataDictionary.Add(SkillListEnum.Naruto,                 
            new SkillData("����ȯ", Naruto, new SkillInfo("���濡�� ������ �������� �߻��մϴ�.", 15)));
        
        skillDataDictionary.Add(SkillListEnum.Tsundere,               
            new SkillData("�����Ÿ���", Tsundere, new SkillInfo("���濡�� �����Ÿ��ϴ�.", 0)));
        
        skillDataDictionary.Add(SkillListEnum.ReTest,                 
            new SkillData("�����", ReTest, new SkillInfo("������ ������ ���غ��� ���� ������� ġ���� �մϴ�.", 15, Stat.ClassType.PROGRAMMER, Stat.ClassType.TEACHER, true, 3)));
        
        skillDataDictionary.Add(SkillListEnum.WaterAttack,            
            new SkillData("������", WaterAttack, new SkillInfo("���濡 ��Ʈ�� Ű���忡 ���� �׽��ϴ�.", 15, Stat.ClassType.PROGRAMMER, Stat.ClassType.NOTYPE, true, 3)));

        skillDataDictionary.Add(SkillListEnum.ALGOHOMEWORK,
            new SkillData("�˰��� ���� ����", AlgorithmHomework, new SkillInfo("�˰��� ������ �� �ݴϴ�.\r\n������ ���� �ִ� �� ���� �������� �����ϴ�.", 12, Stat.ClassType.PROGRAMMER, Stat.ClassType.NOTYPE)));
        
        skillDataDictionary.Add(SkillListEnum.AMONGUS,
            new SkillData("����", Amongus, new SkillInfo("�л���� ���� �÷��� ���� ���ɹ��� �̿��Ͽ� �������͸� ã�Ƴ��ϴ�.\r\n���� Ȯ���� 60%, ���� Ȯ���� 40%�� ��ų�Դϴ�.", 50)));
        
        skillDataDictionary.Add(SkillListEnum.UNFRIEDMANDU,
            new SkillData("�� ���� ����", UnFriedMandu, new SkillInfo("�� ���� ���θ� ��Ź�� �ø��ϴ�.", 10)));
        
        skillDataDictionary.Add(SkillListEnum.HWANJU,
            new SkillData("ȯ��ٶ��", HwanJu, new SkillInfo("ȯ�� ��Ŭ���� �������ϴ�.\r\n���� ȯ�ֶ�� �� ���� �������� �����ϴ�.\r\n��ų�� �������� �ʽ��ϴ�.", 10, Stat.ClassType.HWANJU, Stat.ClassType.NOTYPE)));
        
        skillDataDictionary.Add(SkillListEnum.GUDIAKGAE,
            new SkillData("����ǰ�", GudiAkGae, new SkillInfo("���� �Ǽ� ���� ��Ŭ�� ȸ���� �˴ϴ�.\r\n���� ������ �� ���� �������� �����ϴ�.\r\n��ų�� �������� �ʽ��ϴ�.", 10, Stat.ClassType.GUDIGAN, Stat.ClassType.NOTYPE)));
    }

    #endregion
}

// ���ѽ� ��ų
abstract public partial class Skills : SkillBase
{
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

        damageable.OnDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage), false, true, damageCount);

    }

    #endregion
}

// ������ ��ų
abstract public partial class Skills : SkillBase
{
    #region ������

    private void AlgorithmHomework(ref int skillPoint)
    {
        const int   treeProv    = 95;
        const int   stackProv   = 70;
        const int   listProv    = 60;
        const int   arrayProv   = 50;
              float damageBoost;

        int damage = skillDataDictionary[SkillListEnum.ALGOHOMEWORK].info.damage;
        int damageCount = skillDataDictionary[SkillListEnum.ALGOHOMEWORK].info.continuesCount;

        --skillPoint;
        Debug.Log("�˰��� ���� ����");
        if (!SkillSuccess())
        {
            SkillFailedRoutine();
            return;
        }


        // ������
        int rnd = Random.Range(0, 100);
        if (rnd > treeProv)
        {
            Debug.Log("Ʈ�� ����");
            NoticeUI.instance.SetMsg("Ʈ�� ���� ����!");
            damageBoost = 2.0f;
        }
        else if(rnd > stackProv)
        {
            Debug.Log("���� ����");
            NoticeUI.instance.SetMsg("���� ���� ����!");
            damageBoost = 1.5f;
        }
        else if(rnd > listProv)
        {
            Debug.Log("����Ʈ ����");
            NoticeUI.instance.SetMsg("����Ʈ ���� ����!");
            damageBoost = 1.2f;
        }
        else if(rnd > arrayProv)
        {
            Debug.Log("�迭 ����");
            NoticeUI.instance.SetMsg("�迭 ���� ����!");
            damageBoost = 1.1f;
        }
        else
        {
            Debug.Log("�ݺ��� ����");
            NoticeUI.instance.SetMsg("�ݺ��� ���� ����!");
            damageBoost = 1.0f;
        }

        if (stat.enemyType == Stat.ClassType.PROGRAMMER)
        {
            damageBoost += 0.1f;
        }

        damageable.OnDamage((int)(damage * damageBoost), false, true, damageCount);
    }

    private void Amongus(ref int skillPoint)
    {
        int damage = skillDataDictionary[SkillListEnum.AMONGUS].info.damage;

        --skillPoint;
        Debug.Log("�������� ���ɹ����� ã��");

        if(Random.Range(0, 100) < 60)
        {
            SkillFailedRoutine();
            return;
        }

        damageable.OnDamage(damage);
    }

    private void UnFriedMandu(ref int skillPoint)
    {
        int damage = skillDataDictionary[SkillListEnum.UNFRIEDMANDU].info.damage;

        --skillPoint;
        Debug.Log("�� ���� ���� ����");

        damageable.OnDamage(damage);
    }

    private void HwanJu(ref int skillPoint)
    {
        int damage = skillDataDictionary[SkillListEnum.HWANJU].info.damage;

        --skillPoint;
        Debug.Log("ȯ��ٶ��");

        damageable.OnDamage(stat.enemyType == Stat.ClassType.HWANJU ? damage * 2 : damage);
    }

    private void GudiAkGae(ref int skillPoint)
    {
        int damage = skillDataDictionary[SkillListEnum.GUDIAKGAE].info.damage;

        --skillPoint;
        Debug.Log("����ǰ�");

        damageable.OnDamage(stat.enemyType == Stat.ClassType.GUDIGAN ? damage * 2 : damage);
    }

    #endregion
}

// ���� ��ų
abstract public partial class Skills : SkillBase
{
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

        damageable.OnDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage), false, true, damageCount);
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
            NoticeUI.instance.SetMsg("�������� �ູ�� �󱼷� ���� �����̴�...");
            return;
        }
        damageable.OnDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage));
    }

    #endregion
}

// ��Ÿ �Լ���
abstract public partial class Skills : SkillBase
{
    private void SkillFailedRoutine()
    {
        NoticeUI.instance.SetMsg("�ƾ� �����ߴ�...");
        NoticeUI.instance.CallNoticeUI(true, true);
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

    public override void Skill(int n)
    {
        if (stat.sp_arr[n] > 0)
        {
            NoticeUI.instance.SetMsg($"{stat.charactorName}�� {skillDataDictionary[selectedSkills[n]].name}!");
            skillDataDictionary[selectedSkills[n]].skill(ref stat.sp_arr[n]);
        }
    }

    abstract protected void InitBtn();
}
