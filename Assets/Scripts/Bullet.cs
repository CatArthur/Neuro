using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform Player;

    public Transform Origin;

    public Transform LeftBottom;

    public Animator Animator;

    public float speed = 8f;
    public float attackRate = 1f;
    public int room = 3;
    public float explosionRange = 0.5f;
    public float buletSize = 0.3f;
    public int explosionDamage = 20;
    
    private float nextAttackTime = 0f;
    private Vector3 destination;
    private bool shooted=false;
    

    // Update is called once per frame
    void Update()
    {
        if (Player.position.x >= LeftBottom.position.x && !GlobalData.completedRooms[room])
        {
            if (Time.time > nextAttackTime&&GlobalData.activeEmo!=1)
            {
                Shoot();
                nextAttackTime = Time.time + 1 / attackRate;
            }
        }

        if (shooted)
        {
            transform.position =
                 Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if (Math.Abs(transform.position.y - LeftBottom.position.y) <= explosionRange
            ||Vector2.Distance(Player.position, transform.position) <= buletSize)
            {
                Explosion();
            }
        }

    }

    private void Shoot()
    {
        shooted = true;
        destination = new Vector3(Player.position.x, LeftBottom.position.y);
        transform.position = Origin.position;
        Animator.SetBool("Explosion",false);
    }
    
    private void Explosion()
    {
        shooted = false;
        Animator.SetBool("Explosion",true);
        if (Vector2.Distance(Player.position, transform.position) <= explosionRange)
        {
            Player.GetComponent<PlayerCombat>().TakeDamage(explosionDamage);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,explosionRange);
        Gizmos.DrawWireSphere(transform.position,buletSize);
    }
}