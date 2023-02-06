using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopManager : MonoBehaviour, IDataPersistence
{
    CoinCollect coinCollect;

    private bool Bought1, Bought2,Bought3;
    public Button Button1, Button2, Button3, ButtonEnter1, ButtonEnter2, ButtonEnter3;


    void Start()
    {
        Bought1 = false;
        Bought2 = false;
        Bought3 = false;

        DataPersistenceManager.instance.LoadGame();
    }

    void Update()
    {

        if (Bought1 == false)
        {
            Button1.interactable = true;
            ButtonEnter1.gameObject.SetActive(false);
        }
        else
        {
            Button1.interactable = false;
            Button1.gameObject.SetActive(false);
            ButtonEnter1.gameObject.SetActive(true);
        }

        if(Bought2 == false)
        {
            Button2.interactable = true;
            ButtonEnter3.gameObject.SetActive(false);
        }
        else
        {
            Button2.interactable = false;
            Button2.gameObject.SetActive(false);
            ButtonEnter2.gameObject.SetActive(true);
        }

        if (Bought3 == false)
        {
            Button3.interactable = true;
            ButtonEnter3.gameObject.SetActive(false);
        }
        else
        {
            Button3.interactable = false;
            Button3.gameObject.SetActive(false);
            ButtonEnter3.gameObject.SetActive(true);
        }
    }


    public void Buy(int value)
    {
        if (value == 1)
        {
            if(CoinCollect.instance.coin >= 100)
            {
                CoinCollect.instance.ChangeMinusCoin(-100);
                Bought1 = true;

                DataPersistenceManager.instance.SaveGame();
            }
        }
        if (value == 2)
        {
            //CoinCollect.instance.coin
            if (CoinCollect.instance.coin >= 200)
            {
                CoinCollect.instance.ChangeMinusCoin(-200);
                Bought2 = true;

                DataPersistenceManager.instance.SaveGame();
            }
        }
        if (value == 3)
        {
            if (CoinCollect.instance.coin >= 300)
            {
                CoinCollect.instance.ChangeMinusCoin(-300);
                Bought3 = true;

                DataPersistenceManager.instance.SaveGame();
            }
        }
    }


    public void LoadData(GameData data)
    {
        this.Bought1 = data.Bought1;
        this.Bought2 = data.Bought2;
        this.Bought3 = data.Bought3;
    }

    public void SaveData(GameData data)
    {
        data.Bought1 = this.Bought1;
        data.Bought2 = this.Bought2;
        data.Bought3 = this.Bought3;
    }


    /*
        public int Price;
        public bool[] World1;
        public bool[] currentWorld;

        CoinCollect coinCollect;

        public int SaveID;

        void Update()
        {

            World1[1] = true;

        }

        public void Buy()
        {
            Shop();
        }
        public void Shop()
        {
            if(CoinCollect.instance.coin >= Price)
            {

                CoinCollect.instance.ChangeMinusCoin(Price);
            }
            if (CoinCollect.instance.coin <= Price)
            {

            }

        }


        public int[,] shopWorlds = new int[4, 4];
        public CoinCollect coinCollect;
        CoinCollect coinValue;
        public bool[] shopWorldsBool = new bool[4]{false, false, false, false};

        void Start()
        {
            coinCollect = FindObjectOfType<CoinCollect>();
            DataPersistenceManager.instance.LoadGame();

            //ID's
            shopWorlds[1, 1] = 1;
            shopWorlds[1, 2] = 2;
            shopWorlds[1, 3] = 3;
            shopWorlds[1, 4] = 4;


            //Price
            shopWorlds[2, 1] = 10;
            shopWorlds[2, 2] = 20;
            shopWorlds[2, 3] = 30;
            shopWorlds[2, 4] = 40;


        }

        public void Buy()
        {
            GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

            if (CoinCollect.instance.coin >= shopWorlds[2, ButtonRef.GetComponent<ButtonInfo>().WorldID])
            {
                CoinCollect.instance.ChangeMinusCoin(shopWorlds[2, ButtonRef.GetComponent<ButtonInfo>().WorldID]);

            }
            if (CoinCollect.instance.coin <= shopWorlds[2, ButtonRef.GetComponent<ButtonInfo>().WorldID])
            {


            }

        }
        
        */
}
