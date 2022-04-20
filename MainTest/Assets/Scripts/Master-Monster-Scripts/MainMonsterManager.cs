using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainMonsterManager : MonoBehaviour
{
    #region Urban Map Coordinates
    // Game Objects for the Spawn Cubes
    [Header("Urban Map Spawn Locations")]
    public GameObject UrbanMonsterSpawn;
    public GameObject UrbanClue1Spawn;
    public GameObject UrbanClue2Spawn;
    public GameObject UrbanClue3Spawn;
    public GameObject UrbanClue4Spawn;

    // Coordinates for the Spawn Cubes
    private Vector3 UrbanMonsterSpawnCoordinates;
    private Vector3 UrbanClue1SpawnCoordinates;
    private Vector3 UrbanClue2SpawnCoordinates;
    private Vector3 UrbanClue3SpawnCoordinates;
    private Vector3 UrbanClue4SpawnCoordinates;
    #endregion

    #region Suburb Map Coordinates
    // Game Objects for the Spawn Cubes
    [Header("Urban Map Spawn Locations")]
    public GameObject SuburbMonsterSpawn;
    public GameObject SuburbClue1Spawn;
    public GameObject SuburbClue2Spawn;
    public GameObject SuburbClue3Spawn;
    public GameObject SuburbClue4Spawn;

    // Coordinates for the Spawn Cubes
    private Vector3 SuburbMonsterSpawnCoordinates;
    private Vector3 SuburbClue1SpawnCoordinates;
    private Vector3 SuburbClue2SpawnCoordinates;
    private Vector3 SuburbClue3SpawnCoordinates;
    private Vector3 SuburbClue4SpawnCoordinates;
    #endregion

    #region Map3 Map Coordinates
    // Game Objects for the Spawn Cubes
    [Header("Urban Map Spawn Locations")]
    public GameObject Map3MonsterSpawn;
    public GameObject Map3Clue1Spawn;
    public GameObject Map3Clue2Spawn;
    public GameObject Map3Clue3Spawn;
    public GameObject Map3Clue4Spawn;

    // Coordinates for the Spawn Cubes
    private Vector3 Map3MonsterSpawnCoordinates;
    private Vector3 Map3Clue1SpawnCoordinates;
    private Vector3 Map3Clue2SpawnCoordinates;
    private Vector3 Map3Clue3SpawnCoordinates;
    private Vector3 Map3Clue4SpawnCoordinates;
    #endregion

    #region Map4 Map Coordinates
    // Game Objects for the Spawn Cubes
    [Header("Urban Map Spawn Locations")]
    public GameObject Map4MonsterSpawn;
    public GameObject Map4Clue1Spawn;
    public GameObject Map4Clue2Spawn;
    public GameObject Map4Clue3Spawn;
    public GameObject Map4Clue4Spawn;

    // Coordinates for the Spawn Cubes
    private Vector3 Map4MonsterSpawnCoordinates;
    private Vector3 Map4Clue1SpawnCoordinates;
    private Vector3 Map4Clue2SpawnCoordinates;
    private Vector3 Map4Clue3SpawnCoordinates;
    private Vector3 Map4Clue4SpawnCoordinates;
    #endregion

    #region Werewolf Objects
    // (1) Werewolf Game Objects
    [Header("Werewolf Game Objects")]
    public GameObject werewolfMonsterMain;
    public GameObject werewolfClue1;
    public GameObject werewolfClue2;
    public GameObject werewolfClue3;
    public GameObject werewolfClue4;
    #endregion

    #region Vampire Objects
    // (2) Vampire Game Objects
    [Header("Vampire Game Objects")]
    public GameObject vampirefMonsterMain;
    public GameObject vampireClue1;
    public GameObject vampireClue2;
    public GameObject vampireClue3;
    public GameObject vampireClue4;
    #endregion

    #region Witch Objects
    // (3) Witch Game Objects
    [Header("Witch Game Objects")]
    public GameObject witchMonsterMain;
    public GameObject witchClue1;
    public GameObject witchClue2;
    public GameObject witchClue3;
    public GameObject witchClue4;
    #endregion

    #region Demon Objects
    // (4) Demon Game Objects
    [Header("Demon Game Objects")]
    public GameObject demonMonsterMain;
    public GameObject demonClue1;
    public GameObject demonClue2;
    public GameObject demonClue3;
    public GameObject demonClue4;
    #endregion

    #region Guess Machines
    [Header("Guess Machine Objects")]
    public GameObject urbanGuessMachine;
    public GameObject suburbGuessMachine;
    public GameObject map3GuessMachine;
    public GameObject map4GuessMachine;
    GuessManager urbanGuessObjects;
    GuessManager suburbGuessObjects;
    GuessManager map3GuessObjects;
    GuessManager map4GuessObjects;
    #endregion

    #region Return Machines
    [Header("Return Machine Objects")]
    public GameObject urbanReturnMachine;
    ReturnToBase urbanReturn;
    public GameObject suburbReturnMachine;
    ReturnToBase suburbReturn;
    public GameObject map3ReturnMachine;
    ReturnToBase map3Return;
    public GameObject map4ReturnMachine;
    ReturnToBase map4Return;
    #endregion

    #region Monster AI Scripts
    Werewolf_Master werewolfAI;
    Vampire_Master vampireAI;
    Witch_Master witchAI;
    Demon_Master demonAI;
    #endregion

    public int urbanMonsterSpawn;
    public int suburbMonsterSpawn;
    public int map3MonsterSpawn;
    public int map4MonsterSpawn;

    // Randomize Lists
    private List<int> listRandom = new List<int>() { 1, 2, 3, 4 };

    // Madness Score Items
    [Header("Madness Items")]
    public Text madnessDisplay;
    public int madnessScore;
    private bool startUrbanMadnessClock;
    private bool startSuburbMadnessClock;
    private bool startMap3MadnessClock;
    private bool startMap4MadnessClock;

    // Madness Clock Items
    protected float Timer;
    public float DelayAmount = 0.5f;

    //gameover screen
    public GameObject gameOverScreen;
    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        #region Set the Spawn Points
        #region Set Urban Spawn Coordinates
        UrbanMonsterSpawnCoordinates = UrbanMonsterSpawn.transform.position;
        UrbanClue1SpawnCoordinates = UrbanClue1Spawn.transform.position;
        UrbanClue2SpawnCoordinates = UrbanClue2Spawn.transform.position;
        UrbanClue3SpawnCoordinates = UrbanClue3Spawn.transform.position;
        UrbanClue4SpawnCoordinates = UrbanClue4Spawn.transform.position;
        #endregion

        #region Set Suburb Spawn Coordinates
        SuburbMonsterSpawnCoordinates = SuburbMonsterSpawn.transform.position;
        SuburbClue1SpawnCoordinates = SuburbClue1Spawn.transform.position;
        SuburbClue2SpawnCoordinates = SuburbClue2Spawn.transform.position;
        SuburbClue3SpawnCoordinates = SuburbClue3Spawn.transform.position;
        SuburbClue4SpawnCoordinates = SuburbClue4Spawn.transform.position;
        #endregion

        #region Set Map3 Spawn Coordinates
        Map3MonsterSpawnCoordinates = Map3MonsterSpawn.transform.position;
        Map3Clue1SpawnCoordinates = Map3Clue1Spawn.transform.position;
        Map3Clue2SpawnCoordinates = Map3Clue2Spawn.transform.position;
        Map3Clue3SpawnCoordinates = Map3Clue3Spawn.transform.position;
        Map3Clue4SpawnCoordinates = Map3Clue4Spawn.transform.position;
        #endregion

        #region Set Map4 Spawn Coordinates
        Map4MonsterSpawnCoordinates = Map4MonsterSpawn.transform.position;
        Map4Clue1SpawnCoordinates = Map4Clue1Spawn.transform.position;
        Map4Clue2SpawnCoordinates = Map4Clue2Spawn.transform.position;
        Map4Clue3SpawnCoordinates = Map4Clue3Spawn.transform.position;
        Map4Clue4SpawnCoordinates = Map4Clue4Spawn.transform.position;
        #endregion
        #endregion

        #region Guess Components
        urbanGuessObjects = urbanGuessMachine.GetComponent<GuessManager>();
        suburbGuessObjects = suburbGuessMachine.GetComponent<GuessManager>();
        map3GuessObjects = map3GuessMachine.GetComponent<GuessManager>();
        map4GuessObjects = map4GuessMachine.GetComponent<GuessManager>();
        #endregion

        #region Return Components
        urbanReturn = urbanReturnMachine.GetComponent<ReturnToBase>();
        suburbReturn = suburbReturnMachine.GetComponent<ReturnToBase>();
        map3Return = map3ReturnMachine.GetComponent<ReturnToBase>();
        map4Return = map4ReturnMachine.GetComponent<ReturnToBase>();
        #endregion

        #region AI Components
        werewolfAI = werewolfMonsterMain.GetComponent<Werewolf_Master>();
        vampireAI = vampirefMonsterMain.GetComponent<Vampire_Master>();
        witchAI = witchMonsterMain.GetComponent<Witch_Master>();
        demonAI = demonMonsterMain.GetComponent<Demon_Master>();
        #endregion

        #region Other Items
        // Randomize the Spawn Locations
        RandomizeTheList();

        // Gets the monster spawn int values
        urbanMonsterSpawn = listRandom[0];
        suburbMonsterSpawn = listRandom[1];
        map3MonsterSpawn = listRandom[2];
        map4MonsterSpawn = listRandom[3];

        // Sets the Spawn location objects
        SetSpawnObjects();

        // Disables Monsters on Load
        werewolfMonsterMain.SetActive(false);
        vampirefMonsterMain.SetActive(false);
        witchMonsterMain.SetActive(false);
        demonMonsterMain.SetActive(false);
        #endregion

        madnessScore = 0;
        startUrbanMadnessClock = false;
        startSuburbMadnessClock = false;
        startMap3MadnessClock = false;
        startMap4MadnessClock = false;

        //hides game over at start
        gameOverScreen.SetActive(false);
    }

    public Text madnessText;

    public void GameOver()
    {
        //put number from GAMEOVER CANVAS HERE
        madnessText.text = madnessScore.ToString();
    }
    private void Update()
    {
        if (isGameOver == true)
        {
            gameOverScreen.SetActive(true);
            GameOver();
        }
            

        // Madness Clock Items
        Timer += Time.deltaTime;
        if(startUrbanMadnessClock || startSuburbMadnessClock || startMap3MadnessClock || startMap4MadnessClock)
        {
            if(Timer >= DelayAmount)
            {
                Timer = 0f;
                madnessScore++;
                madnessDisplay.text = madnessScore.ToString();
            }
        }

        if(urbanGuessObjects.spawnMonster)
        {
            SpawnUrbanMonster();
        }
        if(suburbGuessObjects.spawnMonster)
        {
            SpawnSuburbMonster();
        }
        if(map3GuessObjects.spawnMonster)
        {
            SpawnMap3Monster();
        }
        if(map4GuessObjects.spawnMonster)
        {
            SpawnMap4Monster();
        }

        #region Check if Monsters Defeated
        if (werewolfAI.werewolfIsDefeated)
        {
            if(listRandom[0] == 1)
            {
                urbanReturn.LocalMonsterDefeated = true;
                startUrbanMadnessClock = false;
            }
            else if(listRandom[1] == 1)
            {
                suburbReturn.LocalMonsterDefeated = true;
                startSuburbMadnessClock = false;
            }
            else if(listRandom[2] == 1)
            {
                map3Return.LocalMonsterDefeated = true;
                startMap3MadnessClock = false;
            }
            else if(listRandom[3] == 1)
            {
                map4Return.LocalMonsterDefeated = true;
                startMap4MadnessClock = false;
            }
        }
        if (vampireAI.vampireIsDefeated)
        {
            if (listRandom[0] == 2)
            {
                urbanReturn.LocalMonsterDefeated = true;
                startUrbanMadnessClock = false;
            }
            else if (listRandom[1] == 2)
            {
                suburbReturn.LocalMonsterDefeated = true;
                startSuburbMadnessClock = false;
            }
            else if (listRandom[2] == 2)
            {
                map3Return.LocalMonsterDefeated = true;
                startMap3MadnessClock = false;
            }
            else if (listRandom[3] == 2)
            {
                map4Return.LocalMonsterDefeated = true;
                startMap4MadnessClock = false;
            }
        }
        if (witchAI.witchIsDefeated)
        {
            if (listRandom[0] == 3)
            {
                urbanReturn.LocalMonsterDefeated = true;
                startUrbanMadnessClock = false;
            }
            else if (listRandom[1] == 3)
            {
                suburbReturn.LocalMonsterDefeated = true;
                startSuburbMadnessClock = false;
            }
            else if (listRandom[2] == 3)
            {
                map3Return.LocalMonsterDefeated = true;
                startMap3MadnessClock = false;
            }
            else if (listRandom[3] == 3)
            {
                map4Return.LocalMonsterDefeated = true;
                startMap4MadnessClock = false;
            }
        }
        if (demonAI.demnonIsDefeated)
        {
            if (listRandom[0] == 4)
            {
                urbanReturn.LocalMonsterDefeated = true;
                startUrbanMadnessClock = false;
            }
            else if (listRandom[1] == 4)
            {
                suburbReturn.LocalMonsterDefeated = true;
                startSuburbMadnessClock = false;
            }
            else if (listRandom[2] == 4)
            {
                map3Return.LocalMonsterDefeated = true;
                startMap3MadnessClock = false;
            }
            else if (listRandom[3] == 4)
            {
                map4Return.LocalMonsterDefeated = true;
                startMap4MadnessClock = false;
            }
        }
        #endregion
    }

    private void RandomizeTheList()
    {
        // Randomly Sorts the list
        for(int i = 0; i < listRandom.Count; i++)
        {
            int temp = listRandom[i];
            int randomIndex = Random.Range(i, listRandom.Count);
            listRandom[i] = listRandom[randomIndex];
            listRandom[randomIndex] = temp;
        }
    }

    private void SetSpawnObjects()
    {
        SetUrbanSpawnObjects();
        SetSuburbSpawnObjects();
        SetMap3SpawnObjects();
        SetMap4SpawnObjects();
    }

    #region Set Map Spawn Objects
    private void SetUrbanSpawnObjects()
    {
        if (listRandom[0] == 1)
        {
            SetWerewolfSpawns(UrbanMonsterSpawnCoordinates, UrbanClue1SpawnCoordinates, UrbanClue2SpawnCoordinates, UrbanClue3SpawnCoordinates, UrbanClue4SpawnCoordinates);
            urbanGuessObjects.SetClueObjects(1, werewolfClue1, werewolfClue2, werewolfClue3, werewolfClue4);
        }
        else if (listRandom[0] == 2)
        {
            SetVampireSpawns(UrbanMonsterSpawnCoordinates, UrbanClue1SpawnCoordinates, UrbanClue2SpawnCoordinates, UrbanClue3SpawnCoordinates, UrbanClue4SpawnCoordinates);
            urbanGuessObjects.SetClueObjects(2, vampireClue1, vampireClue2, vampireClue3,vampireClue4);
        }
        else if(listRandom[0] == 3)
        {
            SetWitchSpawns(UrbanMonsterSpawnCoordinates, UrbanClue1SpawnCoordinates, UrbanClue2SpawnCoordinates, UrbanClue3SpawnCoordinates, UrbanClue4SpawnCoordinates);
            urbanGuessObjects.SetClueObjects(3, witchClue1, witchClue2,witchClue3,witchClue4);
        }
        else if (listRandom[0] == 4)
        {
            SetDemonSpawns(UrbanMonsterSpawnCoordinates, UrbanClue1SpawnCoordinates, UrbanClue2SpawnCoordinates, UrbanClue3SpawnCoordinates, UrbanClue4SpawnCoordinates);
            urbanGuessObjects.SetClueObjects(4, demonClue1, demonClue2,demonClue3,demonClue4);
        }
    }

    private void SetSuburbSpawnObjects()
    {
        if (listRandom[1] == 1)
        {
            SetWerewolfSpawns(SuburbMonsterSpawnCoordinates, SuburbClue1SpawnCoordinates, SuburbClue2SpawnCoordinates, SuburbClue3SpawnCoordinates, SuburbClue4SpawnCoordinates);
            suburbGuessObjects.SetClueObjects(1, werewolfClue1, werewolfClue2, werewolfClue3, werewolfClue4);
        }
        else if (listRandom[1] == 2)
        {
            SetVampireSpawns(SuburbMonsterSpawnCoordinates, SuburbClue1SpawnCoordinates, SuburbClue2SpawnCoordinates, SuburbClue3SpawnCoordinates, SuburbClue4SpawnCoordinates);
            suburbGuessObjects.SetClueObjects(2, vampireClue1, vampireClue2, vampireClue3, vampireClue4);
        }
        else if (listRandom[1] == 3)
        {
            SetWitchSpawns(SuburbMonsterSpawnCoordinates, SuburbClue1SpawnCoordinates, SuburbClue2SpawnCoordinates, SuburbClue3SpawnCoordinates, SuburbClue4SpawnCoordinates);
            suburbGuessObjects.SetClueObjects(3, witchClue1, witchClue2, witchClue3, witchClue4);
        }
        else if (listRandom[1] == 4)
        {
            SetDemonSpawns(SuburbMonsterSpawnCoordinates, SuburbClue1SpawnCoordinates, SuburbClue2SpawnCoordinates, SuburbClue3SpawnCoordinates, SuburbClue4SpawnCoordinates);
            suburbGuessObjects.SetClueObjects(4, demonClue1, demonClue2, demonClue3, demonClue4);
        }
    }

    private void SetMap3SpawnObjects()
    {
        if (listRandom[2] == 1)
        {
            SetWerewolfSpawns(Map3MonsterSpawnCoordinates, Map3Clue1SpawnCoordinates, Map3Clue2SpawnCoordinates, Map3Clue3SpawnCoordinates, Map3Clue4SpawnCoordinates);
            map3GuessObjects.SetClueObjects(1, werewolfClue1, werewolfClue2, werewolfClue3, werewolfClue4);
        }
        else if (listRandom[2] == 2)
        {
            SetVampireSpawns(Map3MonsterSpawnCoordinates, Map3Clue1SpawnCoordinates, Map3Clue2SpawnCoordinates, Map3Clue3SpawnCoordinates, Map3Clue4SpawnCoordinates);
            map3GuessObjects.SetClueObjects(2, vampireClue1, vampireClue2, vampireClue3, vampireClue4);
        }
        else if (listRandom[2] == 3)
        {
            SetWitchSpawns(Map3MonsterSpawnCoordinates, Map3Clue1SpawnCoordinates, Map3Clue2SpawnCoordinates, Map3Clue3SpawnCoordinates, Map3Clue4SpawnCoordinates);
            map3GuessObjects.SetClueObjects(3, witchClue1, witchClue2, witchClue3, witchClue4);
        }
        else if (listRandom[2] == 4)
        {
            SetDemonSpawns(Map3MonsterSpawnCoordinates, Map3Clue1SpawnCoordinates, Map3Clue2SpawnCoordinates, Map3Clue3SpawnCoordinates, Map3Clue4SpawnCoordinates);
            map3GuessObjects.SetClueObjects(4, demonClue1, demonClue2, demonClue3, demonClue4);
        }
    }

    private void SetMap4SpawnObjects()
    {
        if (listRandom[3] == 1)
        {
            SetWerewolfSpawns(Map4MonsterSpawnCoordinates, Map4Clue1SpawnCoordinates, Map4Clue2SpawnCoordinates, Map4Clue3SpawnCoordinates, Map4Clue4SpawnCoordinates);
            map4GuessObjects.SetClueObjects(1, werewolfClue1, werewolfClue2, werewolfClue3, werewolfClue4);
        }
        else if (listRandom[3] == 2)
        {
            SetVampireSpawns(Map4MonsterSpawnCoordinates, Map4Clue1SpawnCoordinates, Map4Clue2SpawnCoordinates, Map4Clue3SpawnCoordinates, Map4Clue4SpawnCoordinates);
            map4GuessObjects.SetClueObjects(2, vampireClue1, vampireClue2, vampireClue3, vampireClue4);
        }
        else if (listRandom[3] == 3)
        {
            SetWitchSpawns(Map4MonsterSpawnCoordinates, Map4Clue1SpawnCoordinates, Map4Clue2SpawnCoordinates, Map4Clue3SpawnCoordinates, Map4Clue4SpawnCoordinates);
            map4GuessObjects.SetClueObjects(3, witchClue1, witchClue2, witchClue3, witchClue4);
        }
        else if(listRandom[3] == 4)
        {
            SetDemonSpawns(Map4MonsterSpawnCoordinates, Map4Clue1SpawnCoordinates, Map4Clue2SpawnCoordinates, Map4Clue3SpawnCoordinates, Map4Clue4SpawnCoordinates);
            map4GuessObjects.SetClueObjects(4, demonClue1, demonClue2, demonClue3, demonClue4);
        }
    }
    #endregion

    #region Set Monster Spawns
    private void SetWerewolfSpawns(Vector3 monsterCoords, Vector3 clue1Coords, Vector3 clue2Coords, Vector3 clue3Coords, Vector3 clue4Coords)
    {
        werewolfMonsterMain.transform.position = monsterCoords;
        werewolfClue1.transform.position = clue1Coords;
        werewolfClue2.transform.position = clue2Coords;
        werewolfClue3.transform.position = clue3Coords;
        werewolfClue4.transform.position = clue4Coords;
    }

    private void SetVampireSpawns(Vector3 monsterCoords, Vector3 clue1Coords, Vector3 clue2Coords, Vector3 clue3Coords, Vector3 clue4Coords)
    {
        vampirefMonsterMain.transform.position = monsterCoords;
        vampireClue1.transform.position = clue1Coords;
        vampireClue2.transform.position = clue2Coords;
        vampireClue3.transform.position = clue3Coords;
        vampireClue4.transform.position = clue4Coords;
    }

    private void SetWitchSpawns(Vector3 monsterCoords, Vector3 clue1Coords, Vector3 clue2Coords, Vector3 clue3Coords, Vector3 clue4Coords)
    {
        witchMonsterMain.transform.position = monsterCoords;
        witchClue1.transform.position = clue1Coords;
        witchClue2.transform.position = clue2Coords;
        witchClue3.transform.position = clue3Coords;
        witchClue4.transform.position = clue4Coords;
    }

    private void SetDemonSpawns(Vector3 monsterCoords, Vector3 clue1Coords, Vector3 clue2Coords, Vector3 clue3Coords, Vector3 clue4Coords)
    {
        demonMonsterMain.transform.position = monsterCoords;
        demonClue1.transform.position = clue1Coords;
        demonClue2.transform.position = clue2Coords;
        demonClue3.transform.position = clue3Coords;
        demonClue4.transform.position = clue4Coords;
    }
    #endregion

    #region Start Monster Fight
    private void SpawnUrbanMonster()
    {
        StartMonsterFight(listRandom[0]);
        startUrbanMadnessClock = true;
    }

    private void SpawnSuburbMonster()
    {
        StartMonsterFight(listRandom[1]);
        startSuburbMadnessClock = true;
    }

    private void SpawnMap3Monster()
    {
        StartMonsterFight(listRandom[2]);
        startMap3MadnessClock = true;
    }

    private void SpawnMap4Monster()
    {
        StartMonsterFight(listRandom[3]);
        startMap4MadnessClock = true;
    }

    private void StartMonsterFight(int monster)
    {
        if (monster == 1)
        {
            werewolfMonsterMain.SetActive(true);
        }
        else if (monster == 2)
        {
            vampirefMonsterMain.SetActive(true);
        }
        else if (monster == 3)
        {
            witchMonsterMain.SetActive(true);
        }
        else if (monster == 4)
        {
            demonMonsterMain.SetActive(true);
        }
    }
    #endregion

    public void AddMadness(int more)
    {
        madnessScore += more;
    }
}
