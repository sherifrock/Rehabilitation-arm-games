//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.UI;
//using System.IO;
//using System;
//using Debug = UnityEngine.Debug;
//using static GameState;
//using static Unity.VisualScripting.Member;
//using UnityEditor;

//[System.Serializable]
//public class Done_Boundary
//{

//    public float xMin;
//    public float xMax;
//    public float zMin;
//    public float zMax;

//    public Done_Boundary(float minX, float maxX, float minZ, float maxZ)
//    {
//        xMin = minX;
//        xMax = maxX;
//        zMin = minZ;
//        zMax = maxZ;
//    }
//}

//public class Done_PlayerController : MonoBehaviour
//{
//    public static Done_PlayerController instance;
//    public float speed;
//    public float tilt;
//    public Done_Boundary boundary;
//    //public BoundaryDefault boundary;
//    public Player player;
//    public GameObject shot;
//    public Transform shotSpawn;
//    public float fireRate;
//    public float nextFire;
//    public static float playSize;
//    private Rigidbody rb;

//    static float topbound = 17F;
//    static float bottombound = -17F;

//    public int MovingAverageLength = 10;
//    private int count;
//    private float movingAverage;
//    public static float timer_;
//    public static JediSerialCom serReader;
//    public float theta1;
//    public float theta2;
//    public static string Date;
//    public float playSizeX;
//    public float playSizeY;
//    public float[] angXRange;
//    public float[] angZRange;

//    double x_c;
//    double y_c;
//    public float playerSpeed;
//    private Vector2 playerDirection;
//    public AudioClip[] sounds;
//    private AudioSource source;
//    //public float unityXMax = 550, unityXMin = -550, unityYMax = 450, unityYMin = -250;
//    public float UnityXRange;
//    public float UnityZRange;
//    public float fireStopClock;
//    float PlayerX, PlayerY, hazardX, hazardY, savedState;
//    public float singleFireTime;

//    string welcompath = Staticvlass.FolderPath;

//    public static string spacePath;

//    public static class spaceclass
//    {
//        public static string spacepath;
//    }
//    public void Awake()
//    {
//        instance = this;
//    }
//    void Start()
//    {
//        JediDataFormat.ReadSetJediDataFormat(AppData.jdfFilename);
//        serReader = new JediSerialCom("COM8");
//        serReader.ConnectToArduino();

//        Date = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy HH-mm-ss");
//        rb = GetComponent<Rigidbody>();
//        //playSize = Camera.main.orthographicSize;
//        //playSize = 30f;

//        //rb.position = new Vector3(0f, 0f, 0f);
//        source = GetComponent<AudioSource>();
//        // Initialize arrays with correct size
//        angXRange = new float[2];
//        angZRange = new float[2];
//        angXRange[0] = Drawpath.instance.max_x;
//        angXRange[1] = Drawpath.instance.min_x;
//        angZRange[0] = Drawpath.instance.max_y;
//        angZRange[1] = Drawpath.instance.min_y;
//        //// Debug logs
//        Debug.Log("angXRange[0]: " + angXRange[0]);
//        Debug.Log("angXRange[1]: " + angXRange[1]);
//        Debug.Log("angYRange[0]: " + angZRange[0]);
//        Debug.Log("angYRange[1]: " + angZRange[1]);
//        boundary = new Done_Boundary(-12, 12, -4, 12);
//        // Ensure Drawpath instance is initialized
//        //if (Drawpath.instance != null)
//        //{
//        //    // Initialize angXRange and angZRange with Drawpath values
//        //    angXRange = new float[] { Drawpath.instance.max_x, Drawpath.instance.min_x };
//        //    angZRange = new float[] { Drawpath.instance.max_y, Drawpath.instance.min_y };

//        //    // Debug logs
//        //    Debug.Log("angXRange[0]: " + angXRange[0]);
//        //    Debug.Log("angXRange[1]: " + angXRange[1]);
//        //    Debug.Log("angYRange[0]: " + angZRange[0]);
//        //    Debug.Log("angYRange[1]: " + angZRange[1]);
//        //}
//        //else
//        //{
//        //    Debug.LogError("Drawpath instance is not initialized!");
//        //}

