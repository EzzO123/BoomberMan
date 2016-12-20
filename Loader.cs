using UnityEngine;
using System.Collections;

namespace Completed
{	
	public class Loader : MonoBehaviour 
	{
		public GameObject gameManager;			
		public GameObject soundManager;		
		public GameObject playerController;



		void Awake ()
		{
			
			if (GameManager.instance == null)
				
				//Instantiate gameManager prefab
				Instantiate(gameManager);
			

			if (SoundManager.instance == null)
				
				//Instantiate SoundManager prefab
				Instantiate(soundManager);
			

				Instantiate(playerController);
				
				
			
		}
	}
}