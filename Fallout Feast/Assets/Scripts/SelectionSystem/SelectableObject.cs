using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color originalColor;
    [SerializeField] private float pulseDuration = 0.5f; // Total pulse time
    private Coroutine pulseCoroutine;
    private static SelectableObject currentlySelected;

    private bool keepPulsing = false;
    
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    void OnMouseDown()
    {
        Select();
    }

    public void Select()
    {
        if (currentlySelected != this)
        {
            Deselect();
            currentlySelected = this;
            StartPulsing();
        }
        else
        {
            Deselect();
        }
    }

    public static void Deselect()
    {
        if (currentlySelected != null)
        {
            currentlySelected.StopPulsing();
            currentlySelected = null;
        }
    }
    private void StartPulsing()
    {
        if (pulseCoroutine == null)
        {
            keepPulsing = true;
            pulseCoroutine = StartCoroutine(PulseColor());
        }
    }

    private void StopPulsing()
    {
        if (pulseCoroutine != null)
        {
            Debug.Log("Stopping pulse");
            // objectRenderer.material.color = originalColor;
            // StopCoroutine(pulseCoroutine);
            Debug.Log("Stopped pulse");


            
            Debug.Log("Object renderer color "+ objectRenderer.material.color);
            //Debug.Log("Red = " + Color.red);
            objectRenderer.material.color = Color.red; // Red isn't showing 
            Debug.Log("Object renderer color after red"+ objectRenderer.material.color);
            pulseCoroutine = null;
            keepPulsing = false;
        }

    }

    private System.Collections.IEnumerator PulseColor()
    {
        float halfPulse = pulseDuration / 2f;
        while (keepPulsing)
        {
            // Lerp to white
            yield return StartCoroutine(LerpColor(originalColor, Color.white, halfPulse));
            // Lerp back to original
            Debug.Log("Color inside loop "+ objectRenderer.material.color);
            yield return StartCoroutine(LerpColor(Color.white, originalColor, halfPulse));
        }
        //objectRenderer.material.color = Color.red;
        objectRenderer.material.color = originalColor;
    }

    private System.Collections.IEnumerator LerpColor(Color from, Color to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            objectRenderer.material.color = Color.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        objectRenderer.material.color = to;
    }

    private void OnDisable()
    {
        StopPulsing();
    }
}
