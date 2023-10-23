using System;
using UnityEngine;

namespace Card
{
    public class Card : MonoBehaviour, ICloneable,IComparable
    {
        //card info
        public int id = 0; // for maintain the deck size 0-52 
        public Suits suit;  
        public CardRank rank;  
        public bool isHeld = false;
        public bool isFaceSide = false;  
        public string imgFileName = "";

        public Card(Suits _suit, CardRank _rank)
        {
            suit = _suit;
            rank = _rank;
            imgFileName = "img_card_" + suit.imgName + rank.imgName;
        
        }
    
        // used to creating card copy
        public object Clone()
        {
            return new Card(this.suit, this.rank)
            {
                id = this.id,
                isHeld = this.isHeld,
                isFaceSide = this.isFaceSide,
                imgFileName = this.imgFileName
            };
        }
        //function used for sorting the sort in terms rank
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            Card otherCard = (Card)obj;

            if (this.rank.Value() > otherCard.rank.Value())
                return 1;
            if (this.rank.Value() < otherCard.rank.Value())
                return -1;
            else
                return 0;
        }
    }
}
