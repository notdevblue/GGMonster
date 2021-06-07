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
        if(false) // TODO : 어떠한 조건이 필요
        {
            NextTurn();
        }
    }


    private void NextTurn()
    {
        ++turn;
    }
}
