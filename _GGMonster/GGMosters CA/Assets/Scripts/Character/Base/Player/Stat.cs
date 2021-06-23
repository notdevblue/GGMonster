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
    // 다음 enum 이 데미지 부스트 주는 타입이어야 함
    // NOTYPE 이후에는 타 타입 영향이 없는 enum 이어야 함


                      public          string    charactorName = null;               // 캐릭터 이름
    [HideInInspector] public          int       maxHp         = 100;                // 최대 HP
                      public          int       curHp         = 100;                // 현재 HP
                      public          bool      isDead        = false;              // 사망 상태
                      public          int[]     sp_arr        = { -1, -1, -1, -1 }; // SP 상태 배열
    [HideInInspector] public          bool      provoke       = false;              // 도발 상태
    [HideInInspector] public          bool      damageBoost   = false;              // 데미지 버프 상태
    [HideInInspector] public readonly float     dmgBoostAmt   = 1.2f;               // 데미지 부스트
    [HideInInspector] public readonly float     dmgDecAmt     = 0.8f;               // 데미지 감소
    [HideInInspector] public          int       provokeCount  = 0;                  // 도발 스텟
    [HideInInspector] public          bool      startFirst    = false;              // 먼저 시작하는지
    [HideInInspector] public          bool      myturn        = false;              // 턴 상태
    [HideInInspector] public readonly int       ProvMissRate  = 15;                 // 도발 미스율
    [HideInInspector] public readonly int       missRate      = 5;                  // 미스율
    [HideInInspector] public          int       healItemCnt   = 3;                  // 힐 아이탬 소지 수
    [HideInInspector] public readonly int       healAmout     = 10;                 // 힐 아이탬 힐량
    [HideInInspector] public          int       provItemCnt   = 1;                  // 도발 스텟 초기화 아이탬 소지 수
    [HideInInspector] public readonly int       provUseCost   = 5;                  // 도발 스켓 초기화 아이탬 데미지 양
                      public          ClassType myType        = ClassType.NOTYPE;   // 내 타입
    [HideInInspector] public          ClassType enemyType     = ClassType.NOTYPE;   // 적 타입

    #region TickDamage

    [HideInInspector] public bool isTickDamage    = false;  // 지속댐 여부
    [HideInInspector] public int  tickDamageCount = 0;      // 지속댐 카운트
    [HideInInspector] public int  tickDamage      = 0;      // 지속뎀 데미지

    #endregion
}