//        x_c = (angXRange[0] + angXRange[1]) / 2;
//        y_c = (angZRange[0] + angZRange[1]) / 2;

//        //string DataPath = Application.dataPath;

//        // Directory.CreateDirectory(DataPath + "\\");
//        string DataPath = welcompath + "\\" + "spaceshooter";
//        // string pongfile = welcompath + "\\" + "Pong_Data";
//        //if (Directory.Exists(DataPath))
//        //{
//        //    spacePath = Path.Combine(DataPath, "space_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
//        //}
//        //else
//        //{
//        //    Directory.CreateDirectory(DataPath);
//        //    spacePath = Path.Combine(DataPath, "space_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
//        //}

//        string assessfile = Path.Combine(welcompath, "space_Data");
//        if (!Directory.Exists(assessfile))
//        {
//            Directory.CreateDirectory(assessfile);
//        }
//        spacePath = Path.Combine(assessfile, "space_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");

//        spaceclass.spacepath = spacePath;

//    }


//    void Update()
//    {

//        //if ((Input.GetKeyDown(KeyCode.F))|| (hyper1.instance.force_total >= PlayerPrefs.GetFloat("Grip force")) && Time.time > nextFire)
//        //{
//        //    nextFire = Time.time + fireRate;

//        //    Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
//        //    GetComponent<AudioSource>().Play();
//        //}
//        //PlayerControllerInput();
//        //Time.timeScale = 1;
//        PlayerX = PlayerPrefs.GetFloat("Playerx");
//        PlayerY = PlayerPrefs.GetFloat("Playery");
//        // Retrieve the saved positions from PlayerPrefs
//        hazardX = PlayerPrefs.GetFloat("HazardX");
//        hazardY = PlayerPrefs.GetFloat("HazardY");


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

//    }


//    void FixedUpdate()
//    {
//        // Calculate thetaa and thetab
//        float thetaa = theta1 * Mathf.Deg2Rad;
//        float thetab = theta2 * Mathf.Deg2Rad;

//        // Check if it's time to fire
//        if (Time.time > nextFire)
//        {
//            nextFire = Time.time + fireRate;
//            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
//        }

//        //rb.velocity = new Vector3(rb.position.x * playerSpeed, 0.0f,  rb.position.y * playerSpeed);

//        // Calculate player movement based on input
//        float moveHorizontal = Input.GetAxis("Horizontal");
//        float moveVertical = Input.GetAxis("Vertical");
//        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

//        // Apply velocity to the rigidbody for player movement
//        //GetComponent<Rigidbody>().velocity = movement * speed;

//        // Calculate the target position based on thetaa and thetab
//        float y1 = -(Mathf.Cos((thetaa)) * 333 + Mathf.Cos((thetaa + thetab)) * 381);
//        float x1 = -(Mathf.Sin((thetaa)) * 333 + Mathf.Sin((thetaa + thetab)) * 381);

//        // Map the target position to the Unity scene using Angle2ScreenX and Angle2ScreenZ methods
//        //float x_u = Angle2ScreenX(x1);
//        //float z_u = Angle2ScreenZ(y1);

//        //// Move the player object incrementally based on the movement input
//        ////rb.position += movement * Time.deltaTime;
//        //// Set the rigidbody position to the mapped target position
//        //rb.position = new Vector3(
//        //    Mathf.Clamp(x_u, boundary.xMin, boundary.xMax),
//        //    0.0f,
//        //    Mathf.Clamp(z_u, boundary.zMin, boundary.zMax)
//        //);

//        //// Move the player object smoothly towards the target position
//        //Vector3 targetPosition = rb.position;
//        //float step = speed * Time.deltaTime;
//        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
//        float x = ((x1) / (333 + 381) * 12f);
//        float y = ((y1 + 350) / (400 * 2)) * 4.8f;

//        PlayerX = x;
//        PlayerY = y;
//        float x_val = x;
//        float z_val = (y);

//        Debug.Log(x_val + " " + z_val);
//        //Debug.Log("dx"+"dy"+x_val + " " + z_val); 

