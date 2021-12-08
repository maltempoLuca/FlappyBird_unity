using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // si puo salvare in un file
public class PlayerData
{
    public int highScore;
    public PlayerData(GameManager gameManager)
    {
        highScore = gameManager.getScore();
    }
}
