using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            // print("Enemy Hit");
        }
    }
}
