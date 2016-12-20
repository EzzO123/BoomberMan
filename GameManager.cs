using UnityEngine;
using System.Collections;

namespace Completed
{
	using System.Collections.Generic;		 
	using UnityEngine.UI;

	public class GameManager : MonoBehaviour
	{
		public float levelStartDelay = 1f;						
		public float turnDelay = 0.1f;	

		internal static int RoundPayerOne ;
		internal static int RoundPayerTwo ;

		public static GameManager instance = null;				
		[HideInInspector] public bool playersTurn = true;		
		
		private Text RoundTextP1;
		private Text RoundTextP2;
		private Text MainRoundText;
		private GameObject RoundImage;							
		private BoardManager boardScript;
														
		private bool doingSetup = true;							

		internal static int numberStep = 0;

		void Start(){

		}

		void Awake()
		{
			
			
			if (instance == null)
				

				instance = this;
			

			else if (instance != this)
				

				Destroy(gameObject);	
			

			DontDestroyOnLoad(gameObject);
			

			

			boardScript = GetComponent<BoardManager>();

			InitGame();
		}
		

		public void OnLevelWasLoaded()
		{
			
			InitGame();

		}


		public void InitGame()
		{
			
			doingSetup = true;


			RoundImage = GameObject.Find ("RoundImage");
			MainRoundText = GameObject.Find("MainRoundText").GetComponent<Text>();
			RoundTextP1 = GameObject.Find("RoundTextP1").GetComponent<Text>();
			RoundTextP2 = GameObject.Find("RoundTextP2").GetComponent<Text>();

			RoundImage.SetActive(true);

			MainRoundText.text = " Fight ";
			RoundTextP1.text = "Player One-> " + RoundPayerOne ;
			RoundTextP2.text = RoundPayerTwo + " <-Player Two";

			Invoke("HideRoundImage", levelStartDelay);

			boardScript.SetupScene();
			
		}

		void HideRoundImage()
		{
			
			RoundImage.SetActive(false);
			

			doingSetup = false;
		}
		
	
		void Update()
		{
		
			if(playersTurn  || doingSetup)
				
				return;
			
			Invoke("FixedMove",0.1f);
		}
		

		public void GameOver(bool who)
		{
			RoundImage.SetActive(true);
			RoundTextP1.text = "";
			RoundTextP2.text = "";
			if (who) {
				
				MainRoundText.text = " Win: ->Player Two<-" ;
				
			} else {
				
				MainRoundText.text = " Win: ->Player One<-";

			}

		}

		public void FixedMove(){
		
			playersTurn = true;
		
		}
	
	}
}

