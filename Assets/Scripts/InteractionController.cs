using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public Sprite otherSprite;
    public string interactionType;
    SpriteRenderer sr;
    AudioSource audioSource;

    public GameObject auk;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract(){
        if (interactionType == "SpriteChange"){
            sr.sprite = otherSprite;
            auk.GetComponent<Level1_2Transition>().enabledH = true;
            audioSource.Play();
        }
    }
}
