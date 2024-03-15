using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarAleatorios : MonoBehaviour
{
    public GameObject[] itemPrefab;
    public float minTime = 1f;
    public float maxTime = 2f;
    void Start()
    {
        StartCoroutine(SpawnCoRutine(0));
    }

    IEnumerator SpawnCoRutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(itemPrefab[Random.Range(0, itemPrefab.Length)], transform.position, Quaternion.identity);
        StartCoroutine(SpawnCoRutine(Random.Range(minTime, maxTime)));
    }
}
