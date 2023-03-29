using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BolaControl : MonoBehaviour
{
    //seta
    public GameObject setaGo;

    // angulo
    public float zRotate;
    public bool liberaRot = false;
    public bool liberaTiro = false;
    //public bool chutou = false;
    //public float velocidadeMinima = 0.5f;

    // forca
    private Rigidbody2D bola;
    public float force = 0;
    public GameObject seta2Img;

    // paredes
    private Transform paredeLD,paredeLE;

    //Morte Bola Anim
    //[SerializeField]
    //private GameObject MorteBolaAnim;

    //toque
    //private Collider2D toqueCol;


    void Awake()
    {
        setaGo = GameObject.Find ("Seta");
        seta2Img = setaGo.transform.GetChild (0).gameObject;
        setaGo.GetComponent<Image> ().enabled = false;
        seta2Img.GetComponent<Image> ().enabled = false;
        paredeLD = GameObject.Find ("PAREDE_LD").GetComponent<Transform> ();
        paredeLE = GameObject.Find ("PAREDE_LE").GetComponent<Transform> ();
    }

    void Start()
    {
        //forca
        bola = GetComponent<Rigidbody2D> ();
       
    }

    // Update is called once per frame
    void Update()
    {
        RotacaoSeta ();
        InputDeRotacao ();
        LimitaRotacao ();
        PosicionaSeta ();

        //forca
        ControlaForca ();
        AplicaForca ();    

        //paredes
        Paredes ();

        //bola parada destruir
        /*DestroiBolaParada();

         if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }*/
    }

   /*  void DestroiBolaParada()
    {
        if (chutou && bola.velocity.magnitude <= velocidadeMinima)
        {
            DestroiBola();
        }
    }*/

    void PosicionaSeta()
    {
        setaGo.GetComponent<Image>().rectTransform.position = transform.position;
    }

    void RotacaoSeta()
    {
        setaGo.GetComponent<Image>().rectTransform.eulerAngles = new Vector3 (0,0,zRotate);
    }

    void InputDeRotacao()
    {

        if(liberaRot == true)
      {
        //float moveX = Input.GetAxis ("Mouse X");
        float moveY = Input.GetAxis ("Mouse Y");

        if (zRotate < 90)
         {
                if (moveY > 0)
                {
                    zRotate += 2.5f;
                }
         }

        if (zRotate > 0)
          {
                if (moveY < 0)
                {
                    zRotate -= 2.5f;
                }
          }

      }

    }    

      void LimitaRotacao()
    {

      if (zRotate >= 90)
        {
            zRotate = 90;
        }

      if (zRotate <= 0)
        {
            zRotate = 0;
        }

    } 

    void OnMouseDown()
    {
        if(GameManager.instance.tiro == 0)
        {
            liberaRot = true;
            setaGo.GetComponent<Image> ().enabled = true;
            seta2Img.GetComponent<Image> ().enabled = true;

            //toqueCol = GameObject.FindGameObjectWithTag("bola").GetComponentInChildren<Collider2D>();
        }
       
    }

    void OnMouseUp()
    {
        liberaRot = false;
        setaGo.GetComponent<Image> ().enabled = false;
        seta2Img.GetComponent<Image> ().enabled = false;

        if(GameManager.instance.tiro == 0 && force > 0)
        {
            liberaTiro = true;
            seta2Img.GetComponent<Image> ().fillAmount = 0;
            AudioManager.instance.SonsFXToca (1);
            GameManager.instance.tiro = 1;
            //toqueCol.enabled = false;
        }
        
    }

    //forca
    void AplicaForca()
    {
        //cos significa coceno e sin seno
        float x = force + Mathf.Cos (zRotate * Mathf.Deg2Rad);
        float y = force + Mathf.Sin (zRotate * Mathf.Deg2Rad);

        if(liberaTiro == true)
        {
            bola.AddForce(new Vector2 (x,y));
           // StartCoroutine(DelayChute()); // Start Corrotina verificação bola parada !!!
            liberaTiro = false;
        }
    }

    void ControlaForca()
    {
        if(liberaRot == true)
        {
            float moveX = Input.GetAxis ("Mouse X");

            if(moveX < 0)
            {
                seta2Img.GetComponent<Image>().fillAmount += 1 * Time.deltaTime;
                force = seta2Img.GetComponent<Image>().fillAmount * 2000;
            }

             if(moveX > 0)
            {
                seta2Img.GetComponent<Image>().fillAmount -= 1 * Time.deltaTime;
                force = seta2Img.GetComponent<Image>().fillAmount * 2000;
            }
        }
    }

    void bolaDinamica()
    {
        this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
    }

    void Paredes()
    {
        if(this.gameObject.transform.position.x > paredeLD.position.x)
        {
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }

        if(this.gameObject.transform.position.x < paredeLE.position.x)
        {
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if(outro.gameObject.CompareTag("morte"))
        {
            //Instantiate (MorteBolaAnim, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }

        if(outro.gameObject.CompareTag("win"))
        {
            GameManager.instance.win = true;
            int temp = OndeEstou.instance.fase - 1;
            //temp++;
            PlayerPrefs.SetInt ("Level" + temp, 1);
        }
    }

   /* IEnumerator DelayChute()
    {
        yield return new WaitForSeconds(0.1f);
        chutou = true;
    }
 
    void DestroiBola()
    {
        GameManager.instance.bolasEmCena -= 1;
        GameManager.instance.bolasNum -= 1;
        Destroy(this.gameObject);
    }*/
}
