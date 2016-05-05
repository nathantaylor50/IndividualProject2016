using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
/*
    Various static methods     
*/
namespace RunnerGame
{
    public static class MiscTools
    {
        // Determines if an animator contains a certain parameter, based on a type and a name
        public static bool HasParameterOfType (this Animator self, string name, AnimatorControllerParameterType type)
        {
            var parameters = self.parameters;
            foreach (var currParam in parameters)
            {
                if (currParam.type == type && currParam.name == name)
                {
                    return true;
                }
            }
            return false;
        }

		/// <summary>
		/// Updates the animator bool.
		/// </summary>
		/// <param name="animator">Animator.</param>
		/// <param name="parameterName">Parameter name.</param>
		/// <param name="value">If set to <c>true</c> value.</param>
		public static void UpdateAnimatorBool(Animator animator, string parameterName,bool value)
		{
			if (animator.HasParameterOfType (parameterName, AnimatorControllerParameterType.Bool))
				animator.SetBool(parameterName,value);
		}

		/// <summary>
		/// Triggers an animator trigger.
		/// </summary>
		/// <param name="animator">Animator.</param>
		/// <param name="parameterName">Parameter name.</param>
		/// <param name="value">If set to <c>true</c> value.</param>
		public static void SetAnimatorTrigger(Animator animator, string parameterName)
		{
			if (animator.HasParameterOfType (parameterName, AnimatorControllerParameterType.Trigger))
				animator.SetTrigger(parameterName);
		}

		/// <summary>
		/// Updates the animator float.
		/// </summary>
		/// <param name="animator">Animator.</param>
		/// <param name="parameterName">Parameter name.</param>
		/// <param name="value">Value.</param>
		public static void UpdateAnimatorFloat(Animator animator, string parameterName,float value)
		{
			if (animator.HasParameterOfType (parameterName, AnimatorControllerParameterType.Float))
				animator.SetFloat(parameterName,value);
		}

		/// <summary>
		/// Updates the animator integer.
		/// </summary>
		/// <param name="animator">Animator.</param>
		/// <param name="parameterName">Parameter name.</param>
		/// <param name="value">Value.</param>
		public static void UpdateAnimatorInteger(Animator animator, string parameterName,int value)
		{
			if (animator.HasParameterOfType (parameterName, AnimatorControllerParameterType.Int))
				animator.SetInteger(parameterName,value);
		}	 

		/// <summary>
		/// Moves something from something to something by a specified amount and returns a correspoinding value
		/// </summary>
		/// <returns>The from to.</returns>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		/// <param name="amount">Amount.</param>
		public static float MoveFromTo (float from, float to, float amount) 
		{
			if (from < to) {
				from += amount;
				if (from > to) {
					return to;
				}
			} else {
				from -= amount;
				if (from < to) {
					return to;
				}
			}
			return from;
		}

		/// <summary>
		/// Returns a random vector3 from 2 defined vector3.
		/// </summary>
		/// <returns>The vector3.</returns>
		/// <param name="min">Minimum.</param>
		/// <param name="max">Maximum.</param>
		public static Vector3 RandomVector3(Vector3 minimum, Vector3 maximum)
		{
			return new Vector3(UnityEngine.Random.Range(minimum.x, maximum.x), 
				UnityEngine.Random.Range(minimum.y, maximum.y), 
				UnityEngine.Random.Range(minimum.z, maximum.z));
		}


		/// <summary>
		/// Rotates a point around the given pivot.
		/// </summary>
		/// <returns>The new point position.</returns>
		/// <param name="point">The point to rotate.</param>
		/// <param name="pivot">The pivot's position.</param>
		/// <param name="angles">The angle as a Vector3.</param>
		public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angle) 
		{
			// we get point direction from the point to the pivot
			Vector3 direction = point - pivot; 
			// we rotate the direction
			direction = Quaternion.Euler(angle) * direction; 
			// we determine the rotated point's position
			point = direction + pivot; 
			return point; 
		}
			

    }
}
