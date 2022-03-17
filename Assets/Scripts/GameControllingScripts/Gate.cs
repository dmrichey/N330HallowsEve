using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    public float fadeDuration = 1.5f;
    public float displayImageDuration = 1.5f;
    //player
	public GameObject player;
	
	// images 
    public CanvasGroup winImageCanvasGroup; 
    public CanvasGroup loseBackgroundImageCanvasGroup;
	//checks to see if they are at the exit or caught.
    bool atExit;
    bool isCaught;
    float timer;
    
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            atExit = true;
        }
    }

    public void CaughtPlayer ()
    {
        isCaught = true;
    }

    void Update ()
    {
        if (atExit)
        {
            EndLevel (winImageCanvasGroup, false);
        }
        else if (isCaught)
        {
            EndLevel (loseBackgroundImageCanvasGroup, true);
        }
    }

    void EndLevel (CanvasGroup imageCanvasGroup, bool doRestart)
    {
		// display the image for a certain amount of time 
        timer += Time.deltaTime;
        imageCanvasGroup.alpha = timer / fadeDuration;
		
		// once the image is done restart or quit
        if (timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene (0);
            }
            else
            {
                Application.Quit ();
            }
        }
    }
}