using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector2Int currentTilesPos = Vector2Int.zero;
    [SerializeField] Vector2Int playerTilePos;
    Vector2Int onTileGridPlayerPos;
    [SerializeField] float tileSize = 50f;
    GameObject[,] terrainTiles;

    [SerializeField] int terrainTileHorizontalCount = 3;
    [SerializeField] int terrainTileVerticalCount = 3;

    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidth = 3;

    [SerializeField] List<TerrainTile> terrainTileList;
    //[SerializeField] List<GameObject> terrainTileList;

    public void Add(GameObject tileGameObject, Vector2Int tilePos)
    {
        terrainTiles[tilePos.x, tilePos.y] = tileGameObject;
    }

    private void Awake()
    {
        terrainTiles= new GameObject[terrainTileHorizontalCount,terrainTileVerticalCount];
    }
    private void Start()
    {
        tileSize = 50f;
        SetUpMap();
        UpdateTileOnScreen();
    }
    void Update()
    {
        playerTilePos.x = (int)(playerTransform.position.x/ tileSize);
        playerTilePos.y = (int)(playerTransform.position.z/ tileSize);

        playerTilePos.x -= playerTransform.position.x < 0 ? 1 : 0;
        playerTilePos.y -= playerTransform.position.z < 0 ? 1 : 0;
       
        if (currentTilesPos != playerTilePos)
        {
            currentTilesPos = playerTilePos;

            onTileGridPlayerPos.x = CalculatePosOnAxis(onTileGridPlayerPos.x, true);
            onTileGridPlayerPos.y = CalculatePosOnAxis(onTileGridPlayerPos.y, false);
            UpdateTileOnScreen();
        }
    }

    void SetUpMap()
    {
        //terrainTiles = new GameObject[terrainTileHorizontalCount,terrainTileVerticalCount];
        int terrainCount = 0;
        for(int i = 0; i < terrainTileVerticalCount; i++)
        {
            for(int j = 0; j < terrainTileHorizontalCount; j++)
            {
                
                Vector2Int terrainTilePos = new Vector2Int(i, j);
                //Debug.Log("Add terrain" + terrainTilePos);
                terrainTileList[terrainCount].tilePos = terrainTilePos;
                //terrainTileList[terrainCount].GetComponent<TerrainTile>().tilePos = terrainTilePos;

                Add(terrainTileList[terrainCount].gameObject, terrainTilePos);                
                terrainCount++;
            }
        }
    }

    private void UpdateTileOnScreen()
    {
        for (int pov_x = -(fieldOfVisionWidth / 2); pov_x <= fieldOfVisionWidth / 2; pov_x++)
        {
            for (int pov_y = -(fieldOfVisionHeight / 2); pov_y <= fieldOfVisionHeight / 2; pov_y++)
            {
                int TileToUpdate_x = CalculatePosOnAxis(playerTilePos.x + pov_x, true);
                int TileToUpdate_y = CalculatePosOnAxis(playerTilePos.y + pov_y, false);


                GameObject tile = terrainTiles[TileToUpdate_x, TileToUpdate_y];
                tile.transform.position = CalculateTilePos((float)(playerTilePos.x + pov_x), (playerTilePos.y + pov_y));
            }
        }
    }

    private Vector3 CalculateTilePos(float x, float z)
    {
        return new Vector3(x * tileSize, 0f, z * tileSize);
    }

    private int CalculatePosOnAxis(float currentValue, bool horizontal)
    {
        if (horizontal)
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileHorizontalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileHorizontalCount-1  + currentValue % terrainTileHorizontalCount;
            }
        }
        else
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainTileVerticalCount;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainTileVerticalCount -1  + currentValue % terrainTileVerticalCount;
            }
        }
        return (int)currentValue;
    }
}
