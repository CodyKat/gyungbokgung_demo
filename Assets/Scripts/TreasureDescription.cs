using DG.Tweening;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class TreasureDescription : MonoBehaviour
{
    private PanelHandler descriptionPanel;
    private TreasureHuntManager treasureHuntManager;
    private IllustratedGuide illustratedGuide;
    private TextAsset[] descriptionTexts;
    private Sprite[] spriteImages;
    public bool lookPlayerWhenShow = true;
    public bool alwaysLookPlayer = false;
    public bool dockInIllustratedGuide = false;


    void Start()
    {
        descriptionPanel = transform.Find("Panel").GetComponent<PanelHandler>();
        treasureHuntManager = TreasureHuntManager.Instance;
        illustratedGuide = IllustratedGuide.Instance;
        descriptionTexts = treasureHuntManager.descriptionTexts;
        spriteImages = treasureHuntManager.spriteImages;
    }

    // Update is called once per frame
    void Update()
    {
        if (dockInIllustratedGuide)
        {
            Vector3 pos = illustratedGuide.transform.position 
                + new Vector3(0, -illustratedGuide.width / 2, 0)
                + new Vector3(0, -GetComponent<RectTransform>().rect.width / 2, 0);
            lookPlayer();
            setPosition(pos);
        }
        else if (alwaysLookPlayer)
            lookPlayer();
    }
    // Start is called before the first frame update
    private void setText(int treasureIndex)
    {
        descriptionPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>().text
            = descriptionTexts[treasureIndex].ToString();
    }

    private void setImage(int treasureIndex)
    {
        descriptionPanel.transform.Find("Image").GetComponent<Image>().sprite = spriteImages[treasureIndex];
    }

    private void setPosition(Vector3 position)
    {
        transform.position = position;
    }

    private void lookPlayer()
    {
        Vector3 playerPos = treasureHuntManager.player.transform.position;
        transform.LookAt(playerPos);
        transform.Rotate(new Vector3(0, 180, 0));
    }
    
    public void show(int treasureIndex, Vector3 descriptionPos)
    {
        setText(treasureIndex);
        setImage(treasureIndex);
        setPosition(descriptionPos);
        if (lookPlayerWhenShow)
            lookPlayer();
        descriptionPanel.Show();
        var seq = DOTween.Sequence();

        seq.Play().OnComplete(() => {
            descriptionPanel.Show();
        });
    }
}
