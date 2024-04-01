
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1_2Transition : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    public bool enabledH = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Player" && enabledH){
            animator.SetTrigger("HoleCollision");
            col.gameObject.GetComponent<PlayerControllerTopDown>().DisableMovement();
            audioSource.Play();
            Invoke("SceneTransition", 2f);
        }
    }

    void SceneTransition(){
        SceneManager.LoadScene("CutScenes");
    }
}
