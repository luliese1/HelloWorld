using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private ItemIndexHolder ItemIndexHolder;

    public Button Item1;
    public Button Item2;
    public Button Item3;

    public PowerupManager powerupManager;

    // Start is called before the first frame update
    void Start()
    {

        ItemIndexHolder = FindObjectOfType<ItemIndexHolder>();
        if(ItemIndexHolder == null)
        {
            ItemIndexHolder = gameObject.AddComponent<ItemIndexHolder>();
        }
        

        if (ItemIndexHolder.item1)
        {
            Item1.gameObject.SetActive(true);
        }
        else
        {
            Item1.gameObject.SetActive(false);
        }

        if (ItemIndexHolder.item2)
        {
            Item2.gameObject.SetActive(true);
        }
        else
        {
            Item2.gameObject.SetActive(false);
        }

        if (ItemIndexHolder.item3)
        {
            Item3.gameObject.SetActive(true);
        }
        else
        {
            Item3.gameObject.SetActive(false);
        }



    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void ItemClick(string itemname)
    {
        if(itemname == "booster")
        {
            powerupManager.itembooster();
            Properties.getInstance().itemUse(0);
            Item1.interactable = false;

            ItemIndexHolder.item1 = false;

        }
        else if(itemname == "double")
        {
            powerupManager.itemdouble();
            Item2.interactable = false;
            Properties.getInstance().itemUse(1);

            ItemIndexHolder.item2 = false;
        }
        else if(itemname == "safeMode")
        {
            powerupManager.itemsafeMode();
            Item3.interactable = false;
            Properties.getInstance().itemUse(2);

            ItemIndexHolder.item3 = false;
        }
    }
}
