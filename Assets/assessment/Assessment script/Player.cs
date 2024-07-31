//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using System.IO;
//using System;
//using Debug = UnityEngine.Debug;
////using System.Text.RegularExpressions;
////using System.ComponentModel;
////using System.Data;
////using System.Drawing;
////using Random = UnityEngine.Random;
////using UnityEngine.SceneManagement;
////using Unity.Mathematics;
////using UnityEditor.Build.Player;

//public class Player : MonoBehaviour

//{
//    public static Player instance;

//    //public Vector2 positionValue;
//    //public Vector2 x3, y3;

//    public static float x2;
//    public static float y2;
//    public static JediSerialCom serReader;


//    public float movespeed = 5f;
//    public float theta1;
//    public float theta2;
//    public float x1;
//    public float y1;
//    //[SerializeField]
//    public float x;
//    //[SerializeField]
//    public float y;


//    public float l1 = 333;
//    public float l2 = 381;
//    int count;




//    public static string Date;
//    public static string name_sub_;
//    public static string date_;
//    public static string Path;
//    public static string filePath;
//    public static string folderpath;
//    public static float timer;
//    public string[] portnames;
//    public static string savepath;
//    public InputField Hos_;

//    float enc_1, enc_2;
//    float Rob_X, Rob_Y;
//    string TargetPosx, TargetPosy, CurrentStat, PlayerPosx, PlayerPosy, CirclePositionX, CirclePositionY, targetRadii, circleRadiusStr;

//    public static float timer_;


//    private string welcompath = Staticvlass.FolderPath;

//    //private string filePath;

//    public Rigidbody2D _rb;


//    public float xi;
//    public float yi;
//    public InputField Name; // Reference to the input field for player name
//     public GameObject PMR; // Reference to your panel GameObject

//    public static class assessclass
//    {
//        public static string assesspath;
//    }


//    //public Vector2 HomePoint;
//    void Start()
//    {
//        JediDataFormat.ReadSetJediDataFormat(AppData.jdfFilename);
//        serReader = new JediSerialCom("COM9");
//        serReader.ConnectToArduino();

//        Date = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy HH-mm-ss");
//        //// Subscribe to the EndEdit event of the playerNameInput field
//        Name.onEndEdit.AddListener(OnPlayerNameEntered);

//        //string assessfile = welcompath + "\\" + "assessment_Data";
//        //if (Directory.Exists(assessfile))
//        //{
//        //    filePath = Path.Combine(assessfile, "assessment_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
//        //}
//        //else
//        //{
//        //    Directory.CreateDirectory(assessfile);
//        //    filePath = Path.Combine(assessfile, "assessment_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
//        //}

//       // assessclass.assesspath = filePath;





//    }


//    public void Awake()
//    {
//        instance = this;

//    }

//    public void Update()
//    {
//        //Name.onEndEdit.AddListener(OnPlayerNameEntered);
//        enc_1 = PlayerPrefs.GetFloat("Enc1");
//        enc_2 = PlayerPrefs.GetFloat("Enc2");
//        Rob_X = PlayerPrefs.GetFloat("Robx");
//        Rob_Y = PlayerPrefs.GetFloat("Roby");

//        TargetPosx = PlayerPrefs.GetString("targetPos1");
//        TargetPosy = PlayerPrefs.GetString("targetPos2");

//        CurrentStat = PlayerPrefs.GetString("Currentstat");

//        PlayerPosx = PlayerPrefs.GetString("PlayerX");
//        PlayerPosy = PlayerPrefs.GetString("PlayerY");


//        CirclePositionX = PlayerPrefs.GetString("CirclePosX");
//        CirclePositionY = PlayerPrefs.GetString("CirclePosY");
//        PlayerPrefs.DeleteKey("CirclePosX");
//        PlayerPrefs.DeleteKey("CirclePosY");
//        Debug.Log("circle position x:" + CirclePositionX);
//        Debug.Log("circle position y:" + CirclePositionY);
//        targetRadii = PlayerPrefs.GetString("targetRadii");


//        circleRadiusStr = PlayerPrefs.GetString("CircleRadius");


//        Debug.Log("Retrieved Circle Radius: " + circleRadiusStr); // Add this line

//        timer_ += Time.deltaTime;




//        if ((JediSerialPayload.data.Count == 2))
//        {


//            try
//            {


//                theta1 = (float.Parse(JediSerialPayload.data[0].ToString()));
//                theta2 = (float.Parse(JediSerialPayload.data[1].ToString()));


//            }



