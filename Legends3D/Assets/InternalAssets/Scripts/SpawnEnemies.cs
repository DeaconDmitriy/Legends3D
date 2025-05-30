using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public static int countEnemies;
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wawe1;
    public GameObject wawe2;
    public GameObject wawe3;
    public GameObject winPanel;
    public AudioSource music;
    public AudioSource WinMusic;
    public PlayerHealth player;
    public EnemyHealth enemy;

    public bool Level1 = false;
    public bool Level2 = false;
    public bool Level3 = false;
    public bool Level4 = false;

    public void Update()
    {
        if(countEnemies >= 1 && !Level1)
        {
            wall1.SetActive(false);
            wawe1.SetActive(true);
            player.NewLevel();
            enemy.damagecount += 5;
            Level1 = true;
        }

        if (countEnemies >= 3 && !Level2)
        {
            wall2.SetActive(false);
            wawe2.SetActive(true);
            player.NewLevel();
            enemy.damagecount += 5;
            Level2 = true;
        }

        if (countEnemies >= 6 && !Level3)
        {
            wall3.SetActive(false);
            wawe3.SetActive(true);
            player.NewLevel();
            enemy.damagecount += 5;
            Level3 = true;
        }

        if (countEnemies >= 7 && !Level4) 
        {
            winPanel.SetActive(true);
            music.Stop();
            WinMusic.Play();
            Time.timeScale = 0;
            Level4 = true;
            countEnemies = 0;
            Level1 = false;
            Level2 = false;
            Level3 = false;
            Level4 = false;
        }
    }
}
