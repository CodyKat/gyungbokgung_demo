using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidePet : MonoBehaviour
{
    private TreasureHuntManager treasureHuntManager;
    private PanelHandler menuPanel;

    // Start is called before the first frame update
    void Start()
    {
        treasureHuntManager = TreasureHuntManager.Instance;
        menuPanel = transform.Find("Menu").Find("Panel").GetComponent<PanelHandler>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onClick()
    {
        menuPanel.Show();
        var seq = DOTween.Sequence();

        seq.Play().OnComplete(() => {
            menuPanel.Show();
        });
    }
}
