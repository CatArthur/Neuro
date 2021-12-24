using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Hint : MonoBehaviour
{
    public int room;

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().enabled = GlobalData.currentRoom == room;
        if (Input.GetKeyDown("[5]"))
        {
            Debug.Log(GlobalData.currentRoom);
        }
    }
    
}
