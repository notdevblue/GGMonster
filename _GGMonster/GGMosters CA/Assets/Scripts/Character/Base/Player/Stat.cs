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
        TEACHER
    };
    // 다음 enum 이 데미지 부스트 주는 타입이어야 함
    // NOTYPE 이후에는 타 타입 영향이 없는 enum 이어야 함

    [HideInInspector] public int   maxHp        = 100;                // 최대 HP
                      public int   curHp        = 100;                // 현제 HP
                      public int[] sp_arr       = { -1, -1, -1, -1 }; // SP 상태 배열
                      public int[] skillDmg     = { -1, -1, -1, -1 }; // 스킬 데미지
    [HideInInspector] public bool  provoke      = false;              // 도발 상태
    [HideInInspector] public bool  damageBoost  = false;              // 데미지 버프 상태
    [HideInInspector] public float dmgBoostAmt  = 1.2f;               // 데미지 부스트
    [HideInInspector] public float dmgDecAmt    = 0.8f;               // 데미지 감소
    [HideInInspector] public int   provokeCount = 0;                  // 도발 스텟
    [HideInInspector] public bool  startFirst   = false;              // 먼저 시작하는지
    [HideInInspector] public bool  myturn       = false;              // 턴 상태
    //[HideInInspector] public bool  turnEnded    = false;              // 턴 종료 여부

    [HideInInspector] public int   ProvMissRate = 15;                 // 도발 미스율
    [HideInInspector] public int   missRate     = 5;                 // 미스율

    [HideInInspector] public ClassType myType     = ClassType.NOTYPE; // 내 타입
    [HideInInspector] public ClassType enemyType  = ClassType.NOTYPE; // 적 타입
}
