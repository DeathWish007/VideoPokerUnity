using System.Collections.Generic;
using Card;
using UnityEngine;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	/// 
	/// The main game manager
	/// 
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance;
		
		[Header("Script References")]
		public DeckManager deckManager;
		public PayoutDeals payoutDeals;
		public CardDealer cardDealer; 
		
		[Header("GameState")]
		public GameStates gameState = GameStates.Initialize;
		
		[Header("Game Variables")]
		public int currentDealRank = LOSE_HAND;
		public int payTimes =0;
		
		[SerializeField]
		private int balance = 50;
		
		private const int DEAL_SIZE = 5;
		private const int LOSE_HAND = -1;
		
		
		
		private List<Card.Card> _currentDealHands;
		
		
		//-//////////////////////////////////////////////////////////////////////
		/// singleton instance for game manager
		void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(this.gameObject);
			}
			else if(Instance != this)
			{
				Destroy(this.gameObject);
			}
		}

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Start()
		{
			GameStateUpdate(GameStates.Initialize);
		}
		// game flow for different game states
		private void GameStateUpdate(GameStates state)
		{
			gameState = state;

			switch (gameState)
			{
				case GameStates.Initialize:
					//deckManager = DeckManager.Instance;
					UIManager.Instance.SetStarterMessage();
					UIManager.Instance.SetBalanceText(balance.ToString());
					break;
				case GameStates.FirstDeal:
					_currentDealHands = new List<Card.Card>();
					UIManager.Instance.SetActiveBetMultiplierInteract(false);
					deckManager.Reset();
					_currentDealHands = deckManager.DealCards(DEAL_SIZE);
					cardDealer.FirstDealCards(_currentDealHands);
					UIManager.Instance.SetSecondDealMessage();
					balance -= payTimes+1;
					UIManager.Instance.SetBalanceText(balance.ToString());
					break;
				case GameStates.SecondDeal:
					UIManager.Instance.SetActiveBetMultiplierInteract(true);
					_currentDealHands = deckManager.SetReplacementHand(_currentDealHands);
					cardDealer.UpdateSecondDealCardSprite(_currentDealHands);
					cardDealer.InActiveDeckCards();
					DealEvaluator evaluator = new DealEvaluator();
					currentDealRank = evaluator.DealScore(_currentDealHands);
					GameStateUpdate(currentDealRank == LOSE_HAND ? GameStates.Lost : GameStates.Win);
					break;
				case GameStates.Win:
					var amount = payoutDeals.PayoutAmount[currentDealRank];
					UIManager.Instance.SetWinCreditsText(amount[payTimes].ToString());
					balance += amount[payTimes];
					UIManager.Instance.SetBalanceText(balance.ToString());
					break;
				case GameStates.Lost:
					UIManager.Instance.SetLostMessage();
					break;
			}
			
		}
		// game state changer
		public void GameStateChanger()
		{
			switch (gameState)
			{
				case GameStates.Initialize:
					GameStateUpdate(GameStates.FirstDeal);
					break;
				case GameStates.FirstDeal:
					GameStateUpdate(GameStates.SecondDeal);
					break;
				case GameStates.Win:
				case GameStates.Lost:
					GameStateUpdate(GameStates.FirstDeal);
					break;
			}
		}

		public int GetDalSize()
		{
			return DEAL_SIZE;
		}

		public int GetBalance()
		{
			return balance;
		}
	}
}