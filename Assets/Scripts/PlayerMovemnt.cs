using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemnt : MonoBehaviour
{
    CharacterController chController;
    public float movementSpeed;
    float ActualSpeed;

    private void Start()
    {
        chController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMovement();

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            movementSpeed = movementSpeed * 2;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            movementSpeed = movementSpeed / 2;

        }
    }

    void PlayerMovement()
    {
        float horiInput = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
        float vertInput = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horiInput;

        chController.SimpleMove(forwardMovement + rightMovement);
    }

}
