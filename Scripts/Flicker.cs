using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public Light pointLight;                
    public float minIntensity = 0.3f;         
    public float maxIntensity = 1.0f;      
    public float flickerSpeed = 0.1f;         
    public bool useRandomPattern = true;      
    private Coroutine flickerCoroutine;

    void Start()
    {
        flickerCoroutine = StartCoroutine(Flickering());
    }

   IEnumerator Flickering()
    {
        while (true)
        {
            if (useRandomPattern)
            {
                pointLight.intensity = Random.Range(minIntensity, maxIntensity);
                pointLight.enabled = Random.value > 0.2f; //turn off briefly
                yield return new WaitForSeconds(Random.Range(flickerSpeed / 2, flickerSpeed * 1.5f));

            }
        }
    }

    public void StopFlickering()
    {
        if (flickerCoroutine !=null)
        {
            StopCoroutine(flickerCoroutine);
            pointLight.enabled = true;
        }
    }
}
