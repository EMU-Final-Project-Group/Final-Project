using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pick : MonoBehaviour
{

    public GameObject Pick;

    public GameObject winScreen;
    public GameObject loseScreen;


    // Update is called once per frame
    void Start()
    {
        Pick.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);



    }
}
