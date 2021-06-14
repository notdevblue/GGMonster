using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �׸� �ʿ� ���� Ŭ���� ���� �˾Ҵµ� ����Ʈ �������� �ʿ��ϳ׿�.
public class SeonHanDamage : MonoBehaviour, IDamageable
{
    private Stat stat = null;

    private void Awake()
    {
        stat = GetComponent<Stat>();
#if UNITY_EDITOR
        if (stat == null)
        {
            Debug.LogError("SeonHanDamage: Stat �� ã�� �� �����ϴ�.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
    }

    public void OnDamage(int skillIndex)
    {
        
    }
}
