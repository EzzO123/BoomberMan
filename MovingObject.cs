using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Completed
{

	public abstract class MovingObject : MonoBehaviour
	{
		public float moveTime = 0.1f;			
		public LayerMask blockingLayer;
		public LayerMask fireLayer;

		private BoxCollider2D boxCollider; 	
		private Rigidbody2D rb2D;
		private Animator animator;


		private float inverseMoveTime;			

		//private Text levelText;

		protected virtual void Start ()
		{


			//levelText = GameObject.Find ("levelText").GetComponent<Text>();
			inverseMoveTime = 1f / moveTime;


		}



		protected bool Move (int xDir, int yDir, out RaycastHit2D hit,BoxCollider2D boxCollider,Rigidbody2D rb2D,Transform transform)
		{

			Vector2 start = transform.position;



			Vector2 end = start + new Vector2 (xDir, yDir);


			boxCollider.enabled = false;


			hit = Physics2D.Linecast (start, end, blockingLayer);



			boxCollider.enabled = true;

		

			if(hit.transform == null)
			{
				
				StartCoroutine (SmoothMovement (end,rb2D,transform));


				return true;
			}


			return false;
		}



		protected IEnumerator SmoothMovement (Vector3 end,Rigidbody2D rb2D,Transform transform)
		{

			float sqrRemainingDistance = (transform.position - end).sqrMagnitude;


			while(sqrRemainingDistance > float.Epsilon)
			{

				Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime*Time.deltaTime*3 );


				rb2D.MovePosition (newPostion);


				sqrRemainingDistance = (transform.position - end).sqrMagnitude;


				yield return null;
			}
		}



		protected virtual void AttemptMove <T> (int xDir, int yDir,Animator animator,
			BoxCollider2D boxCollider,Rigidbody2D rb2D,Transform transform)
			where T : Component
		{

			RaycastHit2D hit;


			bool canMove = Move (xDir, yDir, out hit,boxCollider,rb2D,transform);


			if(hit.transform == null)

				return;

			T hitComponent = hit.transform.GetComponent <T> ();


			if(!canMove && hitComponent != null)


				OnCantMove (hitComponent,animator);
			
		}



		protected abstract void OnCantMove <T> (T component,Animator animator)
			where T : Component;
		
		
	
	}
}