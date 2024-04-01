using UnityEngine;

public class AxeScript : MonoBehaviour
{
    SpriteRenderer sr;
    Animator animator;
    public Sprite secondSprite;
    Sprite firstSprite;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        firstSprite = sr.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract(){
        animator.enabled = false;
        sr.sprite = secondSprite;
    }

    public void StartBlinking(){
        animator.SetTrigger("startBlinking");
    }

    public void ResetAxe(){
        sr.sprite = firstSprite;
        animator.enabled = true;
    }
}
