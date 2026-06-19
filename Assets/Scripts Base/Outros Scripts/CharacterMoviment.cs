using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMoviment : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;

    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool isRunning = false;
    private Vector2 move;
    


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

  

    void Update()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Mover();
        Correr();
        
    }


    void Mover()
    {
        if(canMove){
            if (isRunning)
            {
                rig.linearVelocity = move.normalized * runSpeed;
            }
            else
            {
                rig.linearVelocity = move.normalized * speed;
            }
        }
        else
        {
            move = Vector2.zero;
        }
        
    }
    void Correr()
    {
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            isRunning = true;
        }

        if (Keyboard.current.leftShiftKey.wasReleasedThisFrame)
        {
            isRunning = false;
        }
    }
}
