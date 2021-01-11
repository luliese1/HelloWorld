using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject Platform;
    //public GameObject[] Platforms;
    public Transform GenerationPoint;

    public float distanceBetweenMin;
    public float distanceBetweenMax;
    private float distanceBetween;

    //private float platformWidth;
    private float[] platformWidths;
    private int platformSelector;

    private float MinHeight;
    public Transform MaxHeightPoint;
    private float MaxHeight;
    public float MaxHeightChange;
    private float HeightChange;
    public float PowerupHeight;

    private CoinGenerator CoinGenerator;

    public float randomCoinThreshold;
    public float randomSpikeThreshold;
    public float randomPowerupThreshold;

    public ObjectPooler spikePools;
    public ObjectPooler powerupPools;
    public ObjectPooler[] objectPools;


    void Start()
    {
        //platformWidth = Platform.GetComponent<BoxCollider2D>().size.x;

        platformWidths = new float[objectPools.Length];

        for (int i = 0; i < objectPools.Length; i++)
        {
            platformWidths[i] = objectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        MinHeight = transform.position.y;
        MaxHeight = MaxHeightPoint.position.y;

        CoinGenerator = FindObjectOfType<CoinGenerator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < GenerationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
            platformSelector = Random.Range(0, objectPools.Length);

            HeightChange = transform.position.y + Random.Range(MaxHeightChange, -MaxHeightChange);

            if(HeightChange > MaxHeight)
            {
                HeightChange = MaxHeight;
            }
            else if(HeightChange < MinHeight)
            {
                HeightChange = MinHeight;
            }


            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector]/2) + distanceBetween, HeightChange , transform.position.z);
            

            //메모리 관리를 위한 오브젝트 풀링 사용
            //Instantiate(Platforms[platformSelector], transform.position, transform.rotation);

            //랜덤블럭을 위해
            GameObject pooledPlatform = objectPools[platformSelector].GetPooledObject();

            pooledPlatform.transform.position = transform.position;
            pooledPlatform.transform.rotation = transform.rotation;

            pooledPlatform.SetActive(true);



            float randomspikenum = Random.Range(0f, 100f);

            if (randomspikenum < randomSpikeThreshold)
            {
                float spikeXposition = Random.Range(-platformWidths[platformSelector] / 2 + 1.3f, platformWidths[platformSelector] / 2 - 1.3f);

                Vector3 spikeSpawnPosition = new Vector3(spikeXposition, 1.2f, 0f);
                Vector3 nearSpikeSpawnOffset = new Vector3(1f, 0, 0);

                GameObject pooledSpike1 = spikePools.GetPooledObject();

                pooledSpike1.transform.position = transform.position + spikeSpawnPosition;
                pooledSpike1.transform.rotation = transform.rotation;
                pooledSpike1.SetActive(true);

                GameObject pooledSpike2 = spikePools.GetPooledObject();

                pooledSpike2.transform.position = transform.position + spikeSpawnPosition - nearSpikeSpawnOffset;
                pooledSpike2.transform.rotation = transform.rotation;
                pooledSpike2.SetActive(true);

                GameObject pooledSpike3 = spikePools.GetPooledObject();

                pooledSpike3.transform.position = transform.position + spikeSpawnPosition + nearSpikeSpawnOffset;
                pooledSpike3.transform.rotation = transform.rotation;
                pooledSpike3.SetActive(true);

            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);

            if (Random.Range(0f, 100f) < randomCoinThreshold)
            {
                if (randomspikenum < randomSpikeThreshold)
                {
                    CoinGenerator.SpawnCoins(new Vector3(pooledPlatform.transform.position.x, pooledPlatform.transform.position.y + 5.0f, pooledPlatform.transform.position.z));
                }
                else
                {
                    CoinGenerator.SpawnCoins(new Vector3(pooledPlatform.transform.position.x, pooledPlatform.transform.position.y + 2.0f, pooledPlatform.transform.position.z));
                }
            }

        

            if (Random.Range(0f, 100f) < randomPowerupThreshold)
            {
                GameObject pooledPowerup = powerupPools.GetPooledObject();

                pooledPowerup.transform.position = transform.position + new Vector3(distanceBetween / 2f, Random.Range(PowerupHeight/2, PowerupHeight), 0);
                pooledPowerup.SetActive(true);

            }

        }
    }
}
