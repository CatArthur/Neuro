using System;
using UnityEngine;

using DefaultNamespace;

public class PlayerMovement : MonoBehaviour {

    public PlayerController controller;
    public Animator animator;

    private float defaultRunSpeed = 40f;
    private float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
	
    // Update is called once per frame
    void Update ()
    {
        UpdatingEmotions();
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Math.Abs(horizontalMove));

        if (Input.GetKeyDown("j"))
        {
            jump = true;
        }

    }

    void FixedUpdate ()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
    
    public void OnJumping()
    {
        animator.SetBool("Jumping", true);
    }

    public void OnLanding()
    {
        animator.SetBool("Jumping", false);
    }

    void UpdatingEmotions() {
        if (GlobalData.activeEmo==3)
    	    runSpeed = 100f;
        else
        {
    	    runSpeed = defaultRunSpeed;
        }
        
    }
}