using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreasureHuntManager : MonoBehaviour
{
    private GameObject[] treasureSpots;
    public GameObject[] treasureObjects;
    // Start is called before the first frame update
    void Start()
    {
        treasureSpots = GameObject.FindGameObjectsWithTag("treasureSpot");
        treasureObjects = GameObject.FindGameObjectsWithTag("treasure");

        Debug.Log(treasureSpots.Length);
        Debug.Log(treasureObjects.Length);
        int[] spotIndices = generatorRandomNumber(1, treasureSpots.Length, treasureObjects.Length);
        Debug.Log("spotIndices = " + spotIndices.Length);
        for (int i = 0;i < spotIndices.Length; i++)
        {
            treasureObjects[i].transform.position = treasureSpots[spotIndices[i]].transform.position;
        }
        Debug.Log("load finish");
    }

    // Update is called once per frame
    void Update()
    {
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

    public static int[] generatorRandomNumber(int min, int max, int count)
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
}
