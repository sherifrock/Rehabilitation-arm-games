//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System;

//using UnityEngine.SceneManagement;
//using System.Linq;
//using static Done_PlayerController;
//using NeuroRehabLibrary;

////public enum EnemyState
////{
////    hazardMoving = 0,
////    hazardDestroyed = 1,
////    hazardExited = 2,
////}
//public class Done_GameController : MonoBehaviour
//{
//    public static Done_GameController instance;
//    public AudioClip sounds;
//    private AudioSource source;

//    public GameObject[] hazards;
//    public GameObject Player;
//    public Vector3 spawnValues;
//    public int hazardCount;
//    public float spawnWait;
//    public float startWait;
//    public float waveWait;

//    public Text scoreText;
//    public Text restartText;
//    public Text gameOverText;
//    public Text durationText;
//    public Text LevelText;

//    public GameObject imagelife1;
//    public GameObject imagelife2;
//    public GameObject imagelife3;

//    private bool gameOver;
//    private bool restart;
//    private bool nextlevel;
//    private int score;
//    private int duration = 0;

//    public static string gameend;

//    public float levelspeed;
//    public float start_levelspeed;

//    public GameObject GameOverCanvas;
//    public GameObject CongratsCanvas;

//    string p_hospno;
//    int gameover_count = 0;
//    int overall_life_count = 0;
//    int hit_count = 0;
//    int life_count_completed = 0;
//    int hazardsDestroyed = 0; // Track hazards destroyed

//    string start_time;
//    string end_time;

//    float timeToAppear = 1f;
//    float timeWhenDisappear;

//    IEnumerator timecoRoutine;
//    IEnumerator wavecoRoutine;

//    float player_level;
//    int level_playing = 0;

//    string path_to_data;
//    public static string datetime;
//    // New variables
//    private GameObject currentHazard;
//    private bool canSpawn;
//    private bool hazardDestroyedByBoundary;
//    private State currentstate;
//    // Add a private variable to store the current level
//    private int currentLevel = 1;
//    int HazardsDestroyed = 0; // Track hazards destroyed
//    int hazardsSpawned = 0;   // Track hazards spawned
//    public Text totalScoreText; // Add a new Text object for total score display

//    public static float gameparametervalue;


//    private GameSession currentGameSession;




//    void Start()
//    {

//        path_to_data = Application.dataPath;
//        start_time = DateTime.Now.ToString("HH:mm:ss.fff");
//        datetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");

//        //p_hospno = PatientHistory.p_hospno;
//        //p_hospno = Welcome.p_hospno;

//        source = GetComponent<AudioSource>();
//        levelspeed = 0.5f;
//        GameOverCanvas.SetActive(false);
//        CongratsCanvas.SetActive(false);


//        //var count = 0;
//        List<string> second_row = new List<string>();

//        levelspeed = 0.5f;
//        start_levelspeed = levelspeed;
//        gameparametervalue = levelspeed;

//        player_level = (levelspeed - 0.5f) / 0.25f;
//        level_playing = (int)(player_level + 1);

//        gameOver = false;
//        restart = false;
//        nextlevel = false;
//        restartText.text = "";
//        gameOverText.text = "";
//        totalScoreText.text = ""; // Initialize the total score text
//        duration = 60;
//        timecoRoutine = SpawnTimer();
//        StartCoroutine(timecoRoutine);
//        // UpdateDuration ();
//        //UpdateLevel();
//        score = 0;
//        UpdateScore();
//        wavecoRoutine = SpawnWaves();
//        StartCoroutine(wavecoRoutine);
//        // Reset the counts for gameover and overall life
//        gameover_count = 0;
//        overall_life_count = 0;
//        // Activate all three initial lives
//        imagelife1.SetActive(true);
//        imagelife2.SetActive(true);
//        imagelife3.SetActive(true);
//        // Initialize canSpawn to true
//        canSpawn = true;
//        //hazardDestroyedByBoundary = false;
//        // Initialize LevelText with the starting level value
//        LevelText.text = "Level: " + currentLevel;



//        StartNewGameSession();
//    }
//    void Data(State state)
//    {
//        // Now save the current data
//        string currentStateStr = ((int)state).ToString();
//       // AutoData();

//    }


//    void OnDestroy()
//    {

//       // StartNewGameSession();
//       // AutoData();
//    }


//    void StartNewGameSession()
//    {
//        currentGameSession = new GameSession
//        {
//            GameName = "SPACESHOOTER",
//            Assessment = 0 // Example assessment value, adjust as needed
//        };

//        SessionManager.Instance.StartGameSession(currentGameSession);
//        Debug.Log($"Started new game session with session number: {currentGameSession.SessionNumber}");

//        SetSessionDetails();
//    }


//    private void SetSessionDetails()
//    {
//        string device = "R2"; // Set the device name
//        string assistMode = "Null"; // Set the assist mode
//        string assistModeParameters = "Null"; // Set the assist mode parameters
//        string deviceSetupLocation = "Null"; // Set the device setup location
//        //string gameParameter = "YourGameParameter"; // Set the game parameter
//        // Adjust path as needed

//        string gameParameter = gameparametervalue.ToString();

//        if (currentGameSession != null)
//        {
//            gameParameter = currentGameSession.GameParameter; // Retrieve existing gameParameter
//        }

