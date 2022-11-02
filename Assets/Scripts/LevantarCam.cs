using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevantarCam : MonoBehaviour
{
    private bool camArriba;
    [SerializeField]private Transform downTransform;
    [SerializeField] private Transform upTransform;
    private float cambioT;
    private float rateToCambio = 1.2f;
    private bool canChange = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && canChange)
        {
            canChange = false;
            if (camArriba)
            {
                Debug.Log("Entro aqui");
                gameObject.transform.DOMove(downTransform.position, 0.8f);
                //gameObject.transform.position = downTransform.position;
                //canChange = false;
                camArriba = false;
                CameraControl.camControlCode.canMoveCam = false;
                StartCoroutine(waitToMoveCam());
            }
            else
            {
                Debug.Log("y entro aqui tambien");
                gameObject.transform.DOMove(upTransform.position, 0.8f);
                //gameObject.transform.position = upTransform.position;
                //canChange = false;
                camArriba = true;
                CameraControl.camControlCode.canMoveCam = false;
                StartCoroutine(waitToMoveCam());

            }
        }

        IEnumerator waitToMoveCam()
        {
            yield return new WaitForSeconds(0.83f);
            CameraControl.camControlCode.canMoveCam = true;
        }

        if (!canChange)
        {
            cambioT += Time.deltaTime;
            if (cambioT < rateToCambio)
            {
                canChange = true;
                cambioT = 0;
            }
        }
        
    }
}
