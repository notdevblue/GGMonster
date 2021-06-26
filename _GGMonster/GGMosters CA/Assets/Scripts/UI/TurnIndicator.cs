using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurnIndicator : MonoBehaviour
{
    [Header("이동 관련")]
    [SerializeField] private RectTransform enemyPos;
    [SerializeField] private RectTransform playerPos;
    [SerializeField] private float moveDur;

    [Header("표시 관련")]
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;

    private void Start()
    {
        transform.position = TurnManager.instance.playerTurn ? playerPos.position : enemyPos.position;
        SetArrow();
        TurnManager.instance.turnEndTasks.Add(ChangeIndicatorLocation);
    }



    private void ChangeIndicatorLocation()
    {
        transform.DOMove(TurnManager.instance.playerTurn ? playerPos.position : enemyPos.position, moveDur).SetEase(Ease.InOutQuad).OnComplete(SetArrow);
    }

    private void SetArrow()
    {
        if(TurnManager.instance.playerTurn)
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(false);
        }
        else
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
        }
    }
}
