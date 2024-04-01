
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class moundController : MonoBehaviour
{
    List<string> asjad = new List<string> {"prillid", "saabas", "ratas", "halg"};
    int kordiKaevatud = 0;
    string asi;

    public GameObject axe;
    public AudioClip ratas;
    public AudioClip paike;
    public AudioClip halg;
    public AudioClip voti;
    public AudioClip axeSound;
    public AudioClip saabas;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        asjad = asjad.OrderBy( x => (int)UnityEngine.Random.Range(0, 4)).ToList();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInteract(){
        if (kordiKaevatud == 4){
            axe.GetComponent<AxeScript>().StartBlinking();
            gameObject.tag = null;
            Invoke("GetAxe", 7);
        }
        
        if (kordiKaevatud == 2){
            asi = "voti";
        } else{
            asi = asjad[kordiKaevatud > 2 ? kordiKaevatud - 1 : kordiKaevatud];
        }
        kordiKaevatud++;

        switch (asi){
            case "saabas":
                audioSource.PlayOneShot(saabas);
                break;
            case "ratas":
                audioSource.PlayOneShot(ratas);
                break;
            case "voti":
                audioSource.PlayOneShot(voti);
                break;
            case "halg":
                audioSource.PlayOneShot(halg);
                break;
            case "prillid":
                audioSource.PlayOneShot(paike);
                break;
        }

        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }


    void GetAxe(){
        audioSource.PlayOneShot(axeSound);
    }
}
