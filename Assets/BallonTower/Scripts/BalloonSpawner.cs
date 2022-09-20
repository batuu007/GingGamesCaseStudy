using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _balloonPrefab;

    private void Start()
    {
        InvokeRepeating("SpawnBallon",2f,2f);
    }

    private void SpawnBallon()
    {
        Instantiate(_balloonPrefab, new Vector3(0, 0.85f, -0.33f), transform.rotation);
    }
}
