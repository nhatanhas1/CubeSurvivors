using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosest : MonoBehaviour
{

    public List<GameObject> targetList;
    [SerializeField] PlayerController player;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        //FindClosestEnemy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IIDamageable>() != null)
        {
            //IIDamageable attackTarget = other.GetComponent<IIDamageable>();
            //attackTarget.TakeDamage(5f);
            //Debug.Log("Player attack ");
            GameObject target = other.gameObject;

            targetList.Add(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IIDamageable>() != null)
        {
            GameObject target = other.gameObject;
            targetList.Remove(target);
        }
    }
    public GameObject FindClosestEnemy()
    {
        if(targetList.Count == 0) { return null; }
        float closestEnemyDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        for (int i = targetList.Count - 1; i > -1 ; i--)
        {
            GameObject currentEnemy = targetList[i];
            //Debug.Log("Check Enemy");
            if (currentEnemy.activeSelf)
            {
                float enemyDistance = (currentEnemy.transform.position - player.transform.position).sqrMagnitude;
                if (enemyDistance < closestEnemyDistance)
                {
                    closestEnemyDistance = enemyDistance;
                    closestEnemy = currentEnemy;
                }
            }
            else { targetList.Remove(currentEnemy); }
        }
        //Debug.DrawLine(player.transform.position, closestEnemy.transform.position);
        return closestEnemy;
        
        
    }


    public void UppdateTargetList()
    {
        if (targetList.Count == 0) { return ; }
        for(int i = targetList.Count - 1; i > -1; i--)
        {
            if (!targetList[i].activeSelf)
            {
                targetList.Remove(targetList[i]);
                //if (targetList.Count == 0) { return null; }
            }
        }
        //return targetList;

    }
}
