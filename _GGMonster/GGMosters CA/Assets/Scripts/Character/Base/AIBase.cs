using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : CharactorBase
{
    // AI�� ����ϴ� ��� �� ���� �Ŀ� ���ư��� ��
    // �ȱ׷��� �η��۷���
    sealed protected override void Init(int[] skillPointArr, int hp, Stat.ClassType myType)
    {
        base.Init(skillPointArr, hp, myType);
    }

}
