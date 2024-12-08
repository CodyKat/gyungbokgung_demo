using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class IllustratedGuide : MonoBehaviour
{
    private static IllustratedGuide _instance;
    private static object _synLock = new object();
    private TreasureHuntManager treasureHuntManager;
    private GameObject[] treasureObjects;
    private bool[] treasureIsFoundFlags;
    private string[] descriptionTexts;
    private float frontDistance = 8f;
    private float moveSpeed = 3f;

    private PanelHandler illustratedGuidePanel;

    private GameObject player;
    private Transform xrCamera;

    private Texture2D[] treasureImages;


    protected IllustratedGuide() { }
    public static IllustratedGuide Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_synLock)
                {
                    _instance = FindObjectOfType(typeof(IllustratedGuide)) as IllustratedGuide;
                }
            }
            return _instance;
        }
    }


    private void Start()
    {
        treasureHuntManager = TreasureHuntManager.Instance;
        treasureObjects = treasureHuntManager.treasureObjects;
        treasureIsFoundFlags = treasureHuntManager.treasureIsFoundFlags;
        treasureImages = treasureHuntManager.treasureImages;

        illustratedGuidePanel = transform.Find("Panel").GetComponent<PanelHandler>();

        player = GameObject.Find("Player");
        xrCamera = Camera.main.transform;
    }

    //TODO : ��ġ�� �������� ��Ƴ���.

    private void Update()
    {
        Vector3 targetPosition = xrCamera.position + xrCamera.forward * frontDistance + new Vector3(0, 2f, 0);
        transform.LookAt(xrCamera.position);
        transform.Rotate(new Vector3(0, 180, 0));
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    public void loadFoundTreasure()
    {
        Vector3 targetPosition = xrCamera.position + xrCamera.forward * frontDistance + new Vector3(0, 2f, 0);

        Debug.Log("in loadFoundTreasure");
        int imageIndex = 0;
        GameObject[] imagesObjects = GetChildren(transform.Find("Panel").Find("TreasureImages").gameObject);
        Debug.Log("flag length " + treasureIsFoundFlags.Length);
        for (int i = 0; i < treasureIsFoundFlags.Length; i++)
        {
            if (treasureIsFoundFlags[i] == false) continue;
            Sprite spriteImage = Sprite.Create(
                treasureImages[i],
                new Rect(0, 0, treasureImages[i].width, treasureImages[i].height),
                new Vector2(0.5f, 0.5f)
            );
            imagesObjects[imageIndex].transform.GetComponent<Image>().sprite = spriteImage;
            imageIndex++;
        }
        transform.position = targetPosition;
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
}
