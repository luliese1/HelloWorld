using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateProperties : MonoBehaviour
{
    private float time;
    private float watingTime;


    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        watingTime = 0.5f;
    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > watingTime)
        {
            time = 0.0f;
            Properties.getInstance().getProperties();
        }
    }
}
