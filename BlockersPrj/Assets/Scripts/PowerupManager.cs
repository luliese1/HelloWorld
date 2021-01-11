using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{

    private bool doublePoints;
    private bool safeMode;
    private bool booster;

    private bool powerupActive;
    private float powerupLengthCounter;

    private ScoreManager scoreManager;
    private PlatformGenerator platformGenerator;
    private GameManager gameManager;
    private PlayerController playerController;

    private float normalSpeed;
    private float normalpointPerSecond;
    private float spikeRate;

    private PlatformDestoryer[] spikeList;


    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        platformGenerator = FindObjectOfType<PlatformGenerator>();
        gameManager = FindObjectOfType<GameManager>();
        playerController = FindObjectOfType<PlayerController>();

        normalpointPerSecond = scoreManager.pointPerSecond;
        spikeRate = platformGenerator.randomSpikeThreshold;
        normalSpeed = playerController.GetComponent<PlayerController>().moveSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.powerupReset)
        {
            powerupLengthCounter = 0;
            gameManager.powerupReset = false;
        }

        if (powerupActive)
        {
            powerupLengthCounter -= Time.deltaTime;

            if (doublePoints)
            {
                scoreManager.pointPerSecond = normalpointPerSecond * 3f;
                scoreManager.PowerupDouble = true;
            }

            if (safeMode)
            {
                platformGenerator.randomSpikeThreshold = 0f;
            }
            
            if(booster)
            {
                playerController.booster = true;
            }


            if(powerupLengthCounter <= 0)
            {
                scoreManager.pointPerSecond = normalpointPerSecond;
                platformGenerator.randomSpikeThreshold = spikeRate;

                playerController.booster = false;
                scoreManager.PowerupDouble = false;
                powerupActive = false;
            }
        }
        

    }

    public void ActivatePowerUp(bool doublePt, bool safe, bool boost, float time)
    {
        doublePoints = doublePt;
        safeMode = safe;
        booster = boost;
        powerupLengthCounter = time;

        if (safeMode)
        {
            spikeList = FindObjectsOfType<PlatformDestoryer>();

            for (int i = 0; i < spikeList.Length; i++)
            {
                if (spikeList[i].gameObject.name.Contains("Spike"))
                {
                    spikeList[i].gameObject.SetActive(false);
                }
            }
        }

        powerupActive = true;

    }

    public void itembooster()
    {
        ActivatePowerUp(false, false, true, 5f);
    }

    public void itemdouble()
    {
        ActivatePowerUp(true, false, false, 5f);
    }

    public void itemsafeMode()
    {
        ActivatePowerUp(false, true, false, 5f);
    }


}
