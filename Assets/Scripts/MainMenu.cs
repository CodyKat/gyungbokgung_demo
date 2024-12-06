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
        // �ʱ�ȭ �ڵ� �ʿ� �� ���⿡ �ۼ�
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
        // ��� ���� ���� �߰�
    }

    // ���� ���� ��ư Ŭ�� �� ȣ��
    public void OnClickSound()
    {
        Debug.Log("���� ����");
        // ���� ���� ���� �߰�
    }

    // ���� ��ư Ŭ�� �� ȣ��
    public void OnClickTreasure()
    {
        Debug.Log("����");
        // ���� ���� ���� �߰�
    }
}
