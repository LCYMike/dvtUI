using UnityEngine;
using UnityEngine.UI;

public class ScriptsManager : MonoBehaviour {

    public Text hpTxt;
    public Text shieldTxt;
    public Text scoreTxt;
    public Text enemyHpTxt;
    public Text enemyShieldTxt;
    public Text gameOverTxt;
    public Text victoryTxt;
    public Text infoTxt;
    public Text nukeTxt;

    public GameObject _Enemy1;
    public GameObject _Enemy2;
    public GameObject _Enemy3;
    public GameObject _Enemy4;
    public GameObject _Enemy5;

    int currentEnemy = 1;

    public AudioSource tacNuke;
    public AudioSource bomb;

    private int health = 20;
    private int enemyHealth = 5;
    private int shield = 20;
    private int enemyShield = 0;

    private float score = 0;

    private bool isGameOver = false;

    private void Start()
    {
        gameOverTxt.enabled = false;
        victoryTxt.enabled = false;
        nukeTxt.enabled = false;
        _Enemy1.SetActive(true);
        _Enemy2.SetActive(false);
        _Enemy3.SetActive(false);
        _Enemy4.SetActive(false);
        _Enemy5.SetActive(false);
        SetText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isGameOver == false)
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.N) && isGameOver == false)
        {
            Nuke();
        }
        if (Input.GetKeyDown(KeyCode.B) && isGameOver == false)
        {
            Bomb();
        }
    }

    private void SetText()
    {
        hpTxt.text = "[HEALTH] : ";
        for (int i = 0; i < health; i++)
        {
            hpTxt.text += "|";
        }
        shieldTxt.text = "[SHIELD] :";
        for (int i = 0; i < shield; i++)
        {
            shieldTxt.text += "|";
        }
        enemyHpTxt.text = "";
        enemyShieldTxt.text = "";
        for (int i = 0; i < enemyHealth; i++)
        {
            enemyHpTxt.text += "|";
        }
        enemyHpTxt.text += " : [HEALTH]";
        for (int i = 0; i < enemyShield; i++)
        {
            enemyShieldTxt.text += "|";
        }
        enemyShieldTxt.text += " : [SHIELD]";

        scoreTxt.text = "[SCORE] : " + score;
    }


    public void TakeDamage()
    {
        if (shield > 0)
        {

            float hit = Mathf.Floor(Random.Range(0, 3));

            if (hit >= 1)
            {
                shield--;
            }
            else if (hit == 0)
            {
                health--;
            }
        }
        else
        {
            health--;
        }


        if (health <= 0)
        {
            GameOver();
        }else
        {
            SetText();
        }
    }

    private void Nuke()
    {
        enemyHealth -= enemyHealth;
        currentEnemy = 5;
        tacNuke.Play();
        LoadNextEnemy();
        nukeTxt.enabled = true;
        score = 9999999999999999999;


        Invoke("DeNuke", 5f);
    }

    private void Bomb()
    {
        enemyHealth -= enemyHealth;
        bomb.Play();
        score += 100000;
        if (enemyHealth <= 0)
        {
            LoadNextEnemy();
        }
        else
        {
            TakeDamage();
        }

        nukeTxt.enabled = true;

        Invoke("DeNuke", 3f);
    }

    private void DeNuke()
    {
        nukeTxt.enabled = false;
    }



    private void Attack()
    {
        float damage = Mathf.Floor(Random.Range(1, 5));

        int willCrit = (int)Mathf.Floor(Random.Range(1, 4));

        if (willCrit == 1)
        {
            damage = damage * 2;
        }

        score += damage;

        if (enemyShield > 0)
        {

            float hit = Mathf.Floor(Random.Range(0, 2) + 1);
            if (hit == 2)
            {
                enemyShield -= (int) damage;
            }
            else if (hit == 1)
            {
                enemyHealth -= (int) damage;
            }
        }
        else
        {
            enemyHealth -= (int) damage;
        }

        if (enemyHealth <= 0)
        {
            LoadNextEnemy();
        } else
        {
            TakeDamage();
        }
    }

    private void LoadNextEnemy()
    {
        switch (currentEnemy)
        {
            case 1:
                currentEnemy++;
                LoadEnemy2();
                break;
            case 2:
                currentEnemy++;
                LoadEnemy3();
                break;
            case 3:
                currentEnemy++;
                LoadEnemy4();
                break;
            case 4:
                currentEnemy++;
                LoadEnemy5();
                break;
            case 5:
                Victory();
                break;

        }
    }

    private void LoadEnemy2()
    {
        _Enemy1.SetActive(false);
        _Enemy2.SetActive(true);
        enemyHealth = 10;
        enemyShield = 0;
        SetText();
    }
    private void LoadEnemy3()
    {
        _Enemy2.SetActive(false);
        _Enemy3.SetActive(true);
        enemyHealth = 10;
        enemyShield = 5;
        SetText();
    }
    private void LoadEnemy4()
    {
        _Enemy3.SetActive(false);
        _Enemy4.SetActive(true);
        enemyHealth = 20;
        enemyShield = 15;
        SetText();
    }
    private void LoadEnemy5()
    {
        _Enemy4.SetActive(false);
        _Enemy5.SetActive(true);
        enemyHealth = 50;
        enemyShield = 30;
        SetText();
    }

    private void GameOver()
    {
        hpTxt.enabled = false;
        infoTxt.enabled = false;
        scoreTxt.enabled = false;
        shieldTxt.enabled = false;
        enemyHpTxt.enabled = false;
        enemyShieldTxt.enabled = false;
        _Enemy1.SetActive(false);
        _Enemy2.SetActive(false);
        _Enemy3.SetActive(false);
        _Enemy4.SetActive(false);
        _Enemy5.SetActive(false);
        gameOverTxt.enabled = true;
        isGameOver = true;
    }

    private void Victory()
    {
        hpTxt.enabled = false;
        infoTxt.enabled = false;
        scoreTxt.enabled = false;
        shieldTxt.enabled = false;
        enemyHpTxt.enabled = false;
        enemyShieldTxt.enabled = false;
        _Enemy1.SetActive(false);
        _Enemy2.SetActive(false);
        _Enemy3.SetActive(false);
        _Enemy4.SetActive(false);
        _Enemy5.SetActive(false);
        victoryTxt.enabled = true;
        isGameOver = true;
    }

}
