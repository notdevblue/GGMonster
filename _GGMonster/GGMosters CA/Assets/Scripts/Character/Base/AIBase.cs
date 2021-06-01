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
    }

}