//            catch (System.Exception)
//            {

//            }


//        }


//        float thetaa = theta1 * Mathf.Deg2Rad;
//        float thetab = theta2 * Mathf.Deg2Rad;
//        //robot position
//        float y1 = -(Mathf.Cos((thetaa)) * l1 + Mathf.Cos((thetaa + thetab)) * l2);
//        float x1 = -(Mathf.Sin((thetaa)) * l1 + Mathf.Sin((thetaa + thetab)) * l2);



//        //unity scene position
//        x2 = ((x1) / (333 + 381)) * 7.5f;
//        y2 = ((y1 + 350) / (400 * 2)) * 4.8f;
//        transform.position = new Vector3(x2, y2, 0);
//        //Debug.Log("xold" + transform.position.x);
//        transform.Translate(transform.position);
//        //Debug.Log("xnew" + transform.position.x);
//        PlayerPrefs.SetFloat("Enc1", theta1);
//        PlayerPrefs.SetFloat("Enc2", theta2);
//        PlayerPrefs.SetFloat("Robx", transform.position.x);
//        PlayerPrefs.SetFloat("Roby", transform.position.y);
//        //robot_data();
//        // Subscribe to the EndEdit event of the playerNameInput field
//        //Name.onEndEdit.AddListener(OnPlayerNameEntered);
//        string playerName = PlayerPrefs.GetString("PlayerName");
//        //if (!string.IsNullOrEmpty(playerName))
//        //{
//        // robot_data(playerName);
//        //}
//        if (!string.IsNullOrEmpty(playerName))
//        {
//            string csvFilePath = Application.dataPath + "\\" + playerName + "\\" + "robo data.csv";
//            SaveCSVData(csvFilePath);  // Save data each update
//        }

//    }

//    void OnPlayerNameEntered(string Name)
//    {
//        PlayerPrefs.SetString("PlayerName", Name);
//        string DataPath = Application.dataPath;
//        Directory.CreateDirectory(DataPath + "\\" + Name);

//        // Save CSV data inside the player's folder
//        string csvFilePath = DataPath + "\\" + Name + "\\" + "robo data.csv"; // Concatenate the strings to form the file path
//        if (SaveCSVData(csvFilePath))
//        {

//        }
//        else
//        {

//        }

//        //SaveCSVData(csvFilePath);// Call a method to handle saving CSV data
//        // Disable the panel
//        // PMR.SetActive(false);

//    }
//    private bool SaveCSVData(string csvFilePath)
//    {

//        if (File.Exists(csvFilePath))
//        {
//            //check the file is empty,write header
//            if (new FileInfo(csvFilePath).Length == 0)
//            {
//                string Endata = "Time,enc_1, enc_2,Robx,Roby,PlayerX, PlayerY,CirclePosX, CirclePoseY,circleRadius,TargetPos1,TargetPos2,targetRadii,CurrentStat\n";
//                //string Endata = "Time,enc_1, enc_2,Robx,Roby,PlayerX, PlayerY,CirclePosX, CirclePoseY,CurrentStat,targetdata\n";
//                File.WriteAllText(csvFilePath, Endata);
//                DateTime currentDateTime = DateTime.Now;
//                string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
//                string data = $"{formattedDateTime},{enc_1},{enc_2},{Rob_X},{Rob_Y},{PlayerPosx},{PlayerPosy},{CirclePositionX},{CirclePositionY},{circleRadiusStr},{TargetPosx},{TargetPosy},{targetRadii},{CurrentStat}\n";

//                return true;
//            }
//            else
//            {
//                //If the file is not empty,return false
//                DateTime currentDateTime = DateTime.Now;
//                string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
//                string data = $"{formattedDateTime},{enc_1},{enc_2},{Rob_X},{Rob_Y},{PlayerPosx},{PlayerPosy},{CirclePositionX},{CirclePositionY},{circleRadiusStr},{TargetPosx},{TargetPosy},{targetRadii},{CurrentStat}\n";

