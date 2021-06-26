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

    protected void InitDictionary() // 함수명은 이렇긴 한데 여기서 IDamageable 찾아서 초기화 해줘요.
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
            new SkillData("코드 설계 욕하기", InsultCodeDesign, new SkillInfo("상대방의 코드 설계를 비판합니다", 25, Stat.ClassType.PROGRAMMER, Stat.ClassType.TEACHER)));

        skillDataDictionary.Add(SkillListEnum.MoneyHeal,
            new SkillData("금융치료", MoneyHeal, new SkillInfo("상대방에게 무상으로 금융치료를 하는 척 하면서 이자를 뜯어냅니다..", 17, Stat.ClassType.NOTYPE, Stat.ClassType.TEACHER)));
        
        skillDataDictionary.Add(SkillListEnum.PowerfulSholderMassage, 
            new SkillData("강력한 어깨 안마", PowerfulShoulderMassage, new SkillInfo("상대방의 어깨를 강력하게 안마합니다.", 20, Stat.ClassType.NOTYPE, Stat.ClassType.TEACHER)));
        
        skillDataDictionary.Add(SkillListEnum.Naruto,                 
            new SkillData("나선환", Naruto, new SkillInfo("상대방에게 강력한 나선한을 발사합니다.", 15)));
        
        skillDataDictionary.Add(SkillListEnum.Tsundere,               
            new SkillData("츤츤거리기", Tsundere, new SkillInfo("상대방에게 츤츤거립니다.", 0)));
        
        skillDataDictionary.Add(SkillListEnum.ReTest,                 
            new SkillData("재시험", ReTest, new SkillInfo("상대방의 점수가 기준보다 낮아 재시험을 치르게 합니다.", 15, Stat.ClassType.PROGRAMMER, Stat.ClassType.TEACHER, true, 3)));
        
        skillDataDictionary.Add(SkillListEnum.WaterAttack,            
            new SkillData("물승핵", WaterAttack, new SkillInfo("상대방에 노트북 키보드에 물을 붓습니다.", 15, Stat.ClassType.PROGRAMMER, Stat.ClassType.NOTYPE, true, 3)));

        skillDataDictionary.Add(SkillListEnum.ALGOHOMEWORK,
            new SkillData("알고리즘 과제 출제", AlgorithmHomework, new SkillInfo("알고리즘 과제를 내 줍니다.\r\n과제에 따라서 최대 두 배의 데미지를 입힙니다.", 12, Stat.ClassType.PROGRAMMER, Stat.ClassType.NOTYPE)));
        
        skillDataDictionary.Add(SkillListEnum.AMONGUS,
            new SkillData("어몽어스", Amongus, new SkillInfo("학생들과 어몽어스 플레이 도중 관심법을 이용하여 임포스터를 찾아냅니다.\r\n실패 확률이 60%, 성공 확률이 40%인 스킬입니다.", 50)));
        
        skillDataDictionary.Add(SkillListEnum.UNFRIEDMANDU,
            new SkillData("덜 익은 만두", UnFriedMandu, new SkillInfo("덜 익은 만두를 식탁에 올립니다.", 10)));
        
        skillDataDictionary.Add(SkillListEnum.HWANJU,
            new SkillData("환쥬바라기", HwanJu, new SkillInfo("환주 팬클럽을 설립힙니다.\r\n적이 환주라면 두 배의 데미지를 입힙니다.\r\n스킬이 실패하지 않습니다.", 10, Stat.ClassType.HWANJU, Stat.ClassType.NOTYPE)));
        
        skillDataDictionary.Add(SkillListEnum.GUDIAKGAE,
            new SkillData("구디악개", GudiAkGae, new SkillInfo("구디 악성 개인 팬클럽 회장이 됩니다.\r\n적이 구디라면 두 배의 데미지를 입힙니다.\r\n스킬이 실패하지 않습니다.", 10, Stat.ClassType.GUDIGAN, Stat.ClassType.NOTYPE)));
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
        Debug.Log("코드 설계 욕하기");
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


    public void PowerfulShoulderMassage(ref int skillPoint) // 강력한 어깨 안마 // n퍼센트의 확률로 상대 ++hp, 선한쌤 용 코드
    {
        int damage = skillDataDictionary[SkillListEnum.PowerfulSholderMassage].info.damage;

        --skillPoint;
        Debug.Log("강력한 어깨 안마");
        if (!SkillSuccess())
        {
            SkillFailedRoutine();
            return;
        }

        // 운빨
        int rand = Random.Range(1, 100);
        if(rand > 95)
        {
            damageable.OnDamage((int)(damage * 0.2f), true);
            Debug.Log("상대가 안마를 편한하게 받아드렸다...");
            return;
        }

        damageable.OnDamage((int)(stat.enemyType == Stat.ClassType.TEACHER ? damage * 0.8f : damage));
    }

    public void Naruto(ref int skillPoint) // 나선환, 선한쌤 용 코드
    {
        int damage = skillDataDictionary[SkillListEnum.Naruto].info.damage;

        --skillPoint;
        Debug.Log("나선환");
        if (!SkillSuccess())
        {
            SkillFailedRoutine();
            return;
        }

        damageable.OnDamage(damage);
    }

    public void Tsundere(ref int skillPoint) // 츤츤거리기, 선한쌤 용 코드, 도발기
    {
        --skillPoint;
        Debug.Log("츤츤거리기");
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
        Debug.Log("재시험");
        if (!SkillSuccess())
        {
            SkillFailedRoutine();
            return;
        }

        damageable.OnDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage), false, true, damageCount);

    }

    #endregion
}

// 하은쌤 스킬
abstract public partial class Skills : SkillBase
{
    #region 하은쌤

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
        Debug.Log("알고리즘 과제 출제");
        if (!SkillSuccess())
        {
            SkillFailedRoutine();
            return;
        }


        // 과제들
        int rnd = Random.Range(0, 100);
        if (rnd > treeProv)
        {
            Debug.Log("트리 과제");
            NoticeUI.instance.SetMsg("트리 과제 출제!");
            damageBoost = 2.0f;
        }
        else if(rnd > stackProv)
        {
            Debug.Log("스택 과제");
            NoticeUI.instance.SetMsg("스택 과제 출제!");
            damageBoost = 1.5f;
        }
        else if(rnd > listProv)
        {
            Debug.Log("리스트 과제");
            NoticeUI.instance.SetMsg("리스트 과제 출제!");
            damageBoost = 1.2f;
        }
        else if(rnd > arrayProv)
        {
            Debug.Log("배열 과제");
            NoticeUI.instance.SetMsg("배열 과제 출제!");
            damageBoost = 1.1f;
        }
        else
        {
            Debug.Log("반복문 과제");
            NoticeUI.instance.SetMsg("반복문 과제 출제!");
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
        Debug.Log("임포스터 관심법으로 찾기");

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
        Debug.Log("덜 익은 만두 대접");

        damageable.OnDamage(damage);
    }

    private void HwanJu(ref int skillPoint)
    {
        int damage = skillDataDictionary[SkillListEnum.HWANJU].info.damage;

        --skillPoint;
        Debug.Log("환쥬바라기");

        damageable.OnDamage(stat.enemyType == Stat.ClassType.HWANJU ? damage * 2 : damage);
    }

    private void GudiAkGae(ref int skillPoint)
    {
        int damage = skillDataDictionary[SkillListEnum.GUDIAKGAE].info.damage;

        --skillPoint;
        Debug.Log("구디악개");

        damageable.OnDamage(stat.enemyType == Stat.ClassType.GUDIGAN ? damage * 2 : damage);
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
        Debug.Log("물승핵");
        if(!SkillSuccess())
        {
            SkillFailedRoutine();
            return;
        }

        damageable.OnDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage), false, true, damageCount);
    }

    public void MoneyHeal(ref int skillPoint) // 금융치료 // 돈 뭉텅이로 던저서 딜입힘. 상대가 선생님이면 공격력의 50% 만큼 힐을 해 줌
    {
        int damage = skillDataDictionary[SkillListEnum.MoneyHeal].info.damage;

        --skillPoint;
        Debug.Log("금융치료");
        if (!SkillSuccess())
        {
            SkillFailedRoutine();
            return;
        }
            
        if (stat.enemyType == Stat.ClassType.TEACHER)
        {
            damageable.OnDamage(damage / 2, true);
            Debug.Log("선생님이 행복한 얼굴로 돈을 받으셨다...");
            NoticeUI.instance.SetMsg("선생님이 행복한 얼굴로 돈을 받으셨다...");
            return;
        }
        damageable.OnDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage));
    }

    #endregion
}

// 기타 함수들
abstract public partial class Skills : SkillBase
{
    private void SkillFailedRoutine()
    {
        NoticeUI.instance.SetMsg("아앗 실패했다...");
        NoticeUI.instance.CallNoticeUI(true, true);
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
            NoticeUI.instance.SetMsg($"{stat.charactorName}의 {skillDataDictionary[selectedSkills[n]].name}!");
            skillDataDictionary[selectedSkills[n]].skill(ref stat.sp_arr[n]);
        }
    }

    abstract protected void InitBtn();
}
