using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SendMenu : MonoBehaviour {

    public Text[] ItemTexts;

    public Toggle BoosterToggle;
    public Toggle DoubleToggle;
    public Toggle SpikeToggle;

    public InputField ReceiveUser;
    public InputField ItemCount;

    public MainMenu MainMenu;

    private int selectedItem;

    private void Awake()
    {
        BoosterToggle.onValueChanged.AddListener(delegate
        {
            if (BoosterToggle.isOn)
            {
                selectedItem = 0;
            }
        });

        DoubleToggle.onValueChanged.AddListener(delegate
        {
            if (DoubleToggle.isOn)
            {
                selectedItem = 1;
            }
        });

        SpikeToggle.onValueChanged.AddListener(delegate
        {
            if (SpikeToggle.isOn)
            {
                selectedItem = 2;
            }
        });

    }

    private void Update()
    {
        for(int i = 0; i < Properties.getInstance().itemListLength(); i++)
        {
            ItemTexts[i].text = " :" + Properties.getInstance().getItemCount(i);
        }
    }
    public void itemSend()
    {
        if(ReceiveUser.text == string.Empty || ItemCount.text == string.Empty)
        {
            MainMenu.failedBuyItem(1);
            return;   
        }


        int _Receieveuser = int.Parse(ReceiveUser.text);
        int _itemcount = int.Parse(ItemCount.text); ;

        Properties.getInstance().send(_Receieveuser, selectedItem, _itemcount);
    }

}
