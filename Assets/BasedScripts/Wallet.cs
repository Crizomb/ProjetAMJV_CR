using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{

    private int argent;
    [SerializeField] TextMeshProUGUI argentTexte;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        argent = GlobalsVariable.money;
        argentTexte.text = argent.ToString();
    }
}
