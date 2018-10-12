using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    //public int LevelSize;
    public GameObject[] LevelPieces;
    List<GameObject> leveltiles = new List<GameObject>();
    Vector2 home = new Vector2(-6, 0);

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
        bool tilegenerated = false;
        bool samepositionError = false;
        GenerateHomeTile();
        for (int p = 0; p <= levelsize; p++)
        {
            GenerateTile(leveltiles[p]);
        }
    }

    void GenerateHomeTile()
    {
        foreach (GameObject levelPiece in LevelPieces)
        {
            if (levelPiece.GetComponent<LevelPiece>().IsLevelBeginning)
            {
                GameObject beginningTile = Instantiate(levelPiece, home, Quaternion.identity);
                beginningTile.GetComponent<LevelPiece>().GridPosition = Vector2.zero;
                leveltiles.Add(beginningTile);
                break;
            }
        }
    }

    void GenerateTile(GameObject currenttile)
    {
        bool Righttilegenerated = false;
        bool Lefttilegenerated = false;
        bool Toptilegenerated = false;
        bool Bottomtilegenerated = false;
        bool samepositionError = false;

        if (currenttile.GetComponent<LevelPiece>().RightConnect)
        {
            samepositionError = false;
            while (!Righttilegenerated)
            {
                foreach (GameObject levelPiece in LevelPieces)
                {
                    if (Random.value < 0.1 && levelPiece.GetComponent<LevelPiece>().LeftConnect && !levelPiece.GetComponent<LevelPiece>().IsLevelBeginning)
                    {
                        Vector2 newgridposition = new Vector2(currenttile.GetComponent<LevelPiece>().GridPosition.x + 1, currenttile.GetComponent<LevelPiece>().GridPosition.y);
                        foreach (GameObject tilecompare in leveltiles)
                        {
                            if (tilecompare.GetComponent<LevelPiece>().GridPosition == newgridposition)
                            {
                                samepositionError = true;
                                Righttilegenerated = true;
                                break;
                            }
                        }
                        if (!samepositionError)
                        {
                            GameObject NewTile = Instantiate(levelPiece, new Vector2(currenttile.transform.position.x + 3, currenttile.transform.position.y), Quaternion.identity);
                            NewTile.GetComponent<LevelPiece>().GridPosition = newgridposition;
                            leveltiles.Add(NewTile);
                            Righttilegenerated = true;
                            break;
                        }
                    }
                }
            }
        }

        if (currenttile.GetComponent<LevelPiece>().LeftConnect)
        {
            samepositionError = false;
            while (!Lefttilegenerated)
            {
                foreach (GameObject levelPiece in LevelPieces)
                {
                    if (Random.value < 0.1 && levelPiece.GetComponent<LevelPiece>().RightConnect && !levelPiece.GetComponent<LevelPiece>().IsLevelBeginning)
                    {
                        Vector2 newgridposition = new Vector2(currenttile.GetComponent<LevelPiece>().GridPosition.x - 1, currenttile.GetComponent<LevelPiece>().GridPosition.y);
                        foreach (GameObject tilecompare in leveltiles)
                        {
                            if (tilecompare.GetComponent<LevelPiece>().GridPosition == newgridposition)
                            {
                                samepositionError = true;
                                Lefttilegenerated = true;
                                break;
                            }
                        }
                        if (!samepositionError)
                        {
                            GameObject NewTile = Instantiate(levelPiece, new Vector2(currenttile.transform.position.x - 3, currenttile.transform.position.y), Quaternion.identity);
                            NewTile.GetComponent<LevelPiece>().GridPosition = newgridposition; leveltiles.Add(NewTile);
                            Lefttilegenerated = true;
                            break;
                        }
                    }
                }
            }
        }

        if (currenttile.GetComponent<LevelPiece>().TopConnect)
        {
            samepositionError = false;
            while (!Toptilegenerated)
            {
                foreach (GameObject levelPiece in LevelPieces)
                {
                    if (Random.value < 0.1 && levelPiece.GetComponent<LevelPiece>().BottomConnect && !levelPiece.GetComponent<LevelPiece>().IsLevelBeginning)
                    {
                        Vector2 newgridposition = new Vector2(currenttile.GetComponent<LevelPiece>().GridPosition.x, currenttile.GetComponent<LevelPiece>().GridPosition.y + 1);
                        foreach (GameObject tilecompare in leveltiles)
                        {
                            if (tilecompare.GetComponent<LevelPiece>().GridPosition == newgridposition)
                            {
                                samepositionError = true;
                                Toptilegenerated = true;
                                break;
                            }
                        }
                        if (!samepositionError)
                        {
                            GameObject NewTile = Instantiate(levelPiece, new Vector2(currenttile.transform.position.x, currenttile.transform.position.y + 3), Quaternion.identity);
                            NewTile.GetComponent<LevelPiece>().GridPosition = newgridposition; leveltiles.Add(NewTile);
                            Toptilegenerated = true;
                            break;
                        }
                    }
                }
            }
        }

        if (currenttile.GetComponent<LevelPiece>().BottomConnect)
        {
            samepositionError = false;
            while (!Bottomtilegenerated)
            {
                foreach (GameObject levelPiece in LevelPieces)
                {
                    if (Random.value < 0.1 && levelPiece.GetComponent<LevelPiece>().TopConnect && !levelPiece.GetComponent<LevelPiece>().IsLevelBeginning)
                    {
                        Vector2 newgridposition = new Vector2(currenttile.GetComponent<LevelPiece>().GridPosition.x, currenttile.GetComponent<LevelPiece>().GridPosition.y - 1);
                        foreach (GameObject tilecompare in leveltiles)
                        {
                            if (tilecompare.GetComponent<LevelPiece>().GridPosition == newgridposition)
                            {
                                samepositionError = true;
                                Bottomtilegenerated = true;
                                break;
                            }
                        }
                        if (!samepositionError)
                        {
                            GameObject NewTile = Instantiate(levelPiece, new Vector2(currenttile.transform.position.x, currenttile.transform.position.y - 3), Quaternion.identity);
                            NewTile.GetComponent<LevelPiece>().GridPosition = newgridposition; leveltiles.Add(NewTile);
                            Bottomtilegenerated = true;
                            break;
                        }
                    }
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
        GenerateLevel(100);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
