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

    public AudioClip soundClip;  // 재생할 소리 클립
    private AudioSource audioSource;  // 오디오 소스 컴포넌트

    // Start is called before the first frame update
    void Start()
    {
        buildingPanel = gameObject.transform.parent.Find("Description").gameObject;
        popupWindow = buildingPanel.GetComponent<PanelHandler>();

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 소리 재생을 시작하지 않도록 초기화
        audioSource.Stop();  // 초기화 시 소리 중지
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
        audioSource.clip = soundClip;
        audioSource.Play();
        var seq = DOTween.Sequence();
        // seq.Append(transform.DOScale(0.95f, 0.1f));
        // seq.Append(transform.DOScale(1.05f, 0.1f));
        // seq.Append(transform.DOScale(1f, 0.1f));

        seq.Play().OnComplete(() => {
            popupWindow.Show();
        });

    }

}
