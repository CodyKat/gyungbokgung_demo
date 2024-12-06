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
    int i = 0;


    // Start is called before the first frame update
    void Start()
    {
        treasureHuntManager = TreasureHuntManager.Instance;
        player = treasureHuntManager.player;
        menuPanel = transform.Find("Panel").GetComponent<PanelHandler>();
        // 초기화 코드 필요 시 여기에 작성
    }

    // Update is called once per frame
    void Update()
    {
        if (i == 100)
            onClickPet();
        i++;
        transform.position = this.transform.parent.transform.position + new Vector3(0, 2, 0);
        transform.LookAt(player.transform.position);
        transform.Rotate(new Vector3(0, 180, 0));
    }

    public void onClickPet()
    {
        menuPanel.Show();
        var seq = DOTween.Sequence();

        seq.Play().OnComplete(() => {
            menuPanel.Show();
        });
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
    }
}
