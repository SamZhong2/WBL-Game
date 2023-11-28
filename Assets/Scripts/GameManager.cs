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

    public int level = 1;

    public float initialGameSpeed = 5f;
    //public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }
    //public bool contact;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI GameOverText;


    public int countDownTime;
    

    private IEnumerator CountdowntoStart()
    {
        
        

        yield return new WaitForSeconds(3f);
        
        

        if (score_in_row >= 5)
        {
            initialGameSpeed += 2;
            level++;
        }
        
        score_in_row = 0;
        NewGame();
        FindObjectOfType<Spawner>().Delete();

    }


    private Player player;
    private Spawner spawner;
    private Point point;
    public int score;
    public int score_in_row;



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
        GameOverText.gameObject.SetActive(false);

        
            Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

            foreach (var obstacle in obstacles)
            {
                Destroy(obstacle.gameObject);
            }


            gameSpeed = initialGameSpeed;




            player.gameObject.SetActive(true);
            spawner.gameObject.SetActive(true);
            point.gameObject.SetActive(true);
        

        
        
        


    }


    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        point.gameObject.SetActive(false);

        GameOverText.text = "respawning...";

        GameOverText.gameObject.SetActive(true);
        StartCoroutine(CountdowntoStart());



        

    }

    public void LevelUp()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        point.gameObject.SetActive(false);

        GameOverText.text = "LEVEl UP...";

        GameOverText.gameObject.SetActive(true);
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
        //if (contact == true)
        //{

        //    score++;
        //    contact = false;


        //}
        //ScoreText.text = Mathf.FloorToInt(score).ToString("D3");

    }

    public void addPoint()
    {
        score++;
        score_in_row++;
        ScoreText.text = Mathf.FloorToInt(score).ToString("D5");

        if (level >= 3)
        {
            score_in_row = 0;
        }

        if (score_in_row >= 5)
        {

            LevelUp();
        }

    }
}
