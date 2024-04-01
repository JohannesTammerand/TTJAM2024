
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class CutsceneController : MonoBehaviour
{
    public VideoClip[] cutscenes;
    VideoPlayer videoPlayer;

    string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.clip = cutscenes[CutsceneTracker.tracker];
        videoPlayer.Play();
        nextScene = "Level" + (CutsceneTracker.tracker + 1).ToString();
        CutsceneTracker.Increment();
        

        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        if (CutsceneTracker.tracker <= 4){
            SceneManager.LoadScene(nextScene);
        }
    }

}