using UnityEngine;

public class BehaviorChoice : MonoBehaviour
{
    public GameObject chosenUnit;


    public void Neutral()
    {
        if (chosenUnit.GetComponent<OffensiveBehaviour>() != null)
        {
            Destroy(chosenUnit.GetComponent<OffensiveBehaviour>());
        }
        if (chosenUnit.GetComponent<DefensiveBehaviour>() != null)
        {
            Destroy(chosenUnit.GetComponent<DefensiveBehaviour>());
        }

        if (chosenUnit.GetComponent<NeutralBehaviour>() == null)
        {
            chosenUnit.AddComponent<NeutralBehaviour>();
            this.gameObject.SetActive(false);
        }
    }

    public void Offensive()
    {
        if (chosenUnit.GetComponent<NeutralBehaviour>() != null)
        {
            Destroy(chosenUnit.GetComponent<NeutralBehaviour>());
        }
        if (chosenUnit.GetComponent<DefensiveBehaviour>() != null)
        {
            Destroy(chosenUnit.GetComponent<DefensiveBehaviour>());
        }

        if (chosenUnit.GetComponent<OffensiveBehaviour>() == null)
        {
            chosenUnit.AddComponent<OffensiveBehaviour>();
            this.gameObject.SetActive(false);
        }
    }

    public void Defensive()
    {
        if (chosenUnit.GetComponent<NeutralBehaviour>() != null)
        {
            Destroy(chosenUnit.GetComponent<NeutralBehaviour>());
        }
        if (chosenUnit.GetComponent<OffensiveBehaviour>() != null)
        {
            Destroy(chosenUnit.GetComponent<OffensiveBehaviour>());
        }

        if (chosenUnit.GetComponent<DefensiveBehaviour>() == null)
        {
            chosenUnit.AddComponent<DefensiveBehaviour>();
            this.gameObject.SetActive(false);
        }
    }
}
