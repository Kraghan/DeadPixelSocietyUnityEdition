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
    public Player player;
    public SpriteRenderer background;
    public Generator generator;
    public double timeToChangeRoom;
    private double timeElapsed;
    public Camera cam;
    private Vector2 distanceCameraMovement;
    private Vector2 cameraMovementSpeed;
    private Vector2 roomSize;


	// Use this for initialization
	void Start ()
    {
        float height = 0.64f * 16;
        float width = 0.64f * 28;
        roomSize = new Vector2(width, height);

        cameraMovementSpeed = new Vector2((float)(width/timeToChangeRoom), (float)(height/timeToChangeRoom));
        Debug.Log(cameraMovementSpeed);

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
        for (int i = 0; i < patern.GetLength(0); ++i)
        {
            for (int j = 0; j < patern.GetLength(1); ++j)
            {
                rand = (int)Random.Range(0.0f, blockPatern.GetLength(0));
                // Salle Normale
                if (patern[i, j] == 1)
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
                {
                    dungeon[i, j].Init(new Vector2(i, j), floorPatern, wallPatern, platformPatern, background);
                    dungeon[i, j].transform.parent = transform;
                }
            }
        }

        // Set first room
        currentBlock.OnEnter(player);
        // Partialy enable neighboor
        if (currentPosition.x - 1 >= 0 && dungeon[(int)currentPosition.x - 1, (int)currentPosition.y] != null)
            dungeon[(int)currentPosition.x - 1, (int)currentPosition.y].Load(false);
        if (currentPosition.x + 1 < room && dungeon[(int)currentPosition.x + 1, (int)currentPosition.y] != null)
            dungeon[(int)currentPosition.x + 1, (int)currentPosition.y].Load(false);
        if (currentPosition.y - 1 >= 0 && dungeon[(int)currentPosition.x, (int)currentPosition.y - 1] != null)
            dungeon[(int)currentPosition.x, (int)currentPosition.y - 1].Load(false);
        if (currentPosition.x + 1 < room && dungeon[(int)currentPosition.x, (int)currentPosition.y + 1] != null)
            dungeon[(int)currentPosition.x, (int)currentPosition.y + 1].Load(false);

    }
	
	// Update is called once per frame
	void Update ()
    {

        if (player.IsFrozen())
            TranslateCamera();
    }

    public void ChangeCurrentBlocToLeft()
    {
        Vector2 tmp = new Vector2(currentPosition.x - 1, currentPosition.y);
        if (CanChange(tmp))
        {
            player.Freeze();
            distanceCameraMovement = new Vector2(-roomSize.x, 0);
            ChangeCurrentBlock(tmp);
        }
    }

    public void ChangeCurrentBlocToRight()
    {
        Vector2 tmp = new Vector2(currentPosition.x + 1, currentPosition.y);
        if (CanChange(tmp))
        {
            player.Freeze();
            distanceCameraMovement = new Vector2(roomSize.x, 0);
            ChangeCurrentBlock(tmp);
        }
    }

    public void ChangeCurrentBlocToTop()
    {
        Vector2 tmp = new Vector2(currentPosition.x, currentPosition.y - 1);
        if (CanChange(tmp))
        {
            player.Freeze();
            distanceCameraMovement = new Vector2(0, -roomSize.y);
            ChangeCurrentBlock(tmp);
        }
    }

    public void ChangeCurrentBlocToBottom()
    {
        Vector2 tmp = new Vector2(currentPosition.x, currentPosition.y + 1);
        if (CanChange(tmp))
        {
            player.Freeze();
            distanceCameraMovement = new Vector2(0, roomSize.y);
            ChangeCurrentBlock(tmp);
        }
    }

    private void ChangeCurrentBlock(Vector2 newPosition)
    {
        // Disable useless rooms
        if (currentPosition.x - 1 >= 0 && dungeon[(int)currentPosition.x - 1, (int)currentPosition.y] != null)
            dungeon[(int)currentPosition.x - 1, (int)currentPosition.y].Unload(true);
        if (currentPosition.x + 1 < room && dungeon[(int)currentPosition.x + 1, (int)currentPosition.y] != null)
            dungeon[(int)currentPosition.x + 1, (int)currentPosition.y].Unload(true);
        if (currentPosition.y - 1 >= 0 && dungeon[(int)currentPosition.x, (int)currentPosition.y - 1] != null)
            dungeon[(int)currentPosition.x, (int)currentPosition.y - 1].Unload(true);
        if (currentPosition.x + 1 < room && dungeon[(int)currentPosition.x, (int)currentPosition.y + 1] != null)
            dungeon[(int)currentPosition.x, (int)currentPosition.y + 1].Unload(true);

        currentBlock.OnExit();
        currentBlock = dungeon[(int)newPosition.x, (int)newPosition.y];
        currentBlock.OnEnter(player);
        currentPosition = newPosition;

        // Partialy enable neighboor
        if (currentPosition.x - 1 >= 0 && dungeon[(int)currentPosition.x - 1, (int)currentPosition.y] != null)
            dungeon[(int)currentPosition.x - 1, (int)currentPosition.y].Load(false);
        if (currentPosition.x + 1 < room && dungeon[(int)currentPosition.x + 1, (int)currentPosition.y] != null)
            dungeon[(int)currentPosition.x + 1, (int)currentPosition.y].Load(false);
        if (currentPosition.y - 1 >= 0 && dungeon[(int)currentPosition.x, (int)currentPosition.y - 1] != null)
            dungeon[(int)currentPosition.x, (int)currentPosition.y - 1].Load(false);
        if (currentPosition.x + 1 < room && dungeon[(int)currentPosition.x, (int)currentPosition.y + 1] != null)
            dungeon[(int)currentPosition.x, (int)currentPosition.y + 1].Load(false);
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

    private void TranslateCamera()
    {
        float distance = 0.0f;

        if (distanceCameraMovement.x > 0)
        {
            distance = cameraMovementSpeed.x * Time.deltaTime;
            if (distanceCameraMovement.x < distance)
                distance = distanceCameraMovement.x;
            cam.transform.Translate(new Vector2(distance, 0));
              
            distanceCameraMovement.x -= distance;
        }
        else if (distanceCameraMovement.x < 0)
        {
            distance = -cameraMovementSpeed.x * Time.deltaTime;
            if (distanceCameraMovement.x > distance)
                distance = distanceCameraMovement.x;
            cam.transform.Translate(new Vector2(distance, 0));

            distanceCameraMovement.x -= distance;
        }

        if (distanceCameraMovement.y > 0)
        {
            distance = cameraMovementSpeed.y * Time.deltaTime;
            if (distanceCameraMovement.y < distance)
                distance = distanceCameraMovement.y;
            cam.transform.Translate(new Vector2(0, distance));

            distanceCameraMovement.y -= distance;
        }
        else if (distanceCameraMovement.y < 0)
        {
            distance = -cameraMovementSpeed.y * Time.deltaTime;
            if (distanceCameraMovement.y > distance)
                distance = distanceCameraMovement.y;
            cam.transform.Translate(new Vector2(0, distance));

            distanceCameraMovement.y -= distance;
        }

        if (distanceCameraMovement.x == 0 && distanceCameraMovement.y == 0)
        {
            distanceCameraMovement = new Vector2(0, 0);
            player.WarmUp();
        }
    }

}
