using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class DungeonBlock : MonoBehaviour {

    private FloorBlock[] floorBlock;
    private WallBlock[] wallBlock;
    private PlatformBlock[] platformBlock;
    private SpriteRenderer backgroundRenderer; 
    private Player player;
    private Vector2 BLOCK_POS_MAX = new Vector2(8.64f, -4.8f);
    public float spriteSize;
    public Array2D patern;
    private Vector2 position;
    private LoadState loadState;

    public enum LoadState
    {
        unloaded,
        partialyLoaded,
        loaded
    }

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Init(Vector2 pos, FloorBlock floorPatern, WallBlock wallPatern, PlatformBlock platformPatern, SpriteRenderer background)
    {
        position = pos;
        backgroundRenderer = Instantiate(background);
        backgroundRenderer.transform.localScale = new Vector2(0.9333f, 0.948f);
        backgroundRenderer.transform.parent = transform;
        backgroundRenderer.gameObject.SetActive(false);

        int platform, wall, floor;
        platform = wall = floor = 0;
        for (int i = 0; i < patern.GetLength(0); ++i)
        {
            for (int j = 0; j < patern.GetLength(1); ++j)
            {
                switch (patern[i, j])
                {
                    // Floor
                    case 1:
                        ++floor;
                        break;
                    // Wall
                    case 2:
                        ++wall;
                        break;
                    // Platform
                    case 3:
                        ++platform;
                        break;
                    // Void
                    default:
                        break;
                }
            }
        }

        floorBlock = new FloorBlock[floor];
        wallBlock = new WallBlock[wall];
        platformBlock = new PlatformBlock[platform];

        Vector2 newPosition = -BLOCK_POS_MAX;

        for (int i = 0; i < patern.GetLength(0); ++i)
        {
            for (int j = 0; j < patern.GetLength(1); ++j)
            {
                switch (patern[i, j])
                {
                    // Floor
                    case 1:
                        --floor;
                        floorBlock[floor] = Instantiate(floorPatern);
                        floorBlock[floor].transform.parent = transform;
                        floorBlock[floor].transform.position = new Vector3(newPosition.x, newPosition.y, 0f);
                        floorBlock[floor].Disable();
                        break;
                    // Wall
                    case 2:
                        --wall;
                        wallBlock[wall] = Instantiate(wallPatern);
                        wallBlock[wall].transform.parent = transform;
                        wallBlock[wall].transform.position = new Vector3(newPosition.x, newPosition.y, 0f);
                        wallBlock[wall].Disable();
                        break;
                    // Platform
                    case 3:
                        --platform;
                        platformBlock[platform] = Instantiate(platformPatern);
                        platformBlock[platform].transform.parent = transform;
                        platformBlock[platform].transform.position = new Vector3(newPosition.x,newPosition.y,0f);
                        platformBlock[platform].Disable();
                        break;
                    // Void
                    default:
                        break;
                }
                newPosition.x += spriteSize;
            }
            newPosition.y -= spriteSize;
            newPosition.x = -BLOCK_POS_MAX.x;
        }

        transform.Translate(new Vector2(position.x*(BLOCK_POS_MAX.x*2)+position.x*spriteSize,position.y*(BLOCK_POS_MAX.y*2) + position.y * spriteSize));
    }

    public void Load(bool completly)
    {
        if(completly)
        {
            loadState = LoadState.loaded;
            backgroundRenderer.gameObject.SetActive(true);
            for (int i = 0; i < floorBlock.GetLength(0); ++i)
            {
                floorBlock[i].Enable();
            }
            for (int i = 0; i < wallBlock.GetLength(0); ++i)
            {
                wallBlock[i].Enable();
            }
            for (int i = 0; i < platformBlock.GetLength(0); ++i)
            {
                platformBlock[i].Enable();
            }

            
        }
        else
        {
            loadState = LoadState.partialyLoaded;
        }
    }

    public void Unload(bool completly)
    {
        if(completly)
        {
            loadState = LoadState.unloaded;
            backgroundRenderer.gameObject.SetActive(false);
            for (int i = 0; i < floorBlock.GetLength(0); ++i)
            {
                floorBlock[i].Disable();
            }
            for (int i = 0; i < wallBlock.GetLength(0); ++i)
            {
                wallBlock[i].Disable();
            }
            for (int i = 0; i < platformBlock.GetLength(0); ++i)
            {
                platformBlock[i].Disable();
            }
        }
        else
        {
            loadState = LoadState.partialyLoaded;
        }
    }

    public void OnEnter(Player p)
    {
        player = p;
        Load(true);
    }

    public void OnExit()
    {
        player = null;
        Unload(false);
    }

    public LoadState GetLoadingstate()
    {
        return loadState;
    }
}
