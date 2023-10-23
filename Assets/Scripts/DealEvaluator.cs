using System.Collections.Generic;
using Card;
using UnityEngine;
using VideoPoker;

public class DealEvaluator : MonoBehaviour
{
    private bool _isStraight;
    private bool _isFlush;

    private List<Card.Card> _currentCards;
    private List<Card.Card> _copyCurrentCards;

    private Dictionary<int, int> _sameRankCount;
    
    //evaluate the final deal cards
    public int DealScore(List<Card.Card> cards)
    {
        _currentCards = cards;
        _sameRankCount = new Dictionary<int, int>();
        _copyCurrentCards = DeckManager._instance.CopyCards(_currentCards);
        _copyCurrentCards.Sort();
       
        if (RoyalFlush())
            return (int)PokerDealRanks.RoyalFlush;
       
        if (StraightFlush())
            return (int)PokerDealRanks.StraightFlush;
        
        if (FourOfAKind())
            return (int)PokerDealRanks.FourOfAKind;
       
        bool fullHouse = _sameRankCount.ContainsValue(3) && _sameRankCount.ContainsValue(2);
        if (fullHouse)
            return (int)PokerDealRanks.FullHouse;

        if (Flush())
            return (int)PokerDealRanks.Flush;

        if (Straight())
            return (int)PokerDealRanks.Straight;

        bool three = _sameRankCount.ContainsValue(3);
        if (three)
            return (int)PokerDealRanks.ThreeOfAKind;
        
        if (TwoPair())
            return (int)PokerDealRanks.TwoPair;
        
        if (JacksOrBetter())
            return (int)PokerDealRanks.JacksOrBetter;
        return -1;

    }

    private bool RoyalFlush()
    {
        _isFlush = Flush();
        if (!_isFlush)
            return false;

        _isStraight = Straight();
        if (!_isStraight)
            return false;

        bool ace = _copyCurrentCards[_copyCurrentCards.Count - 1].rank.Id() == (int)CardRank.IDs.Ace;

        return _isFlush && _isStraight && ace;
    }

    private bool StraightFlush()
    {
        _isFlush = Flush();

        if (!_isFlush)
            return false;
        _isStraight = Straight();
        if (!_isStraight)
            return false;
        bool notAce = _copyCurrentCards[_copyCurrentCards.Count - 1].rank.Id() != (int)CardRank.IDs.Ace;

        return _isFlush && _isStraight && notAce;
    }

    private bool FourOfAKind()
    {
        foreach (Card.Card card in _copyCurrentCards)
        {
            if (_sameRankCount.ContainsKey(card.rank.Value()))
            {
                _sameRankCount[card.rank.Value()] += 1;
            }
            else
            {
                _sameRankCount.Add(card.rank.Value(),1);
            }
        }

        return _sameRankCount.ContainsValue(4);
    }

    private bool Flush()
    {
        for (int i = 0; i < _copyCurrentCards.Count; i++)
        {
            if (i != _copyCurrentCards.Count - 1)
            {
                if (_copyCurrentCards[i].suit.name != _copyCurrentCards[i + 1].suit.name)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool Straight()
    {
        var subIndex = 0;
        if (_copyCurrentCards[_copyCurrentCards.Count - 1].rank.Id() == (int)CardRank.IDs.Ace 
            && _copyCurrentCards[_copyCurrentCards.Count - 2].rank.Id() == (int)CardRank.IDs.Five)
        {
            subIndex = 2;
        }
        else
        {
            subIndex = 1;
        }
        
        for (int i = 0; i < _copyCurrentCards.Count; i++)
        {
            if (i != _copyCurrentCards.Count - subIndex)
            {
                if (_copyCurrentCards[i].rank.Value() != _copyCurrentCards[i + 1].rank.Value() - 1)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool TwoPair()
    {
        int countPair = 0;
        foreach (var item in _sameRankCount)
        {
            if (item.Value == 2)
            {
                countPair++;
            }
        }

        return (countPair == 2);
    }

    private bool JacksOrBetter()
    {
        foreach (var item in _sameRankCount)
        {
            if (item.Value == 2 && item.Key >= (int)CardRank.Values.Jack)
                return true;
        }

        return false;
    }
    
    
    
}
