using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIndexHolder : MonoBehaviour
{
    public bool item1;
    public bool item2;
    public bool item3;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
