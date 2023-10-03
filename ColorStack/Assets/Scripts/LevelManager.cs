using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        if(!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level",1);
        }
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }
}
