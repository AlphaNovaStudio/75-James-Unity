using System.Collections.Generic;
using Other;
using UnityEngine;

namespace UI.Suites {
  public class FavoriteCardKeeper : MonoBehaviour {
    public static List<SuiteCard> FavoriteCards { get; private set; }

    private void Awake() {
      FavoriteCards = new List<SuiteCard>();
      Events.SuiteCardsEvents.OnFavouriteStatusChange += OnCardStatusChange;
    }

    private void OnDestroy() {
      Events.SuiteCardsEvents.OnFavouriteStatusChange -= OnCardStatusChange;
    }

    private void OnCardStatusChange (SuiteCard card, bool isFavorite) {
      if (isFavorite) {
        FavoriteCards.Add(card);
        Events.SuiteCardsEvents.OnKeeperChanged?.Invoke(true);
        return;
      }

      if (FavoriteCards.Contains(card)) {
        FavoriteCards.Remove(card);
      }

      bool isNoEmpty = FavoriteCards.Count > 0;
      Events.SuiteCardsEvents.OnKeeperChanged?.Invoke(isNoEmpty);
    }
  }
}