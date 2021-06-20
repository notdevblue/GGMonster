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

public class SkillData
{
    public int damage;

    public SkillData(int d) { damage = d; }
}

abstract public class Skills : SkillBase
{
    public delegate void SkillExample(ref int skillPoint);
    public Dictionary<SkillListEnum, SkillExample> skillDictionary  = new Dictionary<SkillListEnum, SkillExample>();
    public Dictionary<SkillListEnum, SkillData> skillDataDictionary = new Dictionary<SkillListEnum, SkillData>();

    private IDamageable damageable;
 


    protected void InitDictionary() // 함수명은 이렇긴 한데 여기서 IDamageable 찾아서 초기화 해줘요.
    {
        InitSeonHanDictionary();
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

    private void InitSeonHanDictionary()
    {
        skillDictionary.Add(SkillListEnum.InsultCodeDesign, InsultCodeDesign);
        skillDictionary.Add(SkillListEnum.MoneyHeal, MoneyHeal);
        skillDictionary.Add(SkillListEnum.PowerfulSholderMassage, PowerfulShoulderMassage);
        skillDictionary.Add(SkillListEnum.Naruto, Naruto);
        skillDictionary.Add(SkillListEnum.Tsundere, Tsundere);

        skillDictionary.Add(SkillListEnum.WaterAttack, WaterAttack);

        skillDataDictionary.Add(SkillListEnum.InsultCodeDesign, new SkillData(25));
        skillDataDictionary.Add(SkillListEnum.MoneyHeal, new SkillData(17));
        skillDataDictionary.Add(SkillListEnum.PowerfulSholderMassage, new SkillData(20));
        skillDataDictionary.Add(SkillListEnum.Naruto, new SkillData(15));
        skillDataDictionary.Add(SkillListEnum.Tsundere, new SkillData(0));
        skillDataDictionary.Add(SkillListEnum.WaterAttack, new SkillData(15));
    }

    protected override void InitSkillNameDic()
    {
        InitSkillNameList();

        for(int i = 0; i < skillNameList.Count; ++i)
        {
            skillNameDic.Add((SkillListEnum)i, skillNameList[i]);
        }
    }

    // SkillListEnum 에 있는 스킬만 추가해야 합니다.
    protected override void InitSkillNameList()
    {
        skillNameList.Add("코드 설계 욕하기");
        skillNameList.Add("금융치료");
        skillNameList.Add("강력한 어깨 안마");
        skillNameList.Add("나선환");
        skillNameList.Add("츤츤거리기");
        skillNameList.Add("선환쌤 스킬 끝");
        skillNameList.Add("물승핵");
        skillNameList.Add("공용 스킬 끝");

        #region List check
#if UNITY_EDITOR
        if (skillNameList.Count != (int)SkillListEnum.DEFAULTEND + 1)
        {
            Debug.LogError("Skills: Wrong skill name count");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        #endregion
    }


    #endregion

    #region 선한쌤

    public void InsultCodeDesign(ref int skillPoint) // 코드 설계 욕하기, 선한쌤 용 코드
    {
        int damage = skillDataDictionary[SkillListEnum.InsultCodeDesign].damage;

        --skillPoint;
        Debug.Log("코드 설계 욕하기");
        if (!SkillSuccess())
        {
            Debug.Log("코드 설계 욕하기 실패");
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
        int damage = skillDataDictionary[SkillListEnum.PowerfulSholderMassage].damage;

        --skillPoint;
        Debug.Log("강력한 어깨 안마");
        if (!SkillSuccess())
        {
            Debug.Log("실패");
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
        int damage = skillDataDictionary[SkillListEnum.Naruto].damage;

        --skillPoint;
        Debug.Log("나선환");
        if (!SkillSuccess())
        {
            Debug.Log("실패");
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
            Debug.Log("실패");
            return;
        }

        stat.enemyStat.provoke = true;
        ++stat.enemyStat.provokeCount;
    }

    public void ReTest(ref int skillPoint)
    {
        --skillPoint;
        Debug.Log("재시험");
        if (!SkillSuccess())
        {
            Debug.Log("실패");
            return;
        }


    }

    #endregion

    #region 공용 스킬

    public void WaterAttack(ref int skillPoint) // 물승핵, 컴퓨터에 물을 쏫는다, 지속딜
    {
        int damageCount = 3;
        int damage = skillDataDictionary[SkillListEnum.WaterAttack].damage / damageCount;

        --skillPoint;
        Debug.Log("물승핵");
        if(!SkillSuccess())
        {
            Debug.Log("실패");
            return;
        }
        stat.enemyStat.SetTickDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage), damageCount);
    }

    public void MoneyHeal(ref int skillPoint) // 금융치료 // 돈 뭉텅이로 던저서 딜입힘. 상대가 선생님이면 공격력의 50% 만큼 힐을 해 줌
    {
        int damage = skillDataDictionary[SkillListEnum.MoneyHeal].damage;

        --skillPoint;
        Debug.Log("금융치료");
        if (!SkillSuccess())
        {
            Debug.Log("금융치료 실패");
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
