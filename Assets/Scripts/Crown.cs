using System;
using TMPro;
using UnityEngine;

public class Crown : MonoBehaviour
{

    public event Action OnClicked;
    private bool crowned=false;
    [SerializeField] TextMeshProUGUI texte;


    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask placementLayer;

    private void Start()
    {
        texte.enabled = false;
    }

    public void StartCrown()
    {
        if (!crowned)
        {
            Debug.Log("Crowning right now");
            texte.enabled = true;
            OnClicked += Crowning;
        }
    }



    public GameObject ObjectPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _camera.nearClipPlane;
        Ray ray = _camera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, placementLayer))
        {
            OnClicked -= Crowning;
            return hit.transform.gameObject;
        }
        else
        {
            return null;
        }
    }
    
    public void Crowning()
    {
        GameObject unit = ObjectPosition();
        unit.GetComponent<AbstractUnit>().IsQueen = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }
    }
}
