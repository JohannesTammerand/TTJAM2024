using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerControllerSideOn : MonoBehaviour
{
    float horizontalInput;
    public float movementSpeed;
    public float jumpSpeed;
    bool isGrounded = false;
    bool spacePressed = false;
    bool hasTorch = false;
    bool reset = false;

    bool hasGottenATorch = false;
    public AudioClip warmer;
    public AudioClip grab;
    AudioClip jump;
    AudioClip temp;

    GameObject torch;
    Transform cam;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    AudioSource audioSource;
    public Light2D light2D;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = transform.GetChild(0);
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        jump = audioSource.clip;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space")){
            spacePressed = true;
        } else if (rb.velocity[1] > 0){
            spacePressed = false;
        }

        if (Input.GetKeyDown("r")){
            reset = true;
        }

    }

    void FixedUpdate(){
        horizontalInput = Math.Max(Input.GetAxis("Horizontal"), 0);

        transform.Translate(new Vector3(horizontalInput, 0, 0) * movementSpeed);

        if (spacePressed && isGrounded){
            rb.AddForce(transform.up * jumpSpeed, ForceMode2D.Impulse);
            audioSource.Play();
        }

        if (transform.position.y < -15 || reset){
            cam.position = new Vector3(transform.position.x, -15, cam.position.z);

            if (transform.position.y < -20 || reset){
                transform.position = new Vector3(14, 13, transform.position.z);
                isGrounded = false;
                animator.SetBool("hasTorch", false);
                
                light2D.gameObject.SetActive(false);
                hasTorch = false;
                reset = false;


                //temp = audioSource.clip;
                //audioSource.clip = grab;
                audioSource.PlayOneShot(grab);
                //Invoke(nameof(RestoreClip), 1);

                if (torch != null){
                    torch.SetActive(true);
                    torch = null;
                    CancelInvoke();
                }
            }
        } else {
            cam.position = new Vector3(transform.position.x + 4, transform.position.y + 2, cam.position.z);
        }

        if (rb.velocity[1] > jumpSpeed){
            rb.velocity = new Vector2(rb.velocity[0], jumpSpeed);
        }

        if (horizontalInput > 0){
            sr.flipX = false;
        } else if (horizontalInput < 0) {
            sr.flipX = true;
        }

        if (horizontalInput != 0){
            animator.SetBool("moving", true);
        } else {
            animator.SetBool("moving", false);
        }

        light2D.transform.position = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Floor"){
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.tag == "Floor"){
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.name == "Door"){
            SceneManager.LoadScene("CutScenes");
        }
    }

    void OnTriggerStay2D(Collider2D col){
        if (col.gameObject.CompareTag("Interactable") && Input.GetKeyDown("x") && !hasTorch){
            animator.SetBool("hasTorch", true);
            light2D.gameObject.SetActive(true);
            col.gameObject.SetActive(false);
            hasTorch = true;
            torch = col.gameObject;

            //temp = audioSource.clip;
            //audioSource.clip = warmer;
            audioSource.PlayOneShot(warmer);
            //Invoke("RestoreClip", 1);
            InvokeRepeating("Heal", 1, 1);
        }
    }

    void Heal(){
        Health.Increase(3);
    }

    void RestoreClip(){
        audioSource.clip = jump;
    }
}
