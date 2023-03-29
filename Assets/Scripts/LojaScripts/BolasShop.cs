using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolasShop : MonoBehaviour
{
    public static BolasShop instance;

    public List<Bolas> bolasList = new List<Bolas>();
    public List<GameObject> bolaSuporteList = new List<GameObject> ();
    public List<GameObject> compraBtnList = new List<GameObject> ();

    public GameObject baseBolaItem;
    public Transform conteudo;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }



    void Start()
    {
        //PlayerPrefs.DeleteAll (); 
        FillList ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FillList() //irá percorrer a coleção de bolas que você definiu no Inspector o BolasList utilizando o foreach
    {
        foreach(Bolas b in bolasList) //laço para trabalhar com as bolas
        {
            GameObject itensBola = Instantiate (baseBolaItem) as GameObject;
            itensBola.transform.SetParent (conteudo, false);
            BolasSuporte item = itensBola.GetComponent<BolasSuporte> ();


            item.bolasID = b.bolasID;
            item.bolasPreco.text = b.bolasPreco.ToString ();
            item.btnCompra.GetComponent<CompraBola> ().bolasIDe = b.bolasID;

            //Lista CompraBtn
            compraBtnList.Add(item.btnCompra);

            //Lista bolaSuporteList
            bolaSuporteList.Add(itensBola);

            if(PlayerPrefs.GetInt("BTN"+item.bolasID) == 1)
            {
                b.bolasComprou = true;
            }

            if(PlayerPrefs.HasKey("BTNS"+item.bolasID) && b.bolasComprou)
            {
                item.btnCompra.GetComponent<CompraBola> ().btnText.text = PlayerPrefs.GetString ("BTNS" +item.bolasID);
            }

            if(b.bolasComprou == true) // aqui busca a bola colorida na pasta determinada
            {
                item.bolaSprite.sprite = Resources.Load<Sprite> ("Sprites/" + b.bolasNomeSprite);
                item.bolasPreco.text = "Comprado!";

                if(PlayerPrefs.HasKey("BTNS"+item.bolasID) == false)
                {
                    item.btnCompra.GetComponent<CompraBola>().btnText.text = "Usando";
                }

            } else{
                item.bolaSprite.sprite = Resources.Load<Sprite> ("Sprites/" + b.bolasNomeSprite + "_cinza");
            }
        }
    }

    public void UpdateSprinte(int bola_id)
    {
        for(int i = 0; i < bolaSuporteList.Count; i++)
        {
            BolasSuporte bolasSuportscript = bolaSuporteList [i].GetComponent<BolasSuporte> ();

            if(bolasSuportscript.bolasID == bola_id)
            {
                for(int j = 0; j < bolasList.Count; j++)
                {
                    if(bolasList[j].bolasID == bola_id)
                    {
                        if(bolasList[j].bolasComprou == true)
                        {
                            bolasSuportscript.bolaSprite.sprite = Resources.Load<Sprite> ("Sprites/" + bolasList[j].bolasNomeSprite);
                            bolasSuportscript.bolasPreco.text = "Comprado!";
                            SalvaBolasLojaInfo (bolasSuportscript.bolasID);
                        }else{
                            bolasSuportscript.bolaSprite.sprite = Resources.Load<Sprite> ("Sprites/" + bolasList[j].bolasNomeSprite + "_cinza");
                        }
                    }
                }
            }
        }
    }

    void SalvaBolasLojaInfo(int idBola)
    {
        for(int i = 0; i < bolasList.Count; i++)
        {
            BolasSuporte bolasSup = bolaSuporteList [i].GetComponent<BolasSuporte> ();

            if(bolasSup.bolasID == idBola)
            {
                PlayerPrefs.SetInt ("BTN"+bolasSup.bolasID,bolasSup.btnCompra ? 1 : 0);
            }

        }
    }

    public void SalvaBolasLojaText(int idBola, string s)
    {
        for(int i = 0; i < bolasList.Count; i++)
        {
            BolasSuporte bolasSup = bolaSuporteList [i].GetComponent<BolasSuporte> ();

            if(bolasSup.bolasID == idBola)
            {
                PlayerPrefs.SetString ("BTNS"+bolasSup.bolasID,s);
            }
        }
    }
}
