using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidePet : MonoBehaviour
{
    private TreasureHuntManager treasureHuntManager;

    // Start is called before the first frame update
    void Start()
    {
        treasureHuntManager = TreasureHuntManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onClick()
    {
        treasureHuntManager.showIllustratedGuide();
    }
}
