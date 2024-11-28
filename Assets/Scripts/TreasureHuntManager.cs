using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TreasureHuntManager : MonoBehaviour
{
    private static TreasureHuntManager _instance;
    private static object _synLock = new object();
    private GameObject[] treasureSpots;
    public GameObject[] treasureObjects;
    public bool[] treasureIsFoundFlags;
    public string[] illustrationTexts;


    protected TreasureHuntManager() { }
    public static TreasureHuntManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_synLock)
                {
                    _instance = FindObjectOfType(typeof(TreasureHuntManager)) as TreasureHuntManager;
                }
            }
            return _instance;
        }
    }
    

    private void Awake()
    {
        treasureSpots = GameObject.FindGameObjectsWithTag("treasureSpot");
        treasureObjects = GameObject.FindGameObjectsWithTag("treasure");
        treasureIsFoundFlags = new bool[treasureObjects.Length];
        illustrationTexts = new string[treasureObjects.Length];


        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

        for (int i = 0; i < treasureIsFoundFlags.Length; i++)
        {
            treasureIsFoundFlags[i] = false;
        }

        int[] spotIndices = GeneratorRandomNumber(1, treasureSpots.Length, treasureObjects.Length);
        for (int i = 0;i < spotIndices.Length; i++)
        {
            treasureObjects[i].transform.position = treasureSpots[spotIndices[i]].transform.position + new Vector3(0f, 5f, 0f);
            treasureObjects[i].transform.localScale = new Vector3(10f, 10f, 10f);
            treasureObjects[i].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.black;
        }
    }

    private void Update()
    {
        for (int i = 0; i < treasureIsFoundFlags.Length; i++)
        {
            if (treasureIsFoundFlags[i] == true)
            {
                Debug.Log(i);
            }
        }
    }

    public GameObject[] GetChildren(GameObject parent)
    {
        GameObject[] children = new GameObject[parent.transform.childCount];

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            children[i] = parent.transform.GetChild(i).gameObject;
        }

        return children;
    }

    public static int[] GeneratorRandomNumber(int min, int max, int count)
    {
        int[] intArray = new int[count];
        System.Random rand = new System.Random();

        for (int loop = 0; loop < count; loop++)
        {
            int randNumber = rand.Next(min, max);

            if (intArray.Contains(randNumber))
            {
                loop--;
            }
            else
            {
                intArray[loop] = randNumber;
            }
        }
        return intArray;
    }

    public void OpenIllustration(int treasureIndex)
    {
        
    }
}
