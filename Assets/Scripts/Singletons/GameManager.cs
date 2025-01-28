using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
{
    [SerializeField] private List<string> levelNames;
    [SerializeField] private List<int> levelsMoney;
    int current_level = -1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GoNextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        // Delete, use only for Debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
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

    private void SetGlobals(int current_level)
    {
        GlobalsVariable.AliveUnitsTeamB = new List<AbstractUnit>();
        GlobalsVariable.AliveUnitsTeamA = new List<AbstractUnit>();
        GlobalsVariable.QueenA = null;
        GlobalsVariable.QueenB = null;
        GlobalsVariable.money = levelsMoney[current_level];
    }

    public void ReloadLevel()
    {
        print("get good, reload current scene");
        SetGlobals(current_level);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void GoNextLevel()
    {
        if (current_level < levelNames.Count)
        {
            current_level++;
            SetGlobals(current_level);
            SceneManager.LoadScene(levelNames[current_level]);
        }

        throw new Exception("Bro there is no next level like stop pls");

    }

}
