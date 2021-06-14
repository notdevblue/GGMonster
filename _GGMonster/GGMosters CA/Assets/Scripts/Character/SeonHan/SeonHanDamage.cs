using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 그리 필요 없는 클래스 인줄 알았는데 이팩트 넣으려면 필요하네요.
public class SeonHanDamage : MonoBehaviour, IDamageable
{
    private Stat stat = null;

    private void Awake()
    {
        stat = GetComponent<Stat>();
#if UNITY_EDITOR
        if (stat == null)
        {
            Debug.LogError("SeonHanDamage: Stat 을 찾을 수 없습니다.");
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
    }

    public void OnDamage(int skillIndex)
    {
        
    }
}
