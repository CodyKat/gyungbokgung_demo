using DG.Tweening;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class TreasureDescription : MonoBehaviour
{
    private PanelHandler descriptionPanel;
    private TreasureHuntManager treasureHuntManager;
    private GameObject[] treasureObjects;
    private IllustratedGuide illustratedGuide;
    private TextAsset[] descriptionTexts_eng;
    private TextAsset[] descriptionTexts_kor;
    private Sprite[] spriteImages;
    public bool lookPlayerWhenShow = true;
    public bool alwaysLookPlayer = false;
    public bool dockInIllustratedGuide = false;
    public Vector3[] cornersPos;
    public float worldWidth;
    public float worldHeight;
    private GameObject player;
    private Transform xrCamera;
    private int moveSpeed = 3;

    void Start()
    {
        descriptionPanel = transform.Find("Panel").GetComponent<PanelHandler>();
        treasureHuntManager = TreasureHuntManager.Instance;
        treasureObjects = treasureHuntManager.treasureObjects;
        illustratedGuide = IllustratedGuide.Instance;
        descriptionTexts_kor = treasureHuntManager.descriptionTexts_kor;
        descriptionTexts_eng = treasureHuntManager.descriptionTexts_eng;
        spriteImages = treasureHuntManager.spriteImages;
        cornersPos = new Vector3[4];
        player = treasureHuntManager.player;
        xrCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<RectTransform>().GetWorldCorners(cornersPos);
        worldHeight = (cornersPos[0] - cornersPos[1]).magnitude;
        worldWidth = (cornersPos[0] - cornersPos[2]).magnitude;
        if (dockInIllustratedGuide)
        {
            // TODO adjust panel position.
            Vector3 targetPosition = xrCamera.position + new Vector3(3, 2.5f, 3);
            setPosition(Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime));
            lookPlayer();
        }
        else if (alwaysLookPlayer)
            lookPlayer();
    }


    // showDescription function has three sepc.
    // 1st parameter function get treasureIndex(treasure id in hierarchy)
    // if this function has two param, whether 2nd param is true/false description dock in illustratedGuide
    // if param count is 3, get two Rotation information
    // last, 4th param, second param get position of desciption
    public void showDescription(int treasureIndex, bool lookPlayerWhenShow, bool alwaysLookPlayer)
    {
        Debug.Log("in showDesciption treasureIndex : " + treasureIndex + "has size : " + treasureObjects.Length);
        GameObject foundTreasure = treasureObjects[treasureIndex];
        Vector3 treasurePos = foundTreasure.transform.position;
        Vector3 playerPos = player.transform.position;
        Vector3 directionVec = Vector3.Normalize(playerPos - treasurePos);
        Vector3 descriptionPos = treasurePos + foundTreasure.transform.localScale.x * directionVec / 2;
        descriptionPos.y = 4.8f;
        this.dockInIllustratedGuide = false;
        this.lookPlayerWhenShow = lookPlayerWhenShow;
        this.alwaysLookPlayer = alwaysLookPlayer;
        show(treasureIndex, descriptionPos);
    }

    public void showDescription(int treasureIndex, bool dockInIllustratedGuide)
    {
        Debug.Log("in showDesciption treasureIndex : " + treasureIndex + "has size : " + treasureObjects.Length);
        GameObject foundTreasure = treasureObjects[treasureIndex];
        this.dockInIllustratedGuide = true;
        show(treasureIndex, new Vector3(0, 0, 0));
    }

    public void showDescription(int treasureIndex, Vector3 descriptionPos, bool lookPlayerWhenShow, bool alwaysLookPlayer)
    {
        Debug.Log("in showDesciption treasureIndex : " + treasureIndex + "has size : " + treasureObjects.Length);
        Vector3 playerPos = player.transform.position;
        this.dockInIllustratedGuide = false;
        this.lookPlayerWhenShow = lookPlayerWhenShow;
        this.alwaysLookPlayer = alwaysLookPlayer;
        show(treasureIndex, descriptionPos);
    }
    private void setText(int treasureIndex)
    {
        if (PlayerSetting.Instance.language == SystemLanguage.Korean)
        {
            descriptionPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>().text
                = descriptionTexts_kor[treasureIndex].ToString();
        }
        else
        {
            descriptionPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>().text
                = descriptionTexts_eng[treasureIndex].ToString();
        }
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
        if (dockInIllustratedGuide == false)
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
