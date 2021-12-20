using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    private int maxHealth = 100;
    private int currentHealth=100;

    public int touchDamage = 10; 
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animator.SetBool("dead",true);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Opossum>().enabled = false;
        this.enabled = false;
    }
}
