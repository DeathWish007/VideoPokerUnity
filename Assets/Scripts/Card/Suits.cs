using UnityEngine;

namespace Card
{
    public class Suits : MonoBehaviour
    {
        //suits
        public const string HEARTS = "HEARTS";
        public const string DIAMONDS = "DIAMONDS";
        public const string CLUBS = "CLUBS";
        public const string SPADES = "SPADES";

        //suits name for image 
        public const string HEARTS_IMG = "h";
        public const string DIAMONDS_IMG = "d";
        public const string CLUBS_IMG = "c";
        public const string SPADES_IMG = "s";
   
        public string name;
        public string imgName;

        public Suits(string _name, string _imgName)
        {
            name = _name;
            imgName = _imgName;
        }
    }
}
