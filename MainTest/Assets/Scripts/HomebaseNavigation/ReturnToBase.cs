using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToBase : MonoBehaviour
{
    // Gets the Player Detection script
    PlayerNearbyDetection playerDetection;

    // Gets the controls input
    ControllingPlayer playerControls;

    [Header("Game Objects")]
    public GameObject player;
    public GameObject homeBaseObject;
    HomeBaseInteractions homeBaseInterface;

    public GameObject homeBaseSpawn;
    private Vector3 homeBaseSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerDetection = GetComponent<PlayerNearbyDetection>();
        homeBaseInterface = homeBaseObject.GetComponent<HomeBaseInteractions>();

        homeBaseSpawnPoint = homeBaseSpawn.transform.position;
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new ControllingPlayer();

            playerControls.MenuActions.Select.performed += i => ReturnToHomeBase();
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void ReturnToHomeBase()
    {
        if(playerDetection.PlayerDistanceCheck())
        {
            player.transform.position = homeBaseSpawnPoint;
            homeBaseInterface.EnableHomebaseItems();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetection.PlayerDistanceCheck())
        {
            // prompt.SetActive(false);
            // DisableGuessScreen();
            // guessScreenOpen = false;
        }
    }
}
