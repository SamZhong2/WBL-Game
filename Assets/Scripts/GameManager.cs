using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Collections;
using System.IO;

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
    public int tempscore;

    private IEnumerator CountdowntoStart()
    {
        GameOverText.gameObject.SetActive(true);
        

        yield return new WaitForSeconds(3f);

        
        NewGame();
        FindObjectOfType<Spawner>().Delete();
    }


    private Player player;
    private Spawner spawner;
    private Point point;
    public int score;



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
        point = FindObjectOfType<Point>();


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
        point.gameObject.SetActive(true);
        GameOverText.gameObject.SetActive(false);

        score = tempscore;
       

        

    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        point.gameObject.SetActive(false);
        
        
        StartCoroutine(CountdowntoStart());



      

    }

    public void WriteString(bool contact)
    {
        string path = "Assets/Resources/Text.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        string str = contact.ToString();
        writer.WriteLine("1");
        writer.Close();
    }


    private void Update()
    {
        if (contact == true)
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
