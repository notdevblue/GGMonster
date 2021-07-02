using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowEffects : WindowCore
{
    private WaitForEndOfFrame wait = new WaitForEndOfFrame();


    // TODO : Coroutine 으로 호출해야 한다는 매우 귀찮은 단점이 존재함

    /// <summary>
    /// 창을 화면 가온데로 이동시킵니다.
    /// </summary>
    /// <param name="speed">이동 속도</param>
    /// <param name="callBack"></param>
    /// <param name="snap">에니에이션 없이 이동 여부</param>
    /// <returns></returns>
    
    //public WindowCallback ToMiddle(float speed, bool snap = false)
    //{

    //}
    public IEnumerator ToMiddle(float speed, bool snap = false)
    {
        if (snap)
        {
            SetLocation(MiddleCenter);
            yield break;
        }

        float degree = 0.0f;
        speed /= 100.0f;

        while (degree < Mathf.PI / 2.0f)
        {
            degree += speed; // TODO : 끝내지 않음
            //SetLocation();
        }
    }

    #region Bounce Effects

    /// <summary>
    /// 창이 위로 바운스되는 효과를 재생합니다.
    /// </summary>
    /// <param name="speed">바운스 속도</param>
    /// <param name="amount">바운스 될 거리</param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public IEnumerator UpBounce(float speed, float amount, WindowCallBack callback = null)
    {
        float degree = 0.0f;
        speed /= 100.0f;

        while (degree < Mathf.PI)
        {
            degree += speed;
            SetLocation(MidPosX, MidPosY - (int)(Mathf.Sin(degree) * amount)); // TODO : 위치
            yield return wait;
        }

        callback?.Invoke();
    }

    /// <summary>
    /// 창이 아레로 바운스되는 효과를 재생합니다.
    /// </summary>
    /// <param name="speed">바운스 속도</param>
    /// <param name="amount">바운스 될 거리</param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public IEnumerator DownBounce(float speed, float amount, WindowCallBack callback = null)
    {
        float degree = 0.0f;
        speed /= 100.0f;

        while (degree < Mathf.PI)
        {
            degree += speed;
            SetLocation(MidPosX, MidPosY + (int)(Mathf.Sin(degree) * amount));
            yield return wait;
        }

        callback?.Invoke();
    }

    /// <summary>
    /// 창이 오른쪽으로 바운스되는 효과를 재생합니다.
    /// </summary>
    /// <param name="speed">바운스 속도</param>
    /// <param name="amount">바운스 될 거리</param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public IEnumerator RightBounce(float speed, float amount, WindowCallBack callback = null)
    {
        float degree = 0.0f;
        speed /= 100.0f;

        while (degree < Mathf.PI)
        {
            degree += speed;
            SetLocation(MidPosX + (int)(Mathf.Sin(degree) * amount), MidPosY);
            yield return wait;
        }

        callback?.Invoke();
    }

    /// <summary>
    /// 창이 왼쪽으로 바운스되는 효과를 재생합니다.
    /// </summary>
    /// <param name="speed">바운스 속도</param>
    /// <param name="amount">바운스 될 거리</param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public IEnumerator LeftBounce(float speed, float amount, WindowCallBack callback = null)
    {
        float degree = 0.0f;
        speed /= 100.0f;

        while (degree < Mathf.PI)
        {
            degree += speed;
            SetLocation(MidPosX - (int)(Mathf.Sin(degree) * amount), MidPosY);
            yield return wait;
        }

        callback?.Invoke();
    }

    #endregion

    /// <summary>
    /// 특정한 위치로 창을 움직입니다.
    /// </summary>
    /// <param name="pos">이동시킬 위치</param>
    /// <param name="speed">이동 속도</param>
    /// <param name="callBack"></param>
    /// <param name="snap">에니에이션 없이 이동 여부</param>
    /// <returns></returns>
    public IEnumerator MoveWindow(Vector2Int pos, float speed, bool snap = false)
    {
        if (snap)
        {
            SetLocation(pos);
            yield break;
        }

        float degree = 0.0f;
        speed /= 100.0f;

        while (degree < Mathf.PI / 2.0f)
        {
            degree += speed;
            SetLocation(pos.x, pos.y); // TODO : snap 이랑 다른게 없다
            yield return wait;
        }
    }

    // TODO : Dotween 처럼 뒤에 SetEase 붙이고 싶음
}
