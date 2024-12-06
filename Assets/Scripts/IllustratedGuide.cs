using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// TODO: 싱글턴으로 만들어야 할 수도..
public class IllustratedGuide : MonoBehaviour
{
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

    //TODO : 위치를 땜뺑으로 잡아놨음.

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
        Transform a = transform.Find("Panel");
        if (a == null)
            Debug.Log("a");
        Transform b = a.Find("TreasureImages");
        if (b == null)
            Debug.Log("b");
        GameObject[] imagesObjects = GetChildren(b.gameObject);
        Debug.Log("flag length " + treasureIsFoundFlags.Length);
        for (int i = 0; i < treasureIsFoundFlags.Length; i++)
        {
            Debug.Log("in the loop " + i);
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
