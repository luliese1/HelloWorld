using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject Player;
    private Vector3 lastplayerposition;
    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        lastplayerposition = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        offset = Player.transform.position.x - lastplayerposition.x;

        transform.position = new Vector3( transform.position.x + offset, transform.position.y, transform.position.z);
        lastplayerposition = Player.transform.position;

    }
}
