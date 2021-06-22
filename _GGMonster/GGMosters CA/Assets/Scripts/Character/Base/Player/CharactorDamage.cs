using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorDamage : MonoBehaviour, IDamageable
{
    private Stat stat = null;

    private void Awake()
    {
        stat = GetComponent<Stat>();
        #region null check
#if UNITY_EDITOR
        if (stat == null)
        {
            Debug.LogError("SeonHanDamage: Stat 을 찾을 수 없습니다.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        #endregion

        
    }

    private void Start()
    {
        TurnManager.instance.midturnTasks.Add(CheckDead);
    }

    public void OnDamage(int damage, bool isHeal = false)
    {
        stat.curHp = isHeal ? ((stat.curHp + damage > stat.maxHp) ? stat.maxHp : stat.curHp + damage) : stat.curHp - damage;
        

        // TOOD : n 의 데미지를 받았다

        if (!isHeal)
        {
            StartCoroutine(DamageEffects.instance.ShakeEffect(damage, transform));
        }
    }

    private void CheckDead()
    {
        if (stat.curHp < 1) { Debug.LogWarning($"{name} is Dead."); stat.isDead = true; }
    }
}
