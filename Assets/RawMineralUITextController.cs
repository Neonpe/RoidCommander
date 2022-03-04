using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RawMineralUITextController : MonoBehaviour
{
    public GameObject gameManager;
    public Text rawMineralUIText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rawMineralUIText.text = ("Raw Minerals: " + gameManager.GetComponent<GameManager>().rawMineralCount);
    }
}
