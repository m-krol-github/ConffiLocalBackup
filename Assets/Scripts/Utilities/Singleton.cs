using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component

{
    public static T Instance {
        get {
            if (_instance == null) {
                _instance = FindOrCreateInstance();
            }
            return _instance;
        }
    }
    private static T _instance;
    
    private static T FindOrCreateInstance()
    {
        var Instance = GameObject.FindObjectOfType<T>();
        
        if (Instance != null)
        {
            return Instance;
        }
        
        var name = typeof(T).Name + " Singleton";
        var containerGameObject = new GameObject(name);
        var singletonComponent =  containerGameObject.AddComponent<T>();
        
        return singletonComponent;
    }
}
