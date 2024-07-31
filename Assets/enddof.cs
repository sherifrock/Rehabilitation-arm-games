using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime;
using System.IO;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class Dof : MonoBehaviour

{

    private Vector3 originalPosition;
    private bool isCentered = false;

    public static Dof instance;
    public static JediSerialCom serReader;
    public static float x;
    public static float y;
    public static float x1;
    public static float y1;
    public static float movement;
    // public Int32 val1;
    // public Int32 val2;
    // public Int32 val3;
    // public Int32 val4;
    // public Int32 val5;
    // public float torque;
    public static float theta1;
    public static float theta2;
    //[SerializeField]
    //public float frequency = 1.0f;
    //[SerializeField]
    //public float wavelength = 1.0f;
   

    public float l1 = 333;
    public float l2 = 381;

    [SerializeField]
    public Text textResults;
    

    //public float l1;
    //public float l2;
    //public float X;
    //public float Y;    
    // public float error;
    // public float force_1;
    // public float force_2;
    // public float force_total;
    // public float ang1;
    // public float ang2;
    // public float ang3;
    // public float ang4;
    // public int buttonPin1State;
    // public int buttonPin2State;
    // public int buttonPin3State;
    // public int buttonPin4State;
    // public int buttonPin5State;
    // public int buttonPin6State;
    // public int buttonPin7State;
    // public float distance1;
    // public float distance2;
    // public float Btw_dist;
    // public float Avg_Btw_dist;

    public static string Date;
    public static string name_sub_;
    public static string date_;
    public static string Path;
    public static string filePath;
    public static string folderpath;
    public static float timer;
    public string[] portnames;
    public static string savepath;
    public InputField Hos_;
    // public int condition;

    public string selectedComPort = "COM12"; // Default COM port
                                             // public int MovingAverageLength = 5;
                                             // private int count;
                                             // private float movingAverage;





    public static float timer_;
    private SerialPort serialPort;
    private Thread readThread;
    private bool isRunning;
    private string latestData;
    public Text uiTextComponent;
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
        //    //ConnectToComPort(port);
        //}
        //else
        //{
        //    Debug.LogError("No valid COM port selected.");
        //}

        //Date = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy HH-mm-ss");
        //JediDataFormat.ReadSetJediDataFormat(AppData.jdfFilename);
        //serReader = new JediSerialCom("COM12");
        //serReader.ConnectToArduino();


        Date = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy HH-mm-ss");

    }

   

    public void Update()
    {   
        timer_ += Time.deltaTime;




        if ((JediSerialPayload.data.Count == 2))
        {


            try
            {

                // torque = (float.Parse(JediSerialPayload.data[0].ToString()));
                theta1 = (float.Parse(JediSerialPayload.data[0].ToString()));
                theta2 = (float.Parse(JediSerialPayload.data[1].ToString()));
                // error = (float.Parse(JediSerialPayload.data[3].ToString()));

            }



            catch (System.Exception)
            {

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
        //            // Handle exception
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

        float y1 = -(Mathf.Cos((thetaa)) * l1 + Mathf.Cos((thetaa + thetab)) * l2);
        float x1 = -(Mathf.Sin((thetaa)) * l1 + Mathf.Sin((thetaa + thetab)) * l2);

        //x = ((x1) / (333 + 381) * 7.5f);
        //y = ((y1 + 350) / (400 * 2)) * 4.8f;


        y = ((x1) / (333 + 381)) * 4.8f;
        x = ((y1 + 350) / (400 * 2)) * 7.5f;

       

        //Debug.Log("xaxis" + thetaa + "   " + x2);
        //Debug.Log("yaxis" + thetab + "   " + y2);

        //Debug.Log("xaxis" + "" + x + "y" + "   " + y);
        //Debug.Log("thet1" + "" + thetaa + "theta2" + "   " + thetab);
        // Debug.Log("yaxis" + y + "   " + y2);


        //transform.position = new Vector2(x2, y2);
        //transform.Translate(transform.position);








        //textResults.text = $"Vector2: {transform.position}\n" +
        //$"X = {x} Y = {y}\n" +
        //$"PI ={Mathf.PI}\n" +
        //$"PI = {Mathf.PI * 2}";







    }






    //public void start_data_log()
    //{
    //    string Hosp_number = PlayerPrefs.GetString("Hospital Number");
    //    string Game_name = PlayerPrefs.GetString("Game name");
    //    string Mech_name = PlayerPrefs.GetString("Mechanism");
    //    string pth = "D:\\HypercubeData\\";
    //    string total_path = pth + Hosp_number + "-" + Game_name + "-" + Mech_name;
    //    folderpath = total_path;
    //    Directory.CreateDirectory(total_path);
    //    string filename = total_path + "\\" + Hosp_number + Game_name + Mech_name + AppData.dataFolder + DateTime.UtcNow.ToLocalTime().ToString("yy-MM-dd-HH-mm-ss") +
    //         "-" + ".csv";
    //    savepath = filename;
    //    PlayerPrefs.SetString("data", filename);
    //    //saver(total_path);
    //    AppData.dlogger = new DataLogger(filename, "");

    //    if (!File.Exists(filename))
    //    {
    //        //Debug.Log(filename);
    //        /*****header setting***/
    //        string clientHeader = $"\"theta1\",\"theta1\",{Environment.NewLine}";



    //        File.WriteAllText(filename, clientHeader);

    //        //start.GetComponentInChildren<Text>().text = "STARTED";
    //    }
    //    //if (timer_>= 10f)
    //    //{
    //    //AppData.dlogger.stopDataLog();
    //    //Debug.Log("dataStopped");
    //    //}

    //}
    //public void Stop_data_log()
    //{
    //    AppData.dlogger.stopDataLog();
    //    Debug.Log("dataStopped");
    //}

    //// public float Average(float[] arr)

    //// {
    ////     float sum = 0;
    ////     float average = 0;

    ////     for (var i = 0; i < arr.Length; i++)
    ////     {
    ////         sum += arr[i];
    ////     }
    ////     average = sum / arr.Length;
    ////     return average;

    //// }

    //// public float MovingAverage(float values)
    //// {
    ////     count++;


    ////     if (count > MovingAverageLength)
    ////     {
    ////         movingAverage = movingAverage + (values - movingAverage) / (MovingAverageLength + 1);
    ////     }
    ////     else
    ////     {
    ////         movingAverage += values;

    ////         if (count == MovingAverageLength)
    ////         {
    ////              movingAverage = movingAverage / count;

    ////         }
    ////     }
    ////     return movingAverage;
    //// }

    //// public void ReconnectToArduino()
    //// {
    ////     //serReader.DisconnectArduino();
    ////     //Debug.Log("Dis");
    ////     JediDataFormat.ReadSetJediDataFormat(AppData.jdfFilename);
    ////     serReader = new JediSerialCom("COM5");
    ////     serReader.ConnectToArduino();

    //// }
    public void DconnectToArduino()
    {
        serReader.DisconnectArduino();
    }

    // public void GameSceneLoad(){
    //     SceneManager.LoadScene("FlappyGame");
    //     //SceneManager.LoadScene("loginscene");
    // }


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




}










































