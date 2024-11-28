using UnityEngine;
using System.Threading.Tasks;

public class Treasure : MonoBehaviour
{
    // Start is called before the first frame update
    private TreasureHuntManager treasureHuntManager;
    private GameObject[] treasureObjects;
    private bool[] treasureIsFoundFlags;

    private void Awake()
    {

    }

    private void Start()
    {
        treasureHuntManager = TreasureHuntManager.Instance;
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
                treasureHuntManager.showDescription(i);
            }
        }
    }
}
