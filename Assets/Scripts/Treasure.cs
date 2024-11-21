using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isFound = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onClickInteraction()
    {
        Debug.Log(this.name);
        isFound = true;
        Destroy(this);
    }
}
