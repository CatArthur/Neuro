using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : MonoBehaviour
{
    public Transform left, right;
    public float speed = 5f;

    private bool m_FacingRight=false;

    void Update()
    {
        if(transform.localPosition.x<=left.localPosition.x||transform.localPosition.x>=right.localPosition.x)
            Flip();
        transform.localPosition =
            Vector3.MoveTowards(transform.localPosition, m_FacingRight ? right.localPosition : left.localPosition, speed * Time.deltaTime);
    }
    
    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
