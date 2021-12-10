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
    public int moveYPipeThreshold = 23;
    public int everyNPipe = 9;
    public int moveXPipeThreshold = 9;
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
        if (score >= moveXPipeThreshold)
        {
            changePipeVelocity(score);
        }
        if (score >= moveYPipeThreshold)
        {
            moveYPipe();
        }
    }

    public void restoreState()
    {
        pipesPrefab.gameObject.GetComponent<Pipes>().changeSpeed(originalSpeed);
        pipeSpawner.GetComponent<Spawner>().isMovingOnY(false);
    }

    private void changePipeVelocity(int score)
    {
        if (score % everyNPipe == 0)
        {
            newSpeed = Random.Range(MIN_SPEED, MAX_SPEED);
            halfSpeed = (currentSpeed + newSpeed) / 2;
            currentSpeed = (currentSpeed + halfSpeed) / 2;
            pipesPrefab.gameObject.GetComponent<Pipes>().changeSpeed(currentSpeed);
        }
        else if (score % (everyNPipe + 1) == 0)
        {
            currentSpeed = (currentSpeed + newSpeed) / 2;
            pipesPrefab.gameObject.GetComponent<Pipes>().changeSpeed(currentSpeed);
        }
        else if (score % (everyNPipe + 2) == 0)
        {
            currentSpeed = (currentSpeed + newSpeed) / 2;
            pipesPrefab.gameObject.GetComponent<Pipes>().changeSpeed(currentSpeed);
        }
        else if (score % (everyNPipe + 3) == 0)
        {
            currentSpeed = newSpeed;
            halfSpeed = 0.0f;
            pipesPrefab.gameObject.GetComponent<Pipes>().changeSpeed(currentSpeed);
        }
    }

    private void moveYPipe()
    {
        pipeSpawner.GetComponent<Spawner>().isMovingOnY(true);
    }
}
