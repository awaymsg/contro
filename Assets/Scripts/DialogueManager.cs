using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    private static DialogueManager instance;
    public GameObject textPrefab;
    public List<GameObject> NewDialogueText;
    float dialogueTime;
    bool finishedTalking;
    GameObject currentTextBox;
    GameObject DialogueTextBox;
    string DialogueName;
    string FishName;
    string FishType;
    string gender;
    GameObject Fish;
    int FishGender;
    int saythingnumber;

    void Start()
    {
        NewDialogueText = new List<GameObject>();
        finishedTalking = true;
        saythingnumber = 0;
    }
    public static DialogueManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<DialogueManager>();
            }
            return instance;
        }
    }

    public void createDialogueText(Vector3 position, Transform parent, string txtString)
    {
        DialogueTextBox = Instantiate(textPrefab, position, Quaternion.identity) as GameObject;
        NewDialogueText.Add(DialogueTextBox);
        Debug.Log(NewDialogueText.Count);
        DialogueTextBox.GetComponent<TextMesh>().text = txtString;
        DialogueTextBox.transform.SetParent(parent);
        DialogueTextBox.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
	}

    public void beginDialogue(Vector3 position, Transform parent, string dialoguename, string fishname, int fishgender, string fishtype, GameObject fish, float dialoguetime)
    {
        if (Fish == null)
        {
            //for (int i = 0; i < NewDialogueText.Count; i++)
            //{
            //    NewDialogueText[i].GetComponent<dialogueTextBehaviour>().Terminate(NewDialogueText[i],false);
            //}
            currentTextBox = Instantiate(textPrefab, position, Quaternion.identity) as GameObject;
            currentTextBox.transform.SetParent(parent);
            currentTextBox.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
            currentTextBox.GetComponent<DMGTextBehaviour>().dontTerminate();
            DialogueName = dialoguename;
            FishName = fishname;
            FishGender = fishgender;
            saythingnumber = 0;
            FishType = fishtype;
            Fish = fish;
            dialogueTime = dialoguetime;
            finishedTalking = false;
        }
    }

    void Update()
    {
        if (finishedTalking == false)
        {
            dialogueTime -= Time.deltaTime;
            if (dialogueTime < 0)
            {
                endTalking();
                Debug.Log(DialogueName);
            }
            if (Input.GetKeyDown("t"))
            {
                saythingnumber++;
                dialogueTime = 5;
            }

            //first dialogue
            if (DialogueName == "first")
            {
                if (saythingnumber == 0)
                {
                    currentTextBox.GetComponent<TextMesh>().text = "oh hi there squiddie!";
                }
                if (saythingnumber == 1)
                {
                    currentTextBox.GetComponent<TextMesh>().text = "you must be new around here";
                }
                if (saythingnumber == 2)
                {
                    if (FishGender == 0) { gender = "female"; }
                    if (FishGender == 1) { gender = "male"; }
                    currentTextBox.GetComponent<TextMesh>().text = "i'm " + FishName + " the " + gender + " " + FishType;
                }
                if (saythingnumber == 3)
                {
                    currentTextBox.GetComponent<TextMesh>().text = "this thing seems to work";
                }
                if (saythingnumber == 4)
                {
                    currentTextBox.GetComponent<TextMesh>().text = "we are testing the dialogue system!";
                }
                if (saythingnumber == 5)
                {
                    endTalking();
                }
            }

            //happy dialogue
            if (DialogueName == "happy")
            {
                if (saythingnumber == 0)
                {
                    currentTextBox.GetComponent<TextMesh>().text = "i am so happy today!";
                }
                if (saythingnumber == 1)
                {
                    currentTextBox.GetComponent<TextMesh>().text = "the emperor rode through town!";
                }
                if (saythingnumber == 2)
                {
                    currentTextBox.GetComponent<TextMesh>().text = "he is so regal";
                }
                if (saythingnumber == 3)
                {
                    endTalking();
                }
            }

            //nice day dialogue
            if (DialogueName == "nice day")
            {
                if (saythingnumber == 0)
                {
                    currentTextBox.GetComponent<TextMesh>().text = "hey!";
                }
                if (saythingnumber == 1)
                {
                    currentTextBox.GetComponent<TextMesh>().text = "isn't it a nice day for a swim?";
                }
                if (saythingnumber == 2)
                {
                    currentTextBox.GetComponent<TextMesh>().text = "my dad used to always go swimming on days like this";
                }
                if (saythingnumber == 3)
                {
                    endTalking();
                }
            }
        }
    }
    public void endTalking()
    {
        currentTextBox.GetComponent<DMGTextBehaviour>().Terminate(currentTextBox, true);
        //Fish.GetComponent<FishBehaviour>().StopTalking();
        finishedTalking = true;
        Fish = null;
    }
}