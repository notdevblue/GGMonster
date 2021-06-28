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

    AlgoHomework,
    AmongUs,
    UnfriedMandu,
    HwanJu,
    GudiAkgae,
    HAEUNEND,

    WaterAttack,
    DEFAULTEND
}

// init
abstract public partial class Skills : SkillBase
{
    private IDamageable  damageable;
    private SkillManager skillSprite = null;

    protected void InitDictionary() // �Լ����� �̷��� �ѵ� ���⼭ IDamageable ã�Ƽ� �ʱ�ȭ �����.
    {
        skillSprite = FindObjectOfType<SkillManager>();
        TurnManager.instance.turnEndTasks.Add(ResetSkillUsed);

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
            new SkillData("�ڵ� ���� ���ϱ�", InsultCodeDesign, skillSprite.skillSprite[SkillListEnum.InsultCodeDesign], new SkillInfo("������ �ڵ� ���踦 �����մϴ�", 25, Stat.ClassType.PROGRAMMER, Stat.ClassType.TEACHER)));

        skillDataDictionary.Add(SkillListEnum.MoneyHeal,
            new SkillData("����ġ��", MoneyHeal, skillSprite.skillSprite[SkillListEnum.MoneyHeal], new SkillInfo("���濡�� �������� ����ġ�Ḧ �ϴ� ô �ϸ鼭 ���ڸ� �����ϴ�..", 17, Stat.ClassType.NOTYPE, Stat.ClassType.TEACHER)));
        
        skillDataDictionary.Add(SkillListEnum.PowerfulSholderMassage, 
            new SkillData("������ ��� �ȸ�", PowerfulShoulderMassage, skillSprite.skillSprite[SkillListEnum.PowerfulSholderMassage], new SkillInfo("������ ����� �����ϰ� �ȸ��մϴ�.", 20, Stat.ClassType.NOTYPE, Stat.ClassType.TEACHER)));
        
        skillDataDictionary.Add(SkillListEnum.Naruto,                 
            new SkillData("����ȯ", Naruto, skillSprite.skillSprite[SkillListEnum.Naruto], new SkillInfo("���濡�� ������ �������� �߻��մϴ�.", 15)));
        
        skillDataDictionary.Add(SkillListEnum.Tsundere,               
            new SkillData("�����Ÿ���", Tsundere, skillSprite.skillSprite[SkillListEnum.Tsundere], new SkillInfo("���濡�� �����Ÿ��ϴ�.", 0)));
        
        skillDataDictionary.Add(SkillListEnum.ReTest,                 
            new SkillData("�����", ReTest, skillSprite.skillSprite[SkillListEnum.ReTest], new SkillInfo("������ ������ ���غ��� ���� ������� ġ���� �մϴ�.", 15, Stat.ClassType.PROGRAMMER, Stat.ClassType.TEACHER, true, 3)));
        
        skillDataDictionary.Add(SkillListEnum.WaterAttack,
            new SkillData("������", WaterAttack, skillSprite.skillSprite[SkillListEnum.WaterAttack], new SkillInfo("���濡 ��Ʈ�� Ű���忡 ���� �׽��ϴ�.", 15, Stat.ClassType.PROGRAMMER, Stat.ClassType.NOTYPE, true, 3)));

        skillDataDictionary.Add(SkillListEnum.AlgoHomework,
            new SkillData("�˰��� ���� ����", AlgorithmHomework, skillSprite.skillSprite[SkillListEnum.AlgoHomework], new SkillInfo("�˰��� ������ �� �ݴϴ�.\r\n������ ���� �ִ� �� ���� �������� �����ϴ�.", 12, Stat.ClassType.PROGRAMMER, Stat.ClassType.NOTYPE)));
        
        skillDataDictionary.Add(SkillListEnum.AmongUs,
            new SkillData("����", Amongus, skillSprite.skillSprite[SkillListEnum.AmongUs], new SkillInfo("�л���� ���� �÷��� ���� ���ɹ��� �̿��Ͽ� �������͸� ã�Ƴ��ϴ�.\r\n���� Ȯ���� 60%, ���� Ȯ���� 40%�� ��ų�Դϴ�.", 50)));
        
        skillDataDictionary.Add(SkillListEnum.UnfriedMandu,
            new SkillData("�� ���� ����", UnFriedMandu, skillSprite.skillSprite[SkillListEnum.UnfriedMandu], new SkillInfo("�� ���� ���θ� ��Ź�� �ø��ϴ�.", 20, Stat.ClassType.NOTYPE, Stat.ClassType.NOTYPE, true, 5)));
        
        skillDataDictionary.Add(SkillListEnum.HwanJu,
            new SkillData("ȯ��ٶ��", HwanJu, skillSprite.skillSprite[SkillListEnum.HwanJu], new SkillInfo("ȯ�� ��Ŭ���� �������ϴ�.\r\n���� ȯ�ֶ�� �� ���� �������� �����ϴ�.\r\n��ų�� �������� �ʽ��ϴ�.", 15, Stat.ClassType.HWANJU, Stat.ClassType.NOTYPE)));
        
