using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ProjectileBehavior : MonoBehaviour
{
    public bool isBoulder;
    public bool isArrow;
    public bool isTomato;
    public bool isBomb;
    public bool isHuman;

    // Fade out
    public float objectTime;
    public float fadeOutTime;
    public float delayDestroy;
    private MeshRenderer material;
    private Color originalColor;
    public float throwForce;

    public AudioSource impact;
    private bool CanPlayAudio = true;

    private void Awake()
    {
        if (isBoulder)
        {
            
        }
        else if (isArrow)
        {
            material = GetComponentInChildren<MeshRenderer>();
        }
        else if (isTomato)
        {

        }
        else if (isBomb)
        {

        }
        else if (isHuman)
        {

        }
        else
        {
            Debug.LogError("No projectile type is selected");
        }
        GetFadeComponents();
        StartCoroutine(TimeToDespawn(objectTime));
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (isArrow || isTomato)
        {
            if (!collision.gameObject.CompareTag("Ground"))
            {
                transform.SetParent(collision.transform, true);
            }
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.freezeRotation = true;
        }
        if (CanPlayAudio && impact != null){
            impact.Play();
            CanPlayAudio = false;
            StartCoroutine(AudioDelay());
        }
        
    }
    private IEnumerator AudioDelay(){
        yield return new WaitForSeconds(0.5f);
        CanPlayAudio = true;
    }
    private void GetFadeComponents()
    {
        if (material == null)
            material = GetComponent<MeshRenderer>();
        originalColor = material.materials[0].color;
    }
 
    private IEnumerator TimeToDespawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(FadeOut());
        IEnumerator FadeOut()
        {
            float currentTime = 0f;
            while (currentTime < fadeOutTime)
            {
                float alpha = Mathf.Lerp(1, 0, currentTime / fadeOutTime);
                material.materials[0].color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

                currentTime += Time.deltaTime;
                yield return null;
            }
            material.materials[0].color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

            yield return new WaitForSeconds(delayDestroy);
            Destroy(gameObject);
        }
    }
}
