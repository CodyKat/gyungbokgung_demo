using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro ���
using DG.Tweening;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    TreasureHuntManager treasureHuntManager;
    GameObject player;
    PanelHandler menuPanel;
    private IllustratedGuide IllustratedGuide;
    private GameObject descriptionCanvas;
    int i = 0;

    // TextMeshPro UI ���
    [SerializeField]
    private TMP_Text titleText; // "���� �޴�" �ؽ�Ʈ
    [SerializeField]
    private TMP_Text languageButtonText; // "��� ����" �ؽ�Ʈ
    [SerializeField]
    private TMP_Text soundButtonText; // "���� ����" �ؽ�Ʈ
    [SerializeField]
    private TMP_Text treasureButtonText; // "���� ����" �ؽ�Ʈ
    [SerializeField]
    private TMP_Text CloseButtonText; // "���� ����" �ؽ�Ʈ
    [SerializeField]
    private Button soundButton; // ���� ���� ��ư
    [SerializeField]
    private Slider soundSlider; // ���� ���� �����̴�


    // Start is called before the first frame update
    void Start()
    {
        treasureHuntManager = TreasureHuntManager.Instance;
        player = treasureHuntManager.player;
        menuPanel = transform.Find("Panel").GetComponent<PanelHandler>();
        IllustratedGuide = GameObject.FindObjectOfType<IllustratedGuide>();
        descriptionCanvas = GameObject.Find("TreasureDescription");

        // �� ���� �ʱ� UI �ؽ�Ʈ ����
        UpdateLanguageTexts();
        //���⼳�� 
        soundButton.gameObject.SetActive(true); // �ʱ� ����: ��ư�� Ȱ��ȭ, �����̴��� ��Ȱ��ȭ
        //soundButton.enabled = true;
        //soundSlider.enabled = false;
        soundSlider.gameObject.SetActive(false);
        soundButton.onClick.AddListener(OnClickSound); // ��ư Ŭ�� �̺�Ʈ ����
        soundSlider.onValueChanged.AddListener(OnSliderValueChanged); // �����̴� �� ���� �̺�Ʈ ����
        soundSlider.value = AudioListener.volume; // �����̴� �ʱ� �� (���� ����� ����) 0~1 ����
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = this.transform.parent.transform.position + new Vector3(0, 2, 0);
        transform.LookAt(player.transform.position);
        transform.Rotate(new Vector3(0, 180, 0));
        i++;
        if (i == 100)
            OnClickTreasure();
    }

    // ���� ���� ��ư Ŭ�� �� ȣ��
    public void OnClickGameOver()
    {
        Debug.Log("���� ����");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // ��� ���� ��ư Ŭ�� �� ȣ��
    public void OnClickLanguage()
    {
        Debug.Log("��� ����");

        // ��� ��� (�ѱ��� <-> ����)
        if (PlayerSetting.Instance.language == SystemLanguage.Korean)
        {
            PlayerSetting.Instance.language = SystemLanguage.English;
        }
        else
        {
            PlayerSetting.Instance.language = SystemLanguage.Korean;
        }

        // ����� �� ���� UI �ؽ�Ʈ ������Ʈ
        UpdateLanguageTexts();
    }

    // ���� ���� ��ư Ŭ�� �� ȣ��
    public void OnClickSound()
    {
        Debug.Log("���� ����");
        // ���� ���� ���� �߰�
        soundButton.gameObject.SetActive(false);
        soundSlider.gameObject.SetActive(true);
    }

    // �����̴� �� ���� �� ȣ��
    public void OnSliderValueChanged(float value)
    {
        // ���� ����� �ý����� ���� ����
        AudioListener.volume = value; // 0~1 ������ �����̴� ������ ����� ���� ����
        Debug.Log("Audio Volume: " + value);
    }

    // ���� ��ư Ŭ�� �� ȣ��
    public void OnClickTreasure()
    {
        Debug.Log("����");

        // ���� ���� ����
        IllustratedGuide.loadFoundTreasure();
        IllustratedGuide.showIllustratedGuide();

        // ����â ��ġ�� ����� �ֺ����� �̵�
        Vector3 playerPos = player.transform.position;
        Vector3 playerForward = player.transform.forward; // �÷��̾ ���� ����
        Vector3 offset = new Vector3(0, 1.5f, 0); // �Ӹ� ���� �ణ
        Vector3 descriptionPos = playerPos + playerForward * 2f + offset; // �÷��̾� �� 2����

        Debug.Log("in onclickTreasure : playerpos : " + playerPos);
        descriptionCanvas.transform.position = new Vector3(0, 0, 0);
    }

    // UI �ؽ�Ʈ ������Ʈ
    private void UpdateLanguageTexts()
    {
        if (PlayerSetting.Instance.language == SystemLanguage.Korean)
        {
            titleText.text = "���� �޴�";
            languageButtonText.text = "��� ����";
            soundButtonText.text = "���� ����";
            treasureButtonText.text = "���뵵��";
            CloseButtonText.text = "��������";
        }
        else if (PlayerSetting.Instance.language == SystemLanguage.English)
        {
            titleText.text = "Menu";
            languageButtonText.text = "Korean";
            soundButtonText.text = "Sound Settings";
            treasureButtonText.text = "Treasure Box";
            CloseButtonText.text = "Finish";
        }
    }
}
