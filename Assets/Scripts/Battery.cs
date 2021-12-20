using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public int emotion = 2;
    private float speed = 0.4f;
    public float range = 1.6f;
    public Transform player;
    public Transform table;
    
    private Vector3 scale;
    private Vector3 position;
    private float scaleStart = 0f;
    private float scaleEnd = 0.2211806f;
    private float posStart = -0.12f;
    private float posEnd = -0.009f;

    private float power=0f;
    private bool full = false;

    private void Start()
    {
        scale=transform.localScale;
        position=transform.localPosition;
        scale.y += scaleStart;
        position.y += posStart;
        transform.localScale = scale;
        transform.localPosition = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!full)
        {
            if (GlobalData.activeEmo == emotion && Vector2.Distance(player.position, table.position) < range)
            {
                power += speed * Time.deltaTime;
                if (power >= 1)
                {
                    power = 1;
                    full = true;
                    GlobalData.fullBatteries[emotion] = true;
                }
            }
            else
            {
                power = power <= 0 ? 0 : power - speed * Time.deltaTime;
            }

            scale.y = scaleStart + (scaleEnd - scaleStart) * power;
            position.y = posStart + (posEnd - posStart) * power;
            transform.localScale = scale;
            transform.localPosition = position;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(table.position,range);
    }
}
