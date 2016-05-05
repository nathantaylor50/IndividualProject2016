using System.Collections;
using UnityEngine;

namespace RunnerGame
{
	/// <summary>
	/// Parallax scrolling.
	/// </summary>
	public class ParallaxScrolling : ObjectBounds
	{
		/// speed
		public float ParallaxSpeed = 0.0f;

		protected GameObject clone;
		protected Vector3 movement;
		protected Vector3 initPos;
		protected float width;

		/// <summary>
		/// Start this instance.
		/// </summary>
		protected virtual void Start()
		{
			width = GetObjectBounds ().size.x;
			initPos = transform.position;

			//
			clone = (GameObject)Instantiate(gameObject, new Vector3(transform.position.x  +width, transform.position.y, transform.position.z), transform.rotation);
			//remove the parallax component from the clone to prevent a infinite loop
			ParallaxScrolling ParaComponent = clone.GetComponent<ParallaxScrolling> ();
			Destroy (ParaComponent);
		}


		/// <summary>
		/// Update this instance.
		/// </summary>
		protected virtual void Update(){
			//
			if (LevelManager.Instance != null) {
				movement = Vector3.left * (ParallaxSpeed / 10) * LevelManager.Instance.Speed * Time.deltaTime;
			} else {
				movement = Vector3.left * (ParallaxSpeed / 10) * Time.deltaTime;
			}

			//move both objects
			clone.transform.Translate(movement);
			transform.Translate (movement);

			//
			if (transform.position.x + width < initPos.x) {
				transform.Translate (Vector3.right * width);
				clone.transform.Translate (Vector3.right * width);
			}
		}
	}
}

