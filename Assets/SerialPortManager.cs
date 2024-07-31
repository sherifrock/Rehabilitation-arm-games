//using UnityEngine;
//using UnityEngine.UI;
//using System.IO.Ports;

//public class SerialPortManager : MonoBehaviour
//{
//    public GameObject buttonTemplate; // Reference to the Button template in the Content
//    public Transform contentTransform; // Reference to the Content Transform

//    void Start()
//    {
//        // Get the available serial COM ports
//        string[] portNames = SerialPort.GetPortNames();

//        // Check if there are any available COM ports
//        if (portNames.Length > 0)
//        {
//            foreach (string port in portNames)
//            {
//                // Create a new Button element for each COM port
//                GameObject buttonInstance = Instantiate(buttonTemplate, contentTransform);
//                buttonInstance.GetComponentInChildren<Text>().text = port;
//                buttonInstance.SetActive(true);

//                // Add click event to the button
//                Button button = buttonInstance.GetComponent<Button>();
//                string selectedPort = port; // Local copy for the closure
//                button.onClick.AddListener(() => OnComPortClick(selectedPort));
//            }
//        }
//        else
//        {
//            // Create a new Button element indicating no COM ports are available
//            GameObject buttonInstance = Instantiate(buttonTemplate, contentTransform);
//            buttonInstance.GetComponentInChildren<Text>().text = "No COM ports available.";
//            buttonInstance.GetComponent<Button>().interactable = false; // Disable button interaction
//            buttonInstance.SetActive(true);
//        }
//    }

//    void OnComPortClick(string port)
//    {
//        PlayerPrefs.SetString("SelectedCOMPort", port);
//        Debug.Log("Selected COM Port: " + port);
//    }
//}


//NEW SCRIPT

using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO.Ports;
using System.Threading;

public class SerialPortManager : MonoBehaviour
{
    public GameObject buttonTemplate; // Reference to the Button template in the Content
    public Transform contentTransform; // Reference to the Content Transform

    private SerialPort serialPort;
    private Thread readThread;
    private bool isRunning;
    private string latestData;
    public static SerialPortManager Instance;

    void Awake()
    {
        // Singleton pattern to ensure only one instance of SerialPortManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Get the available serial COM ports
        string[] portNames = SerialPort.GetPortNames();

        // Check if there are any available COM ports
        if (portNames.Length > 0)
        {
            foreach (string port in portNames)
            {
                // Create a new Button element for each COM port
                GameObject buttonInstance = Instantiate(buttonTemplate, contentTransform);
                buttonInstance.GetComponentInChildren<Text>().text = port;
                buttonInstance.SetActive(true);

                // Add click event to the button
                Button button = buttonInstance.GetComponent<Button>();
                string selectedPort = port; // Local copy for the closure
                button.onClick.AddListener(() => OnComPortClick(selectedPort));
            }
        }
        else
        {
            // Create a new Button element indicating no COM ports are available
            GameObject buttonInstance = Instantiate(buttonTemplate, contentTransform);
            buttonInstance.GetComponentInChildren<Text>().text = "No COM ports available.";
            buttonInstance.GetComponent<Button>().interactable = false; // Disable button interaction
            buttonInstance.SetActive(true);
        }
    }

    void OnComPortClick(string port)
    {
        PlayerPrefs.SetString("SelectedCOMPort", port);
        Debug.Log("Selected COM Port: " + port);
        //InitializeSerialPort(port);
    }
}

//using UnityEngine;
//using UnityEngine.UI;
//using System;
//using System.IO.Ports;
//using System.Threading;
//using System.Text.RegularExpressions;

//public class SerialPortManager : MonoBehaviour
//{
//    public GameObject buttonTemplate; // Reference to the Button template in the Content
//    public Transform contentTransform; // Reference to the Content Transform
//    public Text uiTextComponent;
//    private SerialPort serialPort;
//    private Thread readThread;
//    private bool isRunning;
//    private volatile string latestData; // Use volatile to ensure thread safety
//    public static SerialPortManager Instance;
//    public float theta1;
//    public float theta2;
//    void Awake()
//    {
//        // Singleton pattern to ensure only one instance of SerialPortManager exists
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//            return;
//        }
//    }

//    void Start()
//    {
//        //JediDataFormat.ReadSetJediDataFormat(AppData.jdfFilename);
//        // Load Jedi text format file
//        TextAsset jediTextAsset = Resources.Load<TextAsset>("jeditextformat");

