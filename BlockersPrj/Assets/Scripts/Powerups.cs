using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public bool doublePoints;
    public bool safeMode;
    public bool booster;

    public Sprite[] PowerupSprites;

    public float powerupLength;

    private PowerupManager powerupManager;

    // Start is called before the first frame update

    private void Awake()
    {
        int powerupSelector = Random.Range(0, 3);

        switch (powerupSelector)
        {
            case 0: 
                doublePoints = true;
                safeMode = false;
                booster = false;
                break;
            case 1:
                doublePoints = false;
                safeMode = true;
                booster = false;
                break;
            case 2:
                doublePoints = false;
                safeMode = false;
                booster = true;
                break;
        }

        GetComponent<SpriteRenderer>().sprite = PowerupSprites[powerupSelector];

    }


    void Start()
    {
        powerupManager = FindObjectOfType<PowerupManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            powerupManager.ActivatePowerUp(doublePoints, safeMode, booster ,powerupLength);
        }

        gameObject.SetActive(false);
    }
}
