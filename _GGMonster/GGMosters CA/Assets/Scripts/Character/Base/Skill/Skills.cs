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
        Debug.Log(this.name);
        skillDataDictionary.Add(SkillListEnum.InsultCodeDesign,       
            new SkillData("코드 설계 욕하기", InsultCodeDesign, new SkillInfo("상대방의 코드 설계를 비판합니다", 25, Stat.ClassType.PROGRAMMER, Stat.ClassType.TEACHER)));

        skillDataDictionary.Add(SkillListEnum.MoneyHeal,
            new SkillData("금융치료", MoneyHeal, new SkillInfo("상대방에게 무상으로 금융치료를 하는 척 하면서 이자를 뜯어냅니다..", 17, Stat.ClassType.NOTYPE, Stat.ClassType.TEACHER)));
        
        skillDataDictionary.Add(SkillListEnum.PowerfulSholderMassage, 
            new SkillData("강력한 어깨 안마", PowerfulShoulderMassage, new SkillInfo("상대방의 어깨를 강력하게 안마합니다.", 20, Stat.ClassType.NOTYPE, Stat.ClassType.TEACHER)));
        
        skillDataDictionary.Add(SkillListEnum.Naruto,                 
            new SkillData("나선환", Naruto, new SkillInfo("상대방에게 강력한 나선환을 발사합니다.", 15)));
        
        skillDataDictionary.Add(SkillListEnum.Tsundere,               
            new SkillData("츤츤거리기", Tsundere, new SkillInfo("상대방에게 츤츤거립니다.", 0)));
        
        skillDataDictionary.Add(SkillListEnum.ReTest,                 
            new SkillData("재시험", ReTest, new SkillInfo("상대방의 점수가 기준보다 낮아 재시험을 치르게 합니다.", 15, Stat.ClassType.PROGRAMMER, Stat.ClassType.TEACHER, true, 3)));
        
        skillDataDictionary.Add(SkillListEnum.WaterAttack,            
            new SkillData("물승핵", WaterAttack, new SkillInfo("상대방에 노트북 키보드에 물을 붓습니다.", 15, Stat.ClassType.PROGRAMMER, Stat.ClassType.NOTYPE, true, 3)));
    }

    #endregion

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

        stat.enemyStat.SetTickDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage), damageCount);

    }

    #endregion

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
        stat.enemyStat.SetTickDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage), damageCount);
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
            return;
        }
        damageable.OnDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage));
    }

    #endregion

    #region 하은쌤

    private void Foo(ref int skillPoint)
    {

    }

    #endregion

    private void SkillFailedRoutine()
    {
        Debug.Log("스킬 시전 실패");
        TurnManager.instance.EndTurn();
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


    abstract protected void InitBtn();
}
