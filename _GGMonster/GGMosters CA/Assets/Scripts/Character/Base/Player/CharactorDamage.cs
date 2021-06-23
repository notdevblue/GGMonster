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
            Debug.LogError("SeonHanDamage: Stat �� ã�� �� �����ϴ�.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        #endregion

        
    }

    private void Start()
    {
        TurnManager.instance.midturnTasks.Add(CheckDead);
        TurnManager.instance.turnEndTasks.Add(TickDamage);
        TurnManager.instance.turnEndTasks.Add(CheckDead);
        TurnManager.instance.turnEndTasks.Add(TickDamageEffect);
    }

    public void OnDamage(int damage, bool isHeal = false, bool tickDamage = false, int count = 0)
    {
        if(tickDamage)
        {
            SetTickDamage(damage, count);
            return;
        }
        
        
        stat.curHp = isHeal ? ((stat.curHp + damage > stat.maxHp) ? stat.maxHp : stat.curHp + damage) : stat.curHp - damage;
        

        // TOOD : n �� �������� �޾Ҵ�

        if (!isHeal)
        {
            StartCoroutine(DamageEffects.instance.ShakeEffect(damage, transform));
        }
    }

    #region TickDamage

    private void SetTickDamage(int damage, int count)
    {
        stat.isTickDamage = true;
        stat.tickDamage = damage;
        stat.tickDamageCount = count;
    }


    private void TickDamage()
    {
        if (stat.tickDamageCount < 0)
        {
            stat.isTickDamage = false;
        }
        else
        {
            --stat.tickDamageCount;
            stat.curHp -= stat.tickDamage;
            
        }
    }

    private void TickDamageEffect()
    {
        if(stat.isTickDamage)
        {
            StartCoroutine(DamageEffects.instance.ShakeEffect(stat.tickDamage, transform)); // TODO : ��ų ���� �� �θ��� �ǰ�, �� ���� �� �θ��� ��                                                      
        }
    }

    #endregion


    private void CheckDead()
    {
        if (stat.curHp < 1) { Debug.LogWarning($"{name} is Dead."); stat.isDead = true; }
    }
}
