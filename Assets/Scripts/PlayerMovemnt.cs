using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemnt : MonoBehaviour
{
    CharacterController chController;
    public float movementSpeed;
    float ActualSpeed;
    private Vector3 forwardMovement;
    private Vector3 rightMovement;
    private Vector3 Move;
    private float horiInput;
    private float vertInput;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float grav;
    

    private void Start()
    {
        chController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMovement();

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            movementSpeed = movementSpeed * speedMultiplier;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            movementSpeed = movementSpeed / speedMultiplier;

        }
    }

    void PlayerMovement()
    {
        horiInput = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        vertInput = Input.GetAxisRaw("Vertical") * Time.deltaTime;
        if (chController.isGrounded)
        {
            Move = new Vector3(horiInput,0,vertInput);
            Move = transform.TransformDirection(Move) * movementSpeed;
        }

        Move.y -= grav * Time.deltaTime;
        chController.Move(Move);
    }

}
