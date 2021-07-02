using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowEffects : WindowCore
{
    private WaitForEndOfFrame wait = new WaitForEndOfFrame();


    // TODO : Coroutine ���� ȣ���ؾ� �Ѵٴ� �ſ� ������ ������ ������

    /// <summary>
    /// â�� ȭ�� ���µ��� �̵���ŵ�ϴ�.
    /// </summary>
    /// <param name="speed">�̵� �ӵ�</param>
    /// <param name="callBack"></param>
    /// <param name="snap">���Ͽ��̼� ���� �̵� ����</param>
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
            degree += speed; // TODO : ������ ����
            //SetLocation();
        }
    }

    #region Bounce Effects

    /// <summary>
    /// â�� ���� �ٿ�Ǵ� ȿ���� ����մϴ�.
    /// </summary>
    /// <param name="speed">�ٿ �ӵ�</param>
    /// <param name="amount">�ٿ �� �Ÿ�</param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public IEnumerator UpBounce(float speed, float amount, WindowCallBack callback = null)
    {
        float degree = 0.0f;
        speed /= 100.0f;

        while (degree < Mathf.PI)
        {
            degree += speed;
            SetLocation(MidPosX, MidPosY - (int)(Mathf.Sin(degree) * amount)); // TODO : ��ġ
            yield return wait;
        }

        callback?.Invoke();
    }

    /// <summary>
    /// â�� �Ʒ��� �ٿ�Ǵ� ȿ���� ����մϴ�.
    /// </summary>
    /// <param name="speed">�ٿ �ӵ�</param>
    /// <param name="amount">�ٿ �� �Ÿ�</param>
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
    /// â�� ���������� �ٿ�Ǵ� ȿ���� ����մϴ�.
    /// </summary>
    /// <param name="speed">�ٿ �ӵ�</param>
    /// <param name="amount">�ٿ �� �Ÿ�</param>
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
    /// â�� �������� �ٿ�Ǵ� ȿ���� ����մϴ�.
    /// </summary>
    /// <param name="speed">�ٿ �ӵ�</param>
    /// <param name="amount">�ٿ �� �Ÿ�</param>
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
    /// Ư���� ��ġ�� â�� �����Դϴ�.
    /// </summary>
    /// <param name="pos">�̵���ų ��ġ</param>
    /// <param name="speed">�̵� �ӵ�</param>
    /// <param name="callBack"></param>
    /// <param name="snap">���Ͽ��̼� ���� �̵� ����</param>
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
            SetLocation(pos.x, pos.y); // TODO : snap �̶� �ٸ��� ����
            yield return wait;
        }
    }

    // TODO : Dotween ó�� �ڿ� SetEase ���̰� ����
}
