 using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public ShopManager ShopManager;

    public GameObject MainMenuPanel;
    public GameObject ShopMenuPanel;
    public GameObject SendMenuPanel;

    public GameObject FailedMessage;
    public Text FailedMessageText;

    public Toggle item1;
    public Toggle item2;
    public Toggle item3;


    public ItemIndexHolder ItemIndexHolder;

    private void Start()
    {
        BackToMainMenu();

    }

    public void PlayGame()
    {

        SceneManager.LoadScene("GameScene");

    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenShopButton()
    {
        ShopMenuPanel.SetActive(true);
        SendMenuPanel.SetActive(false);
        MainMenuPanel.SetActive(false);

        ShopManager.setMoney();
    }

    public void OpenSendButton()
    {
        ShopMenuPanel.SetActive(false);
        SendMenuPanel.SetActive(true);
        MainMenuPanel.SetActive(false);

        ShopManager.setMoney();
    }

    public void BackToMainMenu()
    {
        MainMenuPanel.SetActive(true);
        SendMenuPanel.SetActive(false);
        ShopMenuPanel.SetActive(false); 
    }

    public void failedBuyItem(int index)
    {
        switch (index)
        {
            case 0:
                FailedMessageText.text = "구매에 실패했습니다.";
                break;
            case 1:
                FailedMessageText.text = "빈칸을 입력해주세요!";
                break;
            default:
                FailedMessageText.text = "실패했습니다.";
                break;
        }

        FailedMessage.SetActive(true);
    }

    public void ConfirmtoFailed()
    {
        FailedMessage.SetActive(false);
    }



    private void Update()
    {
        if(Properties.getInstance().getItemCount(0) <= 0)
        { 
            item1.interactable = false;
        }
        else
        {
            item1.interactable = true;
        }

        if (Properties.getInstance().getItemCount(1) <= 0)
        {
            item2.interactable = false;
        }
        else
        {
            item2.interactable = true;
        }

        if (Properties.getInstance().getItemCount(2) <= 0)
        {
            item3.interactable = false;
        }
        else
        {
            item3.interactable = true;
        }

        if (item1.isOn)
        {
            ItemIndexHolder.item1 = true;
        }
        else
        {
            ItemIndexHolder.item1 = false;
        }

        if (item2.isOn)
        {
            ItemIndexHolder.item2 = true;
        }
        else
        {
            ItemIndexHolder.item2 = false;
        }

        if (item3.isOn)
        {
            ItemIndexHolder.item3 = true;
        }
        else
        {
            ItemIndexHolder.item3 = false;
        }


    }
}
