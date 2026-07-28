using UnityEngine;

/// <summary>
/// This contains logic i would heavily alter based on the button controllers. Not wanting to remake a custom button system
/// I opted for using unity's built in buttons
/// </summary>
public class PickButton : MonoBehaviour
{
    [SerializeField] private byte buttonIndex;
    [SerializeField] private PickHub hub;
    [SerializeField] private CoinController coin;

    /// <summary>
    /// This is the hook for the button click.
    /// </summary>
    public void Pick()
    {
        hub.RunPickCheck(buttonIndex, this);
    }

    /// <summary>
    /// Triggers the hub to unlock other buttons
    /// </summary>
    public void ButtonAnimationComplete()
    {
        hub.ButtonAnimationComplete();
    }

    /// <summary>
    /// Sets the coin what animation to play based on the given symbol name id
    /// </summary>
    /// <param name="id">The symbol name id</param>
    public void TriggerCoinAnimation(string id)
    {
        coin.PlayAnimation(id);
    }

    /// <summary>
    /// Dims the symbol to dim and to be set to a specific symbol
    /// </summary>
    /// <param name="id">The symbol name id</param>
    public void DimSymbol(string id)
    {
        coin.PlayAnimation(id);
        coin.DimCoin();
    }

    /// <summary>
    /// Dims a symbol without changing its symbol type
    /// </summary>
    public void DimSymbol()
    {
        coin.DimCoin();
    }
}
