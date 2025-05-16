using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //count enemy if no enemy then room clear

    public int enemyCount;

    void Update(){
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        enemyCount = enemies.Length;
    }

   
}
