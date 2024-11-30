using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingPanelHandler : MonoBehaviour
{
    public TextAsset desText;
	private PanelHandler bulidingDesPanel;

    public void SetDescriptionText(GameObject scanObj){
        desText = new TextAsset();
        string descriptionTextFilePath = Constants.DESCRIPTIONS_PATH + scanObj.name + '_' + PlayerSetting.Instance.language;
        desText = Resources.Load(descriptionTextFilePath) as TextAsset;
		if (desText == null){
			Debug.LogError("not file found PATH :" + descriptionTextFilePath);
		}
        bulidingDesPanel.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = desText.ToString();

    }
}
