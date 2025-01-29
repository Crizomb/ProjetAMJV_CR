using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
{
    [SerializeField] private List<string> levelNames;
    [SerializeField] private List<string> levelMusics;
    [SerializeField] private List<int> levelsMoney;
    int current_level = 0;

    GameObject _gameUI;
    GameObject _loseUI;
    GameObject _winUI;
    
    // for compativility with other team
    public bool fightStarted = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GoNextLevel();
        }
    }


    public void StartFightForAll()
    {
        AbstractUnit[] units = FindObjectsByType<AbstractUnit>(FindObjectsSortMode.None);
        foreach (var unit in units)
        {
            unit.StartFight();
        }
        fightStarted = true;
    }

    private void SetGlobals(int current_level)
    {
        GlobalsVariable.AliveUnitsTeamB = new List<AbstractUnit>();
        GlobalsVariable.AliveUnitsTeamA = new List<AbstractUnit>();
        GlobalsVariable.QueenA = null;
        GlobalsVariable.QueenB = null;
        GlobalsVariable.money = levelsMoney[current_level];
        fightStarted = false;

    }

    public void ReloadLevel()
    {
        print("get good, reload current scene");
        SetGlobals(current_level);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void GoNextLevel()
    {
        if (current_level <= levelNames.Count)
        {
            current_level++;
        }
        else
        {
            current_level = 0;
        }

        SetGlobals(current_level);
        SceneManager.LoadScene(levelNames[current_level]);
        SoundManager.Instance.PlayMusic(levelMusics[current_level]);

    }

}
