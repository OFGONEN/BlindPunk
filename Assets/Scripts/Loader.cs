﻿/*
Created By OFGONEN
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

    #region Variables
    public static Loader instance = null;
    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

	
	void Update () 
	{
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }		
	}

    #region Methods
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void LoadNextLevel()
    {
        Loader.instance.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1); // Loads the next level
    }
    #endregion
}
