using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //usado para buscar algo dentro da cena
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private TextMeshProUGUI pontosUI, bolasUI;
    [SerializeField]
    private GameObject losePainel, winPainel, pausePainel;
    [SerializeField]
    private Button pauseBtn, pauseBtn_Return;
    [SerializeField]
    private Button btnNovamenteLose, btnLevelLose; // Botões Lose
    private Button btnLevelWin, btnNovamenteWin, btnAvancaWin; //Botões Win

    public int moedasNumAntes, moedasNumDepois, resultado;



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
        PegaDados (); 
        
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        PegaDados (); 
    }

    void PegaDados()
    {
        if(OndeEstou.instance.fase != 4 && OndeEstou.instance.fase != 5)
        {
        //Elementos UI pontos e bolas
        pontosUI = GameObject.Find ("PontosUI").GetComponent<TextMeshProUGUI> ();
        bolasUI = GameObject.Find ("bolasUI").GetComponent<TextMeshProUGUI> ();
        //Paineis
        losePainel = GameObject.Find ("Lose_painel");
        winPainel = GameObject.Find ("Win_painel");
        pausePainel = GameObject.Find ("Pause_painel");
        //Botões Pause
        pauseBtn = GameObject.Find ("Pause").GetComponent<Button> ();
        pauseBtn_Return = GameObject.Find ("pause").GetComponent<Button> ();
        //Botões Lose
        btnNovamenteLose = GameObject.Find ("NovamenteLOSE").GetComponent<Button> ();
        btnLevelLose = GameObject.Find ("MenuFasesLOSE").GetComponent<Button> ();
        //Botões Win
        btnLevelWin = GameObject.Find ("MenuFasesWIN").GetComponent<Button> ();
        btnNovamenteWin = GameObject.Find ("NovamenteWIN").GetComponent<Button> ();
        btnAvancaWin = GameObject.Find ("AvancarWIN").GetComponent<Button> ();

        //Eventos

        //Eventos Pause
        pauseBtn.onClick.AddListener (Pause); //pausa a cena
        pauseBtn_Return.onClick.AddListener (PauseReturn);

        //Eventos You Lose
        btnNovamenteLose.onClick.AddListener (JogarNovamente);
        btnLevelLose.onClick.AddListener (Levels);
        //Eventos You Win
        btnLevelWin.onClick.AddListener (Levels);
        btnNovamenteWin.onClick.AddListener (JogarNovamente);
        btnAvancaWin.onClick.AddListener (ProximaFase);

        //moedas
        moedasNumAntes = PlayerPrefs.GetInt ("moedasSave");
        }
    }

    public void StartUI()
    {
        LigaDesligapainel (); 
    }

    
    public void UpdateUI()
    {
        pontosUI.text = ScoreManager.instance.moedas.ToString();
        bolasUI.text = GameManager.instance.bolasNum.ToString();
        moedasNumDepois = ScoreManager.instance.moedas;
        
    }

    //metodo para verificar que chegou no limite do jogo
    public void GameOverUI()
    {
        losePainel.SetActive (true);
    }

    public void WinGameUI()
    {
        winPainel.SetActive (true);
    }

    void LigaDesligapainel()
    {
        StartCoroutine (tempo());
    }

    void Pause()
    {
        pausePainel.SetActive (true);
        pausePainel.GetComponent<Animator> ().Play ("MOVEUI_PAUSE");
        Time.timeScale = 0;
    }

     void PauseReturn()
    {
        pausePainel.GetComponent<Animator> ().Play ("MOVEUI_PAUSER");
        Time.timeScale = 1;
        StartCoroutine (EsperaPause());
    }

    IEnumerator EsperaPause()
    {
        yield return new WaitForSeconds (0.8f);
        pausePainel.SetActive (false);
    }

    IEnumerator tempo()
    {
        yield return new WaitForSeconds (0.001f);
        losePainel.SetActive (false);
        winPainel.SetActive (false);
        pausePainel.SetActive (false);
    }

    void JogarNovamente()
    {
        if(GameManager.instance.win == false)
        {
            SceneManager.LoadScene (OndeEstou.instance.fase);
            resultado = moedasNumDepois - moedasNumAntes;
            ScoreManager.instance.PerdeMoedas (resultado);
            resultado = 0;
        }
        else
        {
            SceneManager.LoadScene (OndeEstou.instance.fase);
            resultado = 0;
        }

    }

    void Levels()
    {
        if(GameManager.instance.win == false)
        {
            resultado = moedasNumDepois - moedasNumAntes;
            ScoreManager.instance.PerdeMoedas (resultado);
            resultado = 0;
            SceneManager.LoadScene (4);
        }
        else
        {
            resultado = 0;
            SceneManager.LoadScene (4);
        }
    }

    void ProximaFase()
    {
        if(GameManager.instance.win == true)
        {
            int temp = OndeEstou.instance.fase + 1;
            SceneManager.LoadScene (temp);
        }
    }
}
