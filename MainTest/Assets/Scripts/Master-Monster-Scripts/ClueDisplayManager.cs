using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueDisplayManager : MonoBehaviour
{
    #region Werewolf Items
    [Header("Werewolf Display Items")]
    public GameObject werewolfDisplay;
    public GameObject werewolfBullet1;
    public GameObject werewolfBullet2;
    public GameObject werewolfBullet3;
    public GameObject werewolfBullet4;
    public GameObject werewolfClue1;
    public GameObject werewolfClue2;
    public GameObject werewolfClue3;
    public GameObject werewolfClue4;
    #endregion

    #region Vampire Items
    [Header("Vampire Display Items")]
    public GameObject vampireDisplay;
    public GameObject vampireBullet1;
    public GameObject vampireBullet2;
    public GameObject vampireBullet3;
    public GameObject vampireBullet4;
    public GameObject vampireClue1;
    public GameObject vampireClue2;
    public GameObject vampireClue3;
    public GameObject vampireClue4;
    #endregion

    #region Witch Items
    [Header("Witch Display Items")]
    public GameObject witchDisplay;
    public GameObject witchBullet1;
    public GameObject witchBullet2;
    public GameObject witchBullet3;
    public GameObject witchBullet4;
    public GameObject witchClue1;
    public GameObject witchClue2;
    public GameObject witchClue3;
    public GameObject witchClue4;
    #endregion

    #region Demon Items
    [Header("Demon Display Items")]
    public GameObject demonDisplay;
    public GameObject demonBullet1;
    public GameObject demonBullet2;
    public GameObject demonBullet3;
    public GameObject demonBullet4;
    public GameObject demonClue1;
    public GameObject demonClue2;
    public GameObject demonClue3;
    public GameObject demonClue4;
    #endregion

    public void OnSceneLoad(int monster)
    {
        if(monster == 1)
        {
            DisplayBullets(werewolfDisplay, werewolfBullet1, werewolfBullet2, werewolfBullet3, werewolfBullet4);
            HideClues(werewolfClue1,werewolfClue2,werewolfClue3, werewolfClue4);
        }
        else if(monster == 2)
        {
            DisplayBullets(vampireDisplay, vampireBullet1, vampireBullet2, vampireBullet3, vampireBullet4);
            HideClues(vampireClue1, vampireClue2, vampireClue3, vampireClue4);
        }
        else if(monster == 3)
        {
            DisplayBullets(witchDisplay, witchBullet1, witchBullet2, vampireBullet3, witchBullet4);
            HideClues(witchClue1, witchClue2, witchClue3, witchClue4);
        }
        else if(monster == 4)
        {
            DisplayBullets(demonDisplay, demonBullet1, demonBullet2, demonBullet3, demonBullet4);
            HideClues(demonClue1, demonClue2, demonClue3, demonClue4);
        }
    }

    public void DisableAllDisplays()
    {
        werewolfDisplay.SetActive(false);
        vampireDisplay.SetActive(false);
        witchDisplay.SetActive(false);
        demonDisplay.SetActive(false);
    }

    private void DisplayBullets(GameObject display, GameObject bullet1, GameObject bullet2, GameObject bullet3, GameObject bullet4)
    {
        display.SetActive(true);
        bullet1.SetActive(true);
        bullet2.SetActive(true);
        bullet3.SetActive(true);
        bullet4.SetActive(true);
    }

    private void HideClues(GameObject clue1, GameObject clue2, GameObject clue3, GameObject clue4)
    {
        clue1.SetActive(false);
        clue2.SetActive(false);
        clue3.SetActive(false);
        clue4.SetActive(false);
    }
}
