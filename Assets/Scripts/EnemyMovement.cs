using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private byte _iteration;
    private void Start()
     {
         Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
         var path = pathfinder.GetPath();
         StartCoroutine(FollowPath(path));
     }
 
     private IEnumerator FollowPath(List<Waypoint> path)
     {
         foreach (var waypoint in path)
         {
             transform.position = waypoint.transform.position;
             if (_iteration < path.Count - 1)
             {
                 transform.LookAt(path[++_iteration].transform);
             }
             yield return new WaitForSeconds(1f);
         }
     }
}