        skillDataDictionary.Add(SkillListEnum.GudiAkgae,
            new SkillData("����ǰ�", GudiAkGae, skillSprite.skillSprite[SkillListEnum.GudiAkgae], new SkillInfo("���� �Ǽ� ���� ��Ŭ�� ȸ���� �˴ϴ�.\r\n���� ������ �� ���� �������� �����ϴ�.\r\n��ų�� �������� �ʽ��ϴ�.", 15, Stat.ClassType.GUDIGAN, Stat.ClassType.NOTYPE)));
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
        if (!SkillSuccess())
        {
            SkillFailedRoutine(SkillListEnum.InsultCodeDesign);
            return;
        }

        switch (stat.enemyType)
        {
            case Stat.ClassType.PROGRAMMER:
                damage = (int)(damage * stat.dmgBoostAmt);
                break;

            case Stat.ClassType.TEACHER:
                damage = (int)(damage * stat.dmgDecAmt);
                break;

            default:
                break;
        }

        damageable.OnDamage(damage, false, false, 0, (int)SkillListEnum.InsultCodeDesign);
    }


    public void PowerfulShoulderMassage(ref int skillPoint) // ������ ��� �ȸ� // n�ۼ�Ʈ�� Ȯ���� ��� ++hp, ���ѽ� �� �ڵ�
    {
        int damage = skillDataDictionary[SkillListEnum.PowerfulSholderMassage].info.damage;

        --skillPoint;
        if (!SkillSuccess())
        {
            SkillFailedRoutine(SkillListEnum.PowerfulSholderMassage);
            return;
        }

        // �
        int rand = Random.Range(1, 100);
        if(rand > 95)
        {
            damageable.OnDamage((int)(damage * 0.2f), true, false, 0, (int)SkillListEnum.PowerfulSholderMassage);
            return;
        }

        damageable.OnDamage((int)(stat.enemyType == Stat.ClassType.TEACHER ? damage * 0.8f : damage),false, false, 0, (int)SkillListEnum.PowerfulSholderMassage);
    }

    public void Naruto(ref int skillPoint) // ����ȯ, ���ѽ� �� �ڵ�
    {
        int damage = skillDataDictionary[SkillListEnum.Naruto].info.damage;

        --skillPoint;
        if (!SkillSuccess())
        {
            SkillFailedRoutine(SkillListEnum.Naruto);
            return;
        }

        damageable.OnDamage(damage, false, false, 0, (int)SkillListEnum.Naruto);
    }

    public void Tsundere(ref int skillPoint) // �����Ÿ���, ���ѽ� �� �ڵ�, ���߱�
    {
        --skillPoint;
        if(!SkillSuccess())
        {
            SkillFailedRoutine(SkillListEnum.Tsundere);
            return;
        }

        stat.enemyStat.provoke = true;
        ++stat.enemyStat.provokeCount;

        damageable.OnDamage(0, false, false, 0, (int)SkillListEnum.Tsundere);
    }

    public void ReTest(ref int skillPoint)
    {
        int damageCount = skillDataDictionary[SkillListEnum.ReTest].info.continuesCount;
        int damage = skillDataDictionary[SkillListEnum.ReTest].info.damage / damageCount;

        --skillPoint;
        if (!SkillSuccess())
        {
            SkillFailedRoutine(SkillListEnum.ReTest);
            return;
        }

        damageable.OnDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage), false, true, damageCount, (int)SkillListEnum.ReTest);

    }

    #endregion
}

// ������ ��ų
abstract public partial class Skills : SkillBase
{
    #region ������

    private void AlgorithmHomework(ref int skillPoint)
    {
        const float insta       = 99.99f; 
        const float treeProv    = 95.0f;
        const float stackProv   = 70.0f;
        const float listProv    = 60.0f;
        const float arrayProv   = 50.0f;
              float damageBoost;

        int damage = skillDataDictionary[SkillListEnum.AlgoHomework].info.damage;
        int damageCount = skillDataDictionary[SkillListEnum.AlgoHomework].info.continuesCount;

        --skillPoint;
        if (!SkillSuccess())
        {
            SkillFailedRoutine(SkillListEnum.AlgoHomework);
            return;
        }


        // ������
        float rnd = Random.Range(0.0f, 100.0f);
        if (rnd > insta)
        {
            NoticeUI.instance.SetMsg("����Ʈ�� 2�� ����! ������ 10��!");
            damageBoost = 10.0f;
        }
        else if (rnd > treeProv)
        {
            NoticeUI.instance.SetMsg("Ʈ�� ���� ����! ������ 2��!");
            damageBoost = 2.0f;
        }
        else if(rnd > stackProv)
        {
            NoticeUI.instance.SetMsg("���� ���� ����! ������ 1.5��!");
            damageBoost = 1.5f;
        }
        else if(rnd > listProv)
        {
            NoticeUI.instance.SetMsg("����Ʈ ���� ����! ������ 1.2��!");
            damageBoost = 1.2f;
        }
        else if(rnd > arrayProv)
        {
            NoticeUI.instance.SetMsg("�迭 ���� ����! ������ 1.1��!");
            damageBoost = 1.1f;
        }
        else
        {
            NoticeUI.instance.SetMsg("�ݺ��� ���� ����!");
            damageBoost = 1.0f;
        }

        if (stat.enemyType == Stat.ClassType.PROGRAMMER)
        {
            damageBoost += 0.1f;
        }

        damageable.OnDamage((int)(damage * damageBoost), false, false, damageCount, (int)SkillListEnum.AlgoHomework);
    }

