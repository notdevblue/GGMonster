using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Unfade : MonoBehaviour
{
    [SerializeField] private float defadeTime = 0.5f;

    private void Awake()
    {
        GetComponent<UnityEngine.UI.Image>().DOFade(0, defadeTime);
    }
}
