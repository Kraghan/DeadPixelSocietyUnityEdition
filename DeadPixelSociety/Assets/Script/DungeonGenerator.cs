using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour {

    public FloorBlock floorPatern;
    public WallBlock wallPatern;
    public PlatformBlock platformPatern;
    public int room;
    private Vector2 currentPosition;
    private DungeonBlock currentBlock;
    public DungeonBlock[] blockPatern;
    public DungeonBlock[,] dungeon;
    private Player player;
    public SpriteRenderer background;
    public Generator generator;

	// Use this for initialization
	void Start ()
    {
        
        dungeon = new DungeonBlock[room, room];
        for(int i = 0; i < room; ++i)
            for (int j = 0; j < room; ++j)
                dungeon[i, j] = null;

		Random.InitState ((int)System.DateTime.Now.Ticks);

		int rand = (int) Random.Range (0.0f, blockPatern.GetLength(0));

        generator.Generate(room);
        generator.CreateAnnexe(room / 2, Generator.AnnexeType.Tresure);

        /*bool hasShopKeeper = false;
        for (int i = 0; i < (int)Random.Range(2.0f, room / 2); ++i)
        {
            if(hasShopKeeper)
                generator.CreateAnnexe((int)Random.Range(1.0f, room / 2), Generator.AnnexeType.Tresure);
            else
            {
                if((int) Random.Range(0.0f,10.0f) == 5)
                {
                    hasShopKeeper = true;
                    generator.CreateAnnexe((int)Random.Range(1.0f, room / 2), Generator.AnnexeType.Shopkeeper);
                }
                else
                    generator.CreateAnnexe((int)Random.Range(1.0f, room / 2), Generator.AnnexeType.Tresure);
            }
        }*/

        Array2D patern = generator.patern;

        // Generate dungeon
        //dungeon[0, 0] = Instantiate(blockPatern[rand]);
        for (int i = 0; i < patern.GetLength(0); ++i)
        {
            for(int j = 0; j < patern.GetLength(1); ++j)
            {
                rand = (int)Random.Range(0.0f, blockPatern.GetLength(0));
                // Salle Normale
                if (patern[i,j] == 1)
                {
                    dungeon[i, j] = Instantiate(blockPatern[rand]);
                }
                // Spawn 
                if (patern[i, j] == 2)
                {
                    dungeon[i, j] = Instantiate(blockPatern[rand]);
                    currentBlock = dungeon[i, j];
                    currentPosition = new Vector2(i, j);
                }
                // Boss
                if (patern[i, j] == 3)
                {
                    dungeon[i, j] = Instantiate(blockPatern[rand]);
                }

                if (dungeon[i, j] != null)
                    dungeon[i, j].Init(floorPatern, wallPatern, platformPatern, background);
            }
        }

        // Set first room
        currentBlock.OnEnter(player);

    }
	
	// Update is called once per frame
	void Update ()
    {
        /*if (Input.GetKeyDown("z"))
        {
            ChangeCurrentBlock(new Vector2(currentPosition.x,currentPosition.y - 1));
        }
        else if (Input.GetKeyDown("s"))
        {
            ChangeCurrentBlock(new Vector2(currentPosition.x, currentPosition.y + 1));
        }
        else if (Input.GetKeyDown("q"))
        {
            ChangeCurrentBlock(new Vector2(currentPosition.x - 1, currentPosition.y));
        }
        else if (Input.GetKeyDown("d"))
        {
            ChangeCurrentBlock(new Vector2(currentPosition.x + 1, currentPosition.y));
        }*/
    }

    private void ChangeCurrentBlock(Vector2 newPosition)
    {
        if(CanChange(newPosition))
        {
            Debug.Log("Change room");
            currentBlock.OnExit();
            currentBlock = dungeon[(int)newPosition.x, (int)newPosition.y];
            currentBlock.OnEnter(player);
            currentPosition = newPosition;
        }
    }

    private bool CanChange(Vector2 newPosition)
    {
        if (newPosition.x < 0 || newPosition.y < 0 || newPosition.x > room - 1 || newPosition.y > room - 1)
            return false;

        if(currentPosition.x == newPosition.x)
        {
            return Mathf.Abs(currentPosition.y - newPosition.y) == 1 && dungeon[(int)newPosition.x, (int)newPosition.y] != null;
        }
        else if (currentPosition.y == newPosition.y)
        {
            return Mathf.Abs(currentPosition.x - newPosition.x) == 1 && dungeon[(int)newPosition.x, (int)newPosition.y] != null;
        }
        return false;
    }

}
