
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using Michsky.UI.ModernUIPack;
//using NeuroRehabLibrary;
//using static Done_PlayerController;
//using static BirdControl;
//public class FlappyGameControl : MonoBehaviour
//{
//    // public BirdControl birdControl;
//    public AudioClip[] winClip;
//    public AudioClip[] hitClip;
//    public Text ScoreText;
//    public ProgressBar timerObject;
//    public static FlappyGameControl instance;
//    //public RockVR.Video.VideoCapture vdc;
//    public GameObject GameOverText;
//    public GameObject CongratulationsText;
//    public bool gameOver = false;
//    //public float scrollSpeed = -1.5f;
//    public float scrollSpeed;
//    private int score;
//    public GameObject[] pauseObjects;
//    public float gameduration = 90 * 5;
//    //public float gameduration = PlayerPrefs.GetFloat("");
//    public GameObject start;
//    int win = 0;
//    bool endValSet = false;

//    public int startGameLevelSpeed = 1;
//    public int startGameLevelRom = 1;
//    public float ypos;

//    public GameObject menuCanvas;
//    public GameObject Canvas;

//    public BirdControl bc;

//    public Text durationText;
//    private int duration = 0;
//    IEnumerator timecoRoutine;
//    bool column_position_flag;
//    public static bool column_position_flag_topass;

//    public Text LevelText;

//    public static string start_time;
//    string end_time;
//    string p_hospno;
//    int hit_count;

//    float start_speed;
//    public static float auto_speed = -2.0f;

//    int total_life = 3;

//    DateTime last_three_duration;

//    string path_to_data;
//    int currentLevel = 1;

//    public static string gameend;

//    int totalObstacles = 0;
//    int obstaclesCrossed = 0;
//    private bool coroutineStarted = false;
//    public  float columnSpawnRate = 7f; // Default spawn rate



//    private SessionManager sessionManager;
//    private GameSession currentGameSession;


//    void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
//        }
//        else if (instance != null)
//        {
//            Destroy(gameObject);
//        }


//    }


//    // Start is called before the first frame update
//    void Start()
//    {
//        // Initial log
//        //Debug.Log("Initial auto_speed in Start: " + auto_speed);
//        path_to_data = Application.dataPath;
//        start_time = DateTime.Now.ToString("HH:mm:ss.fff");

//        LevelText.enabled = true;
//        scrollSpeed = -(PlayerPrefs.GetFloat("ScrollSpeed"));
//        Time.timeScale = 1;
//        ShowGameMenu();
//        column_position_flag = false;

//        // Start the column spawning coroutine
//        StartCoroutine(SpawnColumns());



//        string baseDirectory = circleclass.circlePath; // Adjust path as needed
//        sessionManager = new SessionManager(baseDirectory);
//        StartNewGameSession();
//    }


//    void StartNewGameSession()
//    {
//        currentGameSession = new GameSession
//        {
//            GameName = "TUK-TUK",
//            Assessment = 0 // Example assessment value, adjust as needed
//        };
//        sessionManager.StartGameSession(currentGameSession);

//        // Set additional session details
//        SetSessionDetails();
//    }


//    private void SetSessionDetails()
//    {
//        string device = "R2"; // Set the device name
//        string assistMode = "Null"; // Set the assist mode
//        string assistModeParameters = "YourAssistModeParameters"; // Set the assist mode parameters
//        string deviceSetupLocation = "YourDeviceSetupLocation"; // Set the device setup location
//        string gameParameter = "YourGameParameter"; // Set the game parameter
//        string trialDataFileLocation = flappyclass.relativepath; // Adjust path as needed

//        sessionManager.SetDevice(device, currentGameSession);
//        sessionManager.SetAssistMode(assistMode, assistModeParameters, currentGameSession);
//        sessionManager.SetDeviceSetupLocation(deviceSetupLocation, currentGameSession);
//        sessionManager.SetGameParameter(gameParameter, currentGameSession);
//        sessionManager.SetTrialDataFileLocation(trialDataFileLocation, currentGameSession);
//    }

//    void EndCurrentGameSession()
//    {
//        if (currentGameSession != null)
//        {
//            sessionManager.EndGameSession(currentGameSession);
//        }
//    }


