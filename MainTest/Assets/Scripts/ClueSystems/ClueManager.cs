using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueManager : MonoBehaviour
{
    // Clue Objects
    public GameObject clue1;
    public GameObject clue2;
    public GameObject clue3;
    public GameObject clue4;

    // Clue Spawners
    RandomSpawner spawner1;
    RandomSpawner spawner2;
    RandomSpawner spawner3;
    RandomSpawner spawner4;


    public bool itemInteraction;

    // Start is called before the first frame update
    void Start()
    {
        // RandomSpawner clue1 = GetComponentInChildren<RandomSpawner>();
        // RandomSpawner clue2 = GetComponentInChildren<RandomSpawner>();

        spawner1 = clue1.GetComponent<RandomSpawner>();
        spawner2 = clue2.GetComponent<RandomSpawner>();
        spawner3 = clue3.GetComponent<RandomSpawner>();
        spawner4 = clue4.GetComponent<RandomSpawner>();

        // clue1 = GetComponent<RandomSpawner>();
        // clue1.clueFound = false;
    }

    public void HandleCluePick()
    {
        // Debug.Log("Picked up clue");
        spawner1.CluePickUpCheck();
        spawner2.CluePickUpCheck();
        spawner3.CluePickUpCheck();
        spawner4.CluePickUpCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