//        GetComponent<Rigidbody>().position = new Vector3
//        (
//            Mathf.Clamp(-Angle2ScreenX(x_val), boundary.xMin, boundary.xMax),
//           0.0f,
//            Mathf.Clamp(-Angle2ScreenZ(z_val), boundary.zMin, boundary.zMax)
//        );

//        // Debug.Log(Angle2ScreenZ(Dof.x) + "   ...  ... " + Angle2ScreenZ(Dof.y));

//        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);


//        Vector3 xxx = GetComponent<Rigidbody>().position;
//        // Save player positions to PlayerPrefs
//        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
//        PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
//        game_data();
//    }

//    public float Angle2ScreenX(float angleX)
//    {
//        //Debug.Log("Boundary X Min: " + boundary.xMin);
//        //Debug.Log("Boundary X Max: " + boundary.xMax);

//        float playSizeX = boundary.xMax - boundary.xMin;
//        return Mathf.Clamp(boundary.xMin + (angleX - angXRange[0]) * (playSizeX) / (angXRange[1] - angXRange[0]), -1.2f * playSizeX, 1.2f * playSizeX);
//    }

//    public float Angle2ScreenZ(float angleZ)
//    {
//        //Debug.Log("Boundary Z Min: " + boundary.zMin);
//        //Debug.Log("Boundary Z Max: " + boundary.zMax);
//        float playSizeZ = (boundary.zMax - boundary.zMin);
//        return Mathf.Clamp(boundary.xMin + (angleZ - angZRange[0]) * (playSizeZ) / (angZRange[1] - angZRange[0]), -1f * playSizeZ, 1f * playSizeZ);
//    }

//    public void game_data()
//    {


//        //pongclass.filepath = spacePath;
//        //string spacefolder = DataPath + "\\" + " spaceshooter";
//         string filepath_Endata = spaceclass.spacepath;
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
//                string Endata = "Time,Playerx,Playery,hazardX, hazardY, savedState\n";
//                //string Endata = "Time,enc_1, enc_2,Robx,Roby,PlayerX, PlayerY,CirclePosX, CirclePoseY,CurrentStat,targetdata\n";
//                File.WriteAllText(filepath_Endata, Endata);
//                DateTime currentDateTime = DateTime.Now;
//                string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
//                string data = $"{formattedDateTime},{PlayerX},{PlayerY},{hazardX},{hazardY},{savedState}\n";
//                //string data = $"{formattedDateTime},{enc_1},{enc_2},{Rob_X},{Rob_Y},{PlayerPosx},{PlayerPosy},{CirclePositionX},{CirclePositionY},{CurrentStat},{targetData}\n";
//                return true;
//            }
//            else
//            {
//                //If the file is not empty,return false
//                DateTime currentDateTime = DateTime.Now;
//                string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
//                string data = $"{formattedDateTime},{PlayerX},{PlayerY},{hazardX},{hazardY},{savedState}\n";

//                File.AppendAllText(filepath_Endata, data);
//                return false;
//            }
//        }
//        else
//        {
//            //If the file doesnt exist
//            //string DataPath = Application.dataPath;
//            //Directory.CreateDirectory(DataPath + "\\" + "Rob_data" + "\\");
//            //string filepath_Endata1 = DataPath + "\\" + "Rob_Data" + "\\" + "\\" + "spacegame data.csv";
//            //string Endata = "Time,Playerx,Playery,hazardX, hazardY, savedState\n";
//            //DateTime currentDateTime = DateTime.Now;
//            //string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
//            //string data = $"{formattedDateTime},{PlayerX},{PlayerY},{hazardX},{hazardY},{savedState}\n";
//            //File.AppendAllText(filepath_Endata1, data);
//            return true;
//        }
//    }
//}













using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEditor;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;

[System.Serializable]
public class Done_Boundary
{
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;

    public Done_Boundary(float minX, float maxX, float minZ, float maxZ)
    {
        xMin = minX;
        xMax = maxX;
        zMin = minZ;
        zMax = maxZ;
    }
}

public class Done_PlayerController : MonoBehaviour
{
    public static Done_PlayerController instance;
    public float speed;
    public float tilt;
    public Done_Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float nextFire;
    private Rigidbody rb;

