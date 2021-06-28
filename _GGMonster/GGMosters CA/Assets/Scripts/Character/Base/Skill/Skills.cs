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

    protected void InitDictionary() // 함수명은 이렇긴 한데 여기서 IDamageable 찾아서 초기화 해줘요.
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
            new SkillData("코드 설계 욕하기", InsultCodeDesign, skillSprite.skillSprite[SkillListEnum.InsultCodeDesign], new SkillInfo("상대방의 코드 설계를 비판합니다", 25, Stat.ClassType.PROGRAMMER, Stat.ClassType.TEACHER)));

        skillDataDictionary.Add(SkillListEnum.MoneyHeal,
            new SkillData("금융치료", MoneyHeal, skillSprite.skillSprite[SkillListEnum.MoneyHeal], new SkillInfo("상대방에게 무상으로 금융치료를 하는 척 하면서 이자를 뜯어냅니다..", 17, Stat.ClassType.NOTYPE, Stat.ClassType.TEACHER)));
        
        skillDataDictionary.Add(SkillListEnum.PowerfulSholderMassage, 
            new SkillData("강력한 어깨 안마", PowerfulShoulderMassage, skillSprite.skillSprite[SkillListEnum.PowerfulSholderMassage], new SkillInfo("상대방의 어깨를 강력하게 안마합니다.", 20, Stat.ClassType.NOTYPE, Stat.ClassType.TEACHER)));
        
        skillDataDictionary.Add(SkillListEnum.Naruto,                 
            new SkillData("나선환", Naruto, skillSprite.skillSprite[SkillListEnum.Naruto], new SkillInfo("상대방에게 강력한 나선한을 발사합니다.", 15)));
        
        skillDataDictionary.Add(SkillListEnum.Tsundere,               
            new SkillData("츤츤거리기", Tsundere, skillSprite.skillSprite[SkillListEnum.Tsundere], new SkillInfo("상대방에게 츤츤거립니다.", 0)));
        
        skillDataDictionary.Add(SkillListEnum.ReTest,                 
            new SkillData("재시험", ReTest, skillSprite.skillSprite[SkillListEnum.ReTest], new SkillInfo("상대방의 점수가 기준보다 낮아 재시험을 치르게 합니다.", 15, Stat.ClassType.PROGRAMMER, Stat.ClassType.TEACHER, true, 3)));
        
        skillDataDictionary.Add(SkillListEnum.WaterAttack,
            new SkillData("물승핵", WaterAttack, skillSprite.skillSprite[SkillListEnum.WaterAttack], new SkillInfo("상대방에 노트북 키보드에 물을 붓습니다.", 15, Stat.ClassType.PROGRAMMER, Stat.ClassType.NOTYPE, true, 3)));

        skillDataDictionary.Add(SkillListEnum.AlgoHomework,
            new SkillData("알고리즘 과제 출제", AlgorithmHomework, skillSprite.skillSprite[SkillListEnum.AlgoHomework], new SkillInfo("알고리즘 과제를 내 줍니다.\r\n과제에 따라서 최대 두 배의 데미지를 입힙니다.", 12, Stat.ClassType.PROGRAMMER, Stat.ClassType.NOTYPE)));
        
        skillDataDictionary.Add(SkillListEnum.AmongUs,
            new SkillData("어몽어스", Amongus, skillSprite.skillSprite[SkillListEnum.AmongUs], new SkillInfo("학생들과 어몽어스 플레이 도중 관심법을 이용하여 임포스터를 찾아냅니다.\r\n실패 확률이 60%, 성공 확률이 40%인 스킬입니다.", 50)));
        
        skillDataDictionary.Add(SkillListEnum.UnfriedMandu,
            new SkillData("덜 익은 만두", UnFriedMandu, skillSprite.skillSprite[SkillListEnum.UnfriedMandu], new SkillInfo("덜 익은 만두를 식탁에 올립니다.", 20, Stat.ClassType.NOTYPE, Stat.ClassType.NOTYPE, true, 5)));
        
        skillDataDictionary.Add(SkillListEnum.HwanJu,
            new SkillData("환쥬바라기", HwanJu, skillSprite.skillSprite[SkillListEnum.HwanJu], new SkillInfo("환주 팬클럽을 설립힙니다.\r\n적이 환주라면 두 배의 데미지를 입힙니다.\r\n스킬이 실패하지 않습니다.", 15, Stat.ClassType.HWANJU, Stat.ClassType.NOTYPE)));
        
        skillDataDictionary.Add(SkillListEnum.GudiAkgae,
            new SkillData("구디악개", GudiAkGae, skillSprite.skillSprite[SkillListEnum.GudiAkgae], new SkillInfo("구디 악성 개인 팬클럽 회장이 됩니다.\r\n적이 구디라면 두 배의 데미지를 입힙니다.\r\n스킬이 실패하지 않습니다.", 15, Stat.ClassType.GUDIGAN, Stat.ClassType.NOTYPE)));
    }

    #endregion
}

// 선한쌤 스킬
abstract public partial class Skills : SkillBase
{
    #region 선한쌤

    public void InsultCodeDesign(ref int skillPoint) // 코드 설계 욕하기, 선한쌤 용 코드
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


    public void PowerfulShoulderMassage(ref int skillPoint) // 강력한 어깨 안마 // n퍼센트의 확률로 상대 ++hp, 선한쌤 용 코드
    {
        int damage = skillDataDictionary[SkillListEnum.PowerfulSholderMassage].info.damage;

        --skillPoint;
        if (!SkillSuccess())
        {
            SkillFailedRoutine(SkillListEnum.PowerfulSholderMassage);
            return;
        }

        // 운빨
        int rand = Random.Range(1, 100);
        if(rand > 95)
        {
            damageable.OnDamage((int)(damage * 0.2f), true, false, 0, (int)SkillListEnum.PowerfulSholderMassage);
            return;
        }

        damageable.OnDamage((int)(stat.enemyType == Stat.ClassType.TEACHER ? damage * 0.8f : damage),false, false, 0, (int)SkillListEnum.PowerfulSholderMassage);
    }

    public void Naruto(ref int skillPoint) // 나선환, 선한쌤 용 코드
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

    public void Tsundere(ref int skillPoint) // 츤츤거리기, 선한쌤 용 코드, 도발기
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

// 하은쌤 스킬
abstract public partial class Skills : SkillBase
{
    #region 하은쌤

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


        // 과제들
        float rnd = Random.Range(0.0f, 100.0f);
        if (rnd > insta)
        {
            NoticeUI.instance.SetMsg("이진트리 2번 출제! 데미지 10배!");
            damageBoost = 10.0f;
        }
        else if (rnd > treeProv)
        {
            NoticeUI.instance.SetMsg("트리 과제 출제! 데미지 2배!");
            damageBoost = 2.0f;
        }
        else if(rnd > stackProv)
        {
            NoticeUI.instance.SetMsg("스택 과제 출제! 데미지 1.5배!");
            damageBoost = 1.5f;
        }
        else if(rnd > listProv)
        {
            NoticeUI.instance.SetMsg("리스트 과제 출제! 데미지 1.2배!");
            damageBoost = 1.2f;
        }
        else if(rnd > arrayProv)
        {
            NoticeUI.instance.SetMsg("배열 과제 출제! 데미지 1.1배!");
            damageBoost = 1.1f;
        }
        else
        {
            NoticeUI.instance.SetMsg("반복문 과제 출제!");
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

// 공용 스킬
abstract public partial class Skills : SkillBase
{
    #region 공용 스킬

    public void WaterAttack(ref int skillPoint) // 물승핵, 컴퓨터에 물을 쏫는다, 지속딜
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

    public void MoneyHeal(ref int skillPoint) // 금융치료 // 돈 뭉텅이로 던저서 딜입힘. 상대가 선생님이면 공격력의 50% 만큼 힐을 해 줌
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
            Debug.Log("선생님이 행복한 얼굴로 돈을 받으셨다...");
            NoticeUI.instance.SetMsg("선생님이 행복한 얼굴로 돈을 받으셨다...");
            return;
        }
        damageable.OnDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage), false, false, 0, (int)SkillListEnum.MoneyHeal);
    }

    #endregion
}

// 기타 함수들
abstract public partial class Skills : SkillBase
{
    private void SkillFailedRoutine(SkillListEnum skill)
    {
        NoticeUI.instance.SetMsg("아앗 실패했다...");
        NoticeUI.instance.CallNoticeUI(true, true, TurnManager.instance.enemyTurn, false, false, skillSprite.skillSprite[skill]);
    }

    // 미스 여부
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
            NoticeUI.instance.SetMsg($"{stat.charactorName}의 {skillDataDictionary[selectedSkills[n]].name}!");
            skillDataDictionary[selectedSkills[n]].skill(ref stat.sp_arr[n]);
        }
        else
        {
            NoticeUI.instance.SetMsg($"더이상 {skillDataDictionary[selectedSkills[n]].name} 을 사용할 수 없다!", () => skillUsed = false);
            NoticeUI.instance.CallNoticeUI(false, true, false, true, false, null);
        }

        return true;
    }

    abstract protected void InitBtn();
}
