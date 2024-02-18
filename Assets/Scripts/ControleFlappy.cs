using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float vitesseHorizontale;
    public float vitesseVerticale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)))  
        {
            transform­.Translate(vitesseHorizontale, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow)))
        {
            transform­.Translate(-vitesseHorizontale, 0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.UpArrow)))
        {
            transform­.Translate(0f, vitesseVerticale, 0f);
        }
    }
}
