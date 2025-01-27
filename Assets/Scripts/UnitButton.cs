using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class UnitButton : MonoBehaviour
{

    [SerializeField] private GameObject Mask;

    [SerializeField] GameObject unitPrefab;
    [SerializeField] UnitPlacement unitPlacement;
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
            GlobalsVariable.Pay(cost);
            //Mask.SetActive(true);
            OnClicked += PlaceUnit;
            OnExit += StopPlacing;
        }
    }

    void PlaceUnit()
    {
        Vector3 mousePos = unitPlacement.MapPosition();
        GameObject go = Instantiate(unitPrefab);
        go.transform.position = mousePos;

    }

    public void StopPlacing()
    {
        //Mask.SetActive(false);
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