//        // Append new speed data to gameParameter
//        gameParameter += $"||{gameparametervalue}";


//        SessionManager.Instance.SetDevice(device, currentGameSession);
//        SessionManager.Instance.SetAssistMode(assistMode, assistModeParameters, currentGameSession);
//        SessionManager.Instance.SetDeviceSetupLocation(deviceSetupLocation, currentGameSession);
//        SessionManager.Instance.SetGameParameter(gameParameter, currentGameSession);


//    }

//    void EndCurrentGameSession()
//    {
//        if (currentGameSession != null)
//        {
//            string trialDataFileLocation = spaceclass.relativepath;
//            SessionManager.Instance.SetTrialDataFileLocation(trialDataFileLocation, currentGameSession);

//            SessionManager.Instance.EndGameSession(currentGameSession);
//        }
//    }






//    void Update()
//    {
//        Time.timeScale = levelspeed;

//        //if (nextlevel)
//        //{
//        //    if (level_playing == 2)
//        //    {
//        //        // Display GameOverCanvas instead of CongratsCanvas
//        //        durationText.text = ""; // Clear any previous duration text
//        //        LevelText.text = ""; // Clear any previous level text
//        //        GameOverCanvas.SetActive(true);
//        //        gameOverText.text = "Game Over"; // Assuming you have a Text component named GameOverText
//        //        totalScoreText.text = "Total Score: " + score; // Assuming you have a method to calculate total score

//        //        // Reset nextlevel flag to allow for level transitions
//        //        nextlevel = false;
//        //        // Pause the game to let the player see the game over screen
//        //        Time.timeScale = 0;
//        //    }
//        //    else
//        //    {
//        //        durationText.text = "";
//        //        LevelText.text = "";
//        //        CongratsCanvas.SetActive(true);
//        //        // duration = 60;
//        //        if (life_count_completed > 2)
//        //        {
//        //            levelspeed = levelspeed + 0.25f;
//        //            player_level = (levelspeed - 0.5f) / 0.25f;
//        //            level_playing = (int)(player_level + 1);
//        //        }

//        //        imagelife1.GetComponent<Image>().color = new Color32(76, 192, 28, 200);
//        //        imagelife2.GetComponent<Image>().color = new Color32(76, 192, 28, 200);
//        //        imagelife3.GetComponent<Image>().color = new Color32(76, 192, 28, 200);

//        //        gameover_count = 0;
//        //        // Reset nextlevel flag to allow for level transitions
//        //        nextlevel = false;
//        //        // Pause the game to let the player see the congrats screen
//        //        Time.timeScale = 0;

//        //    }
//        //}
//        HandleTargetExitedBoundary();
//        // Check if the current hazard is destroyed or out of bounds
//        //if (currentHazard == null && !gameOver)
//        //{
//        //    canSpawn = true;

//        //}
//        //if(gameOver)
//        //{


//        //}
//        //else
//        //{

//        //    EndCurrentGameSession();
//        //}


//       // EndCurrentGameSession();

//    }
//    public void HandleTargetExitedBoundary()
//    {
//        // Check if the current hazard is destroyed or out of bounds
//        if (currentHazard == null && !gameOver)
//        {
//            canSpawn = true;
//            Debug.Log("Current hazard is null, transitioning to TargetExitedBoundary state.");
//            GameStateMachine.Instance.TransitionToState(GameState.TargetExitedBoundary);


//        }
//    }

//    public void OnApplicationQuit()
//    {
//        end_time = DateTime.Now.ToString("HH:mm:ss.fff");
//        string duration_played = (DateTime.Parse(end_time) - DateTime.Parse(start_time)).ToString();
//        //string newFileName = @"D:\AREBO\Unity\MARS demo3\Data\" + p_hospno+"\\"+"hits.csv";
//        System.DateTime today = System.DateTime.Today;
//        string data_csv = today.ToString() + "," + start_levelspeed + "," + levelspeed + "," + start_time + "," + end_time + "," + duration_played + "," + hit_count + "\n";
//        //File.AppendAllText(newFileName,data_csv);
//        Application.Quit();

//    }

//    IEnumerator SpawnWaves()
//    {
//        yield return new WaitForSeconds(startWait);

//        while (!gameOver)
//        {
//            // Check if it's level 1 or level 2 and spawn hazards accordingly
//            if ((level_playing == 1 || level_playing == 2) && canSpawn)
//            {
//                canSpawn = false;
//                GameObject hazard = hazards[UnityEngine.Random.Range(0, hazards.Length)];
//                Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
//                Quaternion spawnRotation = Quaternion.identity;
//                currentHazard = Instantiate(hazard, spawnPosition, spawnRotation);
//                hazardsSpawned++; // Increment hazards spawned
//            }

//            //// Stop spawning hazards when level 1 or level 2 ends
//            //if (nextlevel && (level_playing == 2 || level_playing == 3))
//            //{
//            //    // Reset canSpawn to false to stop spawning
//            //    canSpawn = false;
//            //    // Destroy the player
//            //    if (player != null)
//            //    {
//            //        Destroy(player);
//            //    }
//            //    // Display game over text and total score
//            //    EndGame();
//            //}
//            // Check if level 2 has ended
//            //if (level_playing == 2 && duration <= 0)
//            //{
//            //    // Call EndGame to display Game Over text and total score
//            //    EndGame();
//            //    yield break; // Exit the coroutine
//            //}

//            yield return new WaitForSeconds(spawnWait);
//        }
//    }


//    private IEnumerator SpawnTimer()
//    {
//        while (!gameOver)
//        {

//            duration = duration - 1;
//            UpdateDuration();
//            UpdateLevel();

//            if (duration == 0)
//            {
//                life_count_completed = life_count_completed + 1;

//                //if (level_playing == 1)
//                //{
//                //    nextlevel = true; // Set the flag for next level
//                //    source.clip = sounds;
//                //    source.PlayOneShot(source.clip);

//                //    // Pause spawning and other game logic for next level transition
//                //    StopCoroutine(wavecoRoutine);
//                //}
//                //else if (level_playing == 2)
//                //{
//                //    nextlevel = true; // Set the flag for next level
//                //    source.clip = sounds;
//                //    source.PlayOneShot(source.clip);
//                //    // Destroy the player object at the end of level 2
//                //    if (Player != null)
//                //    {
//                //        Destroy(Player);
//                //    }
//                //    // Pause spawning and other game logic for next level transition
//                //    StopCoroutine(wavecoRoutine);
//                //}
//                //else if (level_playing >= 2)
//                //{
//                //    EndGame(); // Call EndGame when second level is completed
//                //}
//                //else
//                //{
//                //    nextlevel = true;
//                //    source.clip = sounds;
//                //    source.PlayOneShot(source.clip);
//                //    AdjustSpeedForNextLevel();

//                //}
//                if (level_playing == 1)
//                {
//                    StartNextlevel();
//                }
//                else if (level_playing == 2)
//                {
//                    EndGame();
//                }




//            }

//            if (duration == 40)
//            {
//                source.clip = sounds;
//                source.PlayOneShot(source.clip);
//            }

//            if (duration == 20)
//            {
//                source.clip = sounds;
//                source.PlayOneShot(source.clip);
//            }

//            yield return new WaitForSeconds(levelspeed);
//            // yield return new WaitForSeconds(1.0f/levelspeed);

//        }

//    }

//    public void AddScore(int newScoreValue)
//    {
//        score += newScoreValue;
//        Debug.Log("Score updated: " + score);
//        UpdateScore();
//    }
//    void UpdateScore()
//    {
//        scoreText.text = "Score: " + score;
//    }

//    void UpdateDuration()
//    {
//        if (!nextlevel)
//        {
//            durationText.text = "Duration: " + duration;
//        }



//    }

//    void UpdateLevel()
//    {
//        LevelText.text = "Level: " + level_playing;
//        // Debug.Log(duration+"..."+Time.time);

//    }

//    public void GameOver()
//    {
//        //duration = 60;
//        hit_count = hit_count + 1;

//        // Decrease remaining lives and update visuals
//        gameover_count = gameover_count + 1;

//        if (gameover_count == 1)
//        {
//            imagelife1.SetActive(false);
//        }
//        if (gameover_count == 2)
//        {
//            imagelife2.SetActive(false);
//        }
//        if (gameover_count == 3)
//        {
//            imagelife3.SetActive(false);

//        }
//        if (gameover_count > 2)
//        {

//            gameOver = true;
//            // Destroy the player object
//            if (Player != null)
//            {
//                Destroy(Player);
//            }
//            EndCurrentGameSession();

//            StopCoroutine(timecoRoutine);
//            StopCoroutine(wavecoRoutine);
//            GameOverCanvas.SetActive(true);
//            gameOverText.text = "Game Over";
//            totalScoreText.text = "Total Score: " + score;

//           // StartNewGameSession();
//            //AutoData();
//            Time.timeScale = 0;

//            // Reset gameover count
//            gameover_count = 0;

//            // Increment overall life count
//            overall_life_count = overall_life_count + 1;

//            if (overall_life_count > 2)
//            {
//                levelspeed = levelspeed - 0.25f;
//                overall_life_count = 0;
//                // Debug.Log(levelspeed+" : levelspeed");
//                player_level = (levelspeed - 0.5f) / 0.25f;
//                level_playing = (int)(player_level + 1);
//            }
//            //// Transition to TargetAndPlayerCollided state
//            GameStateMachine.Instance.TransitionToState(GameState.TargetAndPlayerCollided);
//        }




//    }
//    void OnCollisionEnter(Collision collision)
//    {
//        Debug.Log("Collision with: " + collision.gameObject.tag);
//        if (collision.gameObject.tag == "PlayerFire")
//        {
//            Debug.Log("Collided with PlayerFire, ignoring collision.");
//            return;
//        }
//        GameOver();
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        Debug.Log("Trigger with: " + other.gameObject.tag);
//        if (other.gameObject.tag == "PlayerFire")
//        {
//            Debug.Log("Triggered with PlayerFire, ignoring trigger.");
//            return;
//        }
//        GameOver();
//    }



//    public void onclick_exit()
//    {

//        SceneManager.LoadScene("homepage");

//    }

//    public void onclick_replaygame()
//    {
//        end_time = DateTime.Now.ToString("HH:mm:ss tt");
//        string duration_played = (DateTime.Parse(end_time) - DateTime.Parse(start_time)).ToString();
//        string newFileName = @"C:\Users\shanu\Newstart\Assets\Patient_Data" + "\\" + "hits.csv";
//        System.DateTime today = System.DateTime.Today;
//        // Define the header
//        string header = "Date,Start_Speed,End_Speed,Start_Time,End_Time,Duration,Hit_Count\n";

//        // Check if the file exists, if not, create and write the header
//        if (!File.Exists(newFileName))
//        {
//            File.WriteAllText(newFileName, header);
//        }

//        string data_csv = today.ToString() + "," + start_levelspeed + "," + levelspeed + "," + start_time + "," + end_time + "," + duration_played + "," + hit_count + "\n";
//        File.AppendAllText(newFileName, data_csv);

//        //GameOverCanvas.SetActive(false);

//        start_time = DateTime.Now.ToString("HH:mm:ss tt");

//        //imagelife1.GetComponent<Image>().color = new Color32(76, 192, 28, 200);
//        //imagelife2.GetComponent<Image>().color = new Color32(76, 192, 28, 200);
//        //imagelife3.GetComponent<Image>().color = new Color32(76, 192, 28, 200);
//        imagelife1.SetActive(true);
//        imagelife2.SetActive(true);
//        imagelife3.SetActive(true);
//        StartCoroutine(timecoRoutine);
//        StartCoroutine(wavecoRoutine);
//    }
//    public void StartNextlevel()
//    {
//        currentLevel++;

//        // Update the LevelText to the new level value
//        LevelText.text = "Level: " + currentLevel;

//        //nextlevel = false;
//        //CongratsCanvas.SetActive(false);
//        duration = 60;
//        UpdateDuration();
//        // Reset the lives and gameover count
//        gameover_count = 0;
//        imagelife1.SetActive(true);
//        imagelife2.SetActive(true);
//        imagelife3.SetActive(true);
//        AdjustSpeedForNextLevel();
//        //level_playing++; // Increment the level
//        //UpdateLevel(); // Update the UI text to reflect the new level
//        // Resume the game
//        Time.timeScale = 1;
//        // Restart the wave coroutine
//        wavecoRoutine = SpawnWaves();
//        StartCoroutine(wavecoRoutine);
//    }

//    public void onclick_nextlevel()
//    {
//        currentLevel++;

//        // Update the LevelText to the new level value
//        LevelText.text = "Level: " + currentLevel;

//        nextlevel = false;
//        CongratsCanvas.SetActive(false);
//        duration = 60;
//        // Reset the lives and gameover count
//        gameover_count = 0;
//        imagelife1.SetActive(true);
//        imagelife2.SetActive(true);
//        imagelife3.SetActive(true);
//        AdjustSpeedForNextLevel();
//        //level_playing++; // Increment the level
//        //UpdateLevel(); // Update the UI text to reflect the new level
//        // Resume the game
//        Time.timeScale = 1;
//        // Restart the wave coroutine
//        wavecoRoutine = SpawnWaves();
//        StartCoroutine(wavecoRoutine);
//    }

//    public void onclick_previouslevel()
//    {
//        // Decrease the level
//        currentLevel--;

//        // Update the LevelText to the new level value
//        LevelText.text = "Level: " + currentLevel;
//        nextlevel = false;
//        CongratsCanvas.SetActive(false);
//        duration = 60;
//        // levelspeed = levelspeed-0.3f;
//        levelspeed = 0.25f;
//        player_level = (levelspeed - 0.5f) / 0.25f;
//        level_playing = (int)(player_level + 1);
//        //nextlevel = false;
//        //CongratsCanvas.SetActive(false);
//        //duration = 60;
//    }

//    public void onclick_replay()
//    {
//        // Reset the level to 1
//        currentLevel = 1;

//        // Update the LevelText to the new level value
//        LevelText.text = "Level: " + currentLevel;

//        nextlevel = false;
//        CongratsCanvas.SetActive(false);
//        duration = 60;
//    }

//    public void onclick_game()
//    {
//        SceneManager.LoadScene("SpaceShooterDemo");

//    }

//    public void doquit()
//    {
//        // Debug.Log("Quit");
//        //SceneManager.LoadScene("Feedback");
//        EndCurrentGameSession();
//        SceneManager.LoadScene("New Scene");

//        Debug.Log("Quit");
//       // OnApplicationQuit();
//        // Application.Quit();
//    }
//    public void SetHazardDestroyedByBoundary(bool destroyedByBoundary)
//    {
//        hazardDestroyedByBoundary = destroyedByBoundary;
//        Debug.Log("SetHazardDestroyedByBoundary called. Value: " + destroyedByBoundary);
//    }
//    public void ResetHazardDestroyedByBoundary()
//    {
//        hazardDestroyedByBoundary = false;
//        Debug.Log("ResetHazardDestroyedByBoundary called.");
//    }
//    // New method to be called when a hazard is destroyed
//    public void HazardDestroyed()
//    {
//        hazardsDestroyed++;

//       // AdjustSpeed();
//        //hazardsDestroyed = 0; // Reset the counter

//    }


//    private void AdjustSpeedForNextLevel()
//    {

//        float destroyedPercentage = (float)hazardsDestroyed / hazardsSpawned;
//        // Add debug statement to print destroyedPercentage
//        Debug.Log("Destroyed Percentage: " + destroyedPercentage);
//        if (destroyedPercentage > 0.8f)
//        {
//            levelspeed = 0.7f;
//        }
//        else
//        {
//            levelspeed = 0.3f;
//        }
//        // Increment level_playing for next level
//       // level_playing++;

//        player_level = (levelspeed - 0.5f) / 0.25f;
//        //level_playing = (int)(player_level + 1);
//        //level_playing = Mathf.FloorToInt(player_level) + 1;
//        level_playing = Mathf.Max(Mathf.FloorToInt(player_level) + 1, 2);
//        hazardsDestroyed = 0; // Reset for next level
//        hazardsSpawned = 0;

//        // Reset for next level
//        gameparametervalue = levelspeed;
//        SetSessionDetails();

//    }
//    public void EndGame()
//    {
//        gameOver = true;
//        //restartText.text = "Press 'R' to Restart";
//        gameOverText.text = "Game Over!";
//        GameOverCanvas.SetActive(true);

//        // Destroy the player if it exists
//        if (Player != null)
//        {
//            Destroy(Player);
//        }

//        // Stop all coroutines to prevent further spawning
//        StopAllCoroutines();

//        // Display the total score on the Game Over screen
//        totalScoreText.text = "Total Score: " + score;
//        Time.timeScale = 0; // Pause the game


//        //AutoData();


//    }


//    public void AutoData()
//    {

//        string GameData_Bird = Application.dataPath;
//        // Directory.CreateDirectory(GameData_Bird + "\\" + "Patient_Data" + "\\" + Welcome.p_hospno);
//        string filepath_Bird = GameData_Bird + "\\" + "Patient_Data" + "\\" + Welcome.p_hospno + "\\" + "gamedata.csv";



//        // string filepath_Bird =  gameclass.gamePath;
//        if (IsCSVEmpty(filepath_Bird))
//        {

//        }
//        else
//        {

//        }

//    }


//    private bool IsCSVEmpty(string filepath_Bird)
//    {


//        int session = GameDataManager.instance.GetSessionNumber();

//        string currentTime = datetime;
//        string device = "R2";
//        string assessment = "0";
//        string starttime = start_time;

//        string endtime =gameend;



//        //string endtime = end_time;
//        string gamename = "SPACESHOOTER";
//        string datalocation = spaceclass.relativepath;
//        string devicesetup = "null";
//        string assistmode = "null";
//        string assistmodeparameter = " null";
//        string gameparameter = "null";





//        if (File.Exists(filepath_Bird))
//        {
//            string Position_Bird = "";
//            //check the file is empty,write header
//            if (new FileInfo(filepath_Bird).Length == 0)
//            {
//                string Endata_Bird = "sessionnumber,datetime,device,assessment,starttime,Stoptime,gamename,traildatafilelocation,devicesetupfile,assistmode,assistmodeparameter,gameparameter\n";
//                File.WriteAllText(filepath_Bird, Endata_Bird);
//                DateTime currentDateTime = DateTime.Now;
//                //string Position_Space = currentDateTime + "," + AppData.plutoData.enc1 + "," + AppData.plutoData.enc2 + AppData.plutoData.enc3 + "," + AppData.plutoData.enc4 + "," + AppData.plutoData.torque1 + "," + AppData.plutoData.torque3 + '\n';

//                Position_Bird = session + "," + currentTime + "," + device + "," + assessment + "," + starttime + "," + endtime + "," + gamename + "," + datalocation + "," + devicesetup + "," + assistmode + "," + assistmodeparameter + "," + gameparameter + '\n';
//                return true;
//            }

//            else
//            {

//                //If the file is not empty,return false
//                DateTime currentDateTime = DateTime.Now;
//                //string Position_SpaceR = currentDateTime + "," + AppData.plutoData.enc1 + "," + AppData.plutoData.enc2 + AppData.plutoData.enc3 + "," + AppData.plutoData.enc4 + "," + AppData.plutoData.torque1 + "," + AppData.plutoData.torque3 + '\n';

//                Position_Bird = session + "," + currentTime + "," + device + "," + assessment + "," + starttime + "," + endtime + "," + gamename + "," + datalocation + "," + devicesetup + "," + assistmode + "," + assistmodeparameter + "," + gameparameter + '\n';

//                File.AppendAllText(filepath_Bird, Position_Bird);
//                return true;
//            }
//        }
//        else
//        {
//            string PositionBird = "";
//            //If the file doesnt exist
//            string DataPath_Bird = Application.dataPath;
//            Directory.CreateDirectory(DataPath_Bird + "\\" + "Patient_Data" + "\\" + Welcome.p_hospno);
//            string filepath_Endata1_Bird = DataPath_Bird + "\\" + "Patient_Data" + "\\" + Welcome.p_hospno + "\\" + "gamedata.csv";
//            string Endata1_Bird = "sessionnumber,datetime,device,assessment,starttime,Stoptime,gamename,traildatafilelocation,devicesetupfile,assistmode,assistmodeparameter,gameparameter\n";
//            File.WriteAllText(filepath_Endata1_Bird, Endata1_Bird);
//            DateTime currentDateTime = DateTime.Now;
//            //string Position = currentDateTime + "," + AppData.plutoData.enc1 + "," + AppData.plutoData.enc2 + AppData.plutoData.enc3 + "," + AppData.plutoData.enc4 + "," + AppData.plutoData.torque1 + "," + AppData.plutoData.torque3 + '\n';
//            PositionBird = session + "," + currentTime + "," + device + "," + assessment + "," + starttime + "," + endtime + "," + gamename + "," + datalocation + "," + devicesetup + "," + assistmode + "," + assistmodeparameter + "," + gameparameter + '\n';

