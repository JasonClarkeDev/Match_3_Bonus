using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickHub : MonoBehaviour
{
    [System.Serializable]
    internal class PickData
    {
        public string id;
        public bool winningPick;

        public PickData(string id, bool winningPick)
        {
            this.id = id;
            this.winningPick = winningPick;
        }
    }

    [SerializeField] private Animator winAnimator;
    [SerializeField] private string winTrigger;
    
    [SerializeField] private byte pickCount = 3;
    [SerializeField] private List<string> pickOptions = new List<string>();
    [SerializeField] private List<PickButton> buttons = new List<PickButton>();

    [ReadOnly,SerializeField] private List<PickData> picks = new List<PickData>();

    private List<PickButton> winSelected = new List<PickButton>();
    private List<PickButton> lossSelected = new List<PickButton>();
    private string selectedPick;
    private byte counter;
    private bool buttonPicked;
    private bool gameOver;
    private int winninPicks = 0;

    /// <summary>
    /// On awake it generates the pick list
    /// </summary>
    private void Awake()
    {
        selectedPick = GameDataModel.Instance.SavedString;
        picks.Clear();
        ShufflePicks(selectedPick);
    }

    /// <summary>
    /// Input for all buttons when pressed. This will prevent buttons from being pressed if another is still playing animations
    /// </summary>
    /// <param name="buttonIndex">The button index to know what button is pressed</param>
    public void RunPickCheck(byte buttonIndex, PickButton button)
    {
        if (buttonPicked) { return; }
        buttonPicked = true;

        var pick = GetPick();

        if (pick.winningPick) 
        {
            winSelected.Add(button);
            winninPicks++; 
        }
        else
        {
            lossSelected.Add(button);
        }

        gameOver = winninPicks == pickCount;

        button.TriggerCoinAnimation(pick.id);

        if (gameOver)
        {
            StartCoroutine(GameOver());
        }
    }

    /// <summary>
    /// Runs the game over sequence
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3);

        foreach (var item in buttons)
        {
            if (!winSelected.Contains(item))
            {
                if (lossSelected.Contains(item))
                {
                    item.DimSymbol();
                }
                else
                {
                    var pick = GetPick();
                    item.DimSymbol(pick.id);
                }
            }
        }

        yield return new WaitForSeconds(1);

        winAnimator.SetTrigger(winTrigger);
        Debug.Log("YOU WIN");

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Indicates the selected button animations are complete and unlocks the other buttons
    /// </summary>
    public void ButtonAnimationComplete()
    {
        if (gameOver) { return; }

        buttonPicked = false;
    }

    /// <summary>
    /// Takes the selected pick option then creates a random list that will award that outcome
    /// </summary>
    /// <param name="selectedPick">The target pick</param>
    private void ShufflePicks(string selectedPick)
    {
        var extras = new List<PickData>();

        foreach (var item in pickOptions)
        {
            for (int i = 0; i < pickCount; i++)
            {
                if (i == 0 && selectedPick != item)
                {
                    extras.Add(new PickData(item, false));
                }
                else
                {
                    picks.Add(new PickData(item, selectedPick == item));
                }
            }
        }

        picks = Shuffle(picks);
        extras = Shuffle(extras);
        picks.AddRange(extras);
    }

    /// <summary>
    /// Takes a given list and shuffles it up
    /// </summary>
    /// <param name="list">The list to suffle</param>
    /// <returns>The suffled list</returns>
    private List<PickData> Shuffle(List<PickData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            // Pick a random index
            int randomIndex = Random.Range(0, list.Count);

            // Swap
            var temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }

        return list;
    }

    /// <summary>
    /// Iterates through the list of picks with each call
    /// </summary>
    /// <returns>The next item in the list</returns>
    private PickData GetPick()
    {
        var selected = Mathf.Clamp(counter, 0, picks.Count-1);
        counter++;
        return picks[selected];
    }
}
