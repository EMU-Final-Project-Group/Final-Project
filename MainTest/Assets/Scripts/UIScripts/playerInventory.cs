using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{

    public string[] clues = new string[5];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool hasGoo()
    {
        for(int i=0;i<= clues.Length; i++)
        {
            if (clues[i].ToLower().Equals("goo"))
            {
                return true;
            }
        }

        return false;
    }
}
