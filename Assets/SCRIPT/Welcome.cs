


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;
using System;
using NeuroRehabLibrary;

public class Welcome : MonoBehaviour
{
    public InputField hospno;
    public static string p_hospno;
    public static string newDirPath;
    public static string finalpath;

    private SessionManager sessionManager;

    void Start()
    {
    }

    void Update()
    {
    }

    public void signup()
    {
        SceneManager.LoadScene("Register");
    }

    public void onCLickQuit()
    {
        Application.Quit();
    }

    public void login()
    {
        p_hospno = hospno.text;
        bool hospno_check = string.IsNullOrEmpty(p_hospno);
        if (hospno_check == true)
        {
            Debug.Log("Empty hospno");
        }
        else
        {
            string path_to_data = Application.dataPath;

            if (!Directory.Exists(path_to_data + "\\" + p_hospno))
            {
                string patientDir = path_to_data + "\\" + "Patient_Data" + "\\" + p_hospno;

                circleclass.circlePath = patientDir;

                if (Directory.Exists(patientDir))
                {
                    // Read patient data
                    string patientJson = File.ReadAllText(patientDir + "\\patient.json");
                    var patient = JsonConvert.DeserializeObject<patient>(patientJson);
                    Staticvlass.CrossSceneInformation = patient.name + "," + patient.hospno;

                    // Create a new directory with the current date and time
                    //string dateTimeNow = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");

                    string dateTimeNow = DateTime.Now.ToString("dd-MM-yyyy");
                    string newDirPath = Path.Combine(patientDir, dateTimeNow);

                    if (Directory.Exists(newDirPath))
                    {
                        Staticvlass.FolderPath = newDirPath;
                    }
                    else
                    {
                        Directory.CreateDirectory(newDirPath);
                        Staticvlass.FolderPath = newDirPath;
                    }
                   
                   

                    SceneManager.LoadScene("New Scene");


                    
                    SessionManager.Instance.Login();

                }
                else
                {
                    Debug.Log("Hospital Number Does not exist");
                }
            }
        }
    }
}

public static class circleclass
{
    public static string circlePath;
    public static string sessionpath;

    // public static string CrossSceneInformation;
}


//public static class pathclass
//{
//    public static string FolderPath;
//    public static string CrossSceneInformation;
//}
