using UnityEngine;

public class GameDataModel : MonoBehaviour
{
    //Static instance allows any other script to access this without a direct reference
    public static GameDataModel Instance { get; private set; }

    public string SavedString = "";

    /// <summary>
    /// Creates a singleton to ensure only one is exists and to make a global stroage item
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
