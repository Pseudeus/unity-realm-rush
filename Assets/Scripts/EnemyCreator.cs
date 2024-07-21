using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    private Pathfinder _pathfinder;
    private Transform _enemySpawnPos;
    [SerializeField] private GameObject enemyBaseInstance;
    private bool _running;
    private uint _noEnemy;
    void Awake()
    {
        _pathfinder = FindObjectOfType<Pathfinder>();
        _pathfinder.FillDictionary();
        _enemySpawnPos = _pathfinder.GetStartPos();
        _running = true;
        //StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while (_running)
        {
            var baby = Instantiate(enemyBaseInstance, _enemySpawnPos);
            baby.transform.parent = transform;
            baby.name = "Enemy_" + _noEnemy++;
            yield return new WaitForSeconds(3f);
        }
    }

    public void CreateEnemy()
    {
        var baby = Instantiate(enemyBaseInstance, _enemySpawnPos);
        baby.transform.parent = transform;
        baby.name = "Enemy_" + _noEnemy++;
    }
}
