//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//public class loginsetup : MonoBehaviour
//{
//    // Start is called before the first frame update
//   public string loginSceneName = "LoginScene";

//    // This function will be called when the login button is clicked
//    public void RestartSession()
//    {
//        // Unload all currently loaded scenes except the login scene
//        for (int i = 0; i < SceneManager.sceneCount; i++)
//        {
//            Scene scene = SceneManager.GetSceneAt(i);
//            if (scene.name != loginSceneName)
//            {
//                SceneManager.UnloadSceneAsync(scene);
//            }
//        }

//        // Reload the login scene
//        SceneManager.LoadScene(loginSceneName, LoadSceneMode.Single);
//    }
//}


using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSetup : MonoBehaviour
{
    public string loginSceneName = "Welcome"; // Ensure this matches the name of your login scene

    // This function will be called when the login button is clicked
    public void RestartApplication()
    {
        StartCoroutine(RestartApplicationCoroutine());
    }

    private IEnumerator RestartApplicationCoroutine()
    {
        // First, unload all currently loaded scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != loginSceneName)
            {
                AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(scene);
                while (!asyncUnload.isDone)
                {
                    yield return null;
                }
            }
        }

        // Wait for all scenes to be unloaded
        yield return new WaitForEndOfFrame();

        // Now, reload the login scene
        SceneManager.LoadScene(loginSceneName, LoadSceneMode.Single);
    }
}
