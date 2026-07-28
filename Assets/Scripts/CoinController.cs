using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [System.Serializable]
    internal struct CoinID
    {
        public string coinName;
        public int coinID;
    }

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string coinLevel;
    [SerializeField] private string dimmed;
    [Header("Data")]
    [SerializeField] private List<CoinID> ids;

    private Dictionary<string, int> coinAnimIds = new Dictionary<string, int>();

    /// <summary>
    /// On start takes the given coin anim ids and stores them into a dictionary for quicker serching
    /// </summary>
    private void Awake()
    {
        foreach (var item in ids)
        {
            if (coinAnimIds.ContainsKey(item.coinName))
            {
                Debug.LogWarning("Duplicate name: " + item.coinName);
            }
            else
            {
                coinAnimIds.Add(item.coinName, item.coinID);
            }
        }
    }

    /// <summary>
    /// Takes a given symbol name id and sets 
    /// </summary>
    /// <param name="coinId">The coin name id to check for a valid defined id</param>
    public void PlayAnimation(string coinId)
    {
        int id = -1;

        if (coinAnimIds.ContainsKey(coinId))
        {
            id = coinAnimIds[coinId];
        }

        animator.SetInteger(coinLevel, id);
    }

    /// <summary>
    /// Dims the select symbol
    /// </summary>
    public void DimCoin()
    {
        animator.SetBool(dimmed, true);
    }
}
