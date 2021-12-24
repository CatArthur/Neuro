using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer deathScreen;

    private void Update()
    {
        if (Input.GetKeyDown("y"))
        {
            if (GlobalData.playerHealth == 0)
            {
                Revivify();
            }
        }
    }

    public void Die()
    {
        deathScreen.enabled = true;
        animator.SetBool("Dead",true);
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerCombat>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    public void Revivify()
    {
        deathScreen.enabled = false;
        animator.SetBool("Dead",false);
        GetComponent<PlayerController>().enabled = true;
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerCombat>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        transform.position = GlobalData.respawn.position;
        GetComponent<PlayerCombat>().Heal(100);
    }
}
