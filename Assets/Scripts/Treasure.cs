using UnityEngine;
using System.Threading.Tasks;

public class Treasure : MonoBehaviour
{
    // Start is called before the first frame update
    private TreasureHuntManager treasureHuntManager;
    private GameObject[] treasureObjects;
    private bool[] treasureIsFoundFlags;
    private GameObject player;
    private PanelHandler descriptionBoard;

    private void Awake()
    {

    }

    private void Start()
    {
        treasureHuntManager = TreasureHuntManager.Instance;
        treasureObjects = treasureHuntManager.treasureObjects;
        treasureIsFoundFlags = treasureHuntManager.treasureIsFoundFlags;
        player = GameObject.Find("Player");
        descriptionBoard = GameObject.Find("TreasureDescription").GetComponent<PanelHandler>();
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
                GameObject foundTreasure = treasureObjects[i];
                Vector3 treasurePos = foundTreasure.transform.position;
                Vector3 playerPos = player.transform.position;
                Vector3 directionVec = Vector3.Normalize(playerPos - treasurePos);
                Vector3 descriptionPos = treasurePos + foundTreasure.transform.localScale.x * directionVec;

                treasureIsFoundFlags[i] = true;
                descriptionBoard.transform.position = descriptionPos;
                descriptionBoard.transform.LookAt(directionVec);
                Task showingDescription = new Task(() => treasureHuntManager.showDescription(descriptionBoard));
                showingDescription.Start();
                showingDescription.Wait();
                Destroy(this);
                Debug.Log("hello");
            }
        }
    }
}
