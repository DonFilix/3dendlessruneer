using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiScores : MonoBehaviour {
    public Text hiscore;
    // Use this for initialization
    CharacterMovement Scrptooo;

    private void Presmetaj()
    {
        hiscore.text = "" + (int)PlayerPrefs.GetFloat("HISCORE");
    }
	void Start () {

       Scrptooo = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Scrptooo.DeathOrNot == true)
            Presmetaj();	
	}
}
