using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShipScreen : MonoBehaviour
{
    [SerializeField] private string infoKey = "i";

    private bool showInfo;
    public Image img;

    // Start is called before the first frame update
    void Start()
    {
        img.enabled = true;
        showInfo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(infoKey))
        {
            //Debug.Log("i Pressed");

            if(showInfo == true)
            {
                img.enabled = false;
                showInfo = false;
            }
            else
            {
                img.enabled = true;
                showInfo = true;
            }
        }
    }
}
