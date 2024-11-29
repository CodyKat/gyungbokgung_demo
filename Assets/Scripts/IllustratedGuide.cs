using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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


    private void Awake()
    {
        treasureHuntManager = TreasureHuntManager.Instance;
        treasureObjects = treasureHuntManager.treasureObjects;
        treasureIsFoundFlags = treasureHuntManager.treasureIsFoundFlags;

        illustratedGuidePanel = transform.Find("Panel").GetComponent<PanelHandler>();

        player = GameObject.Find("Player");
        xrCamera = Camera.main.transform;
    }

    //TODO : À§Ä¡¸¦ ¶«»±À¸·Î Àâ¾Æ³ùÀ½.

    private void Update()
    {
        Vector3 targetPosition = xrCamera.position + xrCamera.forward * frontDistance + new Vector3(0, 2f, 0);
        transform.LookAt(xrCamera.position);
        transform.Rotate(new Vector3(0, 180, 0));
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

}
