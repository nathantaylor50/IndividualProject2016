using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace RunnerGame
{
	/// <summary>
	/// Object bounds.
	/// </summary>
	public class ObjectBounds : MonoBehaviour
	{
		public enum DifferentBounds { Collider, Renderer, Undefined }
		public DifferentBounds BoundsDefined;
		public Vector3 Size { get; set; }


		/// <summary>
		/// Reset this instance.
		/// </summary>
		protected virtual void Reset()
		{
			DefineBoundsFound ();
		}

		/// <summary>
		/// Defines the bounds found.
		/// </summary>
		protected virtual void DefineBoundsFound()
		{
			BoundsDefined = DifferentBounds.Undefined;
			if (GetComponent<Renderer> () != null) {
				BoundsDefined = DifferentBounds.Renderer;
			}
			if (GetComponent<Collider> () != null) {
				BoundsDefined = DifferentBounds.Collider;
			}
		}

		/// <summary>
		/// Gets the object bounds.
		/// </summary>
		/// <returns>The object bounds.</returns>
		public virtual Bounds GetObjectBounds()
		{
			if (BoundsDefined == DifferentBounds.Renderer) {
				if (GetComponent<Renderer> () == null) {
					throw new Exception("The PoolableObject " + gameObject.name + " is set as having Renderer based bounds but no Renderer component can be found.");
				}
				return GetComponent<Renderer> ().bounds;
			}

			if (BoundsDefined == DifferentBounds.Collider) {
				if (GetComponent<Collider> () == null) {
					throw new Exception("The PoolableObject "+gameObject.name+" is set as having Collider based bounds but no Collider component can be found.");
				}
				return GetComponent<Collider> ().bounds;
			}

			return new Bounds (Vector3.zero, Vector3.zero);
		}

	}
}
