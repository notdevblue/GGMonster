using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHistory : MonoBehaviour
{
    public static Stack<string> sceneHistory = new Stack<string>();

    private void OnDestroy()
    {
        sceneHistory.Push(gameObject.scene.name);
    }
}