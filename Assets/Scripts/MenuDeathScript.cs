using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuDeathScript : MonoBehaviour {
    public Text scoreText;
	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuDeath(float FinalScore)
    {
        gameObject.SetActive(true);
        scoreText.text = ((int)FinalScore).ToString();
    }
}
