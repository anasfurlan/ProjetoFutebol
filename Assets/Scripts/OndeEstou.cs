using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OndeEstou : MonoBehaviour
{
   public int fase = -1;
   [SerializeField]
   private GameObject UiManagerGo, GameManagerGo;

   public static OndeEstou instance;

   public int bolaEmUso;

   private float orthoSize = 5; // para ajustar tanhamo de tela em diferentes dispositivos
   [SerializeField]
   private float aspect = 1.76f;

   void Awake()
   {
    if(instance == null)
    {
        instance = this;
        DontDestroyOnLoad (this.gameObject);
    }
    else
    {
        Destroy (gameObject);
    }

    SceneManager.sceneLoaded += VerificaFase;

    bolaEmUso = PlayerPrefs.GetInt ("BolaUse"); 

   }

   void VerificaFase(Scene cena, LoadSceneMode modo)
   {
    fase = SceneManager.GetActiveScene ().buildIndex;

    if(fase != 4 && fase != 5 && fase!= 6)
    {
        Instantiate (UiManagerGo);
        Instantiate (GameManagerGo);
        Camera.main.projectionMatrix = Matrix4x4.Ortho (-orthoSize * aspect, orthoSize * aspect, -orthoSize, orthoSize, Camera.main.nearClipPlane, Camera.main.farClipPlane);
    }

   }
}
