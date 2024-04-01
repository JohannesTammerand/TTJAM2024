using System;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SlidingController : MonoBehaviour
{
    public float slideSpeed;
    public float moveSpeed;
    float horizontalInput;

    public Transform cam;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        horizontalInput = Input.GetAxis("Horizontal");

        if (transform.position.x <= -5.5 && horizontalInput < 0){
            horizontalInput = 0;
        } else if (transform.position.x >= 5.5 && horizontalInput > 0){
            horizontalInput = 0;
        }

        transform.Translate(new Vector3(horizontalInput * moveSpeed, slideSpeed, 0f));

        float yPos = Math.Min(transform.position.y + 4, 170);

        cam.position = new Vector3(0f, yPos, cam.position.z);

        if (transform.position.y > 200){
            CutsceneTracker.tracker = 4;
            SceneManager.LoadScene("Cutscenes");
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        Debug.Log("a");
        transform.position = new Vector3(0, 3, transform.position.z);
        audioSource.Play();
    }

    void OnTriggerStay2D(Collider2D col){
        Debug.Log("b");
    }
}
