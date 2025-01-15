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
            Debug.Log("60�� �� ��ȯ�˴ϴ�.");
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
        //Debug.Log(currentAnimals + " : ���� ���� ��");
        //Debug.Log(maxAnimals + " : �ִ� ���� ��");
        if (currentAnimals > maxAnimals)
        {
            isSpawnPossible = false;
        }
        //Debug.Log(isSpawnPossible + " : ��ȯ ����?");
    }
    
    IEnumerator WaitingSpawn()
    {
        yield return new WaitForSeconds(60);
        isSpawnPossible = true;
    }
}
