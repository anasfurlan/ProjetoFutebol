using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBomba : MonoBehaviour
{
    private GameObject BombaRep;

    void Start()
    {
        BombaRep = GameObject.Find ("Barril");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine (Vida ());
    }

    IEnumerator Vida()
    {
        yield return new WaitForSeconds (0.5f);
        Destroy (BombaRep.gameObject);
        Destroy (this.gameObject);
    }
}
