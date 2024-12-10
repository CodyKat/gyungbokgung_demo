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
        // �ʱ�ȭ �ڵ� �ʿ� �� ���⿡ �ۼ�
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
        IllustratedGuide.loadFoundTreasure();
        IllustratedGuide.showIllustratedGuide();

        // **����â ��ġ�� ����� �ֺ����� �̵�**
        Vector3 playerPos = player.transform.position;
        Vector3 playerForward = player.transform.forward; // �÷��̾ ���� ����
        Vector3 offset = new Vector3(0, 1.5f, 0); // �Ӹ� ���� �ణ
        Vector3 descriptionPos = playerPos + playerForward * 2f + offset; // �÷��̾� �� 2����

        Debug.Log("in onclickTreasure : playerpos : " + playerPos);
        descriptionCanvas.transform.position = new Vector3(0, 0, 0);
    }
}
