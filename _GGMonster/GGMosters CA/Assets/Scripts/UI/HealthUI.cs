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
    public Image[]  hpBarColor    = new Image[2];
    public Text[]   nameTextArr   = new Text[2];
    public Text[]   hpTextArr     = new Text[2];

    [Header("50% 25% 0% 보다 클 때 채력 바의 색상")]
    public Color[]  hpColorArr    = new Color[3];
    private int[]   hpColorStd    = new int[3]; // 몇 HP 에서 색을 바꾸는지에 대한 것

    private void Start()
    {
        stat = GameObject.FindGameObjectWithTag("Player").GetComponent<Stat>();
        ResetColor();

        #region nullcheck function call
#if UNITY_EDITOR
        NullCheck();
#endif
        #endregion

        InitHPBar();
        InitInfoText();
        InitColorStd();

        TurnManager.instance.turnEndTasks.Add(ResetUI);
    }

    #region Init Functions

    private void InitHPBar()
    {
        // HP bar Init
        hpBar[(int)ArrayEnum.Player].maxValue = stat.maxHp;
        hpBar[(int)ArrayEnum.Enemy].maxValue  = stat.enemyStat.maxHp;
        hpBar[(int)ArrayEnum.Player].value    = stat.maxHp;
        hpBar[(int)ArrayEnum.Enemy].value     = stat.enemyStat.maxHp;
    }

    private void InitInfoText()
    {
        // Info text Init
        nameTextArr[(int)ArrayEnum.Player].text  = stat.charactorName;
        nameTextArr[(int)ArrayEnum.Enemy].text   = stat.enemyStat.charactorName;
        hpTextArr[(int)ArrayEnum.Player].text    = $"HP : {stat.curHp} / {stat.maxHp}";
        hpTextArr[(int)ArrayEnum.Enemy].text     = $"HP : {stat.enemyStat.curHp} / {stat.enemyStat.maxHp}";
    }

    private void InitColorStd()
    {
        // colorStd Init
        hpColorStd[0] = 50;
        hpColorStd[1] = 25;
        hpColorStd[2] = 0;
    }

    #endregion


    ///<summary>
    /// UI 다시 그려줌
    ///</summary>
    public void ResetUI()
    {
        ResetColor();

        hpBar[(int)ArrayEnum.Player].value = stat.curHp;
        hpBar[(int)ArrayEnum.Enemy].value  = stat.enemyStat.curHp;

        hpTextArr[(int)ArrayEnum.Player].text = $"HP : {stat.curHp} / {stat.maxHp}";
        hpTextArr[(int)ArrayEnum.Enemy].text  = $"HP : {stat.enemyStat.curHp} / {stat.enemyStat.maxHp}";

    }

    private void ResetColor()
    {
        float hpPercent = ((float)stat.curHp / (float)stat.maxHp) * 100.0f;


        for(int i = 2; i >= 0; --i)
        {
            hpBarColor[0].color = hpPercent > hpColorStd[i] ? hpColorArr[i] : hpBarColor[0].color;
        }

        hpPercent = ((float)stat.enemyStat.curHp / (float)stat.enemyStat.maxHp) * 100;

        for(int i = 2; i >= 0; --i)
        {
            hpBarColor[1].color = hpPercent > hpColorStd[i] ? hpColorArr[i] : hpBarColor[1].color;
        }
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
