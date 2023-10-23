using System;
using System.Collections.Generic;
using Card;
using UnityEngine;

namespace VideoPoker
{
    public class DeckManager : MonoBehaviour
    {

        public static DeckManager _instance = null;
        
        //deck list
        private List<Card.Card> deckList;
        private List<CardRank> cardRankList;
        private List<Suits> suitsList;

        private const int DECK_SIZE = 52;

        private void Initialize()
        {
            InitializeCardRankList();
            InitializeSuitList();
            InitializeDeck();
            Shuffle();
        }

        private void Start()
        {
            Initialize();
        }

        private void Awake()
        {
            _instance = this;
        }

        // suits initialization
        private void InitializeSuitList()
        {

            suitsList = new List<Suits>
            {
                new Suits(Suits.CLUBS, Suits.CLUBS_IMG),
                new Suits(Suits.SPADES, Suits.SPADES_IMG),
                new Suits(Suits.HEARTS, Suits.HEARTS_IMG),
                new Suits(Suits.DIAMONDS, Suits.DIAMONDS_IMG)
            };
        }
        //card rank initialization
        private void InitializeCardRankList()
        {
            cardRankList = new List<CardRank>
            {
                new CardRank(CardRank.ACE, CardRank.ACE_IMG_NAME),
                new CardRank(CardRank.TWO, CardRank.TWO_IMG_NAME),
                new CardRank(CardRank.THREE, CardRank.THREE_IMG_NAME),
                new CardRank(CardRank.FOUR, CardRank.FOUR_IMG_NAME),
                new CardRank(CardRank.FIVE, CardRank.FIVE_IMG_NAME),
                new CardRank(CardRank.SIX, CardRank.SIX_IMG_NAME),
                new CardRank(CardRank.SEVEN, CardRank.SEVEN_IMG_NAME),
                new CardRank(CardRank.EIGHT, CardRank.EIGHT_IMG_NAME),
                new CardRank(CardRank.NINE, CardRank.NINE_IMG_NAME),
                new CardRank(CardRank.TEN, CardRank.TEN_IMG_NAME),
                new CardRank(CardRank.JACK, CardRank.JACK_IMG_NAME),
                new CardRank(CardRank.QUEEN, CardRank.QUEEN_IMG_NAME),
                new CardRank(CardRank.KING, CardRank.KING_IMG_NAME)
            };
        }
        // reset for new game
        public void Reset()
        {
            InitializeDeck();
            Shuffle();
        }
        //getting the initial card deck 
        private void InitializeDeck()
        {
            deckList = new List<Card.Card>();
            var count = 1;
        
            foreach (Suits suit in suitsList)
            {
                foreach (CardRank rank in cardRankList)
                {
                    Card.Card card = new Card.Card(suit, rank);
                    card.id = count;
                    deckList.Add(card);

                    count++;
                }
            }
            //fail safe to have full deck
            if (count < DECK_SIZE)
            {
                UIManager.Instance.SetErrorMessage("Restart the game");
            }

        }
        //shuffle the deck
        private void Shuffle()
        {
            System.Random rand = new System.Random();
            int n = deckList.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                Card.Card value = deckList[k];
                deckList[k] = deckList[n];
                deckList[n] = value;
            }
        }
        //function to get cards from deck
        public List<Card.Card> DealCards(int size)
        {
            List<Card.Card> dealCards = new List<Card.Card>();

            for (int i = 0; i < size; i++)
            {
                Card.Card card = deckList[i];
                dealCards.Add(card);
                deckList.RemoveAt(i);
            }

            return dealCards;
        }
        //replace the new card from deck from the deal cards list
        private Card.Card GetReplacementCard()
        {
            Card.Card newCard = deckList[0];
            deckList.RemoveAt(0);

            return newCard;
        }
    
        //copy the deal cards - used for evaluating the deal
        public List<Card.Card> CopyCards(List<Card.Card> cards)
        {
            List<Card.Card> newCopy = new List<Card.Card>();

            foreach (var item in cards)
            {
                newCopy.Add((Card.Card)item.Clone());
            }

            return newCopy;
        }
        //replacing the new card from current deal
        public List<Card.Card> SetReplacementHand(List<Card.Card> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (!cards[i].isHeld)
                {
                    Card.Card newCard = GameManager.Instance.deckManager.GetReplacementCard();
                    cards[i] = newCard;
                }
            }

            return cards;
        }

    
    }
}