//            File.AppendAllText(filepath_Endata1_Bird, PositionBird);
//            return true;
//        }
//    }




//}











using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

using UnityEngine.SceneManagement;
using System.Linq;
using static Done_PlayerController;
using NeuroRehabLibrary;


public class Done_GameController : MonoBehaviour
{
    public static Done_GameController instance;
    public AudioClip sounds;
    private AudioSource source;

    public GameObject[] hazards;
    public GameObject Player;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text durationText;
    public Text LevelText;

    public GameObject imagelife1;
    public GameObject imagelife2;
    public GameObject imagelife3;

    private bool gameOver;
    private bool restart;
    private bool nextlevel;
    private int score;
    private int duration = 0;

    public static string gameend;

    public float levelspeed;
    public float start_levelspeed;

    public GameObject GameOverCanvas;
    public GameObject CongratsCanvas;

    string p_hospno;
    int gameover_count = 0;
    int overall_life_count = 0;
    int hit_count = 0;
    int life_count_completed = 0;
    int hazardsDestroyed = 0;

    string start_time;
    string end_time;

    float timeToAppear = 1f;
    float timeWhenDisappear;

    IEnumerator timecoRoutine;
    IEnumerator wavecoRoutine;

    float player_level;
    int level_playing = 1;

    string path_to_data;
    public static string datetime;
    // New variables
    private GameObject currentHazard;
    private bool canSpawn;
    private bool hazardDestroyedByBoundary;
    private State currentstate;
    // Add a private variable to store the current level
    private int currentLevel = 1;
    int HazardsDestroyed = 0; // Track hazards destroyed
    int hazardsSpawned = 0;   // Track hazards spawned
    public Text totalScoreText; // Add a new Text object for total score display

    public static float spacegameparametervalue;


    private GameSession currentGameSession;




    void Start()
    {

        path_to_data = Application.dataPath;
        start_time = DateTime.Now.ToString("HH:mm:ss.fff");
        datetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");

        //p_hospno = PatientHistory.p_hospno;
        //p_hospno = Welcome.p_hospno;

        source = GetComponent<AudioSource>();
       // levelspeed = 0.5f;
        GameOverCanvas.SetActive(false);
        CongratsCanvas.SetActive(false);


        //var count = 0;
        List<string> second_row = new List<string>();

        //levelspeed = 0.5f;
        //start_levelspeed = levelspeed;
        

        //player_level = (levelspeed - 0.5f) / 0.25f;
        //level_playing = (int)(player_level + 1);

        gameOver = false;
        restart = false;
        nextlevel = false;
        restartText.text = "";
        gameOverText.text = "";
        totalScoreText.text = ""; // Initialize the total score text
        duration = 60;
        timecoRoutine = SpawnTimer();
        StartCoroutine(timecoRoutine);
        // UpdateDuration ();
        //UpdateLevel();
        score = 0;
        UpdateScore();
        wavecoRoutine = SpawnWaves();
        StartCoroutine(wavecoRoutine);
        // Reset the counts for gameover and overall life
        gameover_count = 0;
        overall_life_count = 0;
        // Activate all three initial lives
        imagelife1.SetActive(true);
        imagelife2.SetActive(true);
        imagelife3.SetActive(true);
        // Initialize canSpawn to true
        canSpawn = true;
        //hazardDestroyedByBoundary = false;
        // Initialize LevelText with the starting level value
        LevelText.text = "Level: " + currentLevel;



        StartNewGameSession();
        RetrieveAndAdjustGameParameterFromCSV();
    }
    void Data(State state)
    {
        // Now save the current data
        string currentStateStr = ((int)state).ToString();
        // AutoData();

    }


    void OnDestroy()
    {

        // StartNewGameSession();
        // AutoData();
    }


