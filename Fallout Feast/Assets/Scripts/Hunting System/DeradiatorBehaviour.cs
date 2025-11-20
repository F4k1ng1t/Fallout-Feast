using UnityEngine;

public class DeradiatorBehaviour : MonoBehaviour
{
    PlayerController p;
    private void Start()
    {
        p = transform.parent.GetComponentInParent<PlayerController>();
    }
    private void OnTriggerStay(Collider other)
    {
        EnemyBehaviour e = other.GetComponent<EnemyBehaviour>();
        if (other.CompareTag("Enemy") && p.isShooting && e != null)
        {
            e.BecomeDeradiated();
        }
    }
}
