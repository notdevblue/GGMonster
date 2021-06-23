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

    private SkillData prevData;

    private void Awake()
    { 
        infoObject.position = closePos.position;
        btnClose.onClick.AddListener(ToPos);
    }

    public void CallPopupInfo(SkillData data)
    {

        SetPopupData(data);
        if(prevData != data)
        {
            CallPopup();
        }

        prevData = data;
    }


    private void SetPopupData(SkillData data)
    {
        // 한줄로 만들고 싶은 욕망이 불러 이르킨 참사

        skillName.text = data.name;
        skillInfo.text = data.info.info;
        skillData.text = "";
        
        // 이야아
        string effectiveClass = $"효과적인 타입: {(data.info.effectiveClass == Stat.ClassType.NOTYPE ? "없음" : data.info.effectiveClass.ToString()[0].ToString().ToUpper() + data.info.effectiveClass.ToString().Substring(1).ToLower())}\r\n효과적이지 않은 타입: {(data.info.uneffectiveClass == Stat.ClassType.NOTYPE ? "없음" : data.info.uneffectiveClass.ToString()[0].ToString().ToUpper() + data.info.uneffectiveClass.ToString().Substring(1).ToLower())}";
        string continues = $"{(data.info.isContinues ? $"{data.info.damage} 데미지가 {data.info.continuesCount} 번 나뉘어 들어갑니다." : "")}";
        string damage = $"{data.info.damage} 데미지를 가합니다.";


        // 이야아아
        skillData.text = $"{effectiveClass}\r\n{(continues != "" ? $"{continues}\r\n" : damage)}";
    }

    private void CallPopup()
    {
        if (isOpen)
        {
            Invoke(nameof(ToPos), moveDur);
            SetPopupData(prevData);
            Invoke(nameof(SetCurrentPopupData), moveDur);
        }
        ToPos();
    }

    // 뭔가 잘못된 함수
    private void SetCurrentPopupData()
    {
        SetPopupData(prevData);
    }

    private void ToPos()
    {
        infoObject.DOMove((isOpen ? closePos.position : openPos.position), moveDur).SetEase(isOpen ? Ease.InCubic : Ease.OutCubic);
        isOpen = !isOpen;
    }



}
