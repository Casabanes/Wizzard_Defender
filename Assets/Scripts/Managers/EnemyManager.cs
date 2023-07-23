using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField] private List<Path> _paths;
    private int index;

    [SerializeField] private float _minTimeToInstanceEnemy;
    [SerializeField] private float _maxTimeToInstanceEnemy;
    [SerializeField] private BaseEnemy[] enemyPrefabs;
    [SerializeField] private int _currentEnemiesOnScreen;
    [SerializeField] private int _maxEnemiesOnScreen;
    [SerializeField] private int totalEnemysInGame;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        StartCoroutine(SpawnEnemy(GetRandomTime()));
    }
    public List<Transform> GetPath()
    {
        index=Random.Range(0,_paths.Count);
        var aux = _paths[index].GetWaypoints();
        return aux;
    }
    private float GetRandomTime()
    {
        return Random.Range(_minTimeToInstanceEnemy, _maxTimeToInstanceEnemy);
    }
    public IEnumerator SpawnEnemy(float time)
    {
        yield return new WaitForSeconds(time);
        if (_currentEnemiesOnScreen < _maxEnemiesOnScreen)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            BaseEnemy instance = Instantiate(enemyPrefabs[randomEnemy]);
            instance.waypoints = GetPath();
            instance.transform.position = instance.waypoints[0].position;
        }
            StartCoroutine(SpawnEnemy(GetRandomTime()));
    }
    public void SuscribeEnemy()
    {
        _currentEnemiesOnScreen++;
        totalEnemysInGame++;
    }
    public void UnSuscribeEnemy()
    {
        _currentEnemiesOnScreen--;
        if (_currentEnemiesOnScreen <= 0 && totalEnemysInGame>=20)
        {
            GameManager.instance.Win();
        }
    }
}
