using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MinecraftUnit))]
[RequireComponent(typeof(MovementHandler))]
public abstract class AbstractBehaviour : MonoBehaviour
{
    [SerializeField] private float pathFps = 1.0f;
    [SerializeField] protected float distanceGoal = 0.0f;
    
    protected abstract void MoveAction();
    
    protected MinecraftUnit CurrentMinecraftUnit;
    

    void Start()
    {
        CurrentMinecraftUnit = GetComponent<MinecraftUnit>();
        StartCoroutine(pathUpdate());
    }
    
    private IEnumerator pathUpdate()
    {
        while (true)
        {
            MoveAction();
            yield return new WaitForSeconds(1.0f/pathFps); 
        }
    }
}
