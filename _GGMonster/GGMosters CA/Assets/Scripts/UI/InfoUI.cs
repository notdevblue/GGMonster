using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InfoUI : MonoBehaviour
{
    [SerializeField] private Transform infoObject = null;
    [SerializeField] private Text      skillName  = null;
    [SerializeField] private Text      skillInfo  = null;
    [SerializeField] private Text      skillData  = null;
    [SerializeField] private Button    btnClose   = null;

    [Header("이동 관련")]
    [SerializeField] private Transform closePos   = null;
    [SerializeField] private Transform openPos    = null;
    [SerializeField] private float     moveDur    = 1.0f;
                     private bool      isOpen     = false;

    private SkillData prevSkillData;
    private ItemData prevItemData;

    private delegate void InfoUICallback();

    private void Awake()
    { 
        infoObject.position = closePos.position;
        btnClose.onClick.AddListener(Close); // 자신을 눌렀을 경우 닫아줌
    }

    bool isClosing = false;
    private void Close()
    {
        if (isClosing) return;

        // 효과음
        BattleSoundManager.PlaySound(SoundEnum.UIBack);

        isClosing = true;
        infoObject.DOMove(closePos.position, moveDur).SetEase(Ease.InCubic).OnComplete(() =>{ isOpen = false; isClosing = false; });
        prevItemData = null;
        prevSkillData = null;
    }

    public void CallItemInfo(ItemData data)
    {
        if (isClosing) return;

        if(prevItemData != data)
        {
            if (isOpen)
            {
                ToPos(() => SetAndOpen(data));
            }
            else
            {
                SetAndOpen(data);
            }
        }
        prevItemData = data;
    }
    public void CallSkillInfo(SkillData data)
    {
        if (isClosing) return;

        if(prevSkillData != data)
        {
            if (isOpen)
            {
                ToPos(() => SetAndOpen(data));
            }
            else
            {
                SetAndOpen(data);
            }
        }
        prevSkillData = data;
    }

    #region Set and open popup
    private void SetAndOpen(ItemData data)
    {
        SetPopupData(data);
        ToPos();
    }
    
    private void SetAndOpen(SkillData data)
    {
        SetPopupData(data);
        ToPos();
    }

    #region SetData
    private void SetPopupData(ItemData data)
    {
        skillName.text = data.name;
        skillInfo.text = data.info;
        skillData.text = data.stat;
    }

    private void SetPopupData(SkillData data)
    {
        // 한줄로 만들고 싶은 욕망이 불러 이르킨 참사
        string effectiveClass = $"효과적인 타입: {(data.info.effectiveClass == Stat.ClassType.NOTYPE ? "없음" : data.info.effectiveClass.ToString()[0].ToString().ToUpper() + data.info.effectiveClass.ToString().Substring(1).ToLower())}\r\n효과적이지 않은 타입: {(data.info.uneffectiveClass == Stat.ClassType.NOTYPE ? "없음" : data.info.uneffectiveClass.ToString()[0].ToString().ToUpper() + data.info.uneffectiveClass.ToString().Substring(1).ToLower())}";
        string continues      = $"{(data.info.isContinues ? $"{data.info.damage} 데미지가 {data.info.continuesCount} 번 나뉘어 들어갑니다." : "")}";
        string damage         = $"{data.info.damage} 데미지를 가합니다.";

        skillName.text = data.name;
        skillInfo.text = data.info.info;
        skillData.text = $"{effectiveClass}\r\n{(continues != "" ? $"{continues}\r\n" : damage)}";

    }
    #endregion

    #endregion

    private void ToPos(InfoUICallback callback = null)
    {
        BattleSoundManager.PlaySound(isOpen ? SoundEnum.UIBack : SoundEnum.UISelect);

        infoObject.DOMove((isOpen ? closePos.position : openPos.position), moveDur).SetEase(isOpen ? Ease.InCubic : Ease.OutCubic).OnComplete(() =>
        {
            isOpen = !isOpen;
            callback?.Invoke();
        });
    }
}
