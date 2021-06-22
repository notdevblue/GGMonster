using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffects : MonoBehaviour
{
    [SerializeField] private float effectDuration = 0.5f;

    private WaitForEndOfFrame wait      = new WaitForEndOfFrame();
    private float             decAmount = 100.0f;
    private Vector2           originPos;

    public static DamageEffects instance = null;

    private void Awake()
    {
        instance = this;
    }


    /// <summary>
    /// Shake effect
    /// </summary>
    /// <param name="amount">input damage</param>
    public IEnumerator ShakeEffect(int amount, Transform obj)
    {
        originPos = obj.position;

        float calledTime = Time.time;

        while (calledTime + effectDuration >= Time.time)
        {
            float x = Random.Range(originPos.x - (float)amount / decAmount, originPos.x + (float)amount / decAmount);
            float y = Random.Range(originPos.y - (float)amount / decAmount, originPos.y + (float)amount / decAmount);

            obj.position = new Vector2(x, y);
            yield return wait;
        }

        obj.position = originPos;
    }
}
