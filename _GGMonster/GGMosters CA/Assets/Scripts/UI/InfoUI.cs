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

    [Header("�̵� ����")]
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
        // ���ٷ� ����� ���� ����� �ҷ� �̸�Ų ����

        skillName.text = data.name;
        skillInfo.text = data.info.info;
        skillData.text = "";
        
        // �̾߾�
        string effectiveClass = $"ȿ������ Ÿ��: {(data.info.effectiveClass == Stat.ClassType.NOTYPE ? "����" : data.info.effectiveClass.ToString()[0].ToString().ToUpper() + data.info.effectiveClass.ToString().Substring(1).ToLower())}\r\nȿ�������� ���� Ÿ��: {(data.info.uneffectiveClass == Stat.ClassType.NOTYPE ? "����" : data.info.uneffectiveClass.ToString()[0].ToString().ToUpper() + data.info.uneffectiveClass.ToString().Substring(1).ToLower())}";
        string continues = $"{(data.info.isContinues ? $"{data.info.damage} �������� {data.info.continuesCount} �� ������ ���ϴ�." : "")}";
        string damage = $"{data.info.damage} �������� ���մϴ�.";


        // �̾߾ƾ�
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

    // ���� �߸��� �Լ�
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
