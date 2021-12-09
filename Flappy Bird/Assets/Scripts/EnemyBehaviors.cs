using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviors : MonoBehaviour
{
    private GameObject pipeSpawner;
    private GameObject pipesPrefab;
    private bool changePipesVelocity = false;
    private float originalSpeed = 3.0f;
    private const float MIN_SPEED = 1.8f;
    private const float MAX_SPEED = 4.5f;
    public int movePipeThreshold = 10;
    public int everyNPipe = 5;
    private float currentSpeed = 3.0f;
    private float newSpeed;

    private float halfSpeed;

    public EnemyBehaviors(GameObject pipeSpawner)
    {
        this.pipeSpawner = pipeSpawner;
        pipesPrefab = pipeSpawner.gameObject.GetComponent<Spawner>().getPipesPrefab();
    }

    public void changeEnemyAttack(int score)
    {
        if (score >= movePipeThreshold)
        {
            changePipeVelocity(score);
        }
    }

    public void restoreState()
    {
        Debug.Log("Restore Speed: " + originalSpeed);
        pipesPrefab.gameObject.GetComponent<Pipes>().changeSpeed(originalSpeed);
    }

    private void changePipeVelocity(int score)
    {
        if (score % everyNPipe == 0)
        {
            newSpeed = Random.Range(MIN_SPEED, MAX_SPEED);
            Debug.Log("currentSpeed " + currentSpeed);
            Debug.Log("newSpeed: " + newSpeed);
            halfSpeed = (currentSpeed + newSpeed) / 2;
            currentSpeed = (currentSpeed + halfSpeed) / 2;
            Debug.Log("velocitaMediaIniziale: " + currentSpeed);
            pipesPrefab.gameObject.GetComponent<Pipes>().changeSpeed(currentSpeed);

        }
        else if (score % (everyNPipe + 1) == 0)
        {
            currentSpeed = (halfSpeed + newSpeed) / 2;
            Debug.Log("velocitaMediaFinale " + currentSpeed);
            pipesPrefab.gameObject.GetComponent<Pipes>().changeSpeed(currentSpeed);
        }
        else if (score % (everyNPipe + 2) == 0)
        {
            currentSpeed = newSpeed;
            halfSpeed = 0.0f;
            Debug.Log("velocita ultima " + currentSpeed);
            pipesPrefab.gameObject.GetComponent<Pipes>().changeSpeed(currentSpeed);
        }
    }
}
