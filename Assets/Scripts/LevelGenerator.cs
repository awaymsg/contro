using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    //public int LevelSize;
    public GameObject[] LevelPieces;
    public GameObject WallPiece;
    public GameObject FloorPiece;
    //public GameObject LadderPiece;
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
        GenerateHomeTile();
        for (int p = 0; p <= levelsize; p++)
        {
            GenerateTile(leveltiles[p]);
        }
        CloseEdges();
        FillLadders();
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

    void CloseEdges()
    {
        foreach (GameObject levelpiece in leveltiles)
        {
            bool spaceoccupiedleft = false;
            bool spaceoccupiedright = false;
            bool spaceoccupiedtop = false;
            bool spaceoccupiedbottom = false;
            foreach (GameObject levelpiececompare in leveltiles)
            {
                if (levelpiece.GetComponent<LevelPiece>().LeftConnect)
                {
                    Vector2 gridpositioncompare = new Vector2(levelpiece.GetComponent<LevelPiece>().GridPosition.x - 1, levelpiece.GetComponent<LevelPiece>().GridPosition.y);
                    if (gridpositioncompare == levelpiececompare.GetComponent<LevelPiece>().GridPosition)
                    {
                        spaceoccupiedleft = true;
                    }
                }
                if (levelpiece.GetComponent<LevelPiece>().RightConnect)
                {
                    Vector2 gridpositioncompare = new Vector2(levelpiece.GetComponent<LevelPiece>().GridPosition.x + 1, levelpiece.GetComponent<LevelPiece>().GridPosition.y);
                    if (gridpositioncompare == levelpiececompare.GetComponent<LevelPiece>().GridPosition)
                    {
                        spaceoccupiedright = true;
                    }
                }
                if (levelpiece.GetComponent<LevelPiece>().TopConnect)
                {
                    Vector2 gridpositioncompare = new Vector2(levelpiece.GetComponent<LevelPiece>().GridPosition.x, levelpiece.GetComponent<LevelPiece>().GridPosition.y + 1);
                    if (gridpositioncompare == levelpiececompare.GetComponent<LevelPiece>().GridPosition)
                    {
                        spaceoccupiedtop = true;
                    }
                }
                if (levelpiece.GetComponent<LevelPiece>().BottomConnect)
                {
                    Vector2 gridpositioncompare = new Vector2(levelpiece.GetComponent<LevelPiece>().GridPosition.x, levelpiece.GetComponent<LevelPiece>().GridPosition.y - 1);
                    if (gridpositioncompare == levelpiececompare.GetComponent<LevelPiece>().GridPosition)
                    {
                        spaceoccupiedbottom = true;
                    }
                }
            }
            if (!spaceoccupiedleft & levelpiece.GetComponent<LevelPiece>().LeftConnect)
            {
                Vector2 WallPosition = new Vector2(levelpiece.transform.position.x, levelpiece.transform.position.y + 0.1f);
                for (int i = 0; i < 10; i++)
                {
                    GameObject NewWall = Instantiate(WallPiece, WallPosition, Quaternion.identity);
                    NewWall.transform.SetParent(levelpiece.transform);
                    WallPosition = new Vector2(WallPosition.x, WallPosition.y - 0.3f);
                }
            }
            if (!spaceoccupiedright & levelpiece.GetComponent<LevelPiece>().RightConnect)
            {
                Vector2 WallPosition = new Vector2(levelpiece.transform.position.x + 3f, levelpiece.transform.position.y + 0.1f);
                for (int i = 0; i < 10; i++)
                {
                    GameObject NewWall = Instantiate(WallPiece, WallPosition, Quaternion.identity);
                    NewWall.transform.SetParent(levelpiece.transform);
                    WallPosition = new Vector2(WallPosition.x, WallPosition.y - 0.3f);
                }
            }
            if (!spaceoccupiedtop & levelpiece.GetComponent<LevelPiece>().TopConnect)
            {
                Vector2 FloorPosition = new Vector2(levelpiece.transform.position.x, levelpiece.transform.position.y + 0.25f);
                for (int i = 0; i < 10; i++)
                {
                    GameObject NewFloor = Instantiate(FloorPiece, FloorPosition, Quaternion.identity);
                    NewFloor.transform.SetParent(levelpiece.transform);
                    FloorPosition = new Vector2(FloorPosition.x + 0.3f, FloorPosition.y);
                    if (levelpiece.GetComponent<LevelPiece>().TopEdgeLadderPiece != null)
                    {
                        foreach(GameObject topladderpiece in levelpiece.GetComponent<LevelPiece>().TopEdgeLadderPiece)
                        {
                            Destroy(topladderpiece);
                        }
                    }
                }
            }
            if (!spaceoccupiedbottom & levelpiece.GetComponent<LevelPiece>().BottomConnect)
            {
                Vector2 FloorPosition = new Vector2(levelpiece.transform.position.x, levelpiece.transform.position.y - 2.45f);
                for (int i = 0; i < 10; i++)
                {
                    GameObject NewFloor = Instantiate(FloorPiece, FloorPosition, Quaternion.identity);
                    NewFloor.transform.SetParent(levelpiece.transform);
                    FloorPosition = new Vector2(FloorPosition.x + 0.3f, FloorPosition.y);
                }
            }
        }
    }

    void FillLadders()
    {
        foreach (GameObject leveltile in leveltiles)
        {
            if (leveltile.GetComponent<LevelPiece>().BottomConnect && leveltile.GetComponent<LevelPiece>().BottomEdgeLadderPiece != null)
            {
                foreach (GameObject bottomladder in leveltile.GetComponent<LevelPiece>().BottomEdgeLadderPiece)
                {
                    double ladderdistance;
                    int ladderamt = 0;
                    //bool laddercomplete = true;
                    Vector3 ladderposition = bottomladder.transform.position;
                    RaycastHit2D orighit = Physics2D.Raycast(bottomladder.transform.position, Vector2.down);
                    if (orighit.collider != null)
                    {
                        if (orighit.collider.gameObject.tag == "Floor" || orighit.collider.gameObject.tag == "Ladder")
                        {
                            ladderdistance = System.Math.Round((ladderposition - orighit.transform.position).magnitude, 1);
                            Debug.Log(ladderdistance);
                            ladderamt = (int)Mathf.Round((float)ladderdistance / 0.3f);
                            Debug.Log(ladderamt);
                            //laddercomplete = false;
                        }
                    }
                    for (int p = 1; p < ladderamt; p++)
                    {
                        Debug.Log("entered loop");
                        ladderposition = new Vector2(ladderposition.x, ladderposition.y - 0.3f);
                        GameObject newladder = Instantiate(bottomladder, ladderposition, Quaternion.identity, leveltile.transform);
                    }
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
        GenerateLevel(100);
	}
	
}
