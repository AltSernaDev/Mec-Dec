using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Vector3 velocity;
    [SerializeField] private Transform[] memoryObjects;
    private int objCounter;
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private float maxSpeed = 100f;
    private Vector3 target;
    [SerializeField] private bool following = true;
    [SerializeField] private Transform player;
    
    
    void Update()
    {
        if (following)
        {
            target = new Vector3(memoryObjects[objCounter].position.x, transform.position.y,
                memoryObjects[objCounter].position.z);
        
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity,
                smoothTime, maxSpeed);
        
            transform.LookAt(target);
        }
        else
        {
            transform.LookAt(player.position);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject == memoryObjects[objCounter].gameObject)
        {
            other.transform.DOMove(other.transform.position+(Vector3.down*5), 4.3f);
            Destroy(other.gameObject,5); // se desvanece
            objCounter++;
            if (objCounter >= memoryObjects.Length)
            {
                Destroy(this.gameObject);
            }
        }

        /*if (other.CompareTag("Memory"))
        {
            Destroy(other.gameObject); // se desvanece
            objCounter++;
        }*/
    }
}
