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


        // TODO : Ÿ���� �þ �� ���� �ڵ带 �����ؾ� ��
        // Ÿ���� �߰��� �� ������ ������ ����
        switch (myType)
        {
            case Stat.ClassType.DIRECTOR:
                if(stat.enemyType == Stat.ClassType.PROGRAMMER)
                {
                    // ������ 1.1��
                }
                break;

            case Stat.ClassType.PROGRAMMER:
                if (stat.enemyType == Stat.ClassType.ARTIST)
                {
                    // ������ 1.1��
                }
                break;

            case Stat.ClassType.ARTIST:
                if (stat.enemyType == Stat.ClassType.DIRECTOR)
                {
                    // ������ 1.1��
                }
                break;

            case Stat.ClassType.TEACHER:
                if (stat.enemyType == Stat.ClassType.TEACHER)
                {
                    // �˽ɽο� �нú�
                }
                break;


            default:
                Debug.LogError("�� �� ���� Ÿ���Դϴ�.");
                break;
        }



    }

}
