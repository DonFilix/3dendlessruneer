using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour {
    private float score = 0.0f;
    public Text scoreText;
    public MenuDeathScript MenuDeath;
    
    private bool DeathOrNot;
    // Use this for initialization

    private int FramesPerSec;
    private float frequency = 1.0f;
    private string fps;



    private IEnumerator FPS()
    {
        for (;;)
        {
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;

            // Display it

            fps = string.Format("FPS: {0}", Mathf.RoundToInt(frameCount / timeSpan));
        }
    }
    void Start () {
      
        StartCoroutine(FPS());
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (DeathOrNot)       
            return;

        scoreText.text = Time.deltaTime.ToString() + ((int)score).ToString() + "/" + fps + "/" + GetComponent<CharacterMovement>().VelocityChar.ToString();
        score += 0.1f;


           GetComponent<CharacterMovement>().VelocityChar += 0.0001f;
 
	}

    public void YouAreDeath()
    {
        DeathOrNot = true;
        if (score > PlayerPrefs.GetFloat("HISCORE"))
        PlayerPrefs.SetFloat("HISCORE",score);
        MenuDeath.MenuDeath(score);
    }


}