//    // Update is called once per frame
//    void Update()
//    {
//        // Log auto_speed in each update for debugging purposes
//        //Debug.Log("auto_speed in Update: " + auto_speed);
//        // LevelText.text = "Level: " + auto_speed * (-0.5);
//        LevelText.text = "Level: " + currentLevel;
//       // Time.timeScale = auto_speed;
//        UpdateGameDurationUI();

//        column_position_flag_topass = column_position_flag;


//        //uses the p button to pause and unpause the game
//        if ((Input.GetKeyDown(KeyCode.P)))
//        {
//            if (!gameOver)
//            {
//                if (Time.timeScale == 1)
//                {
//                    Time.timeScale = 0;
//                    showPaused();
//                }
//                else if (Time.timeScale == 0)
//                {
//                    Time.timeScale = 1;
//                    hidePaused();
//                }
//            }
//            else if (gameOver)
//            {
//                hidePaused();
//                //playAgain();

//            }
//        }


//        if (!gameOver && Time.timeScale == 1)
//        {
//            gameduration -= Time.deltaTime;
//        }
//        if (currentLevel == 1 && gameduration <= 0)
//        {
//            currentLevel = 2;
//            AdjustSpeedForNextLevel();
//        }
//        // Check if it's the end of level 2
//        if (currentLevel == 2 && gameOver && gameduration <= 0)
//        {
//            CongratulationsText.SetActive(false);
//            // Display "Game Over" text
//            GameOverText.SetActive(true);
//        }


//        int control = BirdControl.hit_count;

//        if (control == 3)
//        {

//            EndCurrentGameSession();
//            gameend = DateTime.Now.ToString("HH:mm:ss.fff");
//        }



//    }

//    private IEnumerator SpawnColumns()
//    {
//       // yield return new WaitForSeconds(3f); // Initial 3 seconds delay
//        while (!gameOver)
//        {
//            FlappyColumnPool.instance.spawnColumn();
//            yield return new WaitForSeconds(columnSpawnRate); // Adjust the spawn rate as needed

//        }
//    }
//    void UpdateGameDurationUI()
//    {
//        //// timerObject.specifiedValue = Mathf.Clamp(100 * (90 - gameduration) / 90f, 0, 100); 

//    }

//    //shows objects with ShowOnPause tag
//    public void showPaused()
//    {

//        foreach (GameObject g in pauseObjects)
//        {
//            g.SetActive(true);
//        }
//    }

//    //hides objects with ShowOnPause tag
//    public void hidePaused()
//    {

//        foreach (GameObject g in pauseObjects)
//        {
//            g.SetActive(false);
//        }
//    }
//    public void BirdDied()
//    {
//        GameOverText.SetActive(true);
//        gameOver = true;

//    }

//    public void Birdalive()
//    {
//        //CongratulationsText.SetActive(true);
//        //gameOver = true;
//        if (currentLevel == 1)
//        {
//            // Transition to Level 2 without showing congratulations
//            currentLevel = 2;
//            bc.ResetLives();
//            AdjustSpeedForNextLevel();
//            gameOver = false;
//        }
//        else
//        {
//            CongratulationsText.SetActive(true);
//            gameOver = true;
//        }

//    }

//    public void BirdScored()
//    {
//        if (!bc.startBlinking)
//        {
//            score += 1;
//            obstaclesCrossed += 1; // Increment obstacles crossed
//            Debug.Log("obstaclesCrossed :" + obstaclesCrossed);
//        }


//        ScoreText.text = "Score: " + score.ToString();
//        // Debug.Log(ScoreText.text);
//        //FlappyColumnPool.instance.spawnColumn();
//        // Start spawning columns immediately
//        FlappyColumnPool.instance.spawnColumn();
//        //ColumnSpawned();

//    }



//    public void RandomAngle()
//    {
//        ypos = UnityEngine.Random.Range(-3f, 5.5f);
//    }

//    public void playAgain()
//    {
//        if (gameOver == true)
//        {

//            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

//        }


//    }
//        public void PlayStart()
//    {
//        endValSet = false;
//        start.SetActive(false);
//        Time.timeScale = 1;
//    }

