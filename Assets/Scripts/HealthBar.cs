using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    private Vector3 scale;
    private Vector3 position;
    private float scaleMax = 0.5f;
    private float scaleMin = 0f;
    private float posMax = 0;
    private float posMin = -0.25f;

    private float playerHealth;

    private void Start()
    {
        scale=transform.localScale;
        position=transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = GlobalData.playerHealth;
        scale.x = scaleMin+(scaleMax - scaleMin) * playerHealth;
        position.x = posMin+(posMax - posMin) * playerHealth;
        transform.localScale = scale;
        transform.localPosition = position;
    }
}
