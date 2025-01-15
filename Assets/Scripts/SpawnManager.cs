using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<GameObject> animals;
    [SerializeField] float xRange = 8.0f;
    [SerializeField] float zRange = 7.0f;
    private int maxAnimals = 31;
    public int currentAnimals = 0;
    private bool isSpawnPossible = true;

    // Update is called once per frame
    void Update()
    {
        while (isSpawnPossible && currentAnimals < maxAnimals)
        {
            SpawnAnimals();          
        }

        if (currentAnimals < 0 && !isSpawnPossible)
        {
            Debug.Log("60초 뒤 소환됩니다.");
            StartCoroutine(WaitingSpawn());
        }
    }

    void SpawnAnimals()
    { 
        float randomX = Random.Range(-xRange, xRange);
        float randomZ = Random.Range(-zRange, zRange);
        Vector3 pos = new Vector3(randomX, 0, randomZ);

        for (int i = 0; i < animals.Count; i++)
        {
            Instantiate(animals[i], pos, animals[i].transform.rotation);
            currentAnimals++;    
        }
        //Debug.Log(currentAnimals + " : 현재 동물 수");
        //Debug.Log(maxAnimals + " : 최대 마리 수");
        if (currentAnimals > maxAnimals)
        {
            isSpawnPossible = false;
        }
        //Debug.Log(isSpawnPossible + " : 소환 가능?");
    }
    
    IEnumerator WaitingSpawn()
    {
        yield return new WaitForSeconds(60);
        isSpawnPossible = true;
    }
}
