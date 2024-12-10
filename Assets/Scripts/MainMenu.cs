using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 사용
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

    // TextMeshPro UI 요소
    [SerializeField]
    private TMP_Text titleText; // "게임 메뉴" 텍스트
    [SerializeField]
    private TMP_Text languageButtonText; // "언어 선택" 텍스트
    [SerializeField]
    private TMP_Text soundButtonText; // "음향 조절" 텍스트
    [SerializeField]
    private TMP_Text treasureButtonText; // "도감 보기" 텍스트
    [SerializeField]
    private TMP_Text CloseButtonText; // "도감 보기" 텍스트
    [SerializeField]
    private Button soundButton; // 음향 조절 버튼
    [SerializeField]
    private Slider soundSlider; // 음향 조절 슬라이더


    // Start is called before the first frame update
    void Start()
    {
        treasureHuntManager = TreasureHuntManager.Instance;
        player = treasureHuntManager.player;
        menuPanel = transform.Find("Panel").GetComponent<PanelHandler>();
        IllustratedGuide = GameObject.FindObjectOfType<IllustratedGuide>();
        descriptionCanvas = GameObject.Find("TreasureDescription");

        // 언어에 따라 초기 UI 텍스트 설정
        UpdateLanguageTexts();
        //음향설정 
        soundButton.gameObject.SetActive(true); // 초기 상태: 버튼은 활성화, 슬라이더는 비활성화
        //soundButton.enabled = true;
        //soundSlider.enabled = false;
        soundSlider.gameObject.SetActive(false);
        soundButton.onClick.AddListener(OnClickSound); // 버튼 클릭 이벤트 연결
        soundSlider.onValueChanged.AddListener(OnSliderValueChanged); // 슬라이더 값 변경 이벤트 연결
        soundSlider.value = AudioListener.volume; // 슬라이더 초기 값 (현재 오디오 볼륨) 0~1 범위
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

    // 게임 종료 버튼 클릭 시 호출
    public void OnClickGameOver()
    {
        Debug.Log("게임 종료");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // 언어 선택 버튼 클릭 시 호출
    public void OnClickLanguage()
    {
        Debug.Log("언어 선택");

        // 언어 토글 (한국어 <-> 영어)
        if (PlayerSetting.Instance.language == SystemLanguage.Korean)
        {
            PlayerSetting.Instance.language = SystemLanguage.English;
        }
        else
        {
            PlayerSetting.Instance.language = SystemLanguage.Korean;
        }

        // 변경된 언어에 따라 UI 텍스트 업데이트
        UpdateLanguageTexts();
    }

    // 음향 조절 버튼 클릭 시 호출
    public void OnClickSound()
    {
        Debug.Log("음향 조절");
        // 음향 설정 로직 추가
        soundButton.gameObject.SetActive(false);
        soundSlider.gameObject.SetActive(true);
    }

    // 슬라이더 값 변경 시 호출
    public void OnSliderValueChanged(float value)
    {
        // 실제 오디오 시스템의 볼륨 설정
        AudioListener.volume = value; // 0~1 범위의 슬라이더 값으로 오디오 볼륨 설정
        Debug.Log("Audio Volume: " + value);
    }

    // 도감 버튼 클릭 시 호출
    public void OnClickTreasure()
    {
        Debug.Log("도감");

        // 도감 열기 로직
        IllustratedGuide.loadFoundTreasure();
        IllustratedGuide.showIllustratedGuide();

        // 설명창 위치를 사용자 주변으로 이동
        Vector3 playerPos = player.transform.position;
        Vector3 playerForward = player.transform.forward; // 플레이어가 보는 방향
        Vector3 offset = new Vector3(0, 1.5f, 0); // 머리 위로 약간
        Vector3 descriptionPos = playerPos + playerForward * 2f + offset; // 플레이어 앞 2미터

        Debug.Log("in onclickTreasure : playerpos : " + playerPos);
        descriptionCanvas.transform.position = new Vector3(0, 0, 0);
    }

    // UI 텍스트 업데이트
    private void UpdateLanguageTexts()
    {
        if (PlayerSetting.Instance.language == SystemLanguage.Korean)
        {
            titleText.text = "게임 메뉴";
            languageButtonText.text = "언어 변경";
            soundButtonText.text = "음향 조절";
            treasureButtonText.text = "전통도감";
            CloseButtonText.text = "게임종료";
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
