using UnityEngine;
using System.Threading.Tasks;

public class Treasure : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip soundClip;  // 재생할 소리 클립
    private AudioSource audioSource;  // 오디오 소스 컴포넌트
    private TreasureHuntManager treasureHuntManager;
    private TreasureDescription treasureDescription;
    private GameObject[] treasureObjects;
    private bool[] treasureIsFoundFlags;

    private void Awake()
    {

    }

    private void Start()
    {
        treasureHuntManager = TreasureHuntManager.Instance;
        treasureDescription = GameObject.Find("TreasureDescription").GetComponent<TreasureDescription>();
        treasureObjects = treasureHuntManager.treasureObjects;
        treasureIsFoundFlags = treasureHuntManager.treasureIsFoundFlags;

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 소리 재생을 시작하지 않도록 초기화
        audioSource.Stop();  // 초기화 시 소리 중지
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void onClick()
    {
        for (int i = 0; i < treasureObjects.Length; i++)
        {
            if (treasureObjects[i].gameObject == this.gameObject)
            {

                treasureIsFoundFlags[i] = true;
                treasureDescription.showDescription(i, true, false);
                audioSource.clip = soundClip;
                audioSource.Play();  // 소리 재생
                this.gameObject.SetActive(false);
            }
        }
    }
}
