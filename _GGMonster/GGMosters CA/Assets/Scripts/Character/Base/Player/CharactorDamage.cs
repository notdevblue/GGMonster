using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorDamage : MonoBehaviour, IDamageable
{
                     public UnityEngine.UI.Text damageText = null;
    [SerializeField] private float shakeSpeed = 120.0f;


    private WindowEffects effects      = null;
    private Stat          stat         = null;
    private HealthUI      healthUI     = null;
    private SkillManager  skillManager = null;

    private int decredDmg = 0;
    private int damage = 0;
    private bool isHeal = false;

    private readonly int highDamageBase = 30;

    private void Awake()
    {
        stat = GetComponent<Stat>();
        healthUI = GameObject.FindGameObjectWithTag("CVSHealth").GetComponent<HealthUI>();
        damageText.gameObject.SetActive(false);
        #region null check
#if UNITY_EDITOR
        bool stop = false;

        if (stat == null)
        {
            Debug.LogError("CharactorDamage: Stat �� ã�� �� �����ϴ�.");
            stop = true;
        }
        if (healthUI == null)
        {
            Debug.LogError("CharactorDamage: HealthUI �� ã�� �� �����ϴ�.");
            stop = true;
        }

        if (stop) UnityEditor.EditorApplication.isPlaying = false;
#endif
        #endregion
    }

    private void Start()
    {
        effects = FindObjectOfType<WindowEffects>();

        TurnManager.instance.midturnTasks.Add(CheckDead);
        TurnManager.instance.turnEndTasks.Add(TickDamage);
        TurnManager.instance.turnEndTasks.Add(CheckDead);
        TurnManager.instance.turnEndTasks.Add(TickDamageEffect);

        skillManager = FindObjectOfType<SkillManager>();
    }

    public void OnDamage(int damage, bool isHeal = false, bool tickDamage = false, int count = 0, int skillEnum = -1, bool doNotEndTurn = false)
    {
        if(tickDamage)
        {
            SetTickDamage(damage, count, skillManager.skillSprite[(SkillListEnum)skillEnum]);
            return;
        }

        this.isHeal = isHeal;
        this.damage = damage;

        decredDmg = isHeal ? ((stat.curHp + damage > stat.maxHp) ? stat.maxHp : stat.curHp + damage) : stat.curHp - damage;

        NoticeUI.instance.SetMsg(isHeal ? (TurnManager.instance.playerTurn ? $"{damage} ��ŭ�� HP�� ȸ���ߴ�!" : $"���� {damage} ��ŭ�� HP�� ȸ���ߴ�!")
            : (TurnManager.instance.playerTurn ? $"{damage} ��ŭ �����ߴ�!" : $"{damage} ��ŭ ������ �޾Ҵ�!"), Damage);

        NoticeUI.instance.CallNoticeUI(!doNotEndTurn, true, TurnManager.instance.enemyTurn, false, false, skillEnum != -1 ? skillManager.skillSprite[(SkillListEnum)skillEnum] : null);
        // TOOD : n �� �������� �޾Ҵ�
    }

    private void Damage()
    {
        stat.curHp = decredDmg;

        healthUI.ResetUI();

        if (!isHeal)
        {
            effects.ShakeX(shakeSpeed, damage * 1.5f, 2);
            StartCoroutine(DamageEffects.instance.ShakeEffect(damage, transform));
            DamageEffects.instance.TextEffect(damage, damageText);

            // �ǰ� ȿ���� ���
            BattleSoundManager.PlaySound(damage > highDamageBase ? SoundEnum.HighDamaged : SoundEnum.LowDamaged);
        }
        else
        {
            DamageEffects.instance.HealEffect(transform);
            DamageEffects.instance.TextEffect(damage, damageText);

            // ġ�� ȿ���� ���
            BattleSoundManager.PlaySound(SoundEnum.Healed);
        }
    }

    #region TickDamage

    private void SetTickDamage(int damage, int count, Sprite spr)
    {
        stat.isTickDamage = true;
        stat.tickDamage = damage;
        stat.tickDamageCount = count;
        NoticeUI.instance.CallNoticeUI(true, true, TurnManager.instance.enemyTurn, false, false, spr);
    }


    private void TickDamage()
    {
        if (stat.tickDamageCount < 0)
        {
            stat.isTickDamage = false;
        }
        else
        {
            // ȿ����
            BattleSoundManager.PlaySound(SoundEnum.TickDamaged);

            --stat.tickDamageCount;
            stat.curHp -= stat.tickDamage;
            effects.ShakeX(shakeSpeed, stat.tickDamage * 1.5f, 2);
        }
    }

    private void TickDamageEffect()
    {
        if(stat.isTickDamage)
        {
            StartCoroutine(DamageEffects.instance.ShakeEffect(stat.tickDamage, transform)); // TODO : ��ų ���� �� �θ��� �ǰ�, �� ���� �� �θ��� ��
            DamageEffects.instance.TextEffect(stat.tickDamage, damageText);
        }
    }

    #endregion


    private void CheckDead()
    {
        if (stat.curHp < 1 && !stat.isDead && !stat.enemyStat.isDead)
        {
            NoticeUI.instance.SetMsg($"{stat.charactorName}�� ��������!");
            NoticeUI.instance.SetMsg($"�� �ƾƾ�...", () => { effects.ShakeY(25, 70, 1); });
            NoticeUI.instance.SetMsg($"�� �ƾƾ�...", healthUI.Dead);
            stat.isDead = true;
            NoticeUI.instance.CallNoticeUI(true, true, stat.enemyStat.isDead, false, false, null, true);
        }
    }


}
