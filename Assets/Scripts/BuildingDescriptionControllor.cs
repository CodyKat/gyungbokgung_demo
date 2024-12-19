using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BuildingDescriptionControllor : MonoBehaviour
{
    private GameObject buildingPanel;
    public PanelHandler popupWindow;

    public TextAsset desText;

    // Start is called before the first frame update
    void Start()
    {
        buildingPanel = gameObject.transform.parent.Find("Description").gameObject;
        popupWindow = buildingPanel.GetComponent<PanelHandler>();
    }

    public void SetDescriptionText(GameObject scanObj)
    {
        desText = new TextAsset();
        string descriptionTextFilePath = Constants.DESCRIPTIONS_PATH + "Building/" + scanObj.name + '_' + PlayerSetting.Instance.language;
        desText = Resources.Load(descriptionTextFilePath) as TextAsset;
        if (desText == null)
        {
            Debug.LogError("not file found PATH :" + descriptionTextFilePath);
        }
        buildingPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = desText.ToString();

    }
    public void onClick()
    {
        SetDescriptionText(this.transform.parent.gameObject);
        popupWindow.Show();
        var seq = DOTween.Sequence();

        // seq.Append(transform.DOScale(0.95f, 0.1f));
        // seq.Append(transform.DOScale(1.05f, 0.1f));
        // seq.Append(transform.DOScale(1f, 0.1f));

        seq.Play().OnComplete(() => {
            popupWindow.Show();
        });

    }

}
