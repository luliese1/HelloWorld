using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public string MainMenuName;
    public GameObject _PauseMenu;

    private ItemIndexHolder itemIndexHolder;

    private void Start()
    {
        itemIndexHolder = FindObjectOfType<ItemIndexHolder>();
    }


    public void PauseGame()
    {
        Time.timeScale = 0f;
        _PauseMenu.SetActive(true);

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        _PauseMenu.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        FindObjectOfType<GameManager>().Reset();
        _PauseMenu.SetActive(false);
    }

    public void QuitToMain()
    {
        if (itemIndexHolder != null)
        {
            Destroy(itemIndexHolder.gameObject);
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenuName);
        _PauseMenu.SetActive(false);
    }


}
