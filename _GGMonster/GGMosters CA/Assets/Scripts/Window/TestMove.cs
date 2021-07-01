using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class TestMove : MonoBehaviour
{
    public Text debugText;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern IntPtr FindWindow(string className, string windowName);

    public static void SetPosition(int x, int y, int resX = 0, int resY = 0)
    {
        SetWindowPos(FindWindow(null, "GGMonsters"), 0, x, y, resX, resY, resX * resY == 0 ? 1 : 0);
    }
#endif

    IntPtr ActiveHwnd;
    private void Awake()
    {
        ActiveHwnd = GetActiveWindow();

        //debugText.text =  SetWindowPos(ActiveHwnd, 0, 100, 100, 1280, 720, 1).ToString();

    }


    float degree = 0.0f;
    public GameObject debugObject;
    private void FixedUpdate()
    {
        SetWindowPos(ActiveHwnd, 0, 300, (int)((Mathf.Sin(degree += 0.1f)) * 100) + 150, 1280, 720, 1);
    }

}
