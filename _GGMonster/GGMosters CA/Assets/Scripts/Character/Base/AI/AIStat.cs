using UnityEngine;

public class AIStat : MonoBehaviour
{
    // lowAmount 기준: maxHp 의 20%
    // advantage, disAdvantage 가 둘 다 true 라면 선생님 미러전

    [HideInInspector] public bool advantage     = false;
    [HideInInspector] public bool disAdvantage  = false;

    [HideInInspector] public int  lowHpAmount;
    [HideInInspector] public bool lowHp         = false;

    [HideInInspector] public int  enemyLowHpAmount;
    [HideInInspector] public bool enemyLowHp    = false;

    [HideInInspector] public int  lowSpAmount;
    [HideInInspector] public int  exLowHpAmount;
    [HideInInspector] public bool lowSp         = false;

    [HideInInspector] public int  enemyLowSpAmount;
    [HideInInspector] public bool enemyLowSp    = false;

    [HideInInspector] public bool isHighProvoke = false; // 높은 어그로 스텟
}
