using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    TreasureHuntManager treasureHuntManager;
    GameObject player;
    PanelHandler menuPanel;
    private IllustratedGuide IllustratedGuide;
    private GameObject descriptionCanvas;
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        treasureHuntManager = TreasureHuntManager.Instance;
        player = treasureHuntManager.player;
        menuPanel = transform.Find("Panel").GetComponent<PanelHandler>();
        IllustratedGuide = GameObject.FindObjectOfType<IllustratedGuide>();
        descriptionCanvas = GameObject.Find("TreasureDescription");
        // 초기화 코드 필요 시 여기에 작성
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
        // 언어 변경 로직 추가
    }

    // 음향 조절 버튼 클릭 시 호출
    public void OnClickSound()
    {
        Debug.Log("음향 조절");
        // 음향 설정 로직 추가
    }

    // 도감 버튼 클릭 시 호출
    public void OnClickTreasure()
    {
        Debug.Log("도감");
        // 도감 열기 로직 추가
        IllustratedGuide.loadFoundTreasure();
        IllustratedGuide.showIllustratedGuide();

        // **설명창 위치를 사용자 주변으로 이동**
        Vector3 playerPos = player.transform.position;
        Vector3 playerForward = player.transform.forward; // 플레이어가 보는 방향
        Vector3 offset = new Vector3(0, 1.5f, 0); // 머리 위로 약간
        Vector3 descriptionPos = playerPos + playerForward * 2f + offset; // 플레이어 앞 2미터

        Debug.Log("in onclickTreasure : playerpos : " + playerPos);
        descriptionCanvas.transform.position = new Vector3(0, 0, 0);
    }
}
