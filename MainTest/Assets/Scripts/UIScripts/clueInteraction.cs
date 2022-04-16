using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clueInteraction : MonoBehaviour
{
    playerInventory theInventory;


    public GameObject clue;
    public Text[] slots = new Text[3];
    public List<Text> clueList;
    public GameControls display;
    public string Cluename;
    public bool hasGoo;
    public List<string> objects;
    //bool isOpen;
    public GameObject journal;
    public void Start()
    {
        clue.SetActive(false);
        hasGoo = false;
        journal.SetActive(false);
        //isOpen = false;

    }

    //white clue name in position

    public void Update()
    {
        //open or close
        if (Input.GetKeyDown(KeyCode.J))
        {
            journal.SetActive(!journal.activeSelf);
        }
        


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "clue")
        {
            Debug.Log(collision.gameObject.name);
            Cluename = collision.gameObject.name;
            clue.SetActive(true);
            hasGoo = true;
            objects.Add(Cluename);
            //if slot full go to next one and print
            for(int i = 0; i<= slots.Length; i++)
            {
                if (slots[i].text == "")
                {
                    slots[i].text = collision.gameObject.name;
                    break;
                }
                    
            }
            //detroy self
            Destroy(collision.gameObject);
        }
    }
}


