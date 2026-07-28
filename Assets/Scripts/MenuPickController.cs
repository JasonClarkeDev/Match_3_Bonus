using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPickController : MonoBehaviour
{
    private bool picked = false;

    private void Awake()
    {
        picked = false;
    }

    /// <summary>
    /// Reads in the pick from the UI buttons to be stored off
    /// </summary>
    /// <param name="id">The selected pick ID</param>
    public void SetPick(string id)
    {
        if (picked) { return; }
        picked = true;

        GameDataModel.Instance.SavedString = id;

        SceneManager.LoadScene("MatchBonus");
    }
}
