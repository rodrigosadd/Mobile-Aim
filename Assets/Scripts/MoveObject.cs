using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveObject : MonoBehaviour
{
    public float durantion;
    public float endPosition;

    void Start()
    {
        transform.DOMoveX(endPosition, durantion).SetLoops(-1, LoopType.Yoyo);
    }
}