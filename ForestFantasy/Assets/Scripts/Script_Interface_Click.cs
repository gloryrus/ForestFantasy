using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_Interface_Click : MonoBehaviour
{
    public static bool start = true;
    public void Start()
    {
    }
    public void OpenSceneGachapon()
    {
        SceneManager.LoadScene(1);
    }
    public static void OpenSceneMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void OpenSceneInventory()
    {
        SceneManager.LoadScene(2);
    }
    public void OpenSceneButtle()
    {
        SceneManager.LoadScene(3);
    }
    public void Reset()
    {

        Script_Load_Save.sheet = 10000;
        Script_Load_Save.MySaveSheet();
    }

}
