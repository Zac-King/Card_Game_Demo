using UnityEngine;
using System.Collections;
using Dylan;
using Character;
using UnityEngine.Events;

public class UIDiscardEvent : UnityEvent<CardMono>
{ }
public class UIPlayCard : UnityEvent<CardMono>
{ }
public class UICard : MonoBehaviour
{
    public static UIDiscardEvent DiscardCard;
    private Object o;
    void Start()
    {
        DiscardCard = new UIDiscardEvent();
        o = Resources.Load("CardButton");
        TurnManager.PlayerChange.AddListener(UpdateHand);
        Character.Player.onDrawCard.AddListener(UpdateHand);

    }
    //	[ContextMenu("Populate Cards")]
    //	void PopulateCards()
    //	{
    //		if (transform.childCount > 0) {
    //			foreach (Transform t in transform) {
    //				Destroy (t.gameObject);
    //			}
    //
    //		} 
    //			foreach (ICard c in Dylan.TurnManager.ActivePlayer.hand) {
    //				GameObject card = Instantiate (o) as GameObject;
    //				card.transform.SetParent (this.transform);
    //				card.name = c.Name;
    //				card.GetComponentInChildren<UnityEngine.UI.Text> ().text = card.name;
    //			}
    //
    //	}

    void PopulateCards(Player p)
    {
        if (transform.childCount > 0)
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }

        }
        foreach (ICard c in p.hand)
        {
            GameObject card = Instantiate(o) as GameObject;
            card.transform.SetParent(this.transform);
            card.name = c.Name;
            card.GetComponentInChildren<UnityEngine.UI.Text>().text = card.name;

            card.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate
            {
                PlayCard(card.name, card);
            });



        }

    }

    public void UpdateHand(Player p, string t)
    {
        if (p == TurnManager.ActivePlayer)
            PopulateCards(p);
    }

    public void Discard(string n, GameObject card)
    {
        Player p = TurnManager.ActivePlayer;
        p.Discard(n);
        Destroy(card);
    }

    public void PlayCard(string n, GameObject card)
    {
        Player p = TurnManager.ActivePlayer;
        GameObject c = p.cards.Find(x => x.name == n);

        System.Type cardType = c.GetType();

        UnityAction a = () =>
        {   c.transform.position = new Vector3(0, 0, 0);    };

        UnityAction b = () =>
        {   c.transform.position = new Vector3(0, 0, 0);  };

        (cardType == typeof(MysteryCard) ? a : b)();
        p.Discard(n);   // Removes for players hand
        Destroy(card);  // Destroys GUI GameObject that represented the card
    }
}

