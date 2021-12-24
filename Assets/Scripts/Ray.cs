using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    public Transform Player;
    public Transform LeftTop;
    public Transform RightBottom;
    public Transform RightBottomTriggerArea;
    public float speed;
    private Vector3 origin;
    private Vector3 destination;
    private bool moving=false;

    private void Start()
    {
        origin=new Vector3(LeftTop.position.x,transform.position.y, transform.position.z);
        destination=new Vector3(RightBottom.position.x,transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.position.x >= LeftTop.position.x
            && Player.position.y <= LeftTop.position.y
            && Player.position.x <= RightBottomTriggerArea.position.x
            && Player.position.y >= RightBottomTriggerArea.position.y)
        {
            moving = true;
            transform.position = origin;
        }

        if (moving)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if (transform.position.x >= RightBottom.position.x)
                moving = false;
        }
    }
}