//    public void continueButton()
//    {
//        if (Time.timeScale == 0)
//        {
//            Time.timeScale = 1;
//            hidePaused();

//        }
//    }

//    public void ShowGameMenu()
//    {

//        menuCanvas.SetActive(true);
//        Canvas.SetActive(false);
//        Time.timeScale = 0;
//    }

//    public void StartGame()
//    {
//        gameOver = false;

//        // Debug.Log(start_time+" :start_time");
//        ////p_hospno = Welcome.p_hospno;        
//        // Debug.Log("Start game.."+start_time);
//        Canvas.SetActive(true);
//        menuCanvas.SetActive(false);
//        Time.timeScale = 1;
//        timecoRoutine = SpawnTimer();
//        duration = 60;
//        StartCoroutine(timecoRoutine);
//        StartCoroutine(SpawnColumns()); // Start the column spawning coroutine

//        //FlappyData();

//        StartNewGameSession();

//        // Start the column spawning coroutine once
//        if (!coroutineStarted)
//        {
//            StartCoroutine(SpawnColumns());
//            coroutineStarted = true;
//        }

//    }

//    private IEnumerator SpawnTimer()
//    {
//        while (!gameOver)
//        {

//            duration = duration - 1;
//            UpdateDuration();

//            if (duration == 0)
//            {
//                gameOver = true;
//                duration = 60;
//                Birdalive();
//                total_life = total_life - 1;
//                if (total_life < 0)
//                {
//                    auto_speed = auto_speed - 2.0f;
//                }


//               // Level transition after 60 minutes
//               if (currentLevel == 1)
//               {
//                   currentLevel = 2;

//                   AdjustSpeedForNextLevel();
//               }


//            }

//            yield return new WaitForSeconds(1f);

//        }

//    }
//    void EndGame()
//    {
//        gameOver = true;
//        GameOverText.SetActive(true);
//        Time.timeScale = 0; // Stop the game
//    }
//    void UpdateDuration()
//    {
//        durationText.text = "Duration: " + duration;

//    }

//    void AdjustSpeedForNextLevel()
//    {
//        Debug.Log("Obstacles Crossed: " + obstaclesCrossed);
//        Debug.Log("Total Obstacles: " + totalObstacles);

//        if (totalObstacles == 0)
//        {
//            Debug.LogError("Total Obstacles is zero, cannot calculate crossedPercentage");
//            return;
//        }
//        float crossedPercentage = (float)obstaclesCrossed / totalObstacles;
//        Debug.Log("Crossed Percentage: " + crossedPercentage);

//        Debug.Log("Initial auto_speed: " + auto_speed);
//        if (crossedPercentage > 0.8f)
//        {
//            Debug.Log("Crossed Percentage is greater than 80%. Speed adjustment factor: -2.5");
//            auto_speed *= -3.8f;
//        }
//        else
//        {
//            Debug.Log("Crossed Percentage is 80% or less. Speed adjustment factor: -1.0");
//            auto_speed *= -0.2f;
//        }

//        Debug.Log("Adjusted auto_speed in AdjustSpeedForNextLevel: " + auto_speed);
//        // Ensure auto_speed remains within reasonable bounds
//        auto_speed = Mathf.Clamp(auto_speed, -0.2f, -3.8f); // Adjust the bounds as needed

//        // Debug the new auto_speed value
//        Debug.Log("New auto_speed after clamping in AdjustSpeedForNextLevel: " + auto_speed);

//        LevelText.enabled = true;
//        LevelText.text = "Level: " + currentLevel;
//        // Adjust column spawn rate for next level
//        if (currentLevel == 2)
//        {
//            columnSpawnRate = 4f;
//        }

//    }
//    // Optionally log method calls that might affect auto_speed
//    public void SomeOtherMethodThatChangesSpeed(float newSpeed)
//    {
//        Debug.Log("SomeOtherMethodThatChangesSpeed called. New speed: " + newSpeed);
//        auto_speed = newSpeed;
//        Debug.Log("auto_speed after change in SomeOtherMethodThatChangesSpeed: " + auto_speed);
//    }


//    void RenewLives()
//    {
//        total_life = 3;
//    }



