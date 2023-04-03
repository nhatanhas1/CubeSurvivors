using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

public class AreaFloorBaker : MonoBehaviour
{
    [SerializeField] NavMeshSurface surface;
    [SerializeField] PlayerController playerController;
    [SerializeField] float mapUpdateRate = .2f;
    [SerializeField] float MovementThreshold = 5;
    [SerializeField] Vector3 navMeshSize = new Vector3(30, 20, 30);
    [SerializeField] Vector3 worldAnchor;
    [SerializeField] NavMeshData navMeshData;

    List<NavMeshBuildSource> buildSources = new List<NavMeshBuildSource>();


    private void Start()
    {
        navMeshData = new NavMeshData();
        NavMesh.AddNavMeshData(navMeshData);
        surface.navMeshData = navMeshData;
        BuildNavMesh(false);
        StartCoroutine(CheckPlayerMovement());
    }

    IEnumerator CheckPlayerMovement()
    {
        WaitForSeconds wait = new WaitForSeconds(mapUpdateRate);

        while(playerController != null)
        {
            if(Vector3.Distance(worldAnchor, playerController.transform.position) > MovementThreshold)
            {
                BuildNavMesh(true);
                worldAnchor = playerController.transform.position;
            }
            yield return wait;
        }        
    }

    void BuildNavMesh(bool async)
    {
        Bounds navMeshBounds = new Bounds(playerController.transform.position,navMeshSize);
        List<NavMeshBuildMarkup> buildMarkups = new List<NavMeshBuildMarkup>();

        List<NavMeshModifier> modifiers = new List<NavMeshModifier>();
        if(surface.collectObjects == CollectObjects.Children)
        {
            modifiers = new List<NavMeshModifier>(surface.GetComponentsInChildren<NavMeshModifier>());

        }
        else
        {
            modifiers = NavMeshModifier.activeModifiers;
        }

        for(int i = 0; i < modifiers.Count; i++)
        {
            if(((surface.layerMask &(1 << modifiers[i].gameObject.layer)) == 1) && modifiers[i].AffectsAgentType(surface.agentTypeID))
            {
                buildMarkups.Add(new NavMeshBuildMarkup() 
                { 
                    root = modifiers[i].transform,
                    overrideArea = modifiers[i].overrideArea, 
                    area = modifiers[i].area,
                    ignoreFromBuild = modifiers[i].ignoreFromBuild,
                });
            }
        }

        if(surface.collectObjects == CollectObjects.Children)
        {
            NavMeshBuilder.CollectSources(surface.transform, surface.layerMask, surface.useGeometry, surface.defaultArea, buildMarkups, buildSources);
        }
        else
        {
            NavMeshBuilder.CollectSources(navMeshBounds, surface.layerMask, surface.useGeometry, surface.defaultArea, buildMarkups, buildSources);
        }

        buildSources.RemoveAll(source => source.component !=null && source.component.gameObject.GetComponent<NavMeshAgent>() != null);

        if (async)
        {
            NavMeshBuilder.UpdateNavMeshDataAsync(navMeshData, surface.GetBuildSettings(), buildSources, new Bounds(playerController.transform.position, navMeshSize));
        }
        else
        {
            NavMeshBuilder.UpdateNavMeshData(navMeshData, surface.GetBuildSettings(), buildSources, new Bounds(playerController.transform.position, navMeshSize));
        }
    }
}