    public int MovingAverageLength = 10;
    private float movingAverage;
    public static float timer_;
    public static JediSerialCom serReader;
    private SerialPort serialPort;
    private Thread readThread;
    private bool isRunning;
    private string latestData;
    public float theta1;
    public float theta2;
    public static string Date;
    public float[] angXRange;
    public float[] angZRange;

    private double x_c;
    private double y_c;
    private Vector2 playerDirection;
    public AudioClip[] sounds;
    private AudioSource source;

    public float UnityXRange;
    public float UnityZRange;
    public float fireStopClock;
    private float PlayerX, PlayerY, hazardX, hazardY, savedState;
    public float singleFireTime;

    private string welcompath = Staticvlass.FolderPath;
    private static string spacePath;
     private GameState currentState;
    string CurrentStat;
    public static string csvFilePath;
    public string selectedComPort = "COM12"; // Default COM port
    public Text uiTextComponent;
    public static class spaceclass
    {
        public static string spacepath;
        public static string relativepath;
    }

    void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    //DontDestroyOnLoad(gameObject); // Optional: if you want this instance to persist across scenes
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


        Date = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy HH-mm-ss");
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();

        angXRange = new float[2] { Drawpath.instance.max_x, Drawpath.instance.min_x };
        angZRange = new float[2] { Drawpath.instance.max_y, Drawpath.instance.min_y };

        boundary = new Done_Boundary(-12, 12, -4, 12);

        x_c = (angXRange[0] + angXRange[1]) / 2;
        y_c = (angZRange[0] + angZRange[1]) / 2;

        string DataPath = Path.Combine(welcompath, "spaceshooter");
       // string assessfile = Path.Combine(welcompath, "space_Data");
        if (!Directory.Exists(DataPath))
        {
            Directory.CreateDirectory(DataPath);
        }
        spacePath = Path.Combine(DataPath, "space_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".csv");
        spaceclass.spacepath = spacePath;



        string fullFilePath = spaceclass.spacepath;

        // Define the part of the path you want to store
        string partOfPath = @"Application.dataPath";

        // Use Path class to get the relative path
        string relativePath = Path.GetRelativePath(partOfPath, fullFilePath);
        spaceclass.relativepath = relativePath;
        // Ensure CSV file is ready
        PrepareCsvFile(spacePath);
        // Retrieve the game state from PlayerPrefs
        int gameStateValue = PlayerPrefs.GetInt("GameState", (int)GameState.TargetMoving);
        GameState gameState = (GameState)gameStateValue;
        //UpdateGameState(gameState);
        LogGameState(currentState);

    }
    private void UpdateGameState(GameState state)
    {
        // Perform actions based on the retrieved state
        //switch (state)
        //{
        //    case GameState.TargetMoving:
        //        // Handle state TargetMoving
        //        break;
        //    case GameState.TargetDestroyed:
        //        // Handle state TargetDestroyed
        //        break;
        //    case GameState.TargetExitedBoundary:
        //        // Handle state TargetExitedBoundary
        //        break;
        //    case GameState.TargetAndPlayerCollided:
        //        // Handle state TargetAndPlayerCollided
        //        break;
        //}
        // Perform actions based on the retrieved state
        currentState = state;
        AppendDataToCsv(spaceclass.spacepath, currentState);
    }
    void Update()
    {
        PlayerX = PlayerPrefs.GetFloat("Playerx");
        PlayerY = PlayerPrefs.GetFloat("Playery");
        hazardX = PlayerPrefs.GetFloat("HazardX");
        hazardY = PlayerPrefs.GetFloat("HazardY");
        //CurrentStat = PlayerPrefs.GetString("Currentstat");
         timer_ += Time.deltaTime;

        if (JediSerialPayload.data.Count == 2)
        {
            try
            {
                theta1 = float.Parse(JediSerialPayload.data[0].ToString());
                theta2 = float.Parse(JediSerialPayload.data[1].ToString());
            }
            catch (System.Exception)
            {
                // Handle exception
            }
        }
    }