//    public void continue_pressed()
//    {
//        StartGame();
//        GameOverText.SetActive(false);
//        CongratulationsText.SetActive(false);
//        gameOver = false;
//    }

//    public void quit_pressed()
//    {
//        int control = BirdControl.hit_count;
//        if (control == 3)
//        {

//            SceneManager.LoadScene("New Scene");
//        }
//        else
//        {
//            EndCurrentGameSession();
//            SceneManager.LoadScene("New Scene");
//        }
//        //EndCurrentGameSession();
//        //SceneManager.LoadScene("New Scene");
//    }

//    public void OnApplicationQuit()
//    {

//    }
//    public void ColumnSpawned()
//    {
//       totalObstacles++; // Increment total obstacles when a column is spawned
//        Debug.Log("Total obstacles spawned: " + totalObstacles); // Debug total obstacles
//    }






//}


























using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Michsky.UI.ModernUIPack;
using NeuroRehabLibrary;
using static Done_PlayerController;
using static BirdControl;

public class FlappyGameControl : MonoBehaviour
{
    // Public and private variables
    public AudioClip[] winClip;
    public AudioClip[] hitClip;
    public Text ScoreText;
    public ProgressBar timerObject;
    public static FlappyGameControl instance;
    public GameObject GameOverText;
    public GameObject CongratulationsText;
    public bool gameOver = false;
    public float scrollSpeed;
    private int score;
    public GameObject[] pauseObjects;
    public float gameduration = 90 * 5;
    public GameObject start;
    int win = 0;
    bool endValSet = false;

    public int startGameLevelSpeed = 1;
    public int startGameLevelRom = 1;
    public float ypos;

    public GameObject menuCanvas;
    public GameObject Canvas;

    public BirdControl bc;

    public Text durationText;
    private int duration = 0;
    IEnumerator timecoRoutine;
    bool column_position_flag;
    public static bool column_position_flag_topass;

    public Text LevelText;

    public static string start_time;
    string end_time;
    string p_hospno;
    int hit_count;

    float start_speed;
    public static float auto_speed = -2.0f;

    int total_life = 3;

    DateTime last_three_duration;

    string path_to_data;
    int currentLevel = 1;

    public static string gameend;

    int totalObstacles = 0;
    int obstaclesCrossed = 0;
    private bool coroutineStarted = false;
    public float columnSpawnRate = 7f; // Default spawn rate
    public float gamepercentage;

    public static float gameparametervalue;


    private GameSession currentGameSession;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        path_to_data = Application.dataPath;
        start_time = DateTime.Now.ToString("HH:mm:ss.fff");

        LevelText.enabled = true;
        scrollSpeed = -(PlayerPrefs.GetFloat("ScrollSpeed"));
        Time.timeScale = 1;
        ShowGameMenu();
        column_position_flag = false;
        auto_speed = -2.0f;

