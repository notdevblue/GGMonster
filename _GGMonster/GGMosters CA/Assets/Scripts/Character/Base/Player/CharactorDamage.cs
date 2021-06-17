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

    public void OnDamage(int damage, bool isHeal = false)
    {
        stat.curHp += isHeal ? damage : -damage;

        // TODO : UI
        // TODO : Effects
    }
}
