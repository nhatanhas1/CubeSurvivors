using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 currentTilesPos = Vector3.zero;
    [SerializeField] Vector3Int playerTilePos;

    //[SerializeField] List<GameObject> terrainList = new List<GameObject>();

    GameObject[,] terrainTiles;
    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;


    // Start is called before the first frame update

    private void Awake()
    {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToList(GameObject terrainGameObject, Vector2Int tilePos)
    {
        terrainTiles[tilePos.x, tilePos.y] = terrainGameObject;        
    }


}
