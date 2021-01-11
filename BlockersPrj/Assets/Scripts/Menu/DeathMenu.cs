using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public string MainMenuName;
    private ItemIndexHolder itemIndexHolder;

    private void Start()
    {
        itemIndexHolder = FindObjectOfType<ItemIndexHolder>();
    }
    public void RestartGame()
    {
        FindObjectOfType<GameManager>().Reset();
    }

    public void QuitToMain()
    {
        if (itemIndexHolder != null)
        {
            Destroy(itemIndexHolder.gameObject);
        }
        SceneManager.LoadScene(MainMenuName);
    }


}
