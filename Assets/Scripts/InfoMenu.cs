using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoMenu : MonoBehaviour
{
   private Animator infoAnim;
   private bool sobeInfo;
   private AudioSource musica;
   public Sprite somLigado, somDesligado;
   private Button btnSom;
   

   void Start()
   {
    infoAnim = GameObject.FindGameObjectWithTag ("infoAnimTag").GetComponent<Animator> () as Animator;
    musica = GameObject.Find("AudioManager").GetComponent<AudioSource> () as AudioSource;
    btnSom = GameObject.Find("SOM").GetComponent<Button> () as Button;
   }

   public void AnimaInfo()
   {

    if(sobeInfo == false)
    {
        infoAnim.Play ("AnimaInfo");
        sobeInfo = true;
    }
    else
    {
        infoAnim.Play ("AnimaInfo_Inverse");
        sobeInfo = false;
    }

   }

   public void LigaDesligaSom()
   {
    musica.mute = !musica.mute;

    if(musica.mute == true)
    {
        btnSom.image.sprite = somDesligado;
    } else{
        btnSom.image.sprite = somLigado;
    }

   }

   public void Youtube()
   {
    Application.OpenURL("https://www.youtube.com/channel/UCmUN4irO7AIxgfatggVb3QA");
   }
    
}
