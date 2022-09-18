using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static bool keepAlive = false;

    private static T _instance = null;
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
                if (_instance == null)
                {
                    var singletonObj = new GameObject();
                    singletonObj.name = typeof(T).ToString();
                    _instance = singletonObj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
    public virtual void Awake()
    {
        if (_instance != null)
        {            
            Destroy(gameObject);
            return;
        }

        _instance = GetComponent<T>();

        if (keepAlive)
        {
            DontDestroyOnLoad(gameObject);
        }



    }
}
