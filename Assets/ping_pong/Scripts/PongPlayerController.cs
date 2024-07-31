


















//using UnityEngine;
//using System;
//using UnityEngine.UI; // Add this line to use UI elements
//using System.IO;
//using UnityEditor.Rendering;
////using static UnityEditor.PlayerSettings;
//using UnityEngine.Rendering;

//public static class pongclass
//{
//    public static string filepath;
//    public static string relativepath;
//}

//public class PongPlayerController : MonoBehaviour
//{
//    public static PongPlayerController instance;
//    public static float topBound = 3.6f;
//    public static float bottomBound = -3.6f;
//    public static float theta1;
//    public static float theta2;
//    public static float playSize;
//    public static float player_x, player_y;

//    private string welcompath = Staticvlass.FolderPath; // Replace with your folder path
//    private string filePath;
//    private bool isPaused = false;
//    private bool gameWon = false; // Variable to track if the game is won
//    private int previousPlayerScore = 0; // Variable to track the player's previous score
//    public static JediSerialCom serReader;
//    private DateTime startTime; // Start time of the game
//    //public Text durationText; // Reference to the UI Text element
//    //private int duration = 0;
//    public Text LevelText;
//    public int currentLevel = 1;
//    //private float elapsedTime = 0f; // Track elapsed time for duration updates
//   // public Button nextLevelButton; // Button to transition to the next level
//    public bool levelComplete = false; // Flag to indicate level completion
//    public GameObject GameOverText;
//    public Transform playerPaddle; // Reference to the player's paddle
//    public bool transitioningToNextLevel = false; // Flag to indicate transition to next level
//    public static string relativepath;
//    public static string gameparameterpong;
//    public static float paddleparameter = 1;
//    public static float updatepaddleparameter;
//    public static float ppaddleparameter;

//    void Start()
//    {
//        //JediDataFormat.ReadSetJediDataFormat(AppData.jdfFilename);
//        //serReader = new JediSerialCom("COM12");
//        //serReader.ConnectToArduino();
//        string pongfile = welcompath + "\\" + "Pong_Data";
//        if (Directory.Exists(pongfile))
//        {
//            filePath = Path.Combine(pongfile, "Pong_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
//        }
//        else
//        {
//            Directory.CreateDirectory(pongfile);
//            filePath = Path.Combine(pongfile, "Pong_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
//        }
//        pongclass.filepath = filePath;

//        string fullFilePath = pongclass.filepath;

//        // Define the part of the path you want to store
//        string partOfPath = @"Application.dataPath";

//        // Use Path class to get the relative path
//        string relativePath = Path.GetRelativePath(partOfPath, fullFilePath);
//        pongclass.relativepath = relativePath;
//        relativepath = relativePath;
//        WriteHeader();
//        //startTime = DateTime.Now; // Initialize the start time
//       // elapsedTime = 0f; // Reset elapsed time
//        //nextLevelButton.onClick.AddListener(NextLevel); // Add listener to the next level button
//        //nextLevelButton.gameObject.SetActive(false); // Hide the button initially
//        GameOverText.SetActive(false); // Hide Game Over text initially
//                                       // Set default values for PlayerPrefs for level 1


//        //UIManager.Instance.SetSessionDetails();

//    }

//    void Update()
//    {
//        playSize = topBound - bottomBound;
//        if (!isPaused && !gameWon && !transitioningToNextLevel)
//        {
//            LogData();
//            Game();
//            CheckLevelCompletion(); // Check for level completion
//            //UpdateDurationText();
//        }



//        LevelText.text = "Level: " + currentLevel;


//    }
//    void CheckLevelCompletion()
//    {
//        //if (scoreclass.playerpoint >= 3 || scoreclass.enemypoint >= 3)
//        //{
//        //    transitioningToNextLevel = true; // Set flag to indicate transition to next level
//        //    NextLevel(); // Automatically transition to the next level
//        //}
//        int scoreThreshold = GetScoreThresholdForLevel(currentLevel); // Get score threshold for the current level

//        // Check if the score threshold for the current level has been reached
//        if ((scoreclass.playerpoint >= scoreThreshold || scoreclass.enemypoint >= scoreThreshold) && !transitioningToNextLevel)
//        {
//            transitioningToNextLevel = true; // Set flag to indicate transition to next level
//            NextLevel(); // Automatically transition to the next level
//        }
//    }

//    int GetScoreThresholdForLevel(int level)
//    {
//        // Define score thresholds for each level
//        if (level == 1) return 3;
//        if (level == 2) return 6;
//        // Add more levels and their corresponding score thresholds as needed
//        return 6; // Default threshold for levels beyond level 2
//    }
//    void Game()
//    {
//            Debug.Log("Game Method Entry: currentLevel = " + currentLevel);

