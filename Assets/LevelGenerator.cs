using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    //public int LevelSize;
    public GameObject[] LevelPieces;
    private static LevelGenerator instance;
    public static LevelGenerator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<LevelGenerator>();
            }
            return instance;
        }
    }

    public void GenerateLevel(int levelsize)
    {
        bool generatecomplete = false;
        int tileproblems = 0;
        //Vector2 gridposition = Vector2.zero;
        int i = 0;
        int p = 0;
        List<GameObject> leveltiles = new List<GameObject>();
        Vector2 home = new Vector2(-6, 0);
        foreach (GameObject LevelPiece in LevelPieces)
        {
            if (LevelPiece.GetComponent<LevelPiece>().IsLevelBeginning)
            {
                GameObject beginningTile = Instantiate(LevelPiece, home, Quaternion.identity);
                beginningTile.GetComponent<LevelPiece>().GridPosition = Vector2.zero;
                leveltiles.Add(beginningTile);
                p = 0;
                i = 0;
                break;
            }
        }
        while (!generatecomplete) {
            if (leveltiles[p].GetComponent<LevelPiece>().RightConnect)
            {
                foreach (GameObject LevelPiece in LevelPieces)
                {
                    if (Random.value < 0.1 && LevelPiece.GetComponent<LevelPiece>().LeftConnect && !LevelPiece.GetComponent<LevelPiece>().IsLevelBeginning)
                    {
                        GameObject NewTile = Instantiate(LevelPiece, new Vector2(leveltiles[p].transform.position.x + 3, leveltiles[p].transform.position.y), Quaternion.identity);
                        NewTile.GetComponent<LevelPiece>().GridPosition = new Vector2(leveltiles[p].GetComponent<LevelPiece>().GridPosition.x + 1, leveltiles[p].GetComponent<LevelPiece>().GridPosition.y);
                        i++;
                        break;
                    }
                }
            }
            if (leveltiles[p].GetComponent<LevelPiece>().LeftConnect)
            {
                foreach (GameObject LevelPiece in LevelPieces)
                {
                    if (Random.value < 0.1 && LevelPiece.GetComponent<LevelPiece>().RightConnect && !LevelPiece.GetComponent<LevelPiece>().IsLevelBeginning)
                    {
                        GameObject NewTile = Instantiate(LevelPiece, new Vector2(leveltiles[p].transform.position.x - 3, leveltiles[p].transform.position.y), Quaternion.identity);
                        NewTile.GetComponent<LevelPiece>().GridPosition = new Vector2(leveltiles[p].GetComponent<LevelPiece>().GridPosition.x - 1, leveltiles[p].GetComponent<LevelPiece>().GridPosition.y);
                        i++;
                        break;
                    }
                }
            }
            if (leveltiles[p].GetComponent<LevelPiece>().TopConnect)
            {
                foreach (GameObject LevelPiece in LevelPieces)
                {
                    if (Random.value < 0.1 && LevelPiece.GetComponent<LevelPiece>().BottomConnect && !LevelPiece.GetComponent<LevelPiece>().IsLevelBeginning)
                    {
                        GameObject NewTile = Instantiate(LevelPiece, new Vector2(leveltiles[p].transform.position.x, leveltiles[p].transform.position.y + 3), Quaternion.identity);
                        NewTile.GetComponent<LevelPiece>().GridPosition = new Vector2(leveltiles[p].GetComponent<LevelPiece>().GridPosition.x, leveltiles[p].GetComponent<LevelPiece>().GridPosition.y + 1);
                        i++;
                        break;
                    }
                }
            }
            if (leveltiles[p].GetComponent<LevelPiece>().BottomConnect)
            {
                foreach (GameObject LevelPiece in LevelPieces)
                {
                    if (Random.value < 0.1 && LevelPiece.GetComponent<LevelPiece>().TopConnect && !LevelPiece.GetComponent<LevelPiece>().IsLevelBeginning)
                    {
                        GameObject NewTile = Instantiate(LevelPiece, new Vector2(leveltiles[p].transform.position.x, leveltiles[p].transform.position.y - 3), Quaternion.identity);
                        NewTile.GetComponent<LevelPiece>().GridPosition = new Vector2(leveltiles[p].GetComponent<LevelPiece>().GridPosition.x, leveltiles[p].GetComponent<LevelPiece>().GridPosition.y - 1);
                        i++;
                        break;
                    }
                }
            }
            p++;
            if (p == levelsize)
            {
                tileproblems = 0;
                foreach (GameObject levelpiece in leveltiles)
                {
                    foreach (GameObject levelpiececheck in leveltiles)
                    {
                        if (levelpiece.GetComponent<LevelPiece>().GridPosition == levelpiececheck.GetComponent<LevelPiece>().GridPosition)
                        {
                            Destroy(levelpiececheck);
                            tileproblems++;
                            p--;
                            break;
                        }
                    }
                    if (tileproblems == 0)
                    {
                        generatecomplete = true;
                    }
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
