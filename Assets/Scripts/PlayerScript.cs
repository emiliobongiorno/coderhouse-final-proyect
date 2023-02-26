using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public Animator anim;
    public float moveSpeed = 1.0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Debería ser FixedUpdate si se usan fisicas, pero no me permite direccionar al jugador a los costados con el cursor
    void Update() 
    {
       Move();
    }



    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveInput = new Vector3(horizontal,0,vertical);

        if (moveInput == Vector3.zero)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * 10, 0);
            //transform.rotation = Quaternion.LookRotation(moveInput);
        }

        transform.Translate(moveInput * moveSpeed * Time.deltaTime);
        //rb.MovePosition(transform.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision col) 
    {
        Debug.Log("Collision with " + col.gameObject.name);
        switch(col.gameObject.tag)
        {
        case "Hint":
            Debug.Log("This is a Hint");
            HintScript script = col.gameObject.GetComponent<HintScript>();
            script.TurnOffLight();
            break;
        case "Whistler":
            Debug.Log("End game: You loose");
            break;
        case "Exit":
            Debug.Log("End game: You win");
            break;
        }

    }
    

}
