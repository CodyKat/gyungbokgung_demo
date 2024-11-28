using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using DG.Tweening;


public class TreasureHuntManager : MonoBehaviour
{
    private static TreasureHuntManager _instance;
    private static object _synLock = new object();
    private GameObject[] treasureSpots;
    private GameObject player;
    public GameObject[] treasureObjects;
    public bool[] treasureIsFoundFlags;
    public string[] descriptionTexts;


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
        LoadDescriptionTexts();
        player = GameObject.Find("Player");

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

    private void LoadDescriptionTexts()
    {
        descriptionTexts = new string[treasureObjects.Length];
        for (int i = 0; i < treasureObjects.Length; i++)
        {
            string descriptionTextFilePath = Constants.DESCRIPTIONS_PATH + i + '_' + PlayerSetting.Instance.language + ".txt";
            Debug.Log("Language =" + Application.systemLanguage);
            FileInfo fileInfo = new FileInfo(descriptionTextFilePath);
            if (fileInfo.Exists)
            {
                StreamReader reader = new StreamReader(descriptionTextFilePath);
                descriptionTexts[i] = reader.ReadToEnd();
                reader.Close();
            }
            else
            {
                Debug.Log("not file found PATH :" + descriptionTextFilePath);
            }
        }
    }

    public void showDescription(PanelHandler descriptionBoard)
    {
        if (descriptionBoard == null)
        {
            Debug.Log("Description Board Object not found!!!");
        }
        descriptionBoard.Show();
        var seq = DOTween.Sequence();

        seq.Play().OnComplete(() => {
            descriptionBoard.Show();
        });
    }
}
