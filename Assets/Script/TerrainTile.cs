using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class TerrainTile : MonoBehaviour
{
    public Vector2Int tilePos;
    [SerializeField] WorldScrolling worldScrolling;
    //[SerializeField] MapController mapController;
    // Start is called before the first frame update
    [SerializeField] NavMeshSurface navMeshSurface;
    [SerializeField] NavMeshData terrainNavMeshData;

    private void Awake()
    {
        //mapController = GetComponentInParent<MapController>();
        worldScrolling = GetComponentInParent<WorldScrolling>();
    }

    void Start()
    {
        //worldScrolling.Add(gameObject, tilePos);

        //mapController.AddToList(gameObject, tilePos);
        navMeshSurface.navMeshData = terrainNavMeshData;
    }

    

    

    
}
