using UnityEngine;
using System.Collections;
namespace Completed{
	public class Fire : MonoBehaviour {

		private int tempForStep;
		// Use this for initialization
		void Start () {

			tempForStep = GameManager.numberStep + 3;
		
		}
		
		// Update is called once per frame
		void Update () {

			if (GameManager.numberStep == tempForStep) {

				Invoke ("Destroy", 0);
			}
		
		}
		void Destroy(){

			gameObject.SetActive (false);



		}
	}
}