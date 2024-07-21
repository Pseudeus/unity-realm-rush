using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AliveEnemies;

public class TowerBehaviour : MonoBehaviour
{
    [SerializeField] private Transform objectToPan;
    [SerializeField] private List<Transform> enemyTarget = GetEnemyList();
    [SerializeField] [Range(10f, 50f)] private float maxFireRange = 30f;
    private ParticleSystem _bulletSystem;
    private ParticleSystem.EmissionModule _emitting;

    private void Start()
    {
        _bulletSystem = GetComponentInChildren<ParticleSystem>();
        _emitting = _bulletSystem.emission;
        _emitting.enabled = false;

        //hacer un array con todos los EnemyMovement's que existan, validar su existencia antes de targetearlos
    }
    
    public void UpdateWhenDead(Transform currentEnemy) //called from EnemyDamage script
    {
        enemyTarget.Clear();
        Destroy(currentEnemy.gameObject);
        FindEnemies();
    }

    private void TargetingHandler()
    {
        foreach (var target in enemyTarget)
        {
            if (Vector3.Distance(transform.position, target.position) <= maxFireRange)
            {
                objectToPan.LookAt(target);
                _emitting.enabled = true;
                break;   
            }
            else
            {
                objectToPan.transform.localPosition = new Vector3(0f, 5.5f, 0f);
                _emitting.enabled = false;
            }
        }
    }

    void Update()
    {
        TargetingHandler();
    }
}
