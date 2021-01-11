using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform platformGenerator;
    private Vector3 platformStartPoint;

    public PlayerController Player;
    private Vector3 PlayerStartPoint;

    private PlatformDestoryer[] PlatformList;

    private ScoreManager scoreManager;

    public DeathMenu DeathMenu; 
    public bool powerupReset;

    // Start is called before the first frame update
    void Start()
    {

        platformStartPoint = platformGenerator.position;
        PlayerStartPoint = Player.transform.position;

        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        scoreManager.scoreIncreasing = false;
        Player.gameObject.SetActive(false);
        Properties.getInstance().update(Mathf.RoundToInt(scoreManager.scoreCount));

        DeathMenu.gameObject.SetActive(true);
        //StartCoroutine("RestartGameCo");
    }

    public void Reset()
    {
        DeathMenu.gameObject.SetActive(false);
        PlatformList = FindObjectsOfType<PlatformDestoryer>();

        for (int i = 0; i < PlatformList.Length; i++)
        {
            PlatformList[i].gameObject.SetActive(false);
        }

        Player.transform.position = PlayerStartPoint;
        platformGenerator.position = platformStartPoint;

        Player.gameObject.SetActive(true);

        powerupReset = true;

        scoreManager.scoreCount = 0;
        scoreManager.scoreIncreasing = true;
    }



 /*   public IEnumerator RestartGameCo()
    {
        scoreManager.scoreIncreasing = false;
        Player.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(0.5f);

        PlatformList = FindObjectsOfType<PlatformDestoryer>();

        for(int i = 0; i < PlatformList.Length; i++)
        {
            PlatformList[i].gameObject.SetActive(false);
        }

        Player.transform.position = PlayerStartPoint;
        platformGenerator.position = platformStartPoint;

        Player.gameObject.SetActive(true);

        scoreManager.scoreCount = 0;
        scoreManager.scoreIncreasing = true;
    }
 */
}
