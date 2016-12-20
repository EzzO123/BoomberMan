using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine.UI;



namespace Completed
{
	public class BOOM : MonoBehaviour {

		public LayerMask fireLayer;	
		public LayerMask blockingLayer;

		public GameObject Fire;
		//public float timeLive = 2;
		public int rangeBoob = 1;

		private GameObject go;
		private Rigidbody2D rb;
		private BoxCollider2D boxCollider; 	
		private SpriteRenderer sprite;

		private List<Object> arrayFireObject = new List <Object>();

		private Text levelText;

		private int tempForStep;
		private int tempForStep2;
	
		void Start () {
			
			boxCollider = GetComponent<BoxCollider2D> ();
			rb = GetComponent<Rigidbody2D>();
			sprite = GetComponent<SpriteRenderer>();
			tempForStep = GameManager.numberStep + 3;
	
		}

		void Update(){
			if (GameManager.numberStep == tempForStep) {
				Invoke ("Boom", 0);
			}

		}

		void Boom(){
			gameObject.SetActive (false);
			RaycastHit2D hit;


			Vector2 start = transform.position;
			Vector2 temp = new Vector2(0,0);

				Instantiate (Fire, start + new Vector2 (0, 0), Quaternion.identity);

				for (int i = 1; i < rangeBoob + 1; i++) {

					for (int x = 0; x < 4; x++) {

						switch (x) {
						case 0:
							temp = new Vector2 (0, i);
							break;
						case 1:	
							temp = new Vector2 (0, -i);
							break;
						case 2:	
							temp = new Vector2 (i, 0);
							break;
						case 3:	
							temp = new Vector2 (-i, 0);

							break;
						}
						boxCollider.enabled = false;

						hit = Physics2D.Linecast (start, start + temp, blockingLayer);

						boxCollider.enabled = true;

						if (hit.transform == null) {
						

							Instantiate (Fire, start + temp, Quaternion.identity);

						}
						
					}
				}

	}
			
	}
}