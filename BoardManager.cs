using UnityEngine;
using System;
using System.Collections.Generic; 		
using Random = UnityEngine.Random; 		

namespace Completed
	
{
	
	public class BoardManager : MonoBehaviour
	{
		
		[Serializable]
		public class Count
		{
			public int minimum; 			//Minimum value for our Count class.
			public int maximum; 			//Maximum value for our Count class.
			

			public Count (int min, int max)
			{
				minimum = min;
				maximum = max;
			}
		}	
		
		public int columns = 8; 										
		public int rows = 8;											
		public Count wallCount = new Count (10, 11);						

		public GameObject Player1;
		public GameObject Player2;										
		public GameObject[] floorTiles;								
		public GameObject[] wallTiles;									
									
										
		public GameObject[] outerWallTiles;								
		
		private Transform boardHolder;									
		private List <Vector3> gridPositions = new List <Vector3> ();	
	
		void InitialiseList ()
		{
			gridPositions.Clear ();

			for(int x = 1; x < columns-1; x++)
			{
		
				for(int y = 1; y < rows-1; y++)
				{

					gridPositions.Add (new Vector3(x, y, 0f));
				}
			}
		}

		void BoardSetup ()
		{
			
			boardHolder = new GameObject ("Board").transform;
		
			for(int x = -1; x < columns + 1; x++)
			{
				
				for(int y = -1; y < rows + 1; y++)
				{
					
					GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];
					

					if(x == -1 || x == columns || y == -1 || y == rows)
						toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];
					

					GameObject instance =
						Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
					

					instance.transform.SetParent (boardHolder);
				}
			}
		}
		
		

		Vector3 RandomPosition ()
		{
			
			int randomIndex = Random.Range (0, gridPositions.Count);
			
		
			Vector3 randomPosition = gridPositions[randomIndex];
			

			gridPositions.RemoveAt (randomIndex);
			

			return randomPosition;
		}
		
		

		void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
		{
			
			int objectCount = Random.Range (minimum, maximum+1);;

			for(int i = 0; i < objectCount; i++)
			{
				
				Vector3 randomPosition = RandomPosition();
				

				GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];

				Instantiate(tileChoice, randomPosition, Quaternion.identity);
			}
		}
		
		

		public void SetupScene ()
		{

			BoardSetup ();
			

			InitialiseList ();
			

			LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);


		}
	}
}