//            float y_max = PlayerPrefs.GetFloat("y max");
//            float y_min = PlayerPrefs.GetFloat("y min");
//            Debug.Log($"Retrieved from PlayerPrefs - y_max: {y_max}, y_min: {y_min}");

//            float l1 = 333;
//            float l2 = 381;
//            float theta1 = Dof.theta1;
//            float theta2 = Dof.theta2;
//            float thetaa = theta1 * Mathf.Deg2Rad;
//            float thetab = theta2 * Mathf.Deg2Rad;

//            float y1 = -(Mathf.Cos(thetaa) * l1 + Mathf.Cos(thetaa + thetab) * l2);
//            float x1 = -(Mathf.Sin(thetaa) * l1 + Mathf.Sin(thetaa + thetab) * l2);

//            float x_val = (x1 / (333 + 381) * 12f);
//            float y_val = ((y1 + 350) / (400 * 2)) * 4.8f;

//            // Adding debug logs
//            Debug.Log($"Game Method Called: currentLevel = {currentLevel}");
//            Debug.Log($"theta1: {theta1}, theta2: {theta2}, thetaa: {thetaa}, thetab: {thetab}");
//            Debug.Log($"y1: {y1}, x1: {x1}");
//            Debug.Log($"x_val: {x_val}, y_val: {y_val}");
//            Debug.Log($"y_max: {y_max}, y_min: {y_min}");

//            // Adding debug logs
//            //Debug.Log($"Game Method Called: currentLevel = {currentLevel}");
//            transform.position = new Vector2(transform.position.x, Angle2ScreenZ(y_val, y_min, y_max));

//            if (transform.position.y > topBound)
//            {
//                transform.position = new Vector3(transform.position.x, topBound, 0);
//            }
//            else if (transform.position.y < bottomBound)
//            {
//                transform.position = new Vector3(transform.position.x, bottomBound, 0);
//            }

//            player_x = transform.position.x;
//            player_y = transform.position.y;




//    }

//    public static float Angle2ScreenZ(float angleZ, float y_min, float y_max)
//    {
//        return Mathf.Clamp(bottomBound + (angleZ - y_min) * (playSize / (y_max - y_min)), -3.6f * playSize, 3.6f * playSize);
//    }

//    void WriteHeader()
//    {
//        if (!File.Exists(filePath))
//        {
//            string header = "Time,PlayerX,PlayerY,EnemyX,EnemyY,BallX,BallY,PlayerScore,EnemyScore\n";
//            File.WriteAllText(pongclass.filepath, header);
//        }
//    }

//    void LogData()
//    {
//        //if (gameWon || levelComplete) return; // Stop logging data if the game is won

//        GameObject ball = GameObject.FindGameObjectWithTag("Target");
//        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

//        float ball_x = ball.transform.position.x;
//        float ball_y = ball.transform.position.y;
//        float enemy_x = enemy.transform.position.x;
//        float enemy_y = enemy.transform.position.y;

//        string currentTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
//        string data = $"{currentTime},{player_x},{player_y},{enemy_x},{enemy_y},{ball_x},{ball_y},{scoreclass.playerpoint},{scoreclass.enemypoint}\n";

//        File.AppendAllText(pongclass.filepath, data);


//    }


//    public void PauseGame()
//    {
//        isPaused = !isPaused;
//        Time.timeScale = isPaused ? 0 : 1;
//    }
//    public void ConnectToComPort(string port)
//    {
//        if (serReader != null)
//        {
//            serReader.DisconnectArduino();
//        }
//        serReader = new JediSerialCom(port);
//        serReader.ConnectToArduino();
//    }
//    public void NextLevel()
//    {

//        AdjustPaddleSizeForNextLevel();
//        currentLevel++; // Increment the level
//        // Update the LevelText to the new level value
//        LevelText.text = "Level: " + currentLevel;
//        //levelComplete = false; // Reset the levelComplete flag
//        gameWon = false; // Reset the gameWon flag

//        //AdjustSpeedForNextLevel(); // Adjust ball speed before starting the next level

//        Debug.Log("NextLevel Method Called: currentLevel = " + currentLevel);
//        //Reset the ball's movement
//        GameObject ball = GameObject.FindGameObjectWithTag("Target");
//        if (ball != null)
//        {
//            BallController ballController = ball.GetComponent<BallController>();
//            if (ballController != null)
//            {
//                ballController.ResetBall(); // Reset the ball's movement
//            }
//        }


