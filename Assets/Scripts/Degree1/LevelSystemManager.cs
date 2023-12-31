﻿using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystemManager : MonoBehaviour
{
    [SerializeField] private static LevelSystemManager instance;                             //instance variable
    public static LevelSystemManager Instance { get => instance; }          //instance getter

    [Tooltip("Set the default Level data so when game start 1st time, this data will be saved")]
    [SerializeField] private LevelData levelData;

    public LevelData LevelData { get => levelData; }   //getter

    private int currentLevel;
    public int sceneBuildIndex;                                           //keep track of current level player is playing
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }   //getter and setter for currentLevel

    private void Awake()
    {
        if (instance == null)                                               //if instance is null
        {
            instance = this;                                                //set this as instance
            DontDestroyOnLoad(gameObject);                                  //make it DontDestroyOnLoad
        }
        else
        {
            Destroy(gameObject);                                            //else destroy it
        }
    }
    public void OnClickExit()
    {
        Debug.Log("exit successfully");
        // load the previous scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void OnEnable()
    {
        SaveLoadData.Instance.Initialize();
    }
    public int getCurrentLevel()
    {
        return currentLevel;
    }

    public void LevelComplete(int starAchieved)                             //method called when player win the level
    {
        if (starAchieved > levelData.levelItemsArray[currentLevel].starAchieved)
        {
            // xuwr lys update diem cho user
            int star = starAchieved - levelData.levelItemsArray[currentLevel].starAchieved;
            APIUser.Instance.UpdateExperiences(star * 60);
            levelData.levelItemsArray[currentLevel].starAchieved = starAchieved;
        }

        //save the stars achieved by the player in level
        if (levelData.lastUnlockedLevel <= (currentLevel + 1) && starAchieved > 0)
        {
            levelData.lastUnlockedLevel = currentLevel + 1;           //change the lastUnlockedLevel to next level                                                    //and make next level unlock true
            levelData.levelItemsArray[levelData.lastUnlockedLevel].unlocked = true;

        }
        SaveLoadData.Instance.SaveData();
        SaveLoadData.Instance.LoadData();

    }

}
