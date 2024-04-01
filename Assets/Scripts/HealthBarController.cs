
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        healthBar = gameObject.GetComponent<Slider>();
        healthBar.maxValue = 100;
        healthBar.value = Health.GetHealth();

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = Health.GetHealth();
    }
}
