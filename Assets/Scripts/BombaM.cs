using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaM : MonoBehaviour
{
    [SerializeField]
    private GameObject BombaFx;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D outro)
    {
        if(outro.gameObject.CompareTag("bola"))
        {
            Instantiate (BombaFx, new Vector2 (this.transform.position.x, this.transform.position.y), Quaternion.identity);

        }
    }
}
