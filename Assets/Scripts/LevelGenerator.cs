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
        bool samepositionError = false;
        int tileproblems = 0;
        //Vector2 gridposition = Vector2.zero;
        int i = 0;
        int p = 0;
        List<GameObject> leveltiles = new List<GameObject>();
        Vector2 home = new Vector2(-6, 0);
        foreach (GameObject levelPiece in LevelPieces)
        {
            if (levelPiece.GetComponent<LevelPiece>().IsLevelBeginning)
            {
                GameObject beginningTile = Instantiate(levelPiece, home, Quaternion.identity);
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
                samepositionError = false;
                foreach (GameObject levelPiece in LevelPieces)
                {
                    if (Random.value < 0.1 && levelPiece.GetComponent<LevelPiece>().LeftConnect && !levelPiece.GetComponent<LevelPiece>().IsLevelBeginning)
                    {
                        GameObject NewTile = Instantiate(levelPiece, new Vector2(leveltiles[p].transform.position.x + 3, leveltiles[p].transform.position.y), Quaternion.identity);
                        NewTile.GetComponent<LevelPiece>().GridPosition = new Vector2(leveltiles[p].GetComponent<LevelPiece>().GridPosition.x + 1, leveltiles[p].GetComponent<LevelPiece>().GridPosition.y);
                        foreach (GameObject levelpiececompare in leveltiles)
                        {
                            if (NewTile.GetComponent<LevelPiece>().GridPosition == levelpiececompare.GetComponent<LevelPiece>().GridPosition)
                            {
                                Destroy(NewTile);
                                samepositionError = true;
                                break;
                            }
                        }
                        if (!samepositionError)
                        {
                            leveltiles.Add(NewTile);
                            i++;
                            break;
                        }
                    }
                }
            }
            if (leveltiles[p].GetComponent<LevelPiece>().LeftConnect)
            {
                samepositionError = false;
                foreach (GameObject levelPiece in LevelPieces)
                {
                    if (Random.value < 0.1 && levelPiece.GetComponent<LevelPiece>().RightConnect && !levelPiece.GetComponent<LevelPiece>().IsLevelBeginning)
                    {
                        GameObject NewTile = Instantiate(levelPiece, new Vector2(leveltiles[p].transform.position.x - 3, leveltiles[p].transform.position.y), Quaternion.identity);
                        NewTile.GetComponent<LevelPiece>().GridPosition = new Vector2(leveltiles[p].GetComponent<LevelPiece>().GridPosition.x - 1, leveltiles[p].GetComponent<LevelPiece>().GridPosition.y);
                        foreach (GameObject levelpiececompare in leveltiles)
                        {
                            if (NewTile.GetComponent<LevelPiece>().GridPosition == levelpiececompare.GetComponent<LevelPiece>().GridPosition)
                            {
                                Destroy(NewTile);
                                samepositionError = true;
                                break;
                            }
                        }
                        if (!samepositionError)
                        {
                            leveltiles.Add(NewTile);
                            i++;
                            break;
                        }
                    }
                }
            }
            if (leveltiles[p].GetComponent<LevelPiece>().TopConnect)
            {
                samepositionError = false;
                foreach (GameObject levelPiece in LevelPieces)
                {
                    if (Random.value < 0.1 && levelPiece.GetComponent<LevelPiece>().BottomConnect && !levelPiece.GetComponent<LevelPiece>().IsLevelBeginning)
                    {
                        GameObject NewTile = Instantiate(levelPiece, new Vector2(leveltiles[p].transform.position.x, leveltiles[p].transform.position.y + 3), Quaternion.identity);
                        NewTile.GetComponent<LevelPiece>().GridPosition = new Vector2(leveltiles[p].GetComponent<LevelPiece>().GridPosition.x, leveltiles[p].GetComponent<LevelPiece>().GridPosition.y + 1);
                        foreach (GameObject levelpiececompare in leveltiles)
                        {
                            if (NewTile.GetComponent<LevelPiece>().GridPosition == levelpiececompare.GetComponent<LevelPiece>().GridPosition)
                            {
                                Destroy(NewTile);
                                samepositionError = true;
                                break;
                            }
                        }
                        if (!samepositionError)
                        {
                            leveltiles.Add(NewTile);
                            i++;
                            break;
                        }
                    }
                }
            }
            if (leveltiles[p].GetComponent<LevelPiece>().BottomConnect)
            {
                samepositionError = false;
                foreach (GameObject levelPiece in LevelPieces)
                {
                    if (Random.value < 0.1 && levelPiece.GetComponent<LevelPiece>().TopConnect && !levelPiece.GetComponent<LevelPiece>().IsLevelBeginning)
                    {
                        GameObject NewTile = Instantiate(levelPiece, new Vector2(leveltiles[p].transform.position.x, leveltiles[p].transform.position.y - 3), Quaternion.identity);
                        NewTile.GetComponent<LevelPiece>().GridPosition = new Vector2(leveltiles[p].GetComponent<LevelPiece>().GridPosition.x, leveltiles[p].GetComponent<LevelPiece>().GridPosition.y - 1);
                        foreach (GameObject levelpiececompare in leveltiles)
                        {
                            if (NewTile.GetComponent<LevelPiece>().GridPosition == levelpiececompare.GetComponent<LevelPiece>().GridPosition)
                            {
                                Destroy(NewTile);
                                samepositionError = true;
                                break;
                            }
                        }
                        if (!samepositionError)
                        {
                            leveltiles.Add(NewTile);
                            i++;
                            break;
                        }
                    }
                }
            }
            p++;
            if (p == levelsize)
            {
                p = 0;
            }
            if (i == levelsize)
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
                        Debug.Log("generate complete");
                    }
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
        GenerateLevel(10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
