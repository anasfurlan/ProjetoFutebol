using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Bola
    [SerializeField]
    public GameObject[] bola; //crianção da bola com array
    public int bolasNum = 2;
   // private bool bolaMorreu = false;
    public int bolasEmCena = 0;
    public Transform pos;
    public bool win;
    public int tiro = 0;
    //public int ondeEstou; //usar o index para se locomover melhor entre fases. Variavel vai identificar onde estou. (file; buil set)
    public bool jogoComecou;
    private bool chuteDado;

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

         SceneManager.sceneLoaded += Carrega;

         pos = GameObject.Find ("posStart").GetComponent<Transform> ();
        
    }

     void Carrega(Scene cena, LoadSceneMode modo)
    {
        if(OndeEstou.instance.fase != 4 && OndeEstou.instance.fase != 5)
        {
        //aqui coloca o que estamos buscando dentro da cena
        pos = GameObject.Find ("posStart").GetComponent<Transform> ();
        //OndeEstou = SceneManager.GetActiveScene ().buildIndex;
        StartGame ();
        }
    }
    
    void Start()
    {
        StartGame ();
        ScoreManager.instance.GameStartScoreM ();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreManager.instance.UpdateScore ();
        UIManager.instance.UpdateUI ();

        if(Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene ("Level2");
        }

        NascBolas ();

        if(bolasNum <= 0)
        {
            GameOver ();
        }

        if(win == true)
        {
            WinGame ();
        }
    }

    void NascBolas()
    {
        if(OndeEstou.instance.fase >= 3)
        {
            if(bolasNum > 0 && bolasEmCena == 0 && Camera.main.transform.position.x <= 0.05f)
            {
                Instantiate (bola[OndeEstou.instance.bolaEmUso], new Vector2 (pos.position.x, pos.position.y), Quaternion.identity);
                bolasEmCena += 1;
                tiro = 0;
            }
        }
        else
        {
            if(bolasNum > 0 && bolasEmCena == 0)
            {
             Instantiate (bola[OndeEstou.instance.bolaEmUso],new Vector2 (pos.position.x,pos.position.y),Quaternion.identity);
             bolasEmCena += 1;
             tiro = 0;
            }
        }
    }

    void GameOver()
    {
        UIManager.instance.GameOverUI ();
        jogoComecou = false;
    }

    void WinGame()
    {
        UIManager.instance.WinGameUI ();
        jogoComecou = false;
    }

    void StartGame()
    {
        jogoComecou = true;
        bolasNum = 2;
        bolasEmCena = 0;
        win = false;
        UIManager.instance.StartUI ();
        
    }

   /* public void FixedUpdate()
    {
        if (bola.gameObject.GetComponent<Rigidbody2D>().IsSleeping() && chuteDado == true)
        {
            chuteDado = false;
            Destroy(bola.gameObject);
            //bolasEmCena -= 1;
            //bolasNum -= 1;
        }
    }*/
}
