using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
{
    [SerializeField] private List<string> levelNames;
    
    int current_level = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Delete, use only for Debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("OOKOKOKOKOKOK");
            StartFightForAll();
        }
    }
    
    public void StartFightForAll()
    {
        AbstractUnit[] units = FindObjectsByType<AbstractUnit>(FindObjectsSortMode.None);
        foreach (var unit in units)
        {
            unit.StartFight();
        }
    }
    

    public void GoNextLevel()
    {
        if (current_level < levelNames.Count)
        {
            current_level++;
            SceneManager.LoadScene(levelNames[current_level]);
            return;
        }

        throw new Exception("Bro there is no next level like stop pls");

    }

}
