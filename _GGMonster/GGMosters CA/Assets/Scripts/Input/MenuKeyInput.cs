using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuKeyInput : MonoBehaviour
{
    [Header("Å° ¸ÅÇÎ")]
    [SerializeField] private KeyCode select1    = KeyCode.Space;
    [SerializeField] private KeyCode select2    = KeyCode.Return;
    [SerializeField] private KeyCode moveUp1    = KeyCode.W;
    [SerializeField] private KeyCode moveUp2    = KeyCode.UpArrow;
    [SerializeField] private KeyCode moveDown1  = KeyCode.S;
    [SerializeField] private KeyCode moveDown2  = KeyCode.DownArrow;
    [SerializeField] private KeyCode moveLeft1  = KeyCode.A;
    [SerializeField] private KeyCode moveLeft2  = KeyCode.LeftArrow;
    [SerializeField] private KeyCode moveRight1 = KeyCode.D;
    [SerializeField] private KeyCode moveRight2 = KeyCode.RightArrow;

    public bool inputSelect { get; private set; }
    public bool inputUp     { get; private set; }
    public bool inputDown   { get; private set; }
    public bool inputLeft   { get; private set; }
    public bool inputRight  { get; private set; }



    public void Update()
    {
        inputSelect = Input.GetKeyDown(select1)    || Input.GetKeyDown(select2);
        inputUp     = Input.GetKeyDown(moveUp1)    || Input.GetKeyDown(moveUp2);
        inputDown   = Input.GetKeyDown(moveDown1)  || Input.GetKeyDown(moveDown2);
        inputLeft   = Input.GetKeyDown(moveLeft1)  || Input.GetKeyDown(moveLeft2);
        inputRight  = Input.GetKeyDown(moveRight1) || Input.GetKeyDown(moveRight2);
    }
}
