using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    
    public Transform attackPoint;
    public Animator animator;
    public LayerMask enemyLayer;
    public float attackRange = 0.5f;


    public int attackDamage = 40;
    public int healStep = 10;
    private float attackRate = 3f;
    private float damageRate = 4f;
    private float healingRate = 1f;

    private int maxHealth=100;
    private int currentHealth=100;

    private float nextAttackTime = 0f;
    private float nextDamageTime = 0f;
    private float nextHealingTime = 0f;
    // Update is called once per frame


    void Update()
    {
        UpdatingEmotions();
        if (Input.GetKeyDown("h"))
        {
            if (Time.time > nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }
    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

    }

    public void TakeDamage(int damage)
    {
        nextDamageTime = Time.time + 1f / damageRate;
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GetComponent<PlayerDeath>().Die();
        }
        GlobalData.playerHealth = (float)currentHealth / maxHealth;
    }
    
    public void Heal(int heal)
    {
        currentHealth += heal;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
        GlobalData.playerHealth = (float)currentHealth / maxHealth;
    }


    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            if (Time.time > nextDamageTime)
            {
                TakeDamage(other.collider.GetComponent<Enemy>().touchDamage);
            }
        }

    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spikes"))
        {
            if (Time.time > nextDamageTime)
            {
                TakeDamage(other.GetComponent<Spikes>().touchDamage);
            }
        }
        if (other.CompareTag("Quit"))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    void UpdatingEmotions() {
        if (GlobalData.activeEmo==2)
            attackDamage = 1000;
        else
        {
            attackDamage = 40;
        }

        if (GlobalData.activeEmo == 0)
        {
            if (Time.time > nextHealingTime)
            {
                Heal(healStep);
                nextHealingTime = Time.time + 1f / healingRate;
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint==null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
}
