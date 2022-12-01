using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Neighbourhood {
  public class NeighbourhoodCard : MonoBehaviour {
    [SerializeField]
    private TMP_Text _title;
    [SerializeField]
    private TMP_Text _subTitle;
    [SerializeField]
    private TMP_Text _description;
    [SerializeField]
    private Image _image;
    [SerializeField]
    private GameObject _subtitlePanel;

    private void Awake() {
      ResetCard();
    }

    private void ResetCard() {
      SetupCard("");
    }

    public void SetupCard (string title, string subTitle = "", string description = "", Sprite sprite = null) {
      _title.text = title;

      _subtitlePanel.SetActive(!subTitle.Equals(""));
      _subTitle.text = subTitle;

      _description.gameObject.SetActive(!description.Equals(""));
      _description.text = description;

      _image.gameObject.SetActive(sprite);
      _image.sprite = sprite;
    }
  }
}