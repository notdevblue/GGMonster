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
        CHOI,
        HWANJU,
        GUDIGAN,
        CLASSEND,
        ALL
    };
    // ���� enum �� ������ �ν�Ʈ �ִ� Ÿ���̾�� ��
    // NOTYPE ���Ŀ��� Ÿ Ÿ�� ������ ���� enum �̾�� ��


                      public          string    charactorName = null;               // ĳ���� �̸�
    [HideInInspector] public          int       maxHp         = 100;                // �ִ� HP
                      public          int       curHp         = 100;                // ���� HP
                      public          bool      isDead        = false;              // ��� ����
                      public          int[]     sp_arr        = { -1, -1, -1, -1 }; // SP ���� �迭
    [HideInInspector] public          bool      provoke       = false;              // ���� ����
    [HideInInspector] public          bool      damageBoost   = false;              // ������ ���� ����
    [HideInInspector] public readonly float     dmgBoostAmt   = 1.2f;               // ������ �ν�Ʈ
    [HideInInspector] public readonly float     dmgDecAmt     = 0.8f;               // ������ ����
    [HideInInspector] public          int       provokeCount  = 0;                  // ���� ����
    [HideInInspector] public          bool      startFirst    = false;              // ���� �����ϴ���
    [HideInInspector] public          bool      myturn        = false;              // �� ����
    [HideInInspector] public readonly int       ProvMissRate  = 15;                 // ���� �̽���
    [HideInInspector] public readonly int       missRate      = 5;                  // �̽���
    [HideInInspector] public          int       healItemCnt   = 3;                  // �� ������ ���� ��
    [HideInInspector] public readonly int       healAmout     = 10;                 // �� ������ ����
    [HideInInspector] public          int       provItemCnt   = 1;                  // ���� ���� �ʱ�ȭ ������ ���� ��
    [HideInInspector] public readonly int       provUseCost   = 5;                  // ���� ���� �ʱ�ȭ ������ ������ ��
                      public          ClassType myType        = ClassType.NOTYPE;   // �� Ÿ��
    [HideInInspector] public          ClassType enemyType     = ClassType.NOTYPE;   // �� Ÿ��

    #region TickDamage

    [HideInInspector] public bool isTickDamage    = false;  // ���Ӵ� ����
    [HideInInspector] public int  tickDamageCount = 0;      // ���Ӵ� ī��Ʈ
    [HideInInspector] public int  tickDamage      = 0;      // ���ӵ� ������

    #endregion
}
