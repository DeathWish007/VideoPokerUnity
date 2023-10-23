using UnityEngine;
using UnityEngine.UI;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	///
	/// Manages UI including button events and updates to text fields
	/// 
	public class UIManager : MonoBehaviour
	{
		public static UIManager Instance;

		private int _betMultiplier;
		
		//text variables
		[Header("Button and Text Variables ")]
		[SerializeField]
		private Text currentBalanceText = null;

		[SerializeField]
		private Text winningText = null;

		[SerializeField]
		private Button betButton = null;

		[SerializeField] 
		private Text betMultiplierText;

		[SerializeField] 
		private Button betMultiplierButton;
		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
		}

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Start()
		{
			_betMultiplier = 1;
			betMultiplierText.text = _betMultiplier.ToString();
			betButton.onClick.RemoveAllListeners();
			betButton.onClick.AddListener(OnBetButtonPressed);
			betMultiplierButton.onClick.RemoveAllListeners();
			betMultiplierButton.onClick.AddListener(OnBetMultiplierPressed);
		}

		//-//////////////////////////////////////////////////////////////////////
		///
		/// Event that triggers when bet button is pressed
		//function to change the game states
		private void OnBetButtonPressed()
		{
			if (GameManager.Instance.GetBalance() - _betMultiplier < 0 && GameManager.Instance.gameState != GameStates.FirstDeal)
			{
				SetErrorMessage("Out of credits");
				GameManager.Instance.gameState = GameStates.Initialize;
			}
			else
			{
				GameManager.Instance.GameStateChanger();
			}
		}
		// Managing the bet multiplier 
		private void OnBetMultiplierPressed()
		{
			_betMultiplier += 1;
			if (_betMultiplier > 5)
			{
				_betMultiplier = 1;
			}

			GameManager.Instance.payTimes = _betMultiplier-1;
			betMultiplierText.text = _betMultiplier.ToString();
		}
		//Maintain the balance text
		public void SetBalanceText(string message)
		{
			currentBalanceText.text = "Balance: " + message + " credits";
		}
		//Intro message
		public void SetStarterMessage()
		{
			winningText.text = " Press Bet Button to start a game";
		}
		//Second deal message
		public void SetSecondDealMessage()
		{
			winningText.text = "Select any card to hold and make a second deal";
		}
		// Win message
		public void SetWinCreditsText(string message)
		{
			winningText.text = "Jacks or Better! You won " + message + " credits.";
		}
		//lose message
		public void SetLostMessage()
		{
			winningText.text = " You lost your bet. Click bet to start again";
		}
		//default error message template
		public void SetErrorMessage(string message)
		{
			winningText.text = message;
		}
		//bet multiplier button interact options
		public void SetActiveBetMultiplierInteract(bool flag)
		{
			betMultiplierButton.interactable = flag;
		}

		
		
	}
}