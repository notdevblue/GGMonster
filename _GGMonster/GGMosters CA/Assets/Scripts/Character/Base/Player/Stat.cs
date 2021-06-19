using UnityEngine;

public class Stat : MonoBehaviour
{
    [HideInInspector] public Stat enemyStat;

    public enum ClassType
    {
        ARTIST,
        PROGRAMMER,
        DIRECTOR,
        NOTYPE,
        TEACHER,
        R6SANS,
        CHOI
    };
    // ���� enum �� ������ �ν�Ʈ �ִ� Ÿ���̾�� ��
    // NOTYPE ���Ŀ��� Ÿ Ÿ�� ������ ���� enum �̾�� ��


                      public string charactorName = null;               // ĳ���� �̸�
    [HideInInspector] public int    maxHp         = 100;                // �ִ� HP
                      public int    curHp         = 100;                // ���� HP
                      public int[]  sp_arr        = { -1, -1, -1, -1 }; // SP ���� �迭
    [HideInInspector] public bool   provoke       = false;              // ���� ����
    [HideInInspector] public bool   damageBoost   = false;              // ������ ���� ����
    [HideInInspector] public float  dmgBoostAmt   = 1.2f;               // ������ �ν�Ʈ
    [HideInInspector] public float  dmgDecAmt     = 0.8f;               // ������ ����
    [HideInInspector] public int    provokeCount  = 0;                  // ���� ����
    [HideInInspector] public bool   startFirst    = false;              // ���� �����ϴ���
    [HideInInspector] public bool   myturn        = false;              // �� ����
    [HideInInspector] public int    ProvMissRate  = 15;                 // ���� �̽���
    [HideInInspector] public int    missRate      = 5;                  // �̽���
                      public ClassType myType     = ClassType.NOTYPE;   // �� Ÿ��
    [HideInInspector] public ClassType enemyType  = ClassType.NOTYPE;   // �� Ÿ��


    #region TickDamage

    [HideInInspector] public bool isTickDamage    = false;  // ���Ӵ� ����
    [HideInInspector] public int  tickDamageCount = 0;      // ���Ӵ� ī��Ʈ
    [HideInInspector] public int  tickDamage      = 0;      // ���ӵ� ������

    /// <summary>
    /// Sets tick damage
    /// </summary>
    /// <param name="damage">damage amount</param>
    /// <param name="count">repeat count</param>
    public void SetTickDamage(int damage, int count)
    {
        isTickDamage = false;
        tickDamage = damage;
        tickDamageCount = count;
    }

    public void CheckTickDamage()
    {
        if (tickDamageCount < 0)
        {
            isTickDamage = false;
        }
        else
        {
            --tickDamageCount;
            curHp -= tickDamage;
        }
    }

    public void Start()
    {
        TurnManager.instance.turnEndTasks.Add(CheckTickDamage);
    }

    #endregion
}