    private void Amongus(ref int skillPoint)
    {
        int damage = skillDataDictionary[SkillListEnum.AmongUs].info.damage;

        --skillPoint;

        if(Random.Range(0, 100) < 60)
        {
            SkillFailedRoutine(SkillListEnum.AmongUs);
            return;
        }

        damageable.OnDamage(damage, false, false, 0, (int)SkillListEnum.AmongUs);
    }

    private void UnFriedMandu(ref int skillPoint)
    {
        int damageCount = skillDataDictionary[SkillListEnum.UnfriedMandu].info.continuesCount;
        int damage = skillDataDictionary[SkillListEnum.UnfriedMandu].info.damage / damageCount;

        --skillPoint;

        damageable.OnDamage(damage, false, true, damageCount, (int)SkillListEnum.UnfriedMandu);
    }

    private void HwanJu(ref int skillPoint)
    {
        int damage = skillDataDictionary[SkillListEnum.HwanJu].info.damage;

        --skillPoint;

        damageable.OnDamage(stat.enemyType == Stat.ClassType.HWANJU ? damage * 2 : damage, false, false, 0, (int)SkillListEnum.HwanJu);
    }

    private void GudiAkGae(ref int skillPoint)
    {
        int damage = skillDataDictionary[SkillListEnum.GudiAkgae].info.damage;

        --skillPoint;

        damageable.OnDamage(stat.enemyType == Stat.ClassType.GUDIGAN ? damage * 2 : damage, false, false, 0, (int)SkillListEnum.GudiAkgae);
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
        if(!SkillSuccess())
        {
            SkillFailedRoutine(SkillListEnum.WaterAttack);
            return;
        }

        damageable.OnDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage), false, true, damageCount, (int)SkillListEnum.WaterAttack);
    }

    public void MoneyHeal(ref int skillPoint) // ����ġ�� // �� �����̷� ������ ������. ��밡 �������̸� ���ݷ��� 50% ��ŭ ���� �� ��
    {
        int damage = skillDataDictionary[SkillListEnum.MoneyHeal].info.damage;

        --skillPoint;
        if (!SkillSuccess())
        {
            SkillFailedRoutine(SkillListEnum.MoneyHeal);
            return;
        }
            
        if (stat.enemyType == Stat.ClassType.TEACHER)
        {
            damageable.OnDamage(damage / 2, true);
            Debug.Log("�������� �ູ�� �󱼷� ���� �����̴�...");
            NoticeUI.instance.SetMsg("�������� �ູ�� �󱼷� ���� �����̴�...");
            return;
        }
        damageable.OnDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage), false, false, 0, (int)SkillListEnum.MoneyHeal);
    }

    #endregion
}

// ��Ÿ �Լ���
abstract public partial class Skills : SkillBase
{
    private void SkillFailedRoutine(SkillListEnum skill)
    {
        NoticeUI.instance.SetMsg("�ƾ� �����ߴ�...");
        NoticeUI.instance.CallNoticeUI(true, true, TurnManager.instance.enemyTurn, false, false, skillSprite.skillSprite[skill]);
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
                    return true;
                }
                break;

            case false:
                if (rand > stat.missRate)
                {
                    return true;
                }
                break;
        }
        if (stat.provokeCount > 0) stat.provokeCount = 0;
        return false;
    }

    public override bool Skill(int n)
    {
        if (!base.Skill(n))
        {
            return false;
        }

        
        if (stat.sp_arr[n] > 0)
        {
            NoticeUI.instance.SetMsg($"{stat.charactorName}�� {skillDataDictionary[selectedSkills[n]].name}!");
            skillDataDictionary[selectedSkills[n]].skill(ref stat.sp_arr[n]);
        }
        else
        {
            NoticeUI.instance.SetMsg($"���̻� {skillDataDictionary[selectedSkills[n]].name} �� ����� �� ����!", () => skillUsed = false);
            NoticeUI.instance.CallNoticeUI(false, true, false, true, false, null);
        }

        return true;
    }

    abstract protected void InitBtn();
}
