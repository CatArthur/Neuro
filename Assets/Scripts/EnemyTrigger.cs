using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public int index;

    private Enemy enemy;
    // Update is called once per frame
    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        if (!GlobalData.triggersActivated[index])
        {
            if (enemy.currentHealth <= 0)
            {
                GlobalData.triggersActivated[index] = true;
                GlobalData.checkRoom();
            }
        }
    }
}
