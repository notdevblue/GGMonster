using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeonHanAI : AIBase
{
    #region ����
    
    [Header("��ų ����Ʈ")]
    [SerializeField] private int[] skillPointArr = new int[4];

    [Header("HP")]
    [SerializeField] private int hp = 100;

    [Header("Ÿ��")]
    [SerializeField] private Stat.ClassType myType = Stat.ClassType.NOTYPE;

    #endregion

    private void Awake()
    {
        Debug.Log("SeonHanAI");
        //Init(skillPointArr, hp, myType); <= AI �� ���� ���� �־�� ��
    }


}
