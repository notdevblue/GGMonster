using UnityEngine;

public class Stat : MonoBehaviour
{
    public enum ClassType
    {
        NOTYPE,
        TEACHER,
        PROGRAMMER,
        ARTIST,
        DIRECTOR
    };

    [HideInInspector] public int  hp           = 100;   // HP
    [HideInInspector] public int  sp_a         = -1;    // A 스킬 포인트
    [HideInInspector] public int  sp_b         = -1;    // B 스킬 포인트
    [HideInInspector] public int  sp_c         = -1;    // C 스킬 포인트
    [HideInInspector] public int  sp_d         = -1;    // D 스킬 포인트
    [HideInInspector] public bool provoke      = false; // 도발 상태
    [HideInInspector] public int  provokeCount = 0;     // 도발 스텟

    public ClassType myType     = ClassType.NOTYPE;     // 내 타입
    public ClassType enemyType  = ClassType.NOTYPE;     // 적 타입
}
