using System.Collections;
using UnityEngine;

namespace RunnerGame
{
	/// <summary>
	/// Moving game object.
	/// </summary>
	public class MovingGameObject : MonoBehaviour
	{
		///
		public float Speed = 0.0f;
		///
		public float Acceleration = 0.0f;

		public Vector3 Direction = Vector3.left;

		protected Vector3 movement;
		protected float initialSpeed;

		/// <summary>
		/// Awake this instance.
		/// </summary>
		protected virtual void Awake()
		{

			initialSpeed = Speed;

		}

		/// <summary>
		/// Raises the enable event.
		/// </summary>
		protected virtual void OnEnable() {
			Speed = initialSpeed;
		}

		/// <summary>
		/// Update this instance.
		/// </summary>
		protected virtual void Update()
		{
			Move ();
		}


		public virtual void Move()
		{
			if (LevelManager.Instance==null)
			{
				movement = Direction * (Speed / 10) * Time.deltaTime;
			}
			else
			{
				movement = Direction * (Speed / 10) * LevelManager.Instance.Speed * Time.deltaTime;
			}
			transform.Translate(movement,Space.World);
			// We apply the acceleration to increase the speed
			Speed += Acceleration * Time.deltaTime;
		}



	}
}