    void StartNewGameSession()
    {
        currentGameSession = new GameSession
        {
            GameName = "SPACESHOOTER",
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
        string deviceSetupLocation = "Null"; // Set the device setup location
        //string gameParameter = "YourGameParameter"; // Set the game parameter
        // Adjust path as needed

        string gameParameter = spacegameparametervalue.ToString();

        //if (currentGameSession != null)
        //{
        //    gameParameter = currentGameSession.GameParameter; // Retrieve existing gameParameter
        //}

        //// Append new speed data to gameParameter
        //gameParameter += $"{spacegameparametervalue}";

        SessionManager.Instance.SetGameParameter(gameParameter, currentGameSession);


        SessionManager.Instance.SetDevice(device, currentGameSession);
        SessionManager.Instance.SetAssistMode(assistMode, assistModeParameters, currentGameSession);
        SessionManager.Instance.SetDeviceSetupLocation(deviceSetupLocation, currentGameSession);
        


    }


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
        float spacegameplayPercentage = PlayerPrefs.GetFloat("spacegamepercentage"); ; // Assume this is tracked somewhere

        for (int i = csvLines.Length - 1; i > 0; i--)
        {
            string[] row = csvLines[i].Split(',');

            if (row[gameNameIndex] == currentGameName)
            {
                lastGameParameterValue = row[gameParameterIndex];
                break;
            }
        }

        if (lastGameParameterValue != null)
        {
          float  gameparametervalue = float.Parse(lastGameParameterValue);
            if (spacegameplayPercentage >= 0.8f)
            {
                gameparametervalue = gameparametervalue + 0.1f; // Increase speed by 0.1
               

            }
            else //if (gameplayPercentage < 80)
            {
                gameparametervalue = gameparametervalue - 0.1f; // Decrease speed by 0.1
                

            }

           
            
            levelspeed = gameparametervalue; // Assign the adjusted value to levelspeed
            //player_level = (levelspeed - 0.5f) / 0.25f;
            player_level = (levelspeed - 0.5f) / 0.25f;
            //level_playing = (int)(player_level + 1);
            spacegameparametervalue = levelspeed;
            Debug.Log("Game parameter retrieved and adjusted from CSV: " + gameparametervalue);

            SetSessionDetails();
        }
        else
        {
            levelspeed = 0.5f; // Default speed for the first time
            //player_level = (levelspeed - 0.5f) / 0.25f;

            player_level = (levelspeed - 0.5f) / 0.25f;
            level_playing = (int)( 1);
            Debug.Log("No matching session and game name found in the CSV file. Using default game parameter.");
            spacegameparametervalue = levelspeed;
            SetSessionDetails();
        }

        // Save the new game parameter to the CSV file



    }


    void EndCurrentGameSession()
    {
        if (currentGameSession != null)
        {
            string trialDataFileLocation = spaceclass.relativepath;
            SessionManager.Instance.SetTrialDataFileLocation(trialDataFileLocation, currentGameSession);

            SessionManager.Instance.EndGameSession(currentGameSession);
        }
    }






    void Update()
    {
        Time.timeScale = levelspeed;

        HandleTargetExitedBoundary();


    }
    public void HandleTargetExitedBoundary()
    {
        // Check if the current hazard is destroyed or out of bounds
        if (currentHazard == null && !gameOver)
        {
            canSpawn = true;
            Debug.Log("Current hazard is null, transitioning to TargetExitedBoundary state.");
            GameStateMachine.Instance.TransitionToState(GameState.TargetExitedBoundary);


        }
    }

    public void OnApplicationQuit()
    {
        end_time = DateTime.Now.ToString("HH:mm:ss.fff");
        string duration_played = (DateTime.Parse(end_time) - DateTime.Parse(start_time)).ToString();
        //string newFileName = @"D:\AREBO\Unity\MARS demo3\Data\" + p_hospno+"\\"+"hits.csv";
        System.DateTime today = System.DateTime.Today;
        string data_csv = today.ToString() + "," + start_levelspeed + "," + levelspeed + "," + start_time + "," + end_time + "," + duration_played + "," + hit_count + "\n";
        //File.AppendAllText(newFileName,data_csv);
        Application.Quit();

    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (!gameOver)
        {
            // Check if it's level 1 or level 2 and spawn hazards accordingly
            if ((level_playing == 1 || level_playing == 2) && canSpawn)
            {
                canSpawn = false;
                GameObject hazard = hazards[UnityEngine.Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                currentHazard = Instantiate(hazard, spawnPosition, spawnRotation);
                hazardsSpawned++; // Increment hazards spawned
            }



            yield return new WaitForSeconds(spawnWait);
        }
    }


    private IEnumerator SpawnTimer()
    {
        while (!gameOver)
        {

            duration = duration - 1;
            UpdateDuration();
            //UpdateLevel();

            if (duration == 0)
            {
                life_count_completed = life_count_completed + 1;


                if (level_playing == 1)
                {
                    // StartNextlevel();
                    AdjustSpeedForNextLevel();
                    EndGame();
                }
                //else if (level_playing == 2)
                //{
                //    EndGame();
                //}




            }

            if (duration == 40)
            {
                source.clip = sounds;
                source.PlayOneShot(source.clip);
            }

            if (duration == 20)
            {
                source.clip = sounds;
                source.PlayOneShot(source.clip);
            }

            yield return new WaitForSeconds(levelspeed);
            // yield return new WaitForSeconds(1.0f/levelspeed);

        }

    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        Debug.Log("Score updated: " + score);
        UpdateScore();
    }
    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateDuration()
    {
        if (!nextlevel)
        {
            durationText.text = "Duration: " + duration;
        }



    }

    void UpdateLevel()
    {
        LevelText.text = "Level: " + level_playing;
        // Debug.Log(duration+"..."+Time.time);

    }

    public void GameOver()
    {
        //duration = 60;
        hit_count = hit_count + 1;

        // Decrease remaining lives and update visuals
        gameover_count = gameover_count + 1;

        if (gameover_count == 1)
        {
            imagelife1.SetActive(false);
        }
        if (gameover_count == 2)
        {
            imagelife2.SetActive(false);
        }
        if (gameover_count == 3)
        {
            imagelife3.SetActive(false);

        }
        if (gameover_count > 2)
        {

            gameOver = true;
            // Destroy the player object
            if (Player != null)
            {
                Destroy(Player);
            }
            EndCurrentGameSession();
            AdjustSpeedForNextLevel();
            StopCoroutine(timecoRoutine);
            StopCoroutine(wavecoRoutine);
            GameOverCanvas.SetActive(true);
            gameOverText.text = "Game Over";
            totalScoreText.text = "Total Score: " + score;

            // StartNewGameSession();
            //AutoData();
            Time.timeScale = 0;

            // Reset gameover count
            gameover_count = 0;

            // Increment overall life count
            overall_life_count = overall_life_count + 1;

            if (overall_life_count > 2)
            {
                levelspeed = levelspeed - 0.25f;
                overall_life_count = 0;
                // Debug.Log(levelspeed+" : levelspeed");
                player_level = (levelspeed - 0.5f) / 0.25f;
                level_playing = (int)(player_level + 1);
            }
            //// Transition to TargetAndPlayerCollided state
            GameStateMachine.Instance.TransitionToState(GameState.TargetAndPlayerCollided);
        }




    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.tag);
        if (collision.gameObject.tag == "PlayerFire")
        {
            Debug.Log("Collided with PlayerFire, ignoring collision.");
            return;
        }
        GameOver();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger with: " + other.gameObject.tag);
        if (other.gameObject.tag == "PlayerFire")
        {
            Debug.Log("Triggered with PlayerFire, ignoring trigger.");
            return;
        }
        GameOver();
    }



