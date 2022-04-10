using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vanInteraction : MonoBehaviour
{
    playerInventory theInventory;
    public clueInteraction player;
    public GameObject vanMenu;
    // Start is called before the first frame update
    void Start()
    {
        vanMenu.SetActive(false);
    }
 
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            vanMenu.SetActive(true);
        }
    }

    public void analize()
    {
        if (player.hasGoo)
        {
            Debug.Log("Analyzing...");
        }
        else
            Debug.Log("Nothing to Analyze");

    }
}
