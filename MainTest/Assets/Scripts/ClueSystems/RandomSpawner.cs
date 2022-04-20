using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    // Clue Objects
    [Header("Clue Objects")]
    public GameObject clue1;
    public GameObject clue2;
    public GameObject clue3;
    public GameObject clue4;

    // Clue Player Dection
    PlayerNearbyDetection clueNearby1;
    PlayerNearbyDetection clueNearby2;
    PlayerNearbyDetection clueNearby3;
    PlayerNearbyDetection clueNearby4;
	
	AudioSource audioSrc;

    void Awake()
    {
        // Random clue to load
        int randomClue = Random.Range(1, 5);

        switch(randomClue) {
            case 1:
                clue1.SetActive(true);
                clue2.SetActive(false);
                clue3.SetActive(false);
                clue4.SetActive(false);
                break;
            case 2:
                clue1.SetActive(false);
                clue2.SetActive(true);
                clue3.SetActive(false);
                clue4.SetActive(false);
                break;
            case 3:
                clue1.SetActive(false);
                clue2.SetActive(false);
                clue3.SetActive(true);
                clue4.SetActive(false);
                break;
            case 4:
                clue1.SetActive(false);
                clue2.SetActive(true);
                clue3.SetActive(false);
                clue4.SetActive(false);
                break;
            default:
                clue1.SetActive(true);
                clue2.SetActive(false);
                clue3.SetActive(false);
                clue4.SetActive(false);
                break;
        }
    }

    private void Start()
    {
        clueNearby1 = clue1.GetComponent<PlayerNearbyDetection>();
        clueNearby2 = clue2.GetComponent<PlayerNearbyDetection>();
        clueNearby3 = clue3.GetComponent<PlayerNearbyDetection>();
        clueNearby4 = clue4.GetComponent<PlayerNearbyDetection>();
		
		audioSrc = GetComponent<AudioSource>();
    }

    public void CluePickUpCheck()
    {
        if(clue1.activeSelf)
        {
            clueNearby1.PickUpClue();
			audioSrc.Play();
        }
        if (clue2.activeSelf)
        {
            clueNearby2.PickUpClue();
			audioSrc.Play();
        }
        if (clue3.activeSelf)
        {
            clueNearby3.PickUpClue();
			audioSrc.Play();
        }
        if (clue4.activeSelf)
        {
            clueNearby4.PickUpClue();
			audioSrc.Play();
        }
    }

    void Update()
    {
        
    }
}
