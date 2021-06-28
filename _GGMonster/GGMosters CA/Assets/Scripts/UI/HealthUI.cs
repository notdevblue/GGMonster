using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// ���� �������̱� ���ؼ�
public enum ArrayEnum
{ 
    Player = 0,
    Enemy,
    END
}


public class HealthUI : MonoBehaviour
{
    private Stat stat = null;

    [Header("HP�� ���� �ð�")]
    [SerializeField] private float decDur = 1.0f;

    [Header("ù ���ҿ��� �÷��̾ ���� �մϴ�")]
    public Slider[] hpBar         = new Slider[2];
    public Image[]  hpBarColor    = new Image[2];
    public Text[]   nameTextArr   = new Text[2];
    public Text[]   hpTextArr     = new Text[2];
    public Text[]   statTextArr   = new Text[2];

    [Header("50% 25% 0% ���� Ŭ �� ä�� ���� ����")]
    public Color[]  hpColorArr    = new Color[3];
    private int[]   hpColorStd    = new int[3]; // �� HP ���� ���� �ٲٴ����� ���� ��


    #region Init Functions, includes Start()
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
        ResetStatusText();
        InitColorStd();

        TurnManager.instance.turnEndTasks.Add(ResetUI);
        TurnManager.instance.midturnTasks.Add(ResetUI);
    }

    private void InitHPBar()
    {
        // HP bar Init
        hpBar[(int)ArrayEnum.Player].maxValue = stat.maxHp;
        hpBar[(int)ArrayEnum.Enemy].maxValue  = stat.enemyStat.maxHp;
        hpBar[(int)ArrayEnum.Player].value    = stat.maxHp;
        hpBar[(int)ArrayEnum.Enemy].value     = stat.enemyStat.maxHp;

        ResetUI();
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
        hpColorStd[0] = (stat.curHp / stat.maxHp) * 100 / 2;
        hpColorStd[1] = (stat.curHp / stat.maxHp) * 100 / 4;
        hpColorStd[2] = 0;
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

    #endregion

    ///<summary>
    /// UI �ٽ� �׷���
    ///</summary>
    public void ResetUI()
    {
        ResetColor();
        ResetStatusText();
        ResetHPBar();
    }
    #region ResetUI functions

    // HP �� ��, ��ġ ����
    private void ResetHPBar()
    {
        hpBar[(int)ArrayEnum.Player].DOValue(stat.curHp, decDur + (hpBar[(int)ArrayEnum.Player].value - stat.curHp) / 50);
        hpBar[(int)ArrayEnum.Enemy].DOValue(stat.enemyStat.curHp, decDur + (hpBar[(int)ArrayEnum.Player].value - stat.curHp) / 50);

        hpTextArr[(int)ArrayEnum.Player].text = $"HP : {stat.curHp} / {stat.maxHp}";
        hpTextArr[(int)ArrayEnum.Enemy].text  = $"HP : {stat.enemyStat.curHp} / {stat.enemyStat.maxHp}";
    }

    // ���� �޼��� ����
    private void ResetStatusText()
    {
        string playerContinuesText = $"{stat.tickDamage}�������� �� ���� ���� �޽��ϴ�. ({stat.tickDamageCount + 1}ȸ ����)";
        string enemyContinuesText  = $"{stat.enemyStat.tickDamage}�������� �� ���� ���� �޽��ϴ�. ({stat.enemyStat.tickDamageCount + 1}ȸ ����)";

        statTextArr[0].text = $"���� / {(stat.provoke           ? $"{(stat.isTickDamage           ? $"����, ���� ������\r\n{playerContinuesText}" : "����")}" : $"{(stat.isTickDamage           ? $"���� ������\r\n{playerContinuesText}" : "�ǰ�")}") }";
        statTextArr[0].color = stat.isTickDamage ? new Color(0.7f, 0.1f, 0.1f) : new Color(0.2f, 0.2f, 0.2f);
        statTextArr[1].text = $"���� / {(stat.enemyStat.provoke ? $"{(stat.enemyStat.isTickDamage ? $"����, ���� ������\r\n{enemyContinuesText}"  : "����")}" : $"{(stat.enemyStat.isTickDamage ? $"���� ������\r\n{enemyContinuesText }" : "�ǰ�")}") }";
        statTextArr[1].color = stat.enemyStat.isTickDamage ? new Color(0.7f, 0.1f, 0.1f) : new Color(0.2f, 0.2f, 0.2f);
    }

    // HP �� �� ����
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

    #endregion


    [SerializeField] private Image fader;
    public void Dead()
    {
        if(stat.isDead)
        {
            DOTween.Clear();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
            // �÷��̾� ���
        }
        else if( stat.enemyStat.isDead)
        {
            fader.DOFade(1, 1.0f).OnComplete(() =>
            {
                DOTween.Clear();
                UnityEngine.SceneManagement.SceneManager.LoadScene("SeonHanScene"); // �ƽ�
            });
            // �� ���
        }
    }


}
