using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundSwapper : MonoBehaviour
{
    public Material[] mats;
    public GameObject sky;
    public float TimeElapsed = 0.0f;
    public GameObject sunlight;
    private int index = 0;
    // Update is called once per frame
    void Update()
    {
        // Testing Code
        TimeElapsed += Time.deltaTime;
        if(TimeElapsed > 5f && index < 2){
            index += 1;
            swapBackground(index);
            TimeElapsed = 0f;
        }
    }

    void swapBackground(int i){
        sunlight.transform.Rotate(16, 0, 0);
        sky.GetComponent<Renderer>().material = mats[i];
    }
}
