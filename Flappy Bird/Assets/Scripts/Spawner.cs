using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate = 1.5f;
    public float minHeight = -1.2f;
    public float maxHeight = 3f;
    private bool movingOnY = false;

    private void OnEnable()
    {
        InvokeRepeating(nameof(spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(spawn));
    }
    private void spawn()
    {
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
        if (movingOnY)
            pipes.GetComponent<Pipes>().isMovingOnY(true);
    }

    public void isMovingOnY(bool isMovingOnY){
        this.movingOnY = isMovingOnY;
    }

    public GameObject getPipesPrefab()
    {
        return prefab;
    }

}
