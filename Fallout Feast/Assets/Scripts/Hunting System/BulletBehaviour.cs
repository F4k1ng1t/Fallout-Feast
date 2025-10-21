using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("enemy"))
        {
            Destroy(other.gameObject);
            RestaurantData.RadiatedFood++;
            Destroy(gameObject);
            
        }
    }
}
