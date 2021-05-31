using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CharactorBase : MonoBehaviour
{
    protected Stat stat;

    protected void getStat()
    {
        Debug.Log("CharactorBase");
        stat = GetComponent<Stat>();
        #region null üũ
#if UNITY_EDITOR
        if (stat == null)
        {
            Debug.LogError("SeonHanAI: Stat �� ã�� �� �����ϴ�.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        #endregion
    }


    /// <summary>
    /// ĳ���� ���� �ʱ�ȭ �Լ�
    /// </summary>
    /// <param name="skillPointArr">��ų����Ʈ �迭</param>
    /// <param name="hp">HP</param>
    /// <param name="myType">�ڽ��� Ÿ��</param>
    protected virtual void Init(int[] skillPointArr, int hp, Stat.ClassType myType)
    {
        getStat();

        #region �迭 ���� �߸� �Է� üũ
        #if UNITY_EDITOR
        if (skillPointArr.Length < 4)
        {
            Debug.LogError("skillPoint �迭 ���̰� �߸��Ǿ����ϴ�. �迭 ���̴� ������ 4 �̾�� �մϴ�.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #endif
        #endregion

        // ��ų����Ʈ �ʱ�ȭ
        stat.sp_a = skillPointArr[0];
        stat.sp_b = skillPointArr[1];
        stat.sp_c = skillPointArr[2];
        stat.sp_d = skillPointArr[3];

        // HP �ʱ�ȭ
        stat.hp = hp;

        // myType �ʱ�ȭ
        stat.myType = myType;

        #region �߸��� �� �Ǵ� ���Է� üũ
#if UNITY_EDITOR
        CheckSP();
#endif
        #endregion
    }

    #region ����Ƽ �����Ϳ����� �����
#if UNITY_EDITOR

    void CheckSP()
    {
        // SP
        bool[] statEmpty = { false, false, false, false };

        if (stat.sp_a < 0)
        {
            statEmpty[0] = true;
        }
        if (stat.sp_b < 0)
        {
            statEmpty[1] = true;
        }
        if (stat.sp_c < 0)
        {
            statEmpty[2] = true;
        }
        if (stat.sp_d < 0)
        {
            statEmpty[3] = true;
        }

        bool stop = false;
        for (int i = 0; i < statEmpty.Length; ++i)
        {
            if (statEmpty[i])
            {
                Debug.LogError(i + " ��° ��ų����Ʈ�� �߸� �ԷµǾ��ų� �Էµ��� �ʾҽ��ϴ�.");
                stop = true;
            }
        }

        // HP
        if (stat.hp < 0)
        {
            Debug.LogError("HP �� �߸� �ԷµǾ��ų� �Էµ��� �ʾҽ��ϴ�.");
            stop = true;
        }

        // myType
        if (stat.myType == Stat.ClassType.NOTYPE)
        {
            Debug.LogError("myType �� �Էµ��� �ʾҽ��ϴ�.");
            stop = true;
        }

        if (stop)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }

    }


#endif

    #endregion
}
