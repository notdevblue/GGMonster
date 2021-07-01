using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MoveWindow : MonoBehaviour
{
    #region WinAPI import
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern IntPtr FindWindow(string className, string windowName);

    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(HandleRef hwnd, out RECT lpRect);


    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner
    }
#endif
    #endregion

    // Has current window's handle
    // be carefull when working in unity editor.
    private IntPtr activeHwnd;
    private void SetHwnd() { activeHwnd = GetActiveWindow(); }

    // Han current window's ilocation.
    /// <summary>
    /// Left: x pos of upper-left corner.<br></br>
    /// Top: y pos of upper-left corner.<br></br>
    /// Right: x pos of lower-right coner.<br></br>
    /// Bottom: y pos of lower-right coner.<br></br>
    /// </summary>
    private RECT rc;
    private void SetRect() { GetWindowRect(new HandleRef(this, activeHwnd), out rc); }

    private int midPosX;
    private int midPosY;

    // Sets Application's window position
    private void SetLocation(int xPos, int yPos, int xScale = 1280, int yScale = 720)
    {
        SetWindowPos(activeHwnd, 0, xPos, yPos, xScale, yScale, 1);
    }


    public UnityEngine.UI.Text text = null;
    private void Awake()
    {
        
        #region ## DO NOT EDIT ##
        // Init
        SetHwnd();
        SetRect();

        // Init pos var
        midPosX = Screen.currentResolution.width / 2 - (rc.Right - rc.Left) / 2;
        midPosY = Screen.currentResolution.height / 2 - (rc.Bottom - rc.Top) / 2;

        #endregion
        Application.targetFrameRate = 144;

        //SetLocation();
        text.text = "";
        text.text += "RC LEFT " + rc.Left + "\n";
        text.text += "RC RIGHT " + rc.Right + "\n";
        text.text += "RC TOP " + rc.Top + "\n";
        text.text += "RC BOTTOM " + rc.Bottom + "\n";
        text.text += "RC SIZE X " + (rc.Bottom - rc.Top) + "\n";
        text.text += "RC SIZE Y " + (rc.Right - rc.Left) + "\n";
        text.text += "SCREEN RES / 2 " + (Screen.currentResolution.width / 2.0f).ToString() + "\n";
        text.text += "SCREEN RES / 2 " + (Screen.currentResolution.height / 2.0f).ToString() + "\n";
        text.text += "MID POS X " + (Screen.currentResolution.width / 2- (rc.Right - rc.Left) / 2) + "\n";
        text.text += "MID POS Y " + (Screen.currentResolution.height / 2- (rc.Bottom - rc.Top) / 2) + "\n";

        
    }

    private float degree = 0.0f;



    bool upKeyInput = false;
    bool downKeyInput = false;
    bool selectKeyInput = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            upKeyInput = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            downKeyInput = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            selectKeyInput = true;
        }
    }



    private void FixedUpdate()
    {
        //degree += 0.1f;
        //SetLocation(midPosX, midPosY + (int)(Mathf.Sin(degree) * 100.0f));

        if (upKeyInput)
        {
            StartCoroutine(UpWindowAnim());
            upKeyInput = false;
            // 0 ~ 1 ~ 0
            // sin() 의 1사분면에서 2사분면까지만
            // = pi/2 까지
        }
        else if (downKeyInput)
        {
            StartCoroutine(DownWindowAnim());
            downKeyInput = false;
        }
        else if (selectKeyInput)
        {
            StartCoroutine(SelectWindowAnim());
            selectKeyInput = false;
        }
    }


    private IEnumerator UpWindowAnim()
    {
        float degree = 0.0f;
        while (degree < Mathf.PI)
        {
            degree += 0.15f;
            SetLocation(midPosX, midPosY - (int)(Mathf.Sin(degree) * 25.0f));
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator DownWindowAnim()
    {
        float degree = 0.0f;
        while (degree < Mathf.PI)
        {
            degree += 0.15f;
            SetLocation(midPosX, midPosY + (int)(Mathf.Sin(degree) * 25.0f));
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator SelectWindowAnim()
    {
        float degree = 0.0f;
        while (degree < Mathf.PI)
        {
            degree += 0.15f;
            SetLocation(midPosX+ (int)(Mathf.Sin(degree) * 25.0f), midPosY);
            yield return new WaitForEndOfFrame();
        }
    }
}
