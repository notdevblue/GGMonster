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

    protected void InitDictionary() // 함수명은 이렇긴 한데 여기서 IDamageable 찾아서 초기화 해줘요.
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

    #region 선한쌤

    //delegate void SeonHanAtkDelegate(int damage, ref int skillPoint);

    public void InsultCodeDesign(ref int skillPoint) // 코드 설계 욕하기, 선한쌤 용 코드
    {
        int damage = 25;

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

    public void MoneyHeal(ref int skillPoint) // 금융치료 // 돈 뭉텅이로 던저서 딜입힘. 상대가 선생님이면 공격력의 50% 만큼 힐을 해 줌
    {
        int damage = 17;

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

    public void PowerfulShoulderMassage(ref int skillPoint) // 강력한 어깨 안마 // n퍼센트의 확률로 상대 ++hp, 선한쌤 용 코드
    {
        int damage = 20;

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
        int damage = 15;

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

    #endregion

    #region 공용 스킬

    // TODO : 지속딜 카운트 필요함
    public void WaterAttack(ref int skillPoint) // 물승핵, 컴퓨터에 물을 쏫는다, 지속딜
    {
        int damage = 15;

        --skillPoint;
        Debug.Log("물승핵");
        if(!SkillSuccess())
        {
            Debug.Log("실패");
            return;
        }

        damageable.OnDamage((int)(stat.damageBoost ? damage * stat.dmgBoostAmt : damage));
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
