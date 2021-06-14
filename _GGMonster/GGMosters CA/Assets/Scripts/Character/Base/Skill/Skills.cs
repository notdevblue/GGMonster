using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Skills : SkillBase
{
    #region 선한쌤
    public void InsultCodeDesign(int damage) // 코드 설계 욕하기, 선한쌤 용 코드
    {
        Debug.Log("코드 설계 욕하기");
        if (!SkillSuccess())
        {
            Debug.Log("코드 설계 욕하기 실패");
            return;
        }

        switch (stat.enemyType)
        {
            case Stat.ClassType.PROGRAMMER:
                stat.enemyStat.curHp -= (int)(damage * stat.dmgBoostAmt); // TODO : <= 데미지 계산하고 변수에 담아야 함 (아마도)
                break;

            case Stat.ClassType.TEACHER:
                stat.enemyStat.curHp -= (int)(damage * stat.dmgDecAmt);
                break;

            default:
                stat.enemyStat.curHp -= damage;
                break;
        }
    }

    public void MoneyHeal(int damage) // 금융치료 // 돈 뭉텅이로 던저서 딜입힘. 상대가 선생님이면 공격력의 50% 만큼 힐을 해 줌
    {
        Debug.Log("금융치료");
        if (!SkillSuccess())
        {
            Debug.Log("금융치료 실패");
            return;
        }

        if (stat.enemyType == Stat.ClassType.TEACHER)
        {
            stat.enemyStat.curHp += damage / 2;
            Debug.Log("선생님이 행복한 얼굴로 돈을 받으셨다...");
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

    public void PowerfulShoulderMassage(int damage) // 강력한 어깨 안마 // n퍼센트의 확률로 상대 ++hp, 선한쌤 용 코드
    {
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
            stat.enemyStat.curHp += (int)(damage * 0.2f);
            Debug.Log("상대가 안마를 편한하게 받아드렸다...");
            return;
        }

        // 공격
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

    public void Naruto(int damage) // 나선환, 선한쌤 용 코드
    {
        Debug.Log("나선환");
        if (!SkillSuccess())
        {
            Debug.Log("실패");
            return;
        }
        stat.enemyStat.curHp -= damage;
    }

    public void Tsundere(int damage) // 츤츤거리기, 선한쌤 용 코드, 도발기
    {
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
        return false;
    }


    abstract protected void InitBtn();
}
