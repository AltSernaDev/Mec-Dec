using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(TurnOff());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