    void FixedUpdate()
    {
        float thetaa = theta1 * Mathf.Deg2Rad;
        float thetab = theta2 * Mathf.Deg2Rad;

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        float y1 = -(Mathf.Cos((thetaa)) * 333 + Mathf.Cos((thetaa + thetab)) * 381);
        float x1 = -(Mathf.Sin((thetaa)) * 333 + Mathf.Sin((thetaa + thetab)) * 381);

        float x = ((x1) / (333 + 381) * 12f);
        float y = ((y1 + 350) / (400 * 2)) * 4.8f;

        PlayerX = x;
        PlayerY = y;

        float x_val = x;
        float z_val = y;

        rb.position = new Vector3(
            Mathf.Clamp(-Angle2ScreenX(x_val), boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(-Angle2ScreenZ(z_val), boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);

        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
        // game_data(currentState);
        AppendDataToCsv(spaceclass.spacepath, currentState);
    }

    public float Angle2ScreenX(float angleX)
    {
        float playSizeX = boundary.xMax - boundary.xMin;
        return Mathf.Clamp(boundary.xMin + (angleX - angXRange[0]) * (playSizeX) / (angXRange[1] - angXRange[0]), -1.2f * playSizeX, 1.2f * playSizeX);
    }

    public float Angle2ScreenZ(float angleZ)
    {
        float playSizeZ = boundary.zMax - boundary.zMin;
        return Mathf.Clamp(boundary.xMin + (angleZ - angZRange[0]) * (playSizeZ) / (angZRange[1] - angZRange[0]), -1f * playSizeZ, 1f * playSizeZ);
    }

    //public void game_data(GameState currentState)
    //{
    //    string filepath_Endata = spaceclass.spacepath;
    //    IsCSVEmpty(filepath_Endata, currentState);
    //}

    //private bool IsCSVEmpty(string filepath_Endata, GameState currentState)
    //{
    //    if (File.Exists(filepath_Endata))
    //    {
    //        if (new FileInfo(filepath_Endata).Length == 0)
    //        {
    //            string Endata = "Time,Playerx,Playery,hazardX,hazardY,GameState\n";
    //            File.WriteAllText(filepath_Endata, Endata);
    //        }

    //        DateTime currentDateTime = DateTime.Now;
    //        string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
    //        string data = $"{formattedDateTime},{PlayerX},{PlayerY},{hazardX},{hazardY},{(int)currentState}\n";
    //        File.AppendAllText(filepath_Endata, data);
    //        return false;
    //    }
    //    else
    //    {
    //        string Endata = "Time,Playerx,Playery,hazardX,hazardY,GameState\n";
    //        File.WriteAllText(filepath_Endata, Endata);

    //        DateTime currentDateTime = DateTime.Now;
    //        string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
    //        string data = $"{formattedDateTime},{PlayerX},{PlayerY},{hazardX},{hazardY},{(int)currentState}\n";
    //        File.AppendAllText(filepath_Endata, data);
    //        return true;
    //    }
    //}
    //public void game_data(GameState currentState)
    //{
    //    string filepath_Endata = spaceclass.spacepath;
    //    AppendDataToCsv(filepath_Endata, currentState);
    //}

    private void PrepareCsvFile(string filepath)
    {
        if (!File.Exists(filepath))
        {
            string header = "Time,PlayerX,PlayerY,HazardX,HazardY,GameState\n";
            File.WriteAllText(filepath, header);
        }
    }
    public void LogGameState(GameState currentState)
    {
        AppendDataToCsv(spaceclass.spacepath, currentState);
    }
    private void AppendDataToCsv(string filepath, GameState currentState)
    {
        DateTime currentDateTime = DateTime.Now;
        string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        string data = $"{formattedDateTime},{PlayerX},{PlayerY},{hazardX},{hazardY},{(int)currentState}\n";
        File.AppendAllText(filepath, data);
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

//private bool SaveCSVData(string csvFilePath)
//{
//    string header = "Time,Playerx,Playery,hazardX,hazardY,CurrentStat\n";
//    DateTime currentDateTime = DateTime.Now;
//    string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
//    string data = $"{formattedDateTime},{PlayerX},{PlayerY},{hazardX},{hazardY},{CurrentStat}\n";

//    if (!File.Exists(csvFilePath))
//    {
//        File.WriteAllText(csvFilePath, header);
//    }

//    File.AppendAllText(csvFilePath, data);
//    return true;
//}