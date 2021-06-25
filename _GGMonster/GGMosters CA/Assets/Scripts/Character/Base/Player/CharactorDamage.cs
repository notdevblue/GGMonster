using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorDamage : MonoBehaviour, IDamageable
{
    private Stat stat = null;
    private HealthUI healthUI = null;

    private int decredDmg = 0;
    private int damage = 0;
    private bool isHeal = false;

    private void Awake()
    {
        stat = GetComponent<Stat>();
        healthUI = GameObject.FindGameObjectWithTag("CVSHealth").GetComponent<HealthUI>();
        #region null check
#if UNITY_EDITOR
        bool stop = false;

        if (stat == null)
        {
            Debug.LogError("CharactorDamage: Stat 을 찾을 수 없습니다.");
            stop = true;
        }
        if (healthUI == null)
        {
            Debug.LogError("CharactorDamage: HealthUI 를 찾을 수 없습니다.");
            stop = true;
        }

        if (stop) UnityEditor.EditorApplication.isPlaying = false;
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

        this.isHeal = isHeal;
        this.damage = damage;

        decredDmg = isHeal ? ((stat.curHp + damage > stat.maxHp) ? stat.maxHp : stat.curHp + damage) : stat.curHp - damage;

        NoticeUI.instance.SetMsg(isHeal ? $"{damage} 만큼의 채력을 회복했다." : $"{damage} 만큼의 피해를 봤다!", Damage);


        NoticeUI.instance.CallNoticeUI(true, true);
        // TOOD : n 의 데미지를 받았다
    }

    private void Damage()
    {
        stat.curHp = decredDmg;
        healthUI.ResetUI();

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
        NoticeUI.instance.CallNoticeUI(true, true);
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
            StartCoroutine(DamageEffects.instance.ShakeEffect(stat.tickDamage, transform)); // TODO : 스킬 시전 시 부르게 되고, 턴 종료 시 부르게 됨                                                      
        }
    }

    #endregion


    private void CheckDead()
    {
        if (stat.curHp < 1) { Debug.LogWarning($"{name} is Dead."); stat.isDead = true; }
    }
}
