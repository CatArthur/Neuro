using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int room;
    public Animator animator;
    
    private bool closed = true;
    void FixedUpdate()
    {
        if (closed)
        {
            if (GlobalData.completedRooms[room])
            {
                closed = false;
                animator.SetBool("open", true);
                GetComponent<Collider2D>().enabled = false;
            }

        }
    }
}
