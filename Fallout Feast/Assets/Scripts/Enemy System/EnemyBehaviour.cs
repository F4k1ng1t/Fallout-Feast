using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public bool isRadiated = true;
    public int health = 100;

    float elapsed = 0f;
    int decontaminatePercent = 0;
    bool CR_Running = false;
    Renderer objectRenderer;
    PlayerController p;
    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        p = (PlayerController)FindFirstObjectByType(typeof(PlayerController));
        Debug.Log(p);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            TakeDamage(10);
            if(health <= 0)
            {
                Die();
                
            }
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
    }
    public void Die()
    {
        if (isRadiated)
            RestaurantData.RadiatedFood++;
        
        else
            RestaurantData.Food++;
        Debug.Log($"Contaminated Food : {RestaurantData.RadiatedFood} \nHealthy Food : {RestaurantData.Food}");
            Destroy(gameObject);
    }
    public void BecomeDeradiated()
    {
        if (!isRadiated)
            return;
        if (CR_Running)
            return;
        StartCoroutine(DeradiateOverTime(objectRenderer.material.color, Color.green, 5f));
        if (decontaminatePercent == 100)
        {
            isRadiated = false;
        }
        
    }
    
    private IEnumerator DeradiateOverTime(Color from, Color to, float duration)
    {
        CR_Running = true;
        while (elapsed < duration && p.isShooting)
        {
            objectRenderer.material.color = Color.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            decontaminatePercent = (int)(elapsed / duration * 100);
            Debug.Log(elapsed);
            yield return null;
        }
        //objectRenderer.material.color = to;
        CR_Running = false;
    }
}
