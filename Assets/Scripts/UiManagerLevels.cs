using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManagerLevels : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moedasLevel;

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.instance.UpdateScore ();
        moedasLevel.text = PlayerPrefs.GetInt("moedasSave").ToString ();

        
    }

}