        StartCoroutine(SpawnColumns());

       
        StartNewGameSession();
        RetrieveAndAdjustGameParameterFromCSV();

    }

    void StartNewGameSession()
    {
        currentGameSession = new GameSession
        {
            GameName = "TUK-TUK",
            Assessment = 0 // Example assessment value, adjust as needed
        };

        SessionManager.Instance.StartGameSession(currentGameSession);
        Debug.Log($"Started new game session with session number: {currentGameSession.SessionNumber}");

        SetSessionDetails();
    }


    private void SetSessionDetails()
    {
        string device = "R2"; // Set the device name
        string assistMode = "Null"; // Set the assist mode
        string assistModeParameters = "Null"; // Set the assist mode parameters
        string deviceSetupLocation = "NULL"; // Set the device setup location
        string trialDataFileLocation = flappyclass.relativepath; // Adjust path as needed

        // Initialize gameParameter with the first level speed
        string gameParameter = auto_speed.ToString();

        if (currentGameSession != null)
        {
            gameParameter = currentGameSession.GameParameter; // Retrieve existing gameParameter
        }

        // Append new speed data to gameParameter
        gameParameter += $"{auto_speed}";

        SessionManager.Instance.SetDevice(device, currentGameSession);
        SessionManager.Instance.SetAssistMode(assistMode, assistModeParameters, currentGameSession);
        SessionManager.Instance.SetDeviceSetupLocation(deviceSetupLocation, currentGameSession);
        SessionManager.Instance.SetGameParameter(gameParameter, currentGameSession);
        SessionManager.Instance.SetTrialDataFileLocation(trialDataFileLocation, currentGameSession);
    }
    //private void RetrieveGameParameterFromCSV()
    //{
    //    string csvFilePath = Path.Combine(circleclass.circlePath, "Sessions.csv");

    //    if (!File.Exists(csvFilePath))
    //    {
    //        Debug.LogError("CSV file not found at: " + csvFilePath);
    //        return;
    //    }

    //    string[] csvLines = File.ReadAllLines(csvFilePath);
    //    string[] headers = csvLines[0].Split(',');

    //    // Find the indices for session number, game name, and game parameter columns
    //    int sessionIndex = Array.IndexOf(headers, "SessionNumber");
    //    int gameNameIndex = Array.IndexOf(headers, "GameName");
    //    int gameParameterIndex = Array.IndexOf(headers, "GameParameter");

    //    if (sessionIndex == -1 || gameNameIndex == -1 || gameParameterIndex == -1)
    //    {
    //        Debug.LogError("Required columns not found in the CSV file.");
    //        return;
    //    }

    //    // Find the last row with the same session number and game name
    //    string currentSessionNumber = currentGameSession.SessionNumber.ToString();
    //    string currentGameName = currentGameSession.GameName;

    //    string lastGameParameterValue = null;

    //    for (int i = csvLines.Length - 1; i > 0; i--)
    //    {
    //        string[] row = csvLines[i].Split(',');

    //        if (row[sessionIndex] == currentSessionNumber && row[gameNameIndex] == currentGameName)
    //        {
    //            lastGameParameterValue = row[gameParameterIndex];
    //            break;
    //        }
    //    }

    //    if (lastGameParameterValue != null)
    //    {
    //        gameparametervalue = float.Parse(lastGameParameterValue);
    //        auto_speed = gameparametervalue; // Assign the retrieved value to levelspeed
    //        Debug.Log("Game parameter retrieved from CSV: " + gameparametervalue);
    //    }
    //    else
    //    {
    //        Debug.Log("No matching session and game name found in the CSV file. Using default game parameter.");
    //    }
    //}



    private void RetrieveAndAdjustGameParameterFromCSV()
    {
        string csvFilePath = Path.Combine(circleclass.circlePath, "Sessions.csv");

        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("CSV file not found at: " + csvFilePath);
            return;
        }

        string[] csvLines = File.ReadAllLines(csvFilePath);
        string[] headers = csvLines[0].Split(',');

        // Find the indices for session number, game name, and game parameter columns
        int sessionIndex = Array.IndexOf(headers, "SessionNumber");
        int gameNameIndex = Array.IndexOf(headers, "GameName");
        int gameParameterIndex = Array.IndexOf(headers, "GameParameter");

        if (sessionIndex == -1 || gameNameIndex == -1 || gameParameterIndex == -1)
        {
            Debug.LogError("Required columns not found in the CSV file.");
            return;
        }

        // Find the last row with the same session number and game name
        string currentSessionNumber = currentGameSession.SessionNumber.ToString();
        string currentGameName = currentGameSession.GameName;

        string lastGameParameterValue = null;
        float gameplayPercentage = PlayerPrefs.GetFloat("gamepercentage"); ; // Assume this is tracked somewhere

        for (int i = csvLines.Length - 1; i > 0; i--)
        {
            string[] row = csvLines[i].Split(',');

            if ( row[gameNameIndex] == currentGameName)
            {
                lastGameParameterValue = row[gameParameterIndex];
                break;
            }
        }

        if (lastGameParameterValue != null)
        {
            gameparametervalue = float.Parse(lastGameParameterValue);
            if (gameplayPercentage >= 0.8f)
            {
                gameparametervalue =gameparametervalue - 0.5f; 
                columnSpawnRate =columnSpawnRate - 2.3f;


            }
            else //if (gameplayPercentage < 80)
            {
                gameparametervalue =gameparametervalue + 0.5f; 
                columnSpawnRate = columnSpawnRate +2.2f;

            }

            auto_speed = gameparametervalue; // Assign the adjusted value to levelspeed
            Debug.Log("Game parameter retrieved and adjusted from CSV: " + gameparametervalue);

            SetSessionDetails();
        }
        else
        {
            auto_speed = -2.0f; // Default speed for the first time
            Debug.Log("No matching session and game name found in the CSV file. Using default game parameter.");

            SetSessionDetails();
        }

        // Save the new game parameter to the CSV file

       

    }




    void EndCurrentGameSession()
    {
        if (currentGameSession != null)
        {
            SessionManager.Instance.EndGameSession(currentGameSession);
        }
    }

    // Update is called once per frame
    void Update()
    {
        LevelText.text = "Level: " + currentLevel;
        UpdateGameDurationUI();
        column_position_flag_topass = column_position_flag;

        if ((Input.GetKeyDown(KeyCode.P)))
        {
            if (!gameOver)
            {
                if (Time.timeScale == 1)
                {
                    Time.timeScale = 0;
                    showPaused();
                }
                else if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                    hidePaused();
                }
            }
            else if (gameOver)
            {
                hidePaused();
            }
        }

        if (!gameOver && Time.timeScale == 1)
        {
            gameduration -= Time.deltaTime;
        }
        //if (currentLevel == 1 && gameduration <= 0)
        //{
        //    currentLevel = 2;
        //    AdjustSpeedForNextLevel();
        //}

        //if (currentLevel == 2 && gameOver && gameduration <= 0)
        //{
        //    CongratulationsText.SetActive(false);
        //    GameOverText.SetActive(true);
        //}
        if (currentLevel == 1 && gameOver && gameduration <= 0)
        {
            AdjustSpeedForNextLevel();
            CongratulationsText.SetActive(false);
            GameOverText.SetActive(true);
        }

       
        int control = BirdControl.hit_count;

        if (control == 3)
        {
            EndCurrentGameSession();
            gameend = DateTime.Now.ToString("HH:mm:ss.fff");
        }
    }

    private IEnumerator SpawnColumns()
    {
        while (!gameOver)
        {
            FlappyColumnPool.instance.spawnColumn();
            yield return new WaitForSeconds(columnSpawnRate);
        }
    }

    void UpdateGameDurationUI()
    {
        // Implement the method to update the game duration UI if needed
    }

    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void BirdDied()
    {
        AdjustSpeedForNextLevel();
        GameOverText.SetActive(true);
        gameOver = true;
    }

    public void Birdalive()
    {
        //if (currentLevel == 1)
        //{
        //    currentLevel = 2;
        //    bc.ResetLives();
        //    AdjustSpeedForNextLevel();
        //    gameOver = false;
        //}
        //else
        //{
        //    CongratulationsText.SetActive(true);
        //    gameOver = true;
        //}
        if (currentLevel == 1)
        {

            bc.ResetLives();
            AdjustSpeedForNextLevel();
            GameOverText.SetActive(true);
            gameOver = true;
        }

    }
    

    public void BirdScored()
    {
        if (!bc.startBlinking)
        {
            score += 1;
            obstaclesCrossed += 1;
        }

        ScoreText.text = "Score: " + score.ToString();
        FlappyColumnPool.instance.spawnColumn();
    }

    public void RandomAngle()
    {
        ypos = UnityEngine.Random.Range(-3f, 5.5f);
    }

    public void playAgain()
    {
        if (gameOver == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void PlayStart()
    {
        endValSet = false;
        start.SetActive(false);
        Time.timeScale = 1;
    }

    public void continueButton()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    public void ShowGameMenu()
    {
        menuCanvas.SetActive(true);
        Canvas.SetActive(false);
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        gameOver = false;
        Canvas.SetActive(true);
        menuCanvas.SetActive(false);
        Time.timeScale = 1;
        timecoRoutine = SpawnTimer();
        duration = 60;
        StartCoroutine(timecoRoutine);
        StartCoroutine(SpawnColumns());

        StartNewGameSession();

        if (!coroutineStarted)
        {
            StartCoroutine(SpawnColumns());
            coroutineStarted = true;
        }
    }

    private IEnumerator SpawnTimer()
    {
        while (!gameOver)
        {
            duration = duration - 1;
            UpdateDuration();

            if (duration == 0)
            {
                gameOver = true;
                duration = 60;
                Birdalive();
                total_life = total_life - 1;
                if (total_life < 0)
                {
                    auto_speed = auto_speed - 2.0f;
                }

                //if (currentLevel == 1)
                //{
                //    currentLevel = 2;
                //    AdjustSpeedForNextLevel();
                //}
            }

            yield return new WaitForSeconds(1f);
        }
    }

    void EndGame()
    {
        gameOver = true;
        GameOverText.SetActive(true);
        Time.timeScale = 0; // Stop the game
    }

    void UpdateDuration()
    {
        durationText.text = "Duration: " + duration;
    }

    //void AdjustSpeedForNextLevel()
    //{
    //    Debug.Log("Obstacles Crossed: " + obstaclesCrossed);
    //    Debug.Log("Total Obstacles: " + totalObstacles);

    //    if (totalObstacles == 0)
    //    {
    //        Debug.LogError("Total Obstacles is zero, cannot calculate crossedPercentage");
    //        return;
    //    }
    //    float crossedPercentage = (float)obstaclesCrossed / totalObstacles;
    //    Debug.Log("Crossed Percentage: " + crossedPercentage);

    //    Debug.Log("Initial auto_speed: " + auto_speed);
    //    if (crossedPercentage > 0.8f)
    //    {
    //        Debug.Log("Crossed Percentage is greater than 80%. Speed adjustment factor: -2.5");
    //        auto_speed *= -3.8f;
    //    }
    //    else
    //    {
    //        Debug.Log("Crossed Percentage is 80% or less. Speed adjustment factor: -1.0");
    //        auto_speed *= -0.2f;
    //    }

    //    Debug.Log("Adjusted auto_speed in AdjustSpeedForNextLevel: " + auto_speed);
    //    auto_speed = Mathf.Clamp(auto_speed, -0.2f, -3.8f); // Adjust the bounds as needed

    //    Debug.Log("New auto_speed after clamping in AdjustSpeedForNextLevel: " + auto_speed);

    //    LevelText.enabled = true;
    //    LevelText.text = "Level: " + currentLevel;
    //    if (currentLevel == 2)
    //    {
    //        columnSpawnRate = 4f;
    //    }

    //    // Update session details to include the new speed data
    //    SetSessionDetails();
    //}





    void AdjustSpeedForNextLevel()
    {
        Debug.Log("Obstacles Crossed: " + obstaclesCrossed);
        Debug.Log("Total Obstacles: " + totalObstacles);

        if (totalObstacles == 0)
        {
            Debug.LogError("Total Obstacles is zero, cannot calculate crossedPercentage");
            return;
        }
        float crossedPercentage = (float)obstaclesCrossed / totalObstacles;
        Debug.Log("Crossed Percentage: " + crossedPercentage);


        PlayerPrefs.SetFloat("gamepercentage", crossedPercentage);

        // Update session details to include the new speed data

    }
    public void SomeOtherMethodThatChangesSpeed(float newSpeed)
    {
        Debug.Log("SomeOtherMethodThatChangesSpeed called. New speed: " + newSpeed);
        auto_speed = newSpeed;
        Debug.Log("auto_speed after change in SomeOtherMethodThatChangesSpeed: " + auto_speed);
    }

    void RenewLives()
    {
        total_life = 3;
    }

    public void continue_pressed()
    {
        StartGame();
        GameOverText.SetActive(false);
        CongratulationsText.SetActive(false);
        gameOver = false;
    }

    public void quit_pressed()
    {
        int control = BirdControl.hit_count;
        if (control == 3)
        {
            SceneManager.LoadScene("New Scene");
        }
        else
        {
            EndCurrentGameSession();
            SceneManager.LoadScene("New Scene");
        }
    }


    public void ColumnSpawned()
    {
        totalObstacles++; // Increment total obstacles when a column is spawned
        Debug.Log("Total obstacles spawned: " + totalObstacles); // Debug total obstacles
    }
}

