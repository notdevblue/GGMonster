using UnityEngine;

public class AIStat : MonoBehaviour
{
    // lowAmount ����: maxHp �� 20%
    // advantage, disAdvantage �� �� �� true ��� ������ �̷���

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

    [HideInInspector] public bool isHighProvoke = false; // ���� ��׷� ����
}
