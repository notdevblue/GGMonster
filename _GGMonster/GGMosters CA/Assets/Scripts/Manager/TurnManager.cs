using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance = null;

    public uint turn = 0;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("TurnManager: I exists more than one");
        }
        instance = this;
    }
    
    private void Update()
    {
        if(false) // TODO : ��� ������ �ʿ�
        {
            NextTurn();
        }
    }


    private void NextTurn()
    {
        ++turn;
    }
}
