using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private EnemyBehaviors enemyBehaviors;
    private enum GameState
    {
        startState,
        gameState,
        gameOverState
    }
    private GameState state;
    public GameObject playerPrefab;
    public GameObject spawnerPrefab;
    public GameObject groundPrefab;
    private int score;
    private int highScore;
    private float waitDeathTime = 1.5f;


    private GameObject player;
    private GameObject spawner;
    private GameObject ground;
    public Text scoreText;
    public Text gameOverScore;
    public Text highScoreText;
    public Canvas[] canvas;

    public Image[] medals;

    private void Awake()
    {
        state = GameState.startState;
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        Time.timeScale = 1.0f;
        canvas[0].gameObject.SetActive(true);
    }
    public void gameOver()
    {
        Debug.Log("GameOver");
        pause();
    }
    public void pause()
    {
        Time.timeScale = 0.0f;
        player.GetComponent<Rigidbody2D>().isKinematic = true;
        StartCoroutine(toGameOverCanvas());

    }
    private IEnumerator toGameOverCanvas()
    {
        yield return new WaitForSecondsRealtime(waitDeathTime);
        canvas[1].gameObject.SetActive(false);
        if (enemyBehaviors.Equals(null))
        {
            enemyBehaviors.restoreState();
            Destroy(enemyBehaviors);
        }
        cleanScene();
        Destroy(spawner.gameObject);
        state = GameState.gameOverState;
        canvas[2].gameObject.SetActive(true);
        loadHighScore();
        if (score > highScore)
        {
            setHighScore();
        }
        loadHighScore();
        loadMedals();
        gameOverScore.text = score.ToString();
        highScoreText.text = highScore.ToString();

    }
    public void increaseScore()
    {
        score++;
        enemyBehaviors.changeEnemyAttack(score);
        scoreText.text = score.ToString();
    }

    public void startPressed()
    {
        state = GameState.gameState;
        canvas[0].gameObject.SetActive(false);
        canvas[2].gameObject.SetActive(false);
        canvas[1].gameObject.SetActive(true);
        Time.timeScale = 1.0f;
        clearPipes();
        player = Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation);
        ground = Instantiate(groundPrefab, groundPrefab.transform.position, groundPrefab.transform.rotation);
        resetMedals();
        score = 0;
        scoreText.text = score.ToString();
    }
    private void cleanScene()
    {
        Destroy(player.gameObject);
        Destroy(ground.gameObject);
        clearPipes();
    }

    private void clearPipes()
    {
        Pipes[] pipes = FindObjectsOfType<Pipes>();
        foreach (Pipes pipe in pipes)
        {
            Destroy(pipe.gameObject);
        }
    }
    public void startGame()
    {
        player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        spawner = Instantiate(spawnerPrefab, spawnerPrefab.transform.position, spawnerPrefab.transform.rotation);
        enemyBehaviors = new EnemyBehaviors(spawner);
    }

    private void loadHighScore()
    {
        PlayerData playerData = SaveSystem.loadPlayer();
        if (playerData != null)
            highScore = playerData.highScore;
        else highScore = 0;
    }

    private void setHighScore()
    {
        SaveSystem.saveData(this);
    }

    private void loadMedals()
    {
        if (score > 100)
        {
            medals[3].gameObject.SetActive(true);
        }
        else if (score > 75)
        {
            medals[2].gameObject.SetActive(true);
        }
        else if (score > 50)
        {
            medals[1].gameObject.SetActive(true);
        }
        else if (score > 25)
        {
            medals[0].gameObject.SetActive(true);
        }
    }

    private void resetMedals()
    {
        foreach (Image image in medals)
        {
            image.gameObject.SetActive(false);
        }
    }

    public int getScore()
    {
        return score;
    }
}
