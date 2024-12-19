using System;
using System.Linq;
using UnityEngine;
using System.IO;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using Unity.VisualScripting;


public class TreasureHuntManager : MonoBehaviour
{
    private static TreasureHuntManager _instance;
    private TreasureDescription treasureDescription;
    private static object _synLock = new object();
    private GameObject[] treasureSpots;
    public GameObject player;
    public GameObject[] treasureObjects;
    public GameObject[] treasureImageObjects;
    public bool[] treasureIsFoundFlags;
    public TextAsset[] descriptionTexts_kor;
    public TextAsset[] descriptionTexts_eng;
    public Texture2D[] treasureImages;
    private GameObject illustratedGuideCanvas;
    private PanelHandler illustratedGuidePanel;
    public Sprite[] spriteImages;


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
        LoadTreasureImages();
        player = GameObject.Find("Player");

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

        for (int i = 0; i < treasureIsFoundFlags.Length; i++)
        {
            treasureIsFoundFlags[i] = false;
        }

        Debug.Log("treasureObjects count " + treasureObjects.Length);
        Debug.Log("treasureSpots count " + treasureSpots.Length);

        int[] spotIndices = GeneratorRandomNumber(1, treasureSpots.Length, treasureObjects.Length);
        Debug.Log("spotIndices count " + spotIndices.Length);
        for (int i = 0;i < spotIndices.Length; i++)
        {
            Debug.Log("in TreasureHuntManager loop " + i);
            treasureObjects[i].transform.position = treasureSpots[spotIndices[i]].transform.position;
            Debug.Log("treasureObjects position " + treasureObjects[i].transform.position);
            treasureObjects[i].transform.localScale = new Vector3(10f, 10f, 10f);
        }
        treasureDescription = GameObject.Find("TreasureDescription").GetComponent<TreasureDescription>();

        illustratedGuideCanvas = GameObject.Find("IllustratedGuide");
        illustratedGuidePanel = illustratedGuideCanvas.transform.Find("Panel").GetComponent<PanelHandler>();
    }

    private void Update()
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
        descriptionTexts_kor = new TextAsset[treasureObjects.Length];
        descriptionTexts_eng = new TextAsset[treasureObjects.Length];
        for (int i = 0; i < treasureObjects.Length; i++)
        {
            string descriptionTextFilePath = Constants.DESCRIPTIONS_PATH + treasureObjects[i].name + '_' + SystemLanguage.Korean;
            descriptionTexts_kor[i] = Resources.Load(descriptionTextFilePath) as TextAsset;
            if (descriptionTexts_kor[i] == null)
                Debug.LogError("not file found PATH :" + descriptionTextFilePath);
            descriptionTextFilePath = Constants.DESCRIPTIONS_PATH + treasureObjects[i].name + '_' + SystemLanguage.English;
            descriptionTexts_eng[i] = Resources.Load(descriptionTextFilePath) as TextAsset;
            if (descriptionTexts_eng[i] == null)
                Debug.LogError("not file found PATH :" + descriptionTextFilePath);
        }
    }

    private void LoadTreasureImages()
    {
        treasureImages = new Texture2D[treasureObjects.Length];
        spriteImages = new Sprite[treasureImages.Length];
        for (int i = 0; i < treasureObjects.Length; i++)
        {
            string imageFilePath = Constants.TREASURE_IMAGE_PATH + "treasure_image_" + i;
            treasureImages[i] = Resources.Load(imageFilePath) as Texture2D;
            if (treasureImages[i] == null)
                Debug.LogError("treasureImage" + i + "is not found!!");
            spriteImages[i] = Sprite.Create(
                treasureImages[i],
                new Rect(0, 0, treasureImages[i].width, treasureImages[i].height),
                new Vector2(0.5f, 0.5f)
            );
        }
    }
}
