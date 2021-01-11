using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private int Reward;
    public Text RewardText;

    public MainMenu mainMenu;


    // Update is called once per frame
    void Update()
    {
        setMoney();
    }

    public void setMoney()
    {
        Reward = Properties.getInstance().getReward();
        RewardText.text = "Reward :" + Reward;
    }

    public void buyItems(int index)
    {
        bool IsBought = false;

        switch (index)
        {
            case 0:
                IsBought = Properties.getInstance().buy(new int[] { 1, 0, 0 });
                break;
            case 1:
                IsBought = Properties.getInstance().buy(new int[] { 0, 1, 0 });
                break;
            case 2:
                IsBought = Properties.getInstance().buy(new int[] { 0, 0, 1 });
                break;
            default:
                break;
        }

        if (IsBought)
        {
            
        }
        else if(!IsBought)
        {
            mainMenu.failedBuyItem(0);
        }

    }

    public void getReward(int reward)
    {

        Properties.getInstance().update(reward);
    }
    

}
