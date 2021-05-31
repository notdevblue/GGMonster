using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : CharactorBase
{
    // AI가 사용하는 경우 적 생성 후에 돌아가야 함
    // 안그러면 널레퍼런스
    sealed protected override void Init(int[] skillPointArr, int hp, Stat.ClassType myType)
    {
        base.Init(skillPointArr, hp, myType);


        // TODO : 타입이 늘어날 때 마다 코드를 수정해야 함
        // 타입이 추가될 거 같지는 않지만 흠흠
        switch (myType)
        {
            case Stat.ClassType.DIRECTOR:
                if(stat.enemyType == Stat.ClassType.PROGRAMMER)
                {
                    // 데미지 1.1배
                }
                break;

            case Stat.ClassType.PROGRAMMER:
                if (stat.enemyType == Stat.ClassType.ARTIST)
                {
                    // 데미지 1.1배
                }
                break;

            case Stat.ClassType.ARTIST:
                if (stat.enemyType == Stat.ClassType.DIRECTOR)
                {
                    // 데미지 1.1배
                }
                break;

            case Stat.ClassType.TEACHER:
                if (stat.enemyType == Stat.ClassType.TEACHER)
                {
                    // 쫀심싸움 패시브
                }
                break;


            default:
                Debug.LogError("알 수 없는 타입입니다.");
                break;
        }



    }

}
