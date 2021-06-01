using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CharactorBase : MonoBehaviour
{
    private   Stat.ClassType[] typeArr;
    protected Stat             stat;

    #region �ʱ�ȭ �Լ�
    protected void getStat()
    {
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
    private void InitTypeArr()
    {
        int count;
        for (count = 0; count != (int)Stat.ClassType.NOTYPE; ++count) { }

        typeArr = new Stat.ClassType[count];

        for(int i = 0; i < count; ++i)
        {
            typeArr[i] = (Stat.ClassType)i;
        }
    }
    #endregion

    /// <summary>
    /// ĳ���� ���� �ʱ�ȭ �Լ�
    /// </summary>
    /// <param name="skillPointArr">��ų����Ʈ �迭</param>
    /// <param name="hp">HP</param>
    /// <param name="myType">�ڽ��� Ÿ��</param>
    protected virtual void Init(int[] skillPointArr, int hp, Stat.ClassType myType)
    {
        // stat GetComponent;
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

        // Ÿ�� ���� �迭 �ʱ�ȭ
        InitTypeArr();

        // ��ų����Ʈ �ʱ�ȭ
        stat.sp_a = skillPointArr[0];
        stat.sp_b = skillPointArr[1];
        stat.sp_c = skillPointArr[2];
        stat.sp_d = skillPointArr[3];

        // HP �ʱ�ȭ
        stat.hp    = hp;
        stat.curHp = hp;

        // myType �ʱ�ȭ
        stat.myType = myType;

        #region �߸��� �� �Ǵ� ���Է� üũ
#if UNITY_EDITOR
        CheckSP();
#endif
        #endregion

        ApplyTypeBenefit();
    }

    // ��� Ÿ�� ���� ������ ���� �Ǵ� ��ú�
    protected virtual void ApplyTypeBenefit()
    {
        if(stat.myType == typeArr[((int)stat.enemyType + 1) % typeArr.Length])
        {
            stat.damageBoost = true;
        }
        else if(stat.myType == stat.enemyType && stat.myType == Stat.ClassType.TEACHER) // ���� ������
        {
            stat.damageBoost = true;
        }

        #region Archive: switch comparing
        // TODO : Ÿ���� �þ �� ���� �ڵ带 �����ؾ� ��
        // �ݺ��Ǵ� �۾��� ����
        // ���ʿ��غ��̴� �ڵ尡 ����
        // ���ϼ��� �����Ű���
        // Ÿ���� �߰��� �� ������ ������ ����
        // IDEA : �迭?

        //switch (myType)
        //{
        //    case Stat.ClassType.DIRECTOR:
        //        if (stat.enemyType == Stat.ClassType.PROGRAMMER)
        //        {
        //            stat.damageBoost = true;
        //        }
        //        break;

        //    case Stat.ClassType.PROGRAMMER:
        //        if (stat.enemyType == Stat.ClassType.ARTIST)
        //        {
        //            stat.damageBoost = true;
        //        }
        //        break;

        //    case Stat.ClassType.ARTIST:
        //        if (stat.enemyType == Stat.ClassType.DIRECTOR)
        //        {
        //            stat.damageBoost = true;
        //        }
        //        break;

        //    case Stat.ClassType.TEACHER:
        //        if (stat.enemyType == Stat.ClassType.TEACHER)
        //        {
        //            stat.damageBoost = true;
        //        }
        //        break;

        //    default:
        //        Debug.LogError("�� �� ���� Ÿ���Դϴ�.");
        //        break;
        //}
        #endregion // Do not use
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
