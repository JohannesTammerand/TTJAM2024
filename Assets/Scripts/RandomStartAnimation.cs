
using UnityEngine;

public class RandomStartAnimation : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("StartAnimation", Random.Range(0f, 3f));
        animator.speed = Random.Range(0.3f, 1.3f);
    }

    // Update is called once per frame
    void StartAnimation()
    {
        animator.SetTrigger("start");
    }
}