    public void onclick_exit()
    {

        SceneManager.LoadScene("homepage");

    }


    public void StartNextlevel()
    {
        currentLevel++;

        // Update the LevelText to the new level value
        LevelText.text = "Level: " + currentLevel;

        //nextlevel = false;
        //CongratsCanvas.SetActive(false);
        duration = 60;
        //UpdateDuration();
        // Reset the lives and gameover count
        gameover_count = 0;
        imagelife1.SetActive(true);
        imagelife2.SetActive(true);
        imagelife3.SetActive(true);
        AdjustSpeedForNextLevel();

        Time.timeScale = 1;
        // Restart the wave coroutine
        wavecoRoutine = SpawnWaves();
        StartCoroutine(wavecoRoutine);
    }

    public void onclick_nextlevel()
    {
        currentLevel++;


        LevelText.text = "Level: " + currentLevel;

        nextlevel = false;
        CongratsCanvas.SetActive(false);
        duration = 60;
        // Reset the lives and gameover count
        gameover_count = 0;
        imagelife1.SetActive(true);
        imagelife2.SetActive(true);
        imagelife3.SetActive(true);
        AdjustSpeedForNextLevel();

        Time.timeScale = 1;
        // Restart the wave coroutine
        wavecoRoutine = SpawnWaves();
        StartCoroutine(wavecoRoutine);
    }

    public void onclick_previouslevel()
    {
        // Decrease the level
        currentLevel--;

        // Update the LevelText to the new level value
        LevelText.text = "Level: " + currentLevel;
        nextlevel = false;
        CongratsCanvas.SetActive(false);
        duration = 60;
        // levelspeed = levelspeed-0.3f;
        levelspeed = 0.25f;
        player_level = (levelspeed - 0.5f) / 0.25f;
        level_playing = (int)(player_level + 1);

    }

    public void onclick_replay()
    {
        // Reset the level to 1
        currentLevel = 1;

        // Update the LevelText to the new level value
        LevelText.text = "Level: " + currentLevel;

        nextlevel = false;
        CongratsCanvas.SetActive(false);
        duration = 60;
    }

    public void onclick_game()
    {
        SceneManager.LoadScene("SpaceShooterDemo");

    }

    public void doquit()
    {
        if (Time.timeScale == 0)
        {

        }
        else
        {
            EndCurrentGameSession();
        }
        
        SceneManager.LoadScene("New Scene");

        Debug.Log("Quit");

    }
    public void SetHazardDestroyedByBoundary(bool destroyedByBoundary)
    {
        hazardDestroyedByBoundary = destroyedByBoundary;
        Debug.Log("SetHazardDestroyedByBoundary called. Value: " + destroyedByBoundary);
    }
    public void ResetHazardDestroyedByBoundary()
    {
        hazardDestroyedByBoundary = false;
        Debug.Log("ResetHazardDestroyedByBoundary called.");
    }
    // New method to be called when a hazard is destroyed
    public void HazardDestroyed()
    {
        hazardsDestroyed++;



    }


    private void AdjustSpeedForNextLevel()
    {

        float destroyedPercentage = (float)hazardsDestroyed / hazardsSpawned;

        PlayerPrefs.SetFloat("spacegamepercentage", destroyedPercentage);
       Debug.Log("destroyed percentage"+  destroyedPercentage);

    }
    public void EndGame()
    {
        gameOver = true;
        //restartText.text = "Press 'R' to Restart";
        gameOverText.text = "Game Over!";
        GameOverCanvas.SetActive(true);
        AdjustSpeedForNextLevel();

        // Destroy the player if it exists
        if (Player != null)
        {
            Destroy(Player);
        }

        // Stop all coroutines to prevent further spawning
        StopAllCoroutines();

        // Display the total score on the Game Over screen
        totalScoreText.text = "Total Score: " + score;
        Time.timeScale = 0; // Pause the game


        //AutoData();


    }







}

























