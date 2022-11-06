using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{

    public int maximumHp = 100;
    public int playerHp;
    public int playerLevel = 1;
    public int playerExperience = 0;
    public int attackDamage = 10;
    public int playerKills = 0;
    public int nextLevelExperience = 1000;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerHp = maximumHp;
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (playerExperience >= nextLevelExperience)
        {
            LevelUp();
        }
        */
    }

    /*

    public void PlayerKilledEnemy(int experience)
    {
        playerKills = playerKills + 1;
        playerExperience = playerExperience + experience;
    }

    

    public void LevelUp()
    {
        nextLevelExperience = Mathf.RoundToInt(nextLevelExperience * 1.5f);
        attackDamage = attackDamage + 1;
        maximumHp = maximumHp + 10;
        playerHp = maximumHp;
    }
    */

    public void PlayerTakeDamage(int damage)
    {
        if (playerController.isShieldUp)
        {
            damage = damage / 2;
        }
        playerController.isTakingDamage = true;
        playerHp = playerHp - damage;
    }
}
