using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetect : MonoBehaviour
{
    private GameObject colObject;
    private GameObject[] inRange;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(inRange);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(inRange != null)
        {
            Debug.Log(inRange.Length);
        }
        */
    }

    void FixedUpdate()
    {
        /*
        if(inRange == null)
        {
            Debug.Log("null");
        }
        if(inRange != null)
        {
            Debug.Log(inRange.Length);
        }
        */

        if(inRange != null && inRange.Length > 0)
        {

            int i = 0;
            foreach (GameObject obj in inRange)
            {
                if(obj != null)
                {
                    i++;
                }
            }

            GameObject[] tempArray = new GameObject[i];
            int j = 0;
            foreach(GameObject obj in inRange)
            {
                if(obj != null)
                {
                    tempArray[j] = obj;
                    j++;
                }
            }
            inRange = tempArray;
        }
    }

    
    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Trigger Enter: " + col.gameObject.name);
        if(col.gameObject.tag != "Planet")
        {
            if(inRange == null || inRange.Length == 0)
            {
                inRange = new GameObject[1];
                inRange[0] = col.gameObject;
                //Debug.Log(inRange.Length);
                //Debug.Log(inRange);
            }
            else
            {
                GameObject[] tempArray = new GameObject[inRange.Length + 1];
                
                for(int i=0; i < inRange.Length; i++)
                {
                    tempArray[i] = inRange[i];
                }
                
                /*
                foreach (GameObject obj in inRange)
                {}
                */
                
                tempArray[(tempArray.Length-1)] = col.gameObject;
                inRange = tempArray;
                //Debug.Log(inRange.Length);
            }
        }
        
    }
    

    void OnTriggerExit2D(Collider2D col)
    {
        /*
        if(col.gameObject.tag != "Planet")
        {
            //Debug.Log("trigger exit");
            if(inRange == null || inRange.Length <= 1)
            {
                inRange = new GameObject[0];
            }
            else
            {
                GameObject[] tempArray = new GameObject[inRange.Length - 1];
                int i = 0;
                foreach (GameObject obj in inRange)
                {
                    if(obj != col.gameObject)
                    {
                        tempArray[i] = obj;
                        i++;
                    }
                }
                inRange = tempArray;
            }
        }
        */
        //Debug.Log("Trigger Exit: " + col.gameObject.name);
        if(col.gameObject.tag != "Planet")
        {
            //Debug.Log("trigger exit");
            if(inRange == null || inRange.Length <= 1)
            {
                inRange = new GameObject[0];
            }
            else
            {
                int i = 0;
                foreach (GameObject obj in inRange)
                {
                    if(obj != null && obj != col.gameObject)
                    {
                        i++;
                    }
                }

                GameObject[] tempArray = new GameObject[i];

                int j = 0;
                foreach(GameObject obj in inRange)
                {
                    if(obj != null && obj != col.gameObject)
                    {
                        tempArray[j] = obj;
                        j++;
                    }
                }
                inRange = tempArray;
            }
        }
    }
    
    public GameObject[] getInRange()
    {
        return inRange;
    }

}
