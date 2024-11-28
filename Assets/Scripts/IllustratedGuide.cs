using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustratedGuide : MonoBehaviour
{
    private TreasureHuntManager treasureHuntManager;
    private GameObject[] treasureObjects;
    private bool[] treasureIsFoundFlags;
    private string[] descriptionTexts;


    private void Awake()
    {
        treasureHuntManager = TreasureHuntManager.Instance;
        treasureObjects = treasureHuntManager.treasureObjects;
        treasureIsFoundFlags = treasureHuntManager.treasureIsFoundFlags;
        descriptionTexts = treasureHuntManager.descriptionTexts;
    }


}
