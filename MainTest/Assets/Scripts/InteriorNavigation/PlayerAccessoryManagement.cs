using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAccessoryManagement : MonoBehaviour
{
    #region Accessory Objects
    [Header("Hair Objects")]
    public GameObject jockObject;
    public GameObject longBlackObject;
    public GameObject blondePonytailObject;

    [Header("Hat Objects")]
    public GameObject bucketHatObject;
    public GameObject baseballCapObject;
    public GameObject topHatObject;
    public GameObject antlersObject;
    public GameObject cowboyHatObject;

    [Header("Beard Objects")]
    public GameObject beardObject;

    [Header("Glasses Objects")]
    public GameObject glassesObject;
    #endregion

    #region Accessory Buttons
    [Header("Hair Buttons")]
    public Button noHairButton;
    public Button jockButton;
    public Button longBlackButton;
    public Button blondePonytailButton;

    [Header("Hat Buttons")]
    public Button noHatButton;
    public Button bucketHatButton;
    public Button baseballCapButton;
    public Button topHatButton;
    public Button antlersButton;
    public Button cowboyHatButton;

    [Header("Beard Buttons")]
    public Button noBeardButton;
    public Button beardButton;

    [Header("Glasses Buttons")]
    public Button noGlassesButton;
    public Button glassesButton;
    #endregion

    private Vector3 largeCursor;
    private Vector3 smallCursor;

    // Start is called before the first frame update
    void Start()
    {
        #region Accessory Objects
        jockObject.SetActive(false);
        longBlackObject.SetActive(false);
        blondePonytailObject.SetActive(false);

        bucketHatObject.SetActive(false);
        baseballCapObject.SetActive(false);
        topHatObject.SetActive(false);
        antlersObject.SetActive(false);
        cowboyHatObject.SetActive(false);

        beardObject.SetActive(false);

        glassesObject.SetActive(false);
        #endregion

        #region Accessory Buttons
        noHairButton.GetComponent<Image>().color = Color.red;
        jockButton.GetComponent<Image>().color = Color.white;
        longBlackButton.GetComponent<Image>().color = Color.white;
        blondePonytailButton.GetComponent<Image>().color = Color.white;

        noHatButton.GetComponent<Image>().color = Color.red;
        bucketHatButton.GetComponent<Image>().color = Color.white;
        baseballCapButton.GetComponent<Image>().color = Color.white;
        topHatButton.GetComponent<Image>().color = Color.white;
        antlersButton.GetComponent<Image>().color = Color.white;
        cowboyHatButton.GetComponent<Image>().color = Color.white;

        noBeardButton.GetComponent<Image>().color = Color.red;
        beardButton.GetComponent<Image>().color = Color.white;

        noGlassesButton.GetComponent<Image>().color = Color.red;
        glassesButton.GetComponent<Image>().color = Color.white;
        #endregion

        noHairButton.transform.localScale = new Vector3(0.011f, 0.014f, 0.01f);
        largeCursor = new Vector3(0.011f, 0.014f, 0.01f);
        smallCursor = new Vector3(0.01f, 0.01f, 0.01f);
    }

    public void HandleCursor(int newPosition)
    {
        switch(newPosition)
        {
            case 1:
                glassesButton.transform.localScale = smallCursor;
                noHairButton.transform.localScale = largeCursor;
                jockButton.transform.localScale = smallCursor;
                break;
            case 2:
                noHairButton.transform.localScale = smallCursor;
                jockButton.transform.localScale = largeCursor;
                longBlackButton.transform.localScale = smallCursor;
                break;
            case 3:
                jockButton.transform.localScale = smallCursor;
                longBlackButton.transform.localScale = largeCursor;
                blondePonytailButton.transform.localScale = smallCursor;
                break;
            case 4:
                longBlackButton.transform.localScale = smallCursor;
                blondePonytailButton.transform.localScale = largeCursor;
                noHatButton.transform.localScale = smallCursor;
                break;
            case 5:
                blondePonytailButton.transform.localScale = smallCursor;
                noHatButton.transform.localScale = largeCursor;
                bucketHatButton.transform.localScale = smallCursor;
                break;
            case 6:
                noHatButton.transform.localScale = smallCursor;
                bucketHatButton.transform.localScale = largeCursor;
                baseballCapButton.transform.localScale = smallCursor;
                break;
            case 7:
                bucketHatButton.transform.localScale = smallCursor;
                baseballCapButton.transform.localScale = largeCursor;
                topHatButton.transform.localScale = smallCursor;
                break;
            case 8:
                baseballCapButton.transform.localScale = smallCursor;
                topHatButton.transform.localScale = largeCursor;
                antlersButton.transform.localScale = smallCursor;
                break;
            case 9:
                topHatButton.transform.localScale = smallCursor;
                antlersButton.transform.localScale = largeCursor;
                cowboyHatButton.transform.localScale = smallCursor;
                break;
            case 10:
                antlersButton.transform.localScale = smallCursor;
                cowboyHatButton.transform.localScale = largeCursor;
                noBeardButton.transform.localScale = smallCursor;
                break;
            case 11:
                cowboyHatButton.transform.localScale = smallCursor;
                noBeardButton.transform.localScale = largeCursor;
                beardButton.transform.localScale = smallCursor;
                break;
            case 12:
                noBeardButton.transform.localScale = smallCursor;
                beardButton.transform.localScale = largeCursor;
                noGlassesButton.transform.localScale = smallCursor;
                break;
            case 13:
                beardButton.transform.localScale = smallCursor;
                noGlassesButton.transform.localScale = largeCursor;
                glassesButton.transform.localScale = smallCursor;
                break;
            case 14:
                noGlassesButton.transform.localScale = smallCursor;
                glassesButton.transform.localScale = largeCursor;
                noHairButton.transform.localScale = smallCursor;
                break;
        }
    }

    public void HandleButtonColorsAndObjects(int itemRequest)
    {
        switch(itemRequest)
        {
            case 1:
                // Set the colors
                HairButtonColors(1, 0, 0, 0);

                // Set the objects
                jockObject.SetActive(false);
                longBlackObject.SetActive(false);
                blondePonytailObject.SetActive(false);
                break;
            case 2:
                // Set the colors
                HairButtonColors(0, 1, 0, 0);

                // Set the objects
                jockObject.SetActive(true);
                longBlackObject.SetActive(false);
                blondePonytailObject.SetActive(false);
                break;
            case 3:
                // Set the colors
                HairButtonColors(0, 0, 1, 0);

                // Set the objects
                jockObject.SetActive(false);
                longBlackObject.SetActive(true);
                blondePonytailObject.SetActive(false);
                break;
            case 4:
                // Set the colors
                HairButtonColors(0, 0, 0, 1);

                // Set the objects
                jockObject.SetActive(false);
                longBlackObject.SetActive(false);
                blondePonytailObject.SetActive(true);
                break;
            case 5:
                // Set the colors
                HatButtonColors(1, 0, 0, 0, 0, 0);

                // Set the objects
                HatObjectToggle(1, 0, 0, 0, 0, 0);
                break;
            case 6:
                // Set the colors
                HatButtonColors(0, 1, 0, 0, 0, 0);

                // Set the objects
                HatObjectToggle(0, 1, 0, 0, 0, 0);
                break;
            case 7:
                // Set the colors
                HatButtonColors(0, 0, 1, 0, 0, 0);

                // Set the objects
                HatObjectToggle(0, 0, 1, 0, 0, 0);
                break;
            case 8:
                // Set the colors
                HatButtonColors(0, 0, 0, 1, 0, 0);

                // Set the objects
                HatObjectToggle(0, 0, 0, 1, 0, 0);
                break;
            case 9:
                // Set the colors
                HatButtonColors(0, 0, 0, 0, 1, 0);

                // Set the objects
                HatObjectToggle(0, 0, 0, 0, 1, 0);
                break;
            case 10:
                // Set the colors
                HatButtonColors(0, 0, 0, 0, 0, 1);

                // Set the objects
                HatObjectToggle(0, 0, 0, 0, 0, 1);
                break;
            case 11:
                // Set the colors
                noBeardButton.GetComponent<Image>().color = Color.red;
                beardButton.GetComponent<Image>().color = Color.white;

                // Set the objects
                beardObject.SetActive(false);
                break;
            case 12:
                // Set the colors
                noBeardButton.GetComponent<Image>().color = Color.white;
                beardButton.GetComponent<Image>().color = Color.red;

                // Set the objects
                beardObject.SetActive(true);
                break;
            case 13:
                // Set the colors
                noGlassesButton.GetComponent<Image>().color = Color.red;
                glassesButton.GetComponent<Image>().color = Color.white;

                // Set the objects
                glassesObject.SetActive(false);
                break;
            case 14:
                // Set the colors
                noGlassesButton.GetComponent<Image>().color = Color.white;
                glassesButton.GetComponent<Image>().color = Color.red;

                // Set the objects
                glassesObject.SetActive(true);
                break;
        }
    }

    private void HairButtonColors(int item1, int item2, int item3, int item4)
    {
        if(item1 == 1)
        {
            noHairButton.GetComponent<Image>().color = Color.red;
            jockButton.GetComponent<Image>().color = Color.white;
            longBlackButton.GetComponent<Image>().color = Color.white;
            blondePonytailButton.GetComponent<Image>().color = Color.white;
        }
        else if(item2 == 1)
        {
            noHairButton.GetComponent<Image>().color = Color.white;
            jockButton.GetComponent<Image>().color = Color.red;
            longBlackButton.GetComponent<Image>().color = Color.white;
            blondePonytailButton.GetComponent<Image>().color = Color.white;
        }
        else if(item3 == 1)
        {
            noHairButton.GetComponent<Image>().color = Color.white;
            jockButton.GetComponent<Image>().color = Color.white;
            longBlackButton.GetComponent<Image>().color = Color.red;
            blondePonytailButton.GetComponent<Image>().color = Color.white;
        }
        else if(item4 == 1)
        {
            noHairButton.GetComponent<Image>().color = Color.white;
            jockButton.GetComponent<Image>().color = Color.white;
            longBlackButton.GetComponent<Image>().color = Color.white;
            blondePonytailButton.GetComponent<Image>().color = Color.red;
        }
    }

    private void HatButtonColors(int i1, int i2, int i3, int i4, int i5, int i6)
    {
        if(i1 == 1)
        {
            noHatButton.GetComponent<Image>().color = Color.red;
            bucketHatButton.GetComponent<Image>().color = Color.white;
            baseballCapButton.GetComponent<Image>().color = Color.white;
            topHatButton.GetComponent<Image>().color = Color.white;
            antlersButton.GetComponent<Image>().color = Color.white;
            cowboyHatButton.GetComponent<Image>().color = Color.white;
        }
        else if(i2 == 1)
        {
            noHatButton.GetComponent<Image>().color = Color.white;
            bucketHatButton.GetComponent<Image>().color = Color.red;
            baseballCapButton.GetComponent<Image>().color = Color.white;
            topHatButton.GetComponent<Image>().color = Color.white;
            antlersButton.GetComponent<Image>().color = Color.white;
            cowboyHatButton.GetComponent<Image>().color = Color.white;
        }
        else if(i3 == 1)
        {
            noHatButton.GetComponent<Image>().color = Color.white;
            bucketHatButton.GetComponent<Image>().color = Color.white;
            baseballCapButton.GetComponent<Image>().color = Color.red;
            topHatButton.GetComponent<Image>().color = Color.white;
            antlersButton.GetComponent<Image>().color = Color.white;
            cowboyHatButton.GetComponent<Image>().color = Color.white;
        }
        else if(i4 == 1)
        {
            noHatButton.GetComponent<Image>().color = Color.white;
            bucketHatButton.GetComponent<Image>().color = Color.white;
            baseballCapButton.GetComponent<Image>().color = Color.white;
            topHatButton.GetComponent<Image>().color = Color.red;
            antlersButton.GetComponent<Image>().color = Color.white;
            cowboyHatButton.GetComponent<Image>().color = Color.white;
        }
        else if(i5 == 1)
        {
            noHatButton.GetComponent<Image>().color = Color.white;
            bucketHatButton.GetComponent<Image>().color = Color.white;
            baseballCapButton.GetComponent<Image>().color = Color.white;
            topHatButton.GetComponent<Image>().color = Color.white;
            antlersButton.GetComponent<Image>().color = Color.red;
            cowboyHatButton.GetComponent<Image>().color = Color.white;
        }
        else if(i6 == 1)
        {
            noHatButton.GetComponent<Image>().color = Color.white;
            bucketHatButton.GetComponent<Image>().color = Color.white;
            baseballCapButton.GetComponent<Image>().color = Color.white;
            topHatButton.GetComponent<Image>().color = Color.white;
            antlersButton.GetComponent<Image>().color = Color.white;
            cowboyHatButton.GetComponent<Image>().color = Color.red;
        }
    }

    private void HatObjectToggle(int i1, int i2, int i3, int i4, int i5, int i6)
    {
        if (i1 == 1)
        {
            bucketHatObject.SetActive(false);
            baseballCapObject.SetActive(false);
            topHatObject.SetActive(false);
            antlersObject.SetActive(false);
            cowboyHatObject.SetActive(false);
        }
        else if (i2 == 1)
        {
            bucketHatObject.SetActive(true);
            baseballCapObject.SetActive(false);
            topHatObject.SetActive(false);
            antlersObject.SetActive(false);
            cowboyHatObject.SetActive(false);
        }
        else if (i3 == 1)
        {
            bucketHatObject.SetActive(false);
            baseballCapObject.SetActive(true);
            topHatObject.SetActive(false);
            antlersObject.SetActive(false);
            cowboyHatObject.SetActive(false);
        }
        else if (i4 == 1)
        {
            bucketHatObject.SetActive(false);
            baseballCapObject.SetActive(false);
            topHatObject.SetActive(true);
            antlersObject.SetActive(false);
            cowboyHatObject.SetActive(false);
        }
        else if (i5 == 1)
        {
            bucketHatObject.SetActive(false);
            baseballCapObject.SetActive(false);
            topHatObject.SetActive(false);
            antlersObject.SetActive(true);
            cowboyHatObject.SetActive(false);
        }
        else if (i6 == 1)
        {
            bucketHatObject.SetActive(false);
            baseballCapObject.SetActive(false);
            topHatObject.SetActive(false);
            antlersObject.SetActive(false);
            cowboyHatObject.SetActive(true);
        }
    }
}