//        scoreclass.playerpoint = 0; // Reset player score for the next level
//        scoreclass.enemypoint = 0; // Reset enemy score for the next level
//        transitioningToNextLevel = false; // Reset the flag after transitioning
//    }

//    public void AdjustPaddleSizeForNextLevel()
//    {
//        if (scoreclass.playerpoint > scoreclass.enemypoint)
//        {
//            // Decrease the paddle size if the player wins
//            playerPaddle.localScale = new Vector3(playerPaddle.localScale.x, playerPaddle.localScale.y * 0.8f, playerPaddle.localScale.z);
//            ppaddleparameter = 0.8f;
//            //Vector3 paddleSize = playerPaddle.localScale;
//            //float paddleWidth = paddleSize.x;
//            //float paddleHeight = paddleSize.y;
//            //float paddleDepth = paddleSize.z;
//        }
//        else if (scoreclass.playerpoint < scoreclass.enemypoint)
//        {
//            // Increase the paddle size if the enemy wins
//            playerPaddle.localScale = new Vector3(playerPaddle.localScale.x, playerPaddle.localScale.y * 1.2f, playerPaddle.localScale.z);
//            ppaddleparameter = 1.2f;
//            //Vector3 paddleSize = playerPaddle.localScale;
//            //float paddleWidth = paddleSize.x;
//            //float paddleHeight = paddleSize.y;
//            //float paddleDepth = paddleSize.z;

//        }
//        //// Adjust the collider size to match the new scale
//        //BoxCollider2D collider = playerPaddle.GetComponent<BoxCollider2D>();
//        //if (collider != null)
//        //{
//        //    collider.size = new Vector2(collider.size.x, playerPaddle.localScale.y);
//        //}

//        //Debug.Log("Paddle Size Adjusted: " + playerPaddle.localScale);

//        updatepaddleparameter = ppaddleparameter;

//        UIManager.Instance.SetSessionDetails();
//    }

//    public static float GetBallSpeed()
//    {
//        // Implement the logic to retrieve the ball speed
//        // For example:
//        GameObject ball = GameObject.FindGameObjectWithTag("Target");
//        if (ball != null)
//        {
//            BallController ballController = ball.GetComponent<BallController>();
//            if (ballController != null)
//            {
//                return ballController.speed; // Assuming BallController has a speed property
//            }
//        }
//        return 0f; // Default speed if not found
//    }

//}


using UnityEngine;
using System;
using UnityEngine.UI; // Add this line to use UI elements
using System.IO;
using UnityEditor.Rendering;
//using static UnityEditor.PlayerSettings;
using UnityEngine.Rendering;

public static class pongclass
{
    public static string filepath;
    public static string relativepath;
}

public class PongPlayerController : MonoBehaviour
{
    public static PongPlayerController instance;
    public static float topBound = 3.6f;
    public static float bottomBound = -3.6f;
    public static float theta1;
    public static float theta2;
    public static float playSize;
    public static float player_x, player_y;

    private string welcompath = Staticvlass.FolderPath; // Replace with your folder path
    private string filePath;
    private bool isPaused = false;
    private bool gameWon = false; // Variable to track if the game is won
    private int previousPlayerScore = 0; // Variable to track the player's previous score
    public static JediSerialCom serReader;
    private DateTime startTime; // Start time of the game
    //public Text durationText; // Reference to the UI Text element
    //private int duration = 0;
    public Text LevelText;
    public int currentLevel = 1;
    //private float elapsedTime = 0f; // Track elapsed time for duration updates
    // public Button nextLevelButton; // Button to transition to the next level
    public bool levelComplete = false; // Flag to indicate level completion
    public GameObject GameOverText;
    public Transform playerPaddle; // Reference to the player's paddle
    public bool transitioningToNextLevel = false; // Flag to indicate transition to next level
    public static string relativepath;
    public static string gameparameterpong;
    public static float paddleparameter = 1;
    public static float updatepaddleparameter;
    public static float ppaddleparameter;


