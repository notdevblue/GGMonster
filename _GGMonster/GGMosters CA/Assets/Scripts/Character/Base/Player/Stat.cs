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
                      public int    charactorAge  = -1;                 // ĳ���� ���� ��ô �ϴ� ����
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
}