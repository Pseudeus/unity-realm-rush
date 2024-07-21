using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveEnemies : MonoBehaviour
{
    private static List<Transform> _enemyTarget = new List<Transform>();

    public static ref List<Transform> GetEnemyList()
    {
        return ref _enemyTarget;
    }

    public static void FindEnemies()
    {
        var enemies = FindObjectsOfType<EnemyDamage>();
        foreach (var enemy in enemies)
        {
            _enemyTarget.Add(enemy.transform);
        }
    }
    
    private void Start()
    {
        FindEnemies();
    }
}
