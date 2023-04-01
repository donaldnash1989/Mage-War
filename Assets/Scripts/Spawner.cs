using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    private float spawnTimer = 0.0f;
    [SerializeField] private float spawnRate = 1.0f;
    [SerializeField] private int maxSpawns = 1;
    [SerializeField] private int currentSpawns = 0;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    public void Awake()
    {
        Enemy.OnDeathEvent += OnDeathEventHandler;
    }

    private void OnDeathEventHandler()
    {
        currentSpawns--;
    }

    public void Update()
    {
        if (currentSpawns < maxSpawns) spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            Spawn();
            spawnTimer = 0.0f;
        }

    }

    private void Spawn()
    {
        Instantiate(_prefab, spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
        currentSpawns++;
    }
}
