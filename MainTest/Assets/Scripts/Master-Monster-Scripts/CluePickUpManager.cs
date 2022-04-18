using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePickUpManager : MonoBehaviour
{
    #region Werewolf Items
    [Header("Werewolf Clues")]
    public GameObject werewolfclue1;
    public GameObject werewolfclue2;
    public GameObject werewolfclue3;
    public GameObject werewolfclue4;
    private List<PlayerNearbyDetection> werewolfClues = new List<PlayerNearbyDetection>();

    PlayerNearbyDetection werewolf1;
    PlayerNearbyDetection werewolf2;
    PlayerNearbyDetection werewolf3;
    PlayerNearbyDetection werewolf4;
    #endregion

    #region Vampire Items
    [Header("Vampire Clues")]
    public GameObject vampireclue1;
    public GameObject vampireclue2;
    public GameObject vampireclue3;
    public GameObject vampireclue4;
    private List<PlayerNearbyDetection> vampireClues = new List<PlayerNearbyDetection>();

    PlayerNearbyDetection vampire1;
    PlayerNearbyDetection vampire2;
    PlayerNearbyDetection vampire3;
    PlayerNearbyDetection vampire4;
    #endregion

    #region Witch Items
    [Header("Witch Clues")]
    public GameObject witchclue1;
    public GameObject witchclue2;
    public GameObject witchclue3;
    public GameObject witchclue4;
    private List<PlayerNearbyDetection> witchClues = new List<PlayerNearbyDetection>();

    PlayerNearbyDetection witch1;
    PlayerNearbyDetection witch2;
    PlayerNearbyDetection witch3;
    PlayerNearbyDetection witch4;
    #endregion

    #region Demon Items
    [Header("Demon Clues")]
    public GameObject demonclue1;
    public GameObject demonclue2;
    public GameObject demonclue3;
    public GameObject demonclue4;
    private List<PlayerNearbyDetection> demonClues = new List<PlayerNearbyDetection>();

    PlayerNearbyDetection demon1;
    PlayerNearbyDetection demon2;
    PlayerNearbyDetection demon3;
    PlayerNearbyDetection demon4;
    #endregion

    private void Start()
    {
        #region Get Player Detection Scripts
        werewolf1 = werewolfclue1.GetComponent<PlayerNearbyDetection>();
        werewolf2 = werewolfclue2.GetComponent<PlayerNearbyDetection>();
        werewolf3 = werewolfclue3.GetComponent<PlayerNearbyDetection>();
        werewolf4 = werewolfclue4.GetComponent<PlayerNearbyDetection>();

        vampire1 = vampireclue1.GetComponent<PlayerNearbyDetection>();
        vampire2 = vampireclue2.GetComponent<PlayerNearbyDetection>();
        vampire3 = vampireclue3.GetComponent<PlayerNearbyDetection>();
        vampire4 = vampireclue4.GetComponent<PlayerNearbyDetection>();

        witch1 = witchclue1.GetComponent<PlayerNearbyDetection>();
        witch2 = witchclue2.GetComponent<PlayerNearbyDetection>();
        witch3 = witchclue3.GetComponent<PlayerNearbyDetection>();
        witch4 = witchclue4.GetComponent<PlayerNearbyDetection>();

        demon1 = demonclue1.GetComponent<PlayerNearbyDetection>();
        demon2 = demonclue2.GetComponent<PlayerNearbyDetection>();
        demon3 = demonclue3.GetComponent<PlayerNearbyDetection>();
        demon4 = demonclue4.GetComponent<PlayerNearbyDetection>();
        #endregion

        #region Add Clues to Lists
        werewolfClues.Add(werewolf1);
        werewolfClues.Add(werewolf2);
        werewolfClues.Add(werewolf3);
        werewolfClues.Add(werewolf4);

        vampireClues.Add(vampire1);
        vampireClues.Add(vampire2);
        vampireClues.Add(vampire3);
        vampireClues.Add(vampire4);

        witchClues.Add(witch1);
        witchClues.Add(witch2);
        witchClues.Add(witch3);
        witchClues.Add(witch4);

        demonClues.Add(demon1);
        demonClues.Add(demon2);
        demonClues.Add(demon3);
        demonClues.Add(demon4);
        #endregion
    }

    public void HandleClueInteraction()
    {
        foreach(PlayerNearbyDetection clue in werewolfClues)
        {
            clue.PickUpClue();
        }
        foreach (PlayerNearbyDetection clue in vampireClues)
        {
            clue.PickUpClue();
        }
        foreach (PlayerNearbyDetection clue in witchClues)
        {
            clue.PickUpClue();
        }
        foreach (PlayerNearbyDetection clue in demonClues)
        {
            clue.PickUpClue();
        }
    }
}
