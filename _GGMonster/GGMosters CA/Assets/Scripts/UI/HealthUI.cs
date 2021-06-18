using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 좀더 직관적이기 위해서
public enum ArrayEnum
{ 
    Player = 0,
    Enemy,
    END
}


public class HealthUI : MonoBehaviour
{
    public Stat stat = null;

    [Header("첫 원소에는 플레이어가 들어가야 합니다")]
    public Slider[] hpBar         = new Slider[2];
    public Text[]   nameTextArr   = new Text[2];
    public Text[]   hpTextArr     = new Text[2];

    private void Start()
    {
        stat = GameObject.FindGameObjectWithTag("Player").GetComponent<Stat>();

        #region nullcheck function call
#if UNITY_EDITOR
        NullCheck();
#endif
        #endregion

        // HP bar Init
        hpBar[(int)ArrayEnum.Player].maxValue = stat.maxHp;
        hpBar[(int)ArrayEnum.Enemy].value     = stat.maxHp;

        // Info text Init
        nameTextArr[(int)ArrayEnum.Player].text  = stat.charactorName;
        nameTextArr[(int)ArrayEnum.Enemy].text   = stat.enemyStat.charactorName;
        hpTextArr[(int)ArrayEnum.Player].text    = $"HP : {stat.curHp} / {stat.maxHp}";
        hpTextArr[(int)ArrayEnum.Enemy].text     = $"HP : {stat.enemyStat.curHp} / {stat.enemyStat.maxHp}";


    }
    
    ///<summary>
    /// UI 다시 그려줌
    ///</summary>
    public void ReseetUI()
    {
        hpBar[(int)ArrayEnum.Player].value = stat.curHp;
        hpBar[(int)ArrayEnum.Enemy].value  = stat.enemyStat.curHp;

        hpTextArr[(int)ArrayEnum.Player].text = $"HP : {stat.curHp} / {stat.maxHp}";
    }


    #region nullcheck function
#if UNITY_EDITOR

    private void NullCheck()
    {
        bool bStop = false;

        if(stat == null)
        {
            Debug.LogError("HealthUI: stat is null");
            bStop = true;
        }

        if(hpBar == null)
        {
            Debug.LogError("HealthUI: hpBar is null");
            bStop = true;
        }

        for(int i = 0; i < (int)ArrayEnum.END; ++i)
        {
            if(nameTextArr[i] == null)
            {
                Debug.LogError($"HealthUI: nameTextArr[{i}] is null");
                bStop = true;
            }
            if(hpTextArr[i] == null)
            {
                Debug.LogError($"HealthUI: levelTextArr[{i}] is null");
                bStop = true;
            }
        }


        if(bStop)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        
    }

#endif
    #endregion

}
