using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] private WinCanvas _winCanvas;
    [SerializeField] private LoseUI _loseUI;
    [SerializeField] private GameUI _gameUI;

    // Update is called once per frame
    void LateUpdate()
    {   
        if (GlobalsVariable.QueenA == null)
        {
            _winCanvas.gameObject.SetActive(true);
            _gameUI.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("YOU LOSE");
            _loseUI.gameObject.SetActive(true);
            _gameUI.gameObject.SetActive(false);
        }
        
    }
}
