using Factories;
using Other;
using UI.Suites;
using UnityEngine;

namespace Controllers {
  public class SuiteCardsController : MonoBehaviour {
    [SerializeField]
    private Transform _favouriteRoot;
    [SerializeField]
    private SuiteCardsFactory _factory;
    [SerializeField]
    private Transform _layoutsRoot;
    [SerializeField]
    private int _cardsCount;

    private RectTransform _canvasRectTransform;
    private Transform _suiteCardsRoot;

    private void Awake() {
      SpawnRootObjects();
      _canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
      Events.SuiteCardsEvents.OnFavouriteStatusChange += SetCardFavouriteStatus;
      SpawnCards();
    }

    private void OnDestroy() {
      Events.SuiteCardsEvents.OnFavouriteStatusChange -= SetCardFavouriteStatus;
    }

    private void SpawnRootObjects() {
      var suiteCardRootname = "SuiteCardsRoot";
      var layoutsRootName = "LayoutsRoot";

      _suiteCardsRoot = new GameObject(suiteCardRootname).transform;
      _suiteCardsRoot.SetParent(transform);
      _suiteCardsRoot.localPosition = Vector3.zero;
    }

    private void SpawnCards() {
      for (var i = 0; i < _cardsCount; i++) {
        SuiteCard card = _factory.GetNewInstance(transform);
        card.Init(_canvasRectTransform, _layoutsRoot);
        card.RectTransform.localPosition = Utils.GetRandomPositionInSquare(_canvasRectTransform.sizeDelta, card.RectTransform.sizeDelta, 1);
        card.SetupInfo();
      }

      SortByTransforms();
    }

    private void SortByTransforms() {
      for (var i = 0; i < _factory.SpawnedObjects.Count; i++) {
        _factory.SpawnedObjects[i].transform.SetParent(_suiteCardsRoot);
      }
    }

    private void SetCardFavouriteStatus (SuiteCard suiteCard, bool state) {
      suiteCard.transform.SetParent(state ? _favouriteRoot : _suiteCardsRoot);
      suiteCard.gameObject.SetActive(state);
    }
  }
}