using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGTextBehaviour : MonoBehaviour {

    float timeLeft;
    bool dontDestroy = false;
    // Use this for initialization
    private static DMGTextBehaviour instance;
    public static DMGTextBehaviour Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<DMGTextBehaviour>();
            }
            return instance;
        }
    }

    void Awake () {
        timeLeft = 1;
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0 && dontDestroy == false)
        {
            Terminate(gameObject, false);
        }
	}
    public void Terminate (GameObject currentbox, bool donotremove)
    {
        Destroy(currentbox);
        if (donotremove == false)
        {
            DialogueManager.Instance.NewDialogueText.Remove(currentbox);
        }
        dontDestroy = false;
    }

    public void dontTerminate()
    {
        dontDestroy = true;
    }
}
