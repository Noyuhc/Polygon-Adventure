using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int coin;
    public int health;
    public float[] position;
    
    public PlayerData (GameManager player)
    {
        coin = player.currentCoin;
        health = player.currentHealth;

        position = new float[3];
        position[0] = player.thePlayer.transform.position.x;
        position[1] = player.thePlayer.transform.position.y;
        position[2] = player.thePlayer.transform.position.z;
    }

}
