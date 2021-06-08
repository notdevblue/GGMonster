using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CharactorBase : MonoBehaviour
{
    private   Stat.ClassType[] typeArr; // �ִ� ��� Ÿ�� ������
    protected Stat             stat;


    protected const int SKILLPOINTARRSIZE = 4; // skillPointArr�� ũ��� ������ 4 �̾�� ��

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
    protected virtual void Init(int hp, Stat.ClassType myType, bool calledByAi = false)
    {
        // stat GetComponent;
        getStat();

        // �� ���� �޾ƿ�
        GetEnemyStat(calledByAi);

        // Ÿ�� ���� �迭 �ʱ�ȭ
        InitTypeArr();

        // HP �ʱ�ȭ
        stat.maxHp    = hp;
        stat.curHp = hp;

        // myType �ʱ�ȭ
        stat.myType     = myType;
        stat.enemyType  = stat.enemyStat.myType;

        #region �߸��� �� �Ǵ� ���Է� üũ
#if UNITY_EDITOR
        CheckValue();
#endif
        #endregion
    }

    // ��� Ÿ�� ���� ������ ���� �Ǵ� ��ú�
    protected virtual void ApplyTypeBenefit()
    {
        if(stat.myType == typeArr[((int)stat.enemyType + 1) % typeArr.Length])
        {
            stat.damageBoost = true;
        }
        else if(stat.myType == Stat.ClassType.TEACHER && stat.myType == stat.enemyType) // ���� ������
        {
            stat.damageBoost = true;
        }
        else
        {
            stat.damageBoost = false;
        }
    }

    private void GetEnemyStat(bool calledByAi = false)
    {
        if(calledByAi)
        {
            stat.enemyStat = GameObject.FindGameObjectWithTag("Player").GetComponent<Stat>();
        }
        else
        {
            stat.enemyStat = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Stat>();
        }
        
        #region null üũ
#if UNITY_EDITOR
        if (stat.enemyStat == null)
        {
            Debug.LogError("AIBase: enemyStat �� ã�� �� �����ϴ�.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        #endregion
    }

    #region ����Ƽ �����Ϳ����� �����
#if UNITY_EDITOR

    void CheckValue()
    {
        // SP
        bool[] statEmpty = { false, false, false, false };
        bool stop = false;

        for (int i = 0; i < 4; ++i)
        {
            if (stat.sp_arr[i] < 0)
            {
                statEmpty[i] = true;
                stop = true;
                Debug.LogError(i + " ��° ��ų����Ʈ�� �߸� �ԷµǾ��ų� �Էµ��� �ʾҽ��ϴ�.");
            }
        }
        
        // ��ų ������
        bool[] dmgEmpty = { false, false, false, false };

        for (int i = 0; i < 4; ++i)
        {
            if (stat.skillDmg[i] < 0)
            {
                dmgEmpty[i] = true;
                stop = true;
                Debug.LogError(i + " ��° ��ų �������� �߸� �ԷµǾ��ų� �Էµ��� �ʾҽ��ϴ�.");
            }
        }

        // HP
        if (stat.maxHp < 0)
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
