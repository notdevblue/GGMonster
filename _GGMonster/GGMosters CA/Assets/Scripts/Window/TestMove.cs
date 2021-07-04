using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class TestMove : WindowCore
{
    float x = 1920.0f;
    float y = 1080.0f;

    private void FixedUpdate()
    {
        x = 1920.0f - (Mathf.Sin(Time.time * 2.0f) + 1.0f) * 500.0f;
        y = 1080.0f - (Mathf.Sin(Time.time * 2.0f) + 1.0f) * 250.0f;
        SetWindowSize((int)x, (int)y);
    }
}
