using UnityEngine;
using UnityEngine.UI;
using VideoPoker;

namespace Card
{
    public class CardButtonPrefab : MonoBehaviour
    {

        public Card card;
        
        //Card properties
        private Image _image;
        private Button _cardButton;
        private Text _holdTxt;

        public bool isCardSpriteChanged = false;
    
        void Start()
        {
            _cardButton = GetComponent<Button>();
            _image = transform.GetChild(0).GetComponent<Image>();
            _holdTxt = transform.GetChild(1).GetComponent<Text>();
        
            _holdTxt.gameObject.SetActive(false);
            SetInteractButton(false);
        
            _cardButton.onClick.RemoveAllListeners();
            _cardButton.onClick.AddListener(CardButtonClick);
        
        }
        //initialize the card face
        public void ShowCardFace()
        {
            card.isFaceSide = true;
            UpdateCardImage();
            _holdTxt.gameObject.SetActive(false);
            SetInteractButton(true);
        
        }
        //Loading the card image
        public void UpdateCardImage()
        {
            Sprite sprite = Resources.Load<Sprite>("Art/Cards/" + card.imgFileName);
            _image.sprite = sprite;
        }
    
        // onclick for card buttons
        void CardButtonClick()
        {
            if (GameManager.Instance.gameState == GameStates.FirstDeal)
            {
                var textBool = _holdTxt.IsActive();
                _holdTxt.gameObject.SetActive(!textBool);
                card.isHeld = !textBool;
            }
        }
        //card button interact options
        public void SetInteractButton(bool flag)
        {
            _cardButton.interactable = flag;
        }
    
    }
}