    void Start()
    {
        //JediDataFormat.ReadSetJediDataFormat(AppData.jdfFilename);
        //serReader = new JediSerialCom("COM12");
        //serReader.ConnectToArduino();
        string pongfile = welcompath + "\\" + "Pong_Data";
        if (Directory.Exists(pongfile))
        {
            filePath = Path.Combine(pongfile, "Pong_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
        }
        else
        {
            Directory.CreateDirectory(pongfile);
            filePath = Path.Combine(pongfile, "Pong_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
        }
        pongclass.filepath = filePath;

        string fullFilePath = pongclass.filepath;

        // Define the part of the path you want to store
        string partOfPath = @"Application.dataPath";

        // Use Path class to get the relative path
        string relativePath = Path.GetRelativePath(partOfPath, fullFilePath);
        pongclass.relativepath = relativePath;
        relativepath = relativePath;
        WriteHeader();
        //startTime = DateTime.Now; // Initialize the start time
        // elapsedTime = 0f; // Reset elapsed time
        //nextLevelButton.onClick.AddListener(NextLevel); // Add listener to the next level button
        //nextLevelButton.gameObject.SetActive(false); // Hide the button initially
        GameOverText.SetActive(false); // Hide Game Over text initially
                                       // Set default values for PlayerPrefs for level 1
        AdjustPaddleSizeForNextLevel();

        //UIManager.Instance.SetSessionDetails();

    }

    void Update()
    {
        playSize = topBound - bottomBound;
        if (!isPaused && !gameWon)//!transitioningToNextLevel
        {
            LogData();
            Game();
            CheckLevelCompletion(); // Check for level completion
            //UpdateDurationText();
        }



        LevelText.text = "Level: " + currentLevel;


    }
    void CheckLevelCompletion()
    {
        //if (scoreclass.playerpoint >= 3 || scoreclass.enemypoint >= 3)
        //{
        //    transitioningToNextLevel = true; // Set flag to indicate transition to next level
        //    NextLevel(); // Automatically transition to the next level
        //}
        int scoreThreshold = GetScoreThresholdForLevel(currentLevel); // Get score threshold for the current level

        // Check if the score threshold for the current level has been reached
        if ((scoreclass.playerpoint >= scoreThreshold || scoreclass.enemypoint >= scoreThreshold)) //!transitioningToNextLevel
        {
            //transitioningToNextLevel = true; // Set flag to indicate transition to next level
            //NextLevel(); // Automatically transition to the next level
        }
    }

    int GetScoreThresholdForLevel(int level)
    {
        // Define score thresholds for each level
        if (level == 1) return 5;
        // if (level == 2) return 6;
        // Add more levels and their corresponding score thresholds as needed
        return 5; // Default threshold for levels beyond level 2
    }

    void Game()
    {
        Debug.Log("Game Method Entry: currentLevel = " + currentLevel);

        float y_max = PlayerPrefs.GetFloat("y max");
        float y_min = PlayerPrefs.GetFloat("y min");
        Debug.Log($"Retrieved from PlayerPrefs - y_max: {y_max}, y_min: {y_min}");

        float l1 = 333;
        float l2 = 381;
        float theta1 = Dof.theta1;
        float theta2 = Dof.theta2;
        float thetaa = theta1 * Mathf.Deg2Rad;
        float thetab = theta2 * Mathf.Deg2Rad;

        float y1 = -(Mathf.Cos(thetaa) * l1 + Mathf.Cos(thetaa + thetab) * l2);
        float x1 = -(Mathf.Sin(thetaa) * l1 + Mathf.Sin(thetaa + thetab) * l2);

        float x_val = (x1 / (333 + 381) * 12f);
        float y_val = ((y1 + 350) / (400 * 2)) * 4.8f;

        // Adding debug logs
        Debug.Log($"Game Method Called: currentLevel = {currentLevel}");
        Debug.Log($"theta1: {theta1}, theta2: {theta2}, thetaa: {thetaa}, thetab: {thetab}");
        Debug.Log($"y1: {y1}, x1: {x1}");
        Debug.Log($"x_val: {x_val}, y_val: {y_val}");
        Debug.Log($"y_max: {y_max}, y_min: {y_min}");

        // Adding debug logs
        //Debug.Log($"Game Method Called: currentLevel = {currentLevel}");
        transform.position = new Vector2(transform.position.x, Angle2ScreenZ(y_val, y_min, y_max));

        if (transform.position.y > topBound)
        {
            transform.position = new Vector3(transform.position.x, topBound, 0);
        }
        else if (transform.position.y < bottomBound)
        {
            transform.position = new Vector3(transform.position.x, bottomBound, 0);
        }

        player_x = transform.position.x;
        player_y = transform.position.y;




    }

    public static float Angle2ScreenZ(float angleZ, float y_min, float y_max)
    {
        return Mathf.Clamp(bottomBound + (angleZ - y_min) * (playSize / (y_max - y_min)), -3.6f * playSize, 3.6f * playSize);
    }

    void WriteHeader()
    {
        if (!File.Exists(filePath))
        {
            string header = "Time,PlayerX,PlayerY,EnemyX,EnemyY,BallX,BallY,PlayerScore,EnemyScore\n";
            File.WriteAllText(pongclass.filepath, header);
        }
    }

    void LogData()
    {
        //if (gameWon || levelComplete) return; // Stop logging data if the game is won

        GameObject ball = GameObject.FindGameObjectWithTag("Target");
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        float ball_x = ball.transform.position.x;
        float ball_y = ball.transform.position.y;
        float enemy_x = enemy.transform.position.x;
        float enemy_y = enemy.transform.position.y;

        string currentTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        string data = $"{currentTime},{player_x},{player_y},{enemy_x},{enemy_y},{ball_x},{ball_y},{scoreclass.playerpoint},{scoreclass.enemypoint}\n";

        File.AppendAllText(pongclass.filepath, data);


    }


    public void PauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }
    public void ConnectToComPort(string port)
    {
        if (serReader != null)
        {
            serReader.DisconnectArduino();
        }
        serReader = new JediSerialCom(port);
        serReader.ConnectToArduino();
    }
    public void NextLevel()
    {

        AdjustPaddleSizeForNextLevel();
        currentLevel++; // Increment the level
        // Update the LevelText to the new level value
        LevelText.text = "Level: " + currentLevel;
        //levelComplete = false; // Reset the levelComplete flag
        gameWon = false; // Reset the gameWon flag

        //AdjustSpeedForNextLevel(); // Adjust ball speed before starting the next level

        Debug.Log("NextLevel Method Called: currentLevel = " + currentLevel);
        //Reset the ball's movement
        GameObject ball = GameObject.FindGameObjectWithTag("Target");
        if (ball != null)
        {
            BallController ballController = ball.GetComponent<BallController>();
            if (ballController != null)
            {
                ballController.ResetBall(); // Reset the ball's movement
            }
        }


        scoreclass.playerpoint = 0; // Reset player score for the next level
        scoreclass.enemypoint = 0; // Reset enemy score for the next level
        transitioningToNextLevel = false; // Reset the flag after transitioning
    }

    //public void AdjustPaddleSizeForNextLevel()
    //{
    //    if (scoreclass.playerpoint > scoreclass.enemypoint)
    //    {
    //        // Decrease the paddle size if the player wins
    //        float paddlesize = 0.8f;
    //        playerPaddle.localScale = new Vector3(playerPaddle.localScale.x, playerPaddle.localScale.y * paddlesize, playerPaddle.localScale.z);
    //        ppaddleparameter = 0.8f;
    //        //Vector3 paddleSize = playerPaddle.localScale;
    //        //float paddleWidth = paddleSize.x;
    //        //float paddleHeight = paddleSize.y;
    //        //float paddleDepth = paddleSize.z;
    //    }
    //    else if (scoreclass.playerpoint < scoreclass.enemypoint)
    //    {
    //        // Increase the paddle size if the enemy wins
    //        playerPaddle.localScale = new Vector3(playerPaddle.localScale.x, playerPaddle.localScale.y * 1.2f, playerPaddle.localScale.z);
    //        ppaddleparameter = 1.2f;
    //        //Vector3 paddleSize = playerPaddle.localScale;
    //        //float paddleWidth = paddleSize.x;
    //        //float paddleHeight = paddleSize.y;
    //        //float paddleDepth = paddleSize.z;

    //    }
    //    //// Adjust the collider size to match the new scale
    //    //BoxCollider2D collider = playerPaddle.GetComponent<BoxCollider2D>();
    //    //if (collider != null)
    //    //{
    //    //    collider.size = new Vector2(collider.size.x, playerPaddle.localScale.y);
    //    //}

    //    //Debug.Log("Paddle Size Adjusted: " + playerPaddle.localScale);

    //    updatepaddleparameter = ppaddleparameter;

    //    //UIManager.Instance.SetSessionDetails();
    //}


    public void AdjustPaddleSizeForNextLevel()
    {



        ////float paddlesize = 0.8f;
        playerPaddle.localScale = new Vector3(playerPaddle.localScale.x, playerPaddle.localScale.y * UIManager.pong_speed, playerPaddle.localScale.z);


        //UIManager.Instance.SetSessionDetails();
    }



    public static float GetBallSpeed()
    {
        // Implement the logic to retrieve the ball speed
        // For example:
        GameObject ball = GameObject.FindGameObjectWithTag("Target");
        if (ball != null)
        {
            BallController ballController = ball.GetComponent<BallController>();
            if (ballController != null)
            {
                return ballController.speed; // Assuming BallController has a speed property
            }
        }
        return 0f; // Default speed if not found
    }

}