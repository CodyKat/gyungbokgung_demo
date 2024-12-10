using UnityEngine;
using System.Threading.Tasks;

public class Treasure : MonoBehaviour
{
    // Start is called before the first frame update
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
                this.gameObject.SetActive(false);
            }
        }
    }
}
