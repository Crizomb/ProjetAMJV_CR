using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
{
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

}
