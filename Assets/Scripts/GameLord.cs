using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLord : MonoBehaviour {
    float x;
    float y;
    float cuteFishnum;
    float sunFishnum;
    float chineseFishnum;
    public GameObject cuteFish;
    public GameObject sunFish;
    public GameObject chineseFish;
    public GameObject txtGameInfo;
    public GameObject UICanvas;
    public GameObject txtPlaceHolder;
    public List<GameObject> cuteFishList;
    public List<GameObject> sunFishList;
    public List<GameObject> chineseFishList;
    public List<GameObject> txtgameinfoList;
    Vector3 gameinfoPosition;

    private static GameLord instance;
    public static GameLord Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameLord>();
            }
            return instance;
        }
    }

	// Use this for initialization
	void Start () {
        cuteFishnum = Random.value + 0.7f;
        gameinfoPosition = new Vector2(100f, -30f);
        for (int i = 0; i < cuteFishnum * 100; i++)
        {
            if (Random.value < 0.5)
            {
                x = Random.value * 150;
                while (x < 1)
                {
                    x = Random.value * 150;
                }
            }
            if (Random.value > 0.5) { 
                x = Random.value * -150;
                while (x > -1)
                {
                    x = Random.value * -150;
                }
            }
            if (Random.value < 0.5)
            {
                y = Random.value * 100;
                while (y < 1)
                {
                    y = Random.value * 100;
                }
            }
            if (Random.value > 0.5)
            {
                y = Random.value * -100;
                while (y > -1)
                {
                    y = Random.value * -100;
                }
            }
            Vector2 fishPosition = new Vector2(x, y);
            cuteFishList.Add(Instantiate(cuteFish, fishPosition, Quaternion.identity));
        }
        sunFishnum = Random.value + 0.7f;
        for (int i = 0; i < sunFishnum * 60; i++)
        {
            if (Random.value < 0.5)
            {
                x = Random.value * 150;
            }
            if (Random.value > 0.5)
            {
                x = Random.value * -150;
            }
            if (Random.value < 0.5)
            {
                y = Random.value * 100;
            }
            if (Random.value > 0.5)
            {
                y = Random.value * -100;
            }
            Vector2 fishPosition = new Vector2(x, y);
            sunFishList.Add(Instantiate(sunFish, fishPosition, Quaternion.identity));
        }
        chineseFishnum = Random.value + 0.7f;
        for (int i = 0; i < chineseFishnum * 45; i++)
        {
            if (Random.value < 0.5)
            {
                x = Random.value * 150;
                while (x < 1)
                {
                    x = Random.value * 150;
                }
            }
            if (Random.value > 0.5)
            {
                x = Random.value * -150;
                while (x > -1)
                {
                    x = Random.value * -150;
                }
            }
            if (Random.value < 0.5)
            {
                y = Random.value * 100;
                while (y < 1)
                {
                    y = Random.value * 100;
                }
            }
            if (Random.value > 0.5)
            {
                y = Random.value * -100;
                while (y > -1)
                {
                    y = Random.value * -100;
                }
            }
            Vector2 fishPosition = new Vector2(x, y);
            chineseFishList.Add(Instantiate(chineseFish, fishPosition, Quaternion.identity));
        }
    }

    public void updateGameInfo(string txt)
    {
        //Vector3 offset = new Vector3(0, -30 * txtgameinfoList.Count, 0);
        txtgameinfoList.Add(Instantiate(txtGameInfo, Vector3.zero, Quaternion.identity));
        txtgameinfoList[txtgameinfoList.Count - 1].GetComponent<RectTransform>().SetParent(UICanvas.transform);
        txtgameinfoList[txtgameinfoList.Count - 1].GetComponent<RectTransform>().anchorMax = new Vector2(0, 1f);
        txtgameinfoList[txtgameinfoList.Count - 1].GetComponent<RectTransform>().anchorMin = new Vector2(0, 1f);
        txtgameinfoList[txtgameinfoList.Count - 1].GetComponent<RectTransform>().anchoredPosition = gameinfoPosition;
        txtgameinfoList[txtgameinfoList.Count - 1].GetComponent<Text>().text = txt;
        for (int i = 0; i < txtgameinfoList.Count - 1; i++)
        {
            txtgameinfoList[i].GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 13);
        }
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown("g"))
        {
            updateGameInfo("TEST " + Random.value);
        }

	}
}
