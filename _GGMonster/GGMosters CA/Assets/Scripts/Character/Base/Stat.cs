using UnityEngine;

public class Stat : MonoBehaviour
{
    
    public enum ClassType
    {
        DIRECTOR,
        PROGRAMMER,
        ARTIST,
        NOTYPE,
        TEACHER
    };
    // ���� enum �̲� ������ �ν�Ʈ �ִ� Ÿ���̾�� ��
    // NOTYPE ���Ŀ��� Ÿ Ÿ�� ������ ���� enum �̾�� ��

    [HideInInspector] public int  hp           = 100;   // HP
    [HideInInspector] public int  sp_a         = -1;    // A ��ų ����Ʈ
    [HideInInspector] public int  sp_b         = -1;    // B ��ų ����Ʈ
    [HideInInspector] public int  sp_c         = -1;    // C ��ų ����Ʈ
    [HideInInspector] public int  sp_d         = -1;    // D ��ų ����Ʈ
    [HideInInspector] public bool provoke      = false; // ���� ����
    [HideInInspector] public bool damageBoost  = false; // ������ ���� ����
    [HideInInspector] public int  provokeCount = 0;     // ���� ����
    

    public ClassType myType     = ClassType.NOTYPE;     // �� Ÿ��
    public ClassType enemyType  = ClassType.NOTYPE;     // �� Ÿ��
}
