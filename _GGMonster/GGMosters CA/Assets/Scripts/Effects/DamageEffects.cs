using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageEffects : MonoBehaviour
{
    [SerializeField] private float effectDuration = 0.5f;

    private WaitForEndOfFrame wait      = new WaitForEndOfFrame();
    private float             decAmount = 100.0f;
    private Vector2           originPos;

    [SerializeField] private ParticleSystem healEffect = null;

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

    public void TextEffect(int damage, Text txt, bool isHeal = false)
    {
        Vector3 txtOrigin;

        txt.gameObject.SetActive(true);
        txtOrigin = txt.transform.position;
        txt.text = isHeal ? $"HP + {damage}" : $"HP - {damage}";

        txt.gameObject.transform.DOMoveY(txtOrigin.y + effectDuration, effectDuration).OnComplete(() =>
        {
            txt.DOFade(0, effectDuration).OnComplete(() =>
            {
                txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 1);
                txt.gameObject.SetActive(false);
                txt.transform.position = txtOrigin;
            });
        });
    }


    public void HealEffect(Transform obj)
    {
        Transform pos = obj.GetChild(0);

        healEffect.transform.position = new Vector3(pos.position.x, pos.position.y - 1.3f, healEffect.transform.position.z);
        healEffect.Play();
    }
}
