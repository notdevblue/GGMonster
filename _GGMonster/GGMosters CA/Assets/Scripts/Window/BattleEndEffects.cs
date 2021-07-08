using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEndEffects : MonoBehaviour
{
    private WindowEffects effects;

    void Start()
    {
        effects.ToFullScreen(10.0f);   
    }
}
