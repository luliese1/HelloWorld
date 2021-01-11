﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoin : MonoBehaviour
{
    public int ScoreToCoin;

    private ScoreManager scoreManager;
    private AudioSource CoinSound;


    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        CoinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {

            if (CoinSound.isPlaying) 
                {
                CoinSound.Stop();
                CoinSound.Play();
            }
            else
            {
                CoinSound.Play();
            }
            
            
            scoreManager.AddScore(ScoreToCoin);

            gameObject.SetActive(false);
        }
    }


}
