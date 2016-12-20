using UnityEngine;
using System.Collections;
using UnityEngine.UI;	//Allows us to use UI.
using UnityEngine.SceneManagement;
namespace Completed
{
	
	public class Player : MovingObject
	{
		public float restartRoundDelay = 1f;		//Delay time in seconds to restart level.



		public static Player instance = null;	

		public int wallDamage = 2;					//How much damage a player does to a wall when chopping it.
						
		public AudioClip moveSound1;				//1 of 2 Audio clips to play when player moves.
		public AudioClip moveSound2;				//2 of 2 Audio clips to play when player moves.
		public AudioClip eatSound1;					//1 of 2 Audio clips to play when player collects a food object.
		public AudioClip eatSound2;					//2 of 2 Audio clips to play when player collects a food object.
		public AudioClip drinkSound1;				//1 of 2 Audio clips to play when player collects a soda object.
		public AudioClip drinkSound2;				//2 of 2 Audio clips to play when player collects a soda object.
		public AudioClip gameOverSound;				//Audio clip to play when player dies.

		public GameObject Boomb;

		private Text GO;
	

		private GameObject PlayerOne;
		private GameObject PlayerTwo;


		private BoxCollider2D boxColliderPlayerOne;
		//CharacterController playerOne;
		private Rigidbody2D rb2DPlayerOne;				

		private Animator animatorPlayerOne;


		private Transform playerOneTransform;


		private BoxCollider2D boxColliderPlayerTwo;
		//CharacterController playerTwo;
		private Rigidbody2D rb2DPlayerTwo;				

		private Animator animatorPlayerTwo;

		private Transform playerTwoTransform;
		//RaycastHit2D hitPlayerOne ;
		 
		private Text RoundTextP1;
		private Text RoundTextP2;
		private Text MainRoundText;
		private GameObject RoundImage;

				
		private bool Step =true;
		//private Vector2 touchOrigin = -Vector2.one;	//Used to store location of screen touch origin for mobile controls.


		protected override void Start ()
		{
			PlayerOne = GameObject.Find("Player1");
			PlayerTwo = GameObject.Find ("Player2");

			GameManager.numberStep = 0;

			
			animatorPlayerOne = PlayerOne.GetComponent<Animator>();
			boxColliderPlayerOne = PlayerOne.GetComponent <BoxCollider2D> ();
			rb2DPlayerOne = PlayerOne.GetComponent <Rigidbody2D> ();
			playerOneTransform = PlayerOne.transform;


			animatorPlayerTwo = PlayerTwo.GetComponent<Animator>();
	
			boxColliderPlayerTwo = PlayerTwo.GetComponent <BoxCollider2D> ();
			rb2DPlayerTwo = PlayerTwo.GetComponent <Rigidbody2D> ();
			playerTwoTransform = PlayerTwo.transform;
		
		
		
			RoundImage = GameObject.Find ("RoundImage");
			MainRoundText = GameObject.Find("MainRoundText").GetComponent<Text>();
			RoundTextP1 = GameObject.Find("RoundTextP1").GetComponent<Text>();
			RoundTextP2 = GameObject.Find("RoundTextP2").GetComponent<Text>();
		
			     
			GO = GameObject.Find ("Go!").GetComponent<Text>();

			base.Start ();


		}
		

		private void Update ()
		{

			changeGO ();

			if (!GameManager.instance.playersTurn)
				return;

			if (PlayerOne.tag == "Player1" && Step) {
				
				if (Input.GetKey ("w") || Input.GetKey ("a") || Input.GetKey ("s") || Input.GetKey ("d")) {
								
					int horizontal = 0;  
					int vertical = 0;	

					Step = false;

					horizontal = (int)(Input.GetAxisRaw ("Horizontal"));
						
					vertical = (int)(Input.GetAxisRaw ("Vertical"));
			
			
					if (horizontal != 0) {
						vertical = 0;
					}
			
									
					if (horizontal != 0 || vertical != 0) {
				
						//This place code Woking

						AttemptMove<Wall> (horizontal, vertical, animatorPlayerOne, boxColliderPlayerOne, rb2DPlayerOne, playerOneTransform);
						GameManager.numberStep++;
					}
				}

				if (Input.GetKey (KeyCode.Space)) {
				
					Instantiate (Boomb, playerOneTransform.transform.position, Quaternion.identity);

				}
			}
		
			if (PlayerTwo.tag == "Player2" && !Step) {
				
				if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.LeftArrow)
				     || Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.RightArrow)) {


					Step = true;	

					int horizontal = 0;  
					int vertical = 0;	


					horizontal = (int)(Input.GetAxisRaw ("Horizontal"));


					vertical = (int)(Input.GetAxisRaw ("Vertical"));


					if (horizontal != 0) {
						vertical = 0;
					}


					if (horizontal != 0 || vertical != 0) {
						GameManager.instance.playersTurn = true;

						AttemptMove<Wall> (horizontal, vertical, animatorPlayerTwo, boxColliderPlayerTwo, rb2DPlayerTwo, playerTwoTransform);
						GameManager.numberStep++;
					}
				}

				if (Input.GetKey (KeyCode.RightControl)) {

					Instantiate (Boomb, playerTwoTransform.transform.position, Quaternion.identity);

				}
			}

			if (boxColliderPlayerOne.IsTouchingLayers (fireLayer) == true) {

				finishRound (true);

			}
			if (boxColliderPlayerTwo.IsTouchingLayers (fireLayer) == true) {
				

				finishRound (false);

			}
			
		}	
		

		protected override  void AttemptMove <T> (int xDir, int yDir,Animator animator,
			BoxCollider2D boxCollider,Rigidbody2D rb2D,Transform transform)

		{

			base.AttemptMove <T>(xDir, yDir, animator, boxCollider, rb2D,transform);

			RaycastHit2D hit;


			if (Move (xDir, yDir, out hit,boxCollider,rb2D,transform)) 
			{
				
				SoundManager.instance.RandomizeSfx (moveSound1, moveSound2);
			}


			GameManager.instance.playersTurn = false;
		}


		protected override void OnCantMove <T> (T component,Animator animator)
			
		{
			animator.SetTrigger ("playerChop");
			
			Wall hitWall = component as Wall;



			hitWall.DamageWall (wallDamage);
			


		}


		private void Restart ()
		{
			
			SceneManager.LoadScene (0);
		}
		

		private void finishRound (bool who)
		{
			if (who) {
				
				PlayerOne.SetActive (false);

				SoundManager.instance.PlaySingle (gameOverSound);
				GameManager.RoundPayerTwo++;

				if (GameManager.RoundPayerTwo == 3) {

					GameManager.instance.GameOver (true);
						
				} else
					Invoke ("Restart", restartRoundDelay);
			}

			else {

				PlayerTwo.SetActive (false);

				SoundManager.instance.PlaySingle (gameOverSound);
				GameManager.RoundPayerOne ++;

				if(GameManager.RoundPayerOne  == 3){

					GameManager.instance.GameOver (false);

					}else{
						Invoke ("Restart", restartRoundDelay);
					}
			}

		}

		internal  void changeGO(){

			if (GameManager.numberStep % 2 == 0) {

				GO.color = new Color (0, 170, 0,255);//green

			} else {

				GO.color = new Color (255, 0, 0);//red
			
			}

		}
	}
}