//                File.AppendAllText(csvFilePath, data);
//                return false;
//            }
//        }
//        else
//        {
//            //If the file doesnt exist
//            string DataPath = Application.dataPath;
//            //Directory.CreateDirectory(DataPath + "\\" + Name + "\\" + "robo data.csv");
//            //string filepath_Endata1 = DataPath + "\\" + Name + "\\" + "robo data.csv";
//            Directory.CreateDirectory(DataPath + "\\" + Name + "\\" + "robo data.csv");
//            string filepath_Endata1 = DataPath + "\\" + Name + "\\" + "robo data.csv";
//            string Endata = "Time,enc_1, enc_2,Robx,Roby,PlayerX, PlayerY,CirclePosX, CirclePosY,circleRadius,TargetPos1,TargetPos2,targetRadii,CurrentStat\n";
//            File.WriteAllText(csvFilePath, Endata);
//            DateTime currentDateTime = DateTime.Now;
//            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
//            string data = $"{formattedDateTime},{enc_1},{enc_2},{Rob_X},{Rob_Y},{PlayerPosx},{PlayerPosy},{CirclePositionX},{CirclePositionY},{circleRadiusStr},{TargetPosx},{TargetPosy},{targetRadii},{CurrentStat}\n";
//            File.AppendAllText(csvFilePath, data);
//            return true;
//        }

//    }
//    public void robot_data(string playerName)
//    {

//        string DataPath = Application.dataPath;
//        Directory.CreateDirectory(DataPath + "\\" + playerName);
//        string filepath_Endata = DataPath + "\\" + " robo data.csv";
//        if (IsCSVEmpty(filepath_Endata))
//        {

//        }
//        else
//        {

//        }
//    }
//    private bool IsCSVEmpty(string filepath_Endata)
//    {

//        if (File.Exists(filepath_Endata))
//        {
//            //check the file is empty,write header
//            if (new FileInfo(filepath_Endata).Length == 0)
//            {
//                string Endata = "Time,enc_1, enc_2,Robx,Roby,PlayerX, PlayerY,CirclePosX, CirclePoseY,circleRadius,TargetPos1,TargetPos2,targetRadii,CurrentStat\n";
//                //string Endata = "Time,enc_1, enc_2,Robx,Roby,PlayerX, PlayerY,CirclePosX, CirclePoseY,CurrentStat,targetdata\n";
//                File.WriteAllText(filepath_Endata, Endata);
//                DateTime currentDateTime = DateTime.Now;
//                string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
//                string data = $"{formattedDateTime},{enc_1},{enc_2},{Rob_X},{Rob_Y},{PlayerPosx},{PlayerPosy},{CirclePositionX},{CirclePositionY},{circleRadiusStr},{TargetPosx},{TargetPosy},{targetRadii},{CurrentStat}\n";
//                //string data = $"{formattedDateTime},{enc_1},{enc_2},{Rob_X},{Rob_Y},{PlayerPosx},{PlayerPosy},{CirclePositionX},{CirclePositionY},{CurrentStat},{targetData}\n";
//                return true;
//            }
//        }
//        return false;
//    }




//}









using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;
using UnityEngine.Rendering.Universal;
using static Done_PlayerController;
using NeuroRehabLibrary;

public class Player : MonoBehaviour
{
    public static Player instance;
    public float movespeed = 5f;
    public float theta1;
    public float theta2;
    public float l1 = 333;
    public float l2 = 381;
    public static string Date;
    public static string filePath;
    public InputField Hos_;

    public static string csvFilePath;
    public string selectedComPort = "COM12"; // Default COM port
    public float x2;
    public float y2;
    public static JediSerialCom serReader;

    float enc_1, enc_2;
    float Rob_X, Rob_Y;
    string TargetPosx, TargetPosy, CurrentStat, PlayerPosx, PlayerPosy, CirclePositionX, CirclePositionY, targetRadii, circleRadiusStr;

    public static float timer_;

    private string welcompath = Staticvlass.FolderPath;

    public Rigidbody2D _rb;
    private SerialPort serialPort;
    private Thread readThread;
    private bool isRunning;
    private string latestData;
    public Text uiTextComponent;

    public static string datetime;
    public static string start_time;
    public static string gameend_time;


    private SessionManager sessionManager;
    private GameSession currentGameSession;




