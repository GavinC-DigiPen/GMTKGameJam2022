//------------------------------------------------------------------------------
//
// File Name:	EnemySpawner.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public struct EnemyInfo
    {
        public GameObject enemyPrefab;
        public int numberOfEnemies;
    }

    [Tooltip("The enemies that will be spawned in")] [SerializeField]
    private EnemyInfo[] enemyInfo;

    private List<Transform> locations;

    // Start is called before the first frame update
    void Start()
    {
        // Get locations
        locations = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            locations.Add(transform.GetChild(i).gameObject.transform);
        }

        for (int i = 0; i < enemyInfo.Length; i++)
        {
            for (int j = 0; j < enemyInfo[i].numberOfEnemies; j++)
            {
                Instantiate(enemyInfo[i].enemyPrefab, locations[Random.Range(0, locations.Count)]);
            }
        }
    }
}
