using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private TowerBehaviour _towerBehaviour;
    private EnemyCreator _creator;
    [SerializeField] private byte hitPoints = 10;
    private void Awake()
    {
        gameObject.transform.GetChild(0).gameObject.AddComponent<BoxCollider>();
        Rigidbody rigidbodyComponent = gameObject.AddComponent<Rigidbody>();
        rigidbodyComponent.isKinematic = true;
        _towerBehaviour = FindObjectOfType<TowerBehaviour>();
        _creator = FindObjectOfType<EnemyCreator>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (hitPoints <= 0)
        {
            KillEnemy();
            return;
        }
        ProcessHit();
    }

    private void KillEnemy()
    {
        _creator.CreateEnemy();
        _towerBehaviour.UpdateWhenDead(transform);
    }

    private void ProcessHit()
    {
        hitPoints -= 1;
        print("current hitPoints are " + hitPoints);
    }
}
