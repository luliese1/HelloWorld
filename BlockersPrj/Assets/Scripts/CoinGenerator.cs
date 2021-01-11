using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public ObjectPooler coinPools;

    public float distanceBetweenCoin;

    public void SpawnCoins(Vector3 startPosition)
    {
        GameObject coin1 = coinPools.GetPooledObject();
        coin1.transform.position = startPosition;
        coin1.SetActive(true);

        GameObject coin2 = coinPools.GetPooledObject();
        coin2.transform.position = new Vector3(startPosition.x - distanceBetweenCoin, startPosition.y, startPosition.z);
        coin2.SetActive(true);

        GameObject coin3 = coinPools.GetPooledObject();
        coin3.transform.position = new Vector3(startPosition.x + distanceBetweenCoin, startPosition.y , startPosition.z);
        coin3.SetActive(true);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
