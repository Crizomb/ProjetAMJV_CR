using UnityEngine;

public class Lazer : MonoBehaviour
{
    private float i = 0.0f;
    public float damage;
    public GameObject parent; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        //Uniformisation des codes de balles, pour la d�truire au 2e impact avec un collider d� au fait qu'elle spawn DANS un collider
        //Reste clairement Junky et est sujet � am�lioration
        i += 1;
        if (i>0)
            {
            if (LayerMask.LayerToName(other.gameObject.layer) == "Duck")
            {
                other.gameObject.GetComponent<BaseDuckScript>().TakeDamage(damage);
            }
                Destroy(gameObject);
            }
    }
}
