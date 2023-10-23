using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VideoPoker;

namespace Card
{
    public class CardDealer : MonoBehaviour
    {
        //card references
        [Header("Card References")]
        public List<CardButtonPrefab> cardButtonPrefabs;

        private int _dealSize;
    
    
        void Start()
        {
            _dealSize = GameManager.Instance.GetDalSize();
        }
    
        //initial card deals
        public void FirstDealCards(List<Card> cards)
        {
            if (cards != null)
            {
                int cardIndex = 0;

                foreach (CardButtonPrefab item in cardButtonPrefabs)
                {
                    item.card = cards[cardIndex];
                    item.isCardSpriteChanged = false;
                
                    item.ShowCardFace();
                    cardIndex++;
                }
            
            }
        }
        //deActive the card button interactable
        public void InActiveDeckCards()
        {
            foreach (CardButtonPrefab item in cardButtonPrefabs)
            {
                item.SetInteractButton(false);
            }
        }
        //second deal cards and update the card sprite
        public void UpdateSecondDealCardSprite(List<Card> cards)
        {
            foreach (var item in cards.Where(item => !item.isHeld))
            {
                for (var i = 0; i < _dealSize; i++)
                {
                    if (!cardButtonPrefabs[i].card.isHeld && !cardButtonPrefabs[i].isCardSpriteChanged)
                    {
                        cardButtonPrefabs[i].card = item;
                        cardButtonPrefabs[i].UpdateCardImage();
                        cardButtonPrefabs[i].isCardSpriteChanged = true;
                        break;
                    }
                }
            }
        }
    


    }
}
