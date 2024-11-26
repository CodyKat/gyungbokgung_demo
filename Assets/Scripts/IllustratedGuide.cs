using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustratedGuideManager : MonoBehaviour
{
    private TreasureHuntManager treasureHuntManager;
    private GameObject[] treasureObjects;
    

    private void Awake()
    {
        treasureHuntManager = TreasureHuntManager.Instance;
        treasureObjects = treasureHuntManager.treasureObjects;
    }


}
