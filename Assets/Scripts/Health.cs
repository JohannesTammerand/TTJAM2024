using UnityEngine;


public static class Health
{
    private static int health;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    static void Initialize(){
        health = 100;
    }
    

    public static void Decrease (int amount){
        health -= amount;
    }

    public static void Increase (int amount){
        health += amount;
    }

    public static int GetHealth(){
        return health;
    }

    public static void Reset(){
        health = 100;
    }
}
