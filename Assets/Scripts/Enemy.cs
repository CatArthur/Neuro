using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    public int currentHealth=100;

    public int touchDamage = 15;

    public bool immortal = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    void Update()
    {
        if(GetComponent<Collider2D>().CompareTag("Enemy"))
            GetComponent<Collider2D>().enabled = GlobalData.activeEmo != 1;
    }

    public void TakeDamage(int damage)
    {
        if (!immortal)
        {
            currentHealth -= damage;

            animator.SetTrigger("Hurt");
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        animator.SetBool("dead",true);
        GetComponent<Collider2D>().enabled = false;
        if(GetComponent<Collider2D>().CompareTag("Enemy"))
            GetComponent<EnemyMovement>().enabled = false;
        if(GetComponent<Collider2D>().CompareTag("Crate"))
            GetComponent<Rigidbody2D>().gravityScale=0;
        this.enabled = false;
    }
}
