using System.Collections.Generic;
using UnityEngine;

public class PayoutDeals : MonoBehaviour
{
    //payout amount details as per bet multiplier
    public  Dictionary<int, List<int>> PayoutAmount;

    void Start()
    {
        PayoutAmount = new Dictionary<int, List<int>>
        {
            { (int)PokerDealRanks.RoyalFlush, new List<int>{ 250,500,750,1000,4000 } },
            { (int)PokerDealRanks.StraightFlush, new List<int>{ 50,100,150,200,250 } },
            { (int)PokerDealRanks.FourOfAKind, new List<int>{ 25,50,75,100,125 } },
            { (int)PokerDealRanks.FullHouse, new List<int>{ 9,18,27,36,45 } },
            { (int)PokerDealRanks.Flush, new List<int>{ 6,12,18,24,30 } },
            { (int)PokerDealRanks.Straight, new List<int>{ 4,8,12,16,20 } },
            { (int)PokerDealRanks.ThreeOfAKind, new List<int>{ 3,6,9,12,15 } },
            { (int)PokerDealRanks.TwoPair, new List<int>{ 2,4,6,8,10 } },
            { (int)PokerDealRanks.JacksOrBetter, new List<int>{ 1,2,3,4,5 } }
            
        };
    }

    

}
