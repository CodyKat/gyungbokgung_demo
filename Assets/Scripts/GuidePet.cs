using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidePet : MonoBehaviour
{
    private TreasureHuntManager treasureHuntManager;
    private IllustratedGuide IllustratedGuide;

    // Start is called before the first frame update
    void Start()
    {
        treasureHuntManager = TreasureHuntManager.Instance;
        IllustratedGuide = GameObject.FindObjectOfType<IllustratedGuide>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onClick()
    {
        //IllustratedGuide.loadFoundTreasure();
        treasureHuntManager.showIllustratedGuide();
    }
}