//        if (jediTextAsset != null)
//        {
//            // Access the text content
//            string jediText = jediTextAsset.text;

//            // Now you can use 'jediText' as needed, e.g., parse it, process it, etc.
//            Debug.Log("Loaded Jedi text format:\n" + jediText);
//        }
//        // Get the available serial COM ports
//        string[] portNames = SerialPort.GetPortNames();

//        // Check if there are any available COM ports
//        if (portNames.Length > 0)
//        {
//            foreach (string port in portNames)
//            {
//                // Create a new Button element for each COM port
//                GameObject buttonInstance = Instantiate(buttonTemplate, contentTransform);
//                buttonInstance.GetComponentInChildren<Text>().text = port;
//                buttonInstance.SetActive(true);

//                // Add click event to the button
//                Button button = buttonInstance.GetComponent<Button>();
//                string selectedPort = port; // Local copy for the closure
//                button.onClick.AddListener(() => OnComPortClick(selectedPort));
//            }
//        }
//        else
//        {
//            // Create a new Button element indicating no COM ports are available
//            GameObject buttonInstance = Instantiate(buttonTemplate, contentTransform);
//            buttonInstance.GetComponentInChildren<Text>().text = "No COM ports available.";
//            buttonInstance.GetComponent<Button>().interactable = false; // Disable button interaction
//            buttonInstance.SetActive(true);
//        }
//    }

//    void OnComPortClick(string port)
//    {
//        PlayerPrefs.SetString("SelectedCOMPort", port);
//        Debug.Log("Selected COM Port: " + port);
//        InitializeSerialPort(port);
//    }

//    void InitializeSerialPort(string portName)
//    {
//        int baudRate = 9600; // Adjust this to your baud rate

//        if (serialPort != null && serialPort.IsOpen)
//        {
//            isRunning = false;
//            readThread.Join();
//            serialPort.Close();
//            Debug.Log("Closed previous serial port connection.");
//        }

//        // Initialize the serial port
//        serialPort = new SerialPort(portName, baudRate);
//        serialPort.ReadTimeout = 1000;

//        try
//        {
//            // Open the serial port
//            serialPort.Open();
//            isRunning = true;
//            readThread = new Thread(ReadSerialPort);
//            readThread.Start();
//            Debug.Log("Connected to Arduino on " + portName);
//        }
//        catch (Exception ex)
//        {
//            Debug.LogError("Error: " + ex.Message);
//        }
//    }

//    void ReadSerialPort()
//    {
//        while (isRunning && serialPort.IsOpen)
//        {
//            try
//            {
//                latestData = serialPort.ReadLine();
//                Debug.Log("Data received: " + latestData);
//            }
//            catch (TimeoutException) { }
//            catch (Exception ex)
//            {
//                Debug.LogError("Error: " + ex.Message);
//            }
//        }
//    }

//    public string GetLatestData()
//    {
//        return latestData;
//    }

//    void Update()
//    {
//        // Process and display latest data
//        if (!string.IsNullOrEmpty(latestData))
//        {
//            var data = latestData.Split(',');
//            //// Clean up the data string to remove unwanted characters
//            //latestData = latestData.Trim(); // Remove leading and trailing whitespace

//            //// Split the data string by comma and remove any empty entries
//            //var data = latestData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

//            if (data.Length == 2)
//            {
//                try
//                {
//                    float value1 = float.Parse(data[0]); // Parse the first float value
//                    float value2 = float.Parse(data[1]); // Parse the second float value
//                    Debug.Log("Received values: " + data + ", " + data);


//                    // Update UI Text Component with the latest data
//                    uiTextComponent.text = $"Theta1: {theta1}\nTheta2: {theta2}";
//                }
//                catch (Exception ex)
//                {
//                    Debug.LogError("Error parsing data: " + ex.Message);
//                }
//            }
//            else
//            {
//                Debug.LogWarning("Unexpected data format: " + latestData);
//            }
//        }
//    }
//    void OnApplicationQuit()
//    {
//        // Ensure the thread is stopped and the port is closed when the application quits
//        isRunning = false;
//        if (readThread != null && readThread.IsAlive)
//        {
//            readThread.Join();
//        }

//        if (serialPort != null && serialPort.IsOpen)
//        {
//            serialPort.Close();
//            Debug.Log("Disconnected from Arduino");
//        }
//    }
//}

