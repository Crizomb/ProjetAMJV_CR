using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class UnitButton : MonoBehaviour
{


    [SerializeField] GameObject unitPrefab;
    [SerializeField] ShopCanvas unitPlacement;
    public event Action OnClicked, OnExit;


    private int cost;
    [SerializeField] TextMeshProUGUI texteCout;
    [SerializeField] TextMeshProUGUI unit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        cost = GlobalsVariable.prices[unit.text];
        texteCout.text = cost.ToString();
    }

    public void StartPlacing()
    {
        if (GlobalsVariable.money < cost)
        {
            return;
        }
        else
        {
            Debug.Log("I'm *in");
            
        
            OnClicked += PlaceUnit;
            OnExit += StopPlacing;
        
        }
    }

    void PlaceUnit()
    {
        Vector3 mousePos = unitPlacement.MapPosition();
        GameObject go = Instantiate(unitPrefab, mousePos, Quaternion.identity);
        GlobalsVariable.Pay(cost);
        OnClicked -= PlaceUnit;

    }

    public void StopPlacing()
    {
        OnClicked -= PlaceUnit;
        OnExit -= StopPlacing;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnExit?.Invoke();
        }
    }
}
