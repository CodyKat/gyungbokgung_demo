using System;
using System.Linq;
using UnityEngine;
using System.IO;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;


public class TreasureHuntManager : MonoBehaviour
{
    private static TreasureHuntManager _instance;
    private static object _synLock = new object();
    private GameObject[] treasureSpots;
    private GameObject player;
    public GameObject[] treasureObjects;
    public bool[] treasureIsFoundFlags;
    public TextAsset[] descriptionTexts;
    public Texture2D[] treasureImages;
    private GameObject descriptionCanvas;
    private PanelHandler descriptionPanel;
    private GameObject illustratedGuideCanvas;
    private PanelHandler illustratedGuidePanel;


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

        int[] spotIndices = GeneratorRandomNumber(1, treasureSpots.Length, treasureObjects.Length);
        for (int i = 0;i < spotIndices.Length; i++)
        {
            treasureObjects[i].transform.position = treasureSpots[spotIndices[i]].transform.position + new Vector3(0f, 5f, 0f);
            treasureObjects[i].transform.localScale = new Vector3(10f, 10f, 10f);
            treasureObjects[i].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.black;
        }

        descriptionCanvas = GameObject.Find("TreasureDescription");
        descriptionPanel = descriptionCanvas.transform.Find("Panel").GetComponent<PanelHandler>();

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
        descriptionTexts = new TextAsset[treasureObjects.Length];
        for (int i = 0; i < treasureObjects.Length; i++)
        {
            string descriptionTextFilePath = Constants.DESCRIPTIONS_PATH + i + '_' + PlayerSetting.Instance.language;
            descriptionTexts[i] = Resources.Load(descriptionTextFilePath) as TextAsset;
            if (descriptionTexts[i] == null)
                Debug.LogError("not file found PATH :" + descriptionTextFilePath);
        }
    }

    private void LoadTreasureImages()
    {
        treasureImages = new Texture2D[treasureObjects.Length];
        for (int i = 0; i < treasureObjects.Length; i++)
        {
            string imageFilePath = Constants.TREASURE_IMAGE_PATH + "treasure_image_" + i;
            treasureImages[i] = Resources.Load(imageFilePath) as Texture2D;
            if (treasureImages[i] == null)
                Debug.LogError("treasureImage" + i + "is not found!!");
        }
    }

    public void showIllustratedGuide()
    {
        illustratedGuidePanel.Show();
        var seq = DOTween.Sequence();

        seq.Play().OnComplete(() => {
            illustratedGuidePanel.Show();
        });
    }

    public void showDescription(int treasureIndex)
    {
        GameObject foundTreasure = treasureObjects[treasureIndex];
        Vector3 treasurePos = foundTreasure.transform.position;
        Vector3 playerPos = player.transform.position;
        Vector3 directionVec = Vector3.Normalize(playerPos - treasurePos);
        Vector3 descriptionPos = treasurePos + foundTreasure.transform.localScale.x * directionVec / 2;
        descriptionPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>().text
            = descriptionTexts[treasureIndex].ToString();

        Sprite spriteImage = Sprite.Create(
        treasureImages[treasureIndex],
        new Rect(0, 0, treasureImages[treasureIndex].width, treasureImages[treasureIndex].height),
                new Vector2(0.5f, 0.5f)
            );
        descriptionPanel.transform.Find("Image").GetComponent<Image>().sprite = spriteImage;

        if (descriptionPanel == null)
        {
            Debug.Log("Description Board Object not found!!!");
        }

        descriptionCanvas.transform.position = descriptionPos;
        // �ѹ��� �÷��̾ �ٶ󺸰� �ϰ� ������ -playerpos�� LookAt�� �ص� ��ü�� �ٶ�
        descriptionCanvas.transform.LookAt(playerPos);
        descriptionCanvas.transform.Rotate(new Vector3(0, 180, 0));

        

        descriptionPanel.Show();
        var seq = DOTween.Sequence();

        seq.Play().OnComplete(() => {
            descriptionPanel.Show();
        });
    }
}
