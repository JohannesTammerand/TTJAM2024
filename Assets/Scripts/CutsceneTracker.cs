
using UnityEngine;

public static class CutsceneTracker
{
    public static int tracker;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    static void Initialize(){
        tracker = 0;
    }

    public static void Increment(){
        tracker++;
    }
}
