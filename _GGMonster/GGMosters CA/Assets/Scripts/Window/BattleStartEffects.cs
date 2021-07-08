using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStartEffects : MonoBehaviour
{
    WindowEffects effects;
    private void Awake()
    {
        effects = GetComponent<WindowEffects>();
    }

    private void Start()
    {
        effects.ToWindowed(1280, 720, 10.0f);
    }
}