    public static class assessmentclass
    {
        public static string assessmentpath;
        public static string relativepath;
    }
    void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject); // Optional: if you want this instance to persist across scenes
        //    Debug.Log("Player instance initialized.");
        //}
        //else
        //{
        //    Destroy(gameObject);
        //    Debug.LogError("Another Player instance already exists. Destroying this instance.");
        //}
        instance = this;
    }
    void Start()
    {
        JediDataFormat.ReadSetJediDataFormat(AppData.jdfFilename);
        serReader = new JediSerialCom("COM12");

        serReader.ConnectToArduino();
        //string port = PlayerPrefs.GetString("SelectedCOMPort", "defaultPort");
        //if (!string.IsNullOrEmpty(port) && port != "defaultPort")
        //{
        //    ConnectToComPort(port);
        //}
        //else
        //{
        //    Debug.LogError("No valid COM port selected.");
        //}

        start_time = DateTime.Now.ToString("HH:mm:ss.fff");
        datetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");

        string assessfile = Path.Combine(welcompath, "assessment_Data");
        if (!Directory.Exists(assessfile))
        {
            Directory.CreateDirectory(assessfile);
        }
        csvFilePath = Path.Combine(assessfile, "assessment_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");

        assessmentclass.assessmentpath = csvFilePath;

        string fullFilePath = assessmentclass.assessmentpath;

        // Define the part of the path you want to store
        string partOfPath = @"Application.dataPath";

        // Use Path class to get the relative path
        string relativePath = Path.GetRelativePath(partOfPath, fullFilePath);
        assessmentclass.relativepath = relativePath;
        //filePath = Path.Combine(assessfile);


        StartNewGameSession();
    }
    void StartNewGameSession()
    {
        currentGameSession = new GameSession
        {
            GameName = "ASSESSMENT",
            Assessment = 1 // Example assessment value, adjust as needed
        };

        SessionManager.Instance.StartGameSession(currentGameSession);
        Debug.Log($"Started new game session with session number: {currentGameSession.SessionNumber}");

        SetSessionDetails();
    }

    void OnDisable()
    {
        EndCurrentGameSession();


    }

 


    private void SetSessionDetails()
    {
        string device = "R2"; // Set the device name
        string assistMode = "Null"; // Set the assist mode
        string assistModeParameters = "Null"; // Set the assist mode parameters
        string deviceSetupLocation = "Null"; // Set the device setup location
        string gameParameter = "Null"; // Set the game parameter
        string trialDataFileLocation = assessmentclass.relativepath; // Adjust path as needed

        SessionManager.Instance.SetDevice(device, currentGameSession);
        SessionManager.Instance.SetAssistMode(assistMode, assistModeParameters, currentGameSession);
        SessionManager.Instance.SetDeviceSetupLocation(deviceSetupLocation, currentGameSession);
        SessionManager.Instance.SetGameParameter(gameParameter, currentGameSession);
        SessionManager.Instance.SetTrialDataFileLocation(trialDataFileLocation, currentGameSession);
    }

    void EndCurrentGameSession()
    {
        if (currentGameSession != null)
        {
            SessionManager.Instance.EndGameSession(currentGameSession);
        }
    }

    void Update()
    {
        enc_1 = PlayerPrefs.GetFloat("Enc1");
        enc_2 = PlayerPrefs.GetFloat("Enc2");
        Rob_X = PlayerPrefs.GetFloat("Robx");
        Rob_Y = PlayerPrefs.GetFloat("Roby");

        TargetPosx = PlayerPrefs.GetString("targetPos1");
        TargetPosy = PlayerPrefs.GetString("targetPos2");
        CurrentStat = PlayerPrefs.GetString("Currentstat");

        PlayerPosx = PlayerPrefs.GetString("PlayerX");
        PlayerPosy = PlayerPrefs.GetString("PlayerY");

        CirclePositionX = PlayerPrefs.GetString("CirclePosX");
        CirclePositionY = PlayerPrefs.GetString("CirclePosY");
        targetRadii = PlayerPrefs.GetString("targetRadii");
        circleRadiusStr = PlayerPrefs.GetString("CircleRadius");

        timer_ += Time.deltaTime;

        if (JediSerialPayload.data.Count == 2)
        {
            try
            {
                theta1 = float.Parse(JediSerialPayload.data[0].ToString());
                theta2 = float.Parse(JediSerialPayload.data[1].ToString());
            }
            catch (Exception)
            {
                // Handle parsing exception
            }
        }
        //if (!string.IsNullOrEmpty(latestData))
        //{
        //    var data = latestData.Split(',');
        //    // Clean up the data string to remove unwanted characters
        //    //latestData = latestData.Trim(); // Remove leading and trailing whitespace

        //    //// Split the data string by comma and remove any empty entries
        //    //var data = latestData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        //    if (data.Length == 2)
        //    {
        //        try
        //        {
        //            float value1 = float.Parse(data[0]); // Parse the first float value
        //            float value2 = float.Parse(data[1]); // Parse the second float value
        //            Debug.Log("Received values: " + data + ", " + data);
        //            // Update UI Text Component with the latest data
        //            uiTextComponent.text = $"Theta1: {theta1}\nTheta2: {theta2}";
        //        }
        //        catch (System.Exception ex)
        //        {
        //            Debug.LogError("Error parsing data: " + ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Unexpected data format: " + latestData);
        //    }


        //}

        float thetaa = theta1 * Mathf.Deg2Rad;
        float thetab = theta2 * Mathf.Deg2Rad;

        float y1 = -(Mathf.Cos(thetaa) * l1 + Mathf.Cos(thetaa + thetab) * l2);
        float x1 = -(Mathf.Sin(thetaa) * l1 + Mathf.Sin(thetaa + thetab) * l2);

        x2 = (x1 / (l1 + l2)) * 7.5f;
        y2 = ((y1 + 350) / (400 * 2)) * 4.8f;
        transform.position = new Vector3(x2, y2, 0);
        transform.Translate(transform.position);
        PlayerPrefs.SetFloat("Enc1", theta1);
        PlayerPrefs.SetFloat("Enc2", theta2);
        PlayerPrefs.SetFloat("Robx", transform.position.x);
        PlayerPrefs.SetFloat("Roby", transform.position.y);

        //string playerName = PlayerPrefs.GetString("PlayerName");
        //if (!string.IsNullOrEmpty(playerName))
        //{
        //    string csvFilePath = Path.Combine(Application.dataPath, playerName, "robo_data.csv");
        //    SaveCSVData(csvFilePath);
        //}


        // string csvFilePath = Path.Combine(filePath, "robo_data.csv");
        SaveCSVData(csvFilePath);

        gameend_time = DateTime.Now.ToString("HH:mm:ss.fff");
        //datetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");
       // EndCurrentGameSession();
    }

    //void OnPlayerNameEntered(string Name)
    //{
    //    PlayerPrefs.SetString("PlayerName", Name);
    //    string DataPath = Path.Combine(Application.dataPath, Name);
    //    Directory.CreateDirectory(DataPath);

    //    string csvFilePath = Path.Combine(DataPath, "robo_data.csv");
    //    SaveCSVData(csvFilePath);
    //}

    private bool SaveCSVData(string csvFilePath)
    {
        string header = "Time,enc_1,enc_2,Robx,Roby,PlayerX,PlayerY,CirclePosX,CirclePosY,circleRadius,TargetPos1,TargetPos2,targetRadii,CurrentStat\n";
        DateTime currentDateTime = DateTime.Now;
        string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        string data = $"{formattedDateTime},{enc_1},{enc_2},{Rob_X},{Rob_Y},{PlayerPosx},{PlayerPosy},{CirclePositionX},{CirclePositionY},{circleRadiusStr},{TargetPosx},{TargetPosy},{targetRadii},{CurrentStat}\n";

        if (!File.Exists(csvFilePath))
        {
            File.WriteAllText(csvFilePath, header);
        }

        File.AppendAllText(csvFilePath, data);
        return true;
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
    //public void ConnectToComPort(string port)
    //{
    //    if (serialPort != null)
    //    {
    //        if (serialPort.IsOpen)
    //        {
    //            isRunning = false;
    //            readThread.Join();
    //            serialPort.Close();
    //            Debug.Log("Closed previous serial port connection.");
    //        }
    //    }

    //    serialPort = new SerialPort(port, 9600);
    //    serialPort.ReadTimeout = 1000;
    //    serialPort.Encoding = System.Text.Encoding.ASCII; // Ensure correct encoding
    //    try
    //    {
    //        serialPort.Open();
    //        isRunning = true;
    //        readThread = new Thread(ReadSerialPort);
    //        readThread.Start();
    //        Debug.Log("Connected to Arduino on " + port);
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.LogError("Error: " + ex.Message);
    //    }
    //}

    //void ReadSerialPort()
    //{
    //    while (isRunning && serialPort.IsOpen)
    //    {
    //        try
    //        {
    //            latestData = serialPort.ReadLine();
    //            Debug.Log("Data received: " + latestData);
    //        }
    //        catch (TimeoutException) { }
    //        catch (Exception ex)
    //        {
    //            Debug.LogError("Error: " + ex.Message);
    //        }
    //    }
    //}

    //void OnApplicationQuit()
    //{
    //    isRunning = false;
    //    if (readThread != null && readThread.IsAlive)
    //    {
    //        readThread.Join();
    //    }

    //    if (serialPort != null && serialPort.IsOpen)
    //    {
    //        serialPort.Close();
    //        Debug.Log("Disconnected from Arduino");
    //    }
    //}



    public void AutoData()
    {

        string GameData_Bird = Application.dataPath;
        // Directory.CreateDirectory(GameData_Bird + "\\" + "Patient_Data" + "\\" + Welcome.p_hospno);
        string filepath_Bird = GameData_Bird + "\\" + "Patient_Data" + "\\" + Welcome.p_hospno + "\\" + "gamedata.csv";



        // string filepath_Bird =  gameclass.gamePath;
        if (IsCSVEmpty(filepath_Bird))
        {

        }
        else
        {

        }

    }


    private bool IsCSVEmpty(string filepath_Bird)
    {


        int session = GameDataManager.instance.GetSessionNumber();

        string currentTime = datetime;
        string device = "R2";
        string assessment = "1";
        string starttime = start_time;
       string  endtime = gameend_time;


        //if (scoreclass.playerpoint == 5 || scoreclass.enemypoint == 5) // Example winning condition
        //{
        //    // Set the gameWon flag to true
        //    endtime = end_time;
        //}
        //else
        //{
        //    endtime = gameend_time;
        //}

        //string endtime = end_time;
        string gamename = "ASSESSMENT";
        string datalocation = assessmentclass.relativepath;
        string devicesetup = "null";
        string assistmode = "null";
        string assistmodeparameter = " null";
        string gameparameter = "null";





        if (File.Exists(filepath_Bird))
        {
            string Position_Bird = "";
            //check the file is empty,write header
            if (new FileInfo(filepath_Bird).Length == 0)
            {
                string Endata_Bird = "sessionnumber,datetime,device,assessment,starttime,Stoptime,gamename,traildatafilelocation,devicesetupfile,assistmode,assistmodeparameter,gameparameter\n";
                File.WriteAllText(filepath_Bird, Endata_Bird);
                DateTime currentDateTime = DateTime.Now;
                //string Position_Space = currentDateTime + "," + AppData.plutoData.enc1 + "," + AppData.plutoData.enc2 + AppData.plutoData.enc3 + "," + AppData.plutoData.enc4 + "," + AppData.plutoData.torque1 + "," + AppData.plutoData.torque3 + '\n';

                Position_Bird = session + "," + currentTime + "," + device + "," + assessment + "," + starttime + "," + endtime + "," + gamename + "," + datalocation + "," + devicesetup + "," + assistmode + "," + assistmodeparameter + "," + gameparameter + '\n';
                return true;
            }

            else
            {

                //If the file is not empty,return false
                DateTime currentDateTime = DateTime.Now;
                //string Position_SpaceR = currentDateTime + "," + AppData.plutoData.enc1 + "," + AppData.plutoData.enc2 + AppData.plutoData.enc3 + "," + AppData.plutoData.enc4 + "," + AppData.plutoData.torque1 + "," + AppData.plutoData.torque3 + '\n';

                Position_Bird = session + "," + currentTime + "," + device + "," + assessment + "," + starttime + "," + endtime + "," + gamename + "," + datalocation + "," + devicesetup + "," + assistmode + "," + assistmodeparameter + "," + gameparameter + '\n';

                File.AppendAllText(filepath_Bird, Position_Bird);
                return true;
            }
        }
        else
        {
            string PositionBird = "";
            //If the file doesnt exist
            string DataPath_Bird = Application.dataPath;
            Directory.CreateDirectory(DataPath_Bird + "\\" + "Patient_Data" + "\\" + Welcome.p_hospno);
            string filepath_Endata1_Bird = DataPath_Bird + "\\" + "Patient_Data" + "\\" + Welcome.p_hospno + "\\" + "gamedata.csv";
            string Endata1_Bird = "sessionnumber,datetime,device,assessment,starttime,Stoptime,gamename,traildatafilelocation,devicesetupfile,assistmode,assistmodeparameter,gameparameter\n";
            File.WriteAllText(filepath_Endata1_Bird, Endata1_Bird);
            DateTime currentDateTime = DateTime.Now;
            //string Position = currentDateTime + "," + AppData.plutoData.enc1 + "," + AppData.plutoData.enc2 + AppData.plutoData.enc3 + "," + AppData.plutoData.enc4 + "," + AppData.plutoData.torque1 + "," + AppData.plutoData.torque3 + '\n';
            PositionBird = session + "," + currentTime + "," + device + "," + assessment + "," + starttime + "," + endtime + "," + gamename + "," + datalocation + "," + devicesetup + "," + assistmode + "," + assistmodeparameter + "," + gameparameter + '\n';

            File.AppendAllText(filepath_Endata1_Bird, PositionBird);
            return true;
        }
    }
}
