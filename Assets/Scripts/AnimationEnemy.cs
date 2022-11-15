using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimationEnemy : MonoBehaviour
{
    private bool up;
    [SerializeField] private Transform[] bodyParts;
    [SerializeField] private float speed;

    private void Start()
    {
        Moving();
    }

    void Moving()
    {
        foreach (Transform part in bodyParts)
        {
            if (up)
            {
                up =! up;
                part.DOMove(part.position + (Vector3.up / speed), 0.15f);
            }
            else
            {
                up =! up;
                part.DOMove(part.position + (Vector3.down / speed), 0.15f);
            }
        }
        Invoke("Moving",0.6f);
    }
}
