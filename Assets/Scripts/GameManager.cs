using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Collections;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }
    public bool contact;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI Countdown;

    public int countDownTime;

    private IEnumerator CountdowntoStart()
    {
        yield return new WaitForSeconds(1f);
    }


    private Player player;
    private Spawner spawner;
    private float score;



    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();


        NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }


        gameSpeed = initialGameSpeed;


        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        GameOverText.gameObject.SetActive(false);
       



    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(true);
        StartCoroutine(CountdowntoStart());



        //IEnumerator Countdown()
        //{
        //    while(countDownTimer > 0)
        //    {
        //        Countdown1.text = countDownTimer.ToString();

        //        yield return new WaitForSeconds(1f);

        //        countDownTimer--;
        //    }
        //}

        //StartCoroutine(Countdown());


        //NewGame();

    }

   

    private void Update()
    {
        if (contact)
        {
            score++;
            contact = false;
        }
        ScoreText.text = Mathf.FloorToInt(score).ToString("D5");

    }

    public void addPoint()
    {
        contact = true;
        
        
    }
}
