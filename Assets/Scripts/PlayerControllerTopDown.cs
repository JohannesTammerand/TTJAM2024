using System;

using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerControllerTopDown : MonoBehaviour
{
    public int level;
    float horizontalInput;
    float verticalInput;
    public float moveSpeed = 1;
    bool movementEnabled = true;
    bool moveRight = false;
    bool moveDown = false;
    public bool hasTowel = false;
    bool hasShovel = false;
    bool hasAxe = false;
    bool pickUpPressed;
    bool isDigging = false;
    float[] startPos = new float[2];
    int timesFrozen = 0;
    bool hasSeenMound = false;

    public GameObject hurt;
    public Animator holeAnimator;
    Animator animator;
    AudioSource audioSource;
    SpriteRenderer sr;
    public AxeScript axeScript;
    public GameObject shovel;
    GameObject mound;
    public GameObject freeze;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("environmentalDamage", 1f, 1f);
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();

        startPos[0] = transform.position.x;
        startPos[1] = transform.position.y;

        if (level == 1){
            freeze = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        } else if (level == 3){
            freeze = transform.GetChild(0).GetChild(0).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        pickUpPressed = false;

        hurt.GetComponent<SpriteRenderer>().enabled = false;
        if ((Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("left") || Input.GetKeyDown("up")) && level == 1 && !audioSource.isPlaying){
            audioSource.PlayOneShot(clips[4]);
        }

        if (Input.GetKeyDown("d") || Input.GetKeyDown("right")){
            moveRight = true;
        } else if (Input.GetKeyDown("s") || Input.GetKeyDown("down")){
            moveDown = true;
        }

        if (Input.GetKeyDown("x")){
            pickUpPressed = true;
        }
    }

    void FixedUpdate(){
        if (movementEnabled){
            if(level == 3){
                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");
                
                transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * moveSpeed);

                //float xPos = Mathf.Clamp(transform.position.x, xBounds[0], xBounds[1]);
                //float yPos = Mathf.Clamp(transform.position.y, yBounds[1], yBounds[0]);

                //transform.position = new Vector3(xPos, yPos, transform.position.z);

                transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * moveSpeed);

                animator.SetFloat("verticalInput", verticalInput == 0 ? -2 : verticalInput);
                animator.SetFloat("horizontalInput", horizontalInput == 0 ? -2 : horizontalInput);

                if (horizontalInput > 0){
                    sr.flipX = false;
                    //Debug.Log(horizontalInput);
                } else if (horizontalInput < 0){
                    sr.flipX = true;
                    //Debug.Log(horizontalInput);
                }
            } else if (level == 1){
                if (moveRight){
                    transform.Translate(Vector3.right * moveSpeed);
                    animator.SetTrigger("StepRight");
                    moveRight = false;
                } else if (moveDown){
                    transform.Translate(Vector3.down * moveSpeed);
                    animator.SetTrigger("StepDown");
                    moveDown = false;
                }
            }
            //horizontalInput = Math.Max(Input.GetAxis("Horizontal"), 0);
            //verticalInput = Math.Min(Input.GetAxis("Vertical"), 0);
        }
    }

    void OnTriggerStay2D(Collider2D col){
        if (col.gameObject.tag == "Interactable" && pickUpPressed){
            
            switch (col.name){
                case "Towel":
                    col.gameObject.GetComponent<InteractionController>().OnInteract();
                    animator.SetTrigger("changeClothes");
                    hasTowel = true;
                    break;
                case "Mound":
                    if (hasShovel && !isDigging){
                        if(col.gameObject.GetComponent<Animator>())
                        Invoke("DeleteMound", 2f);
                        col.gameObject.GetComponent<Animator>().SetTrigger("dig");
                        mound = col.gameObject;
                        isDigging = true;
                    }
                    break;
                case "Shovel":
                    hasShovel = true;
                    col.gameObject.SetActive(false);
                    animator.SetBool("hasShovel", true);
                    animator.SetTrigger("send");
                    axeScript.ResetAxe();
                    break;
                case "Axe":
                    hasAxe = true;
                    col.gameObject.GetComponent<AxeScript>().OnInteract();
                    animator.SetBool("hasAxe", true);
                    animator.SetTrigger("send");
                    shovel.SetActive(true);
                    break;
                case "Sauna":
                    if (hasAxe){
                        SceneManager.LoadScene("Cutscenes");
                    }
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.name == "Sauna" && !hasAxe){
            if (!audioSource.isPlaying){
                audioSource.PlayOneShot(clips[0]);
            }
        } else if (col.name == "Mound" && !hasShovel && !hasSeenMound){
            audioSource.PlayOneShot(clips[1]);
            hasSeenMound = true;
        }
    }

    void environmentalDamage(){
        if (level != 2 && Health.GetHealth() > 0){         
            Health.Decrease(hasTowel ? 1 : 14);

            if (Health.GetHealth() <= 0){
                freeze.SetActive(true);
                animator.enabled = false;
                movementEnabled = false;
                Invoke("Restart", 2f);
            }
        }
    }

    void DeleteMound(){
        mound.GetComponentInParent<moundController>().OnInteract();
        mound.SetActive(false);
        isDigging = false;
    }

    public void DisableMovement(){
        movementEnabled = false;
    }

    void Restart(){
        transform.position = new Vector3(startPos[0], startPos[1], transform.position.z);

        movementEnabled = true;
        moveRight = false;
        moveDown = false;
        hasTowel = level == 1 ? false : true;
        hasShovel = false;
        hasAxe = false;
        isDigging = false;
        animator.enabled = true;
        freeze.SetActive(false);

        
        AudioClip temp = audioSource.clip;
        audioSource.clip = clips[Math.Min(timesFrozen, 3)];
        
        audioSource.Play();

        CancelInvoke();
        InvokeRepeating("environmentalDamage", 1f, 1f);

        Health.Reset();
        timesFrozen++;
    }
}
