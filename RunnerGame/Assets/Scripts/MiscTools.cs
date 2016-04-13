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

        //Updates the animater tool
        public static void updateAnimatorBool(Animator animator, string parameterName, bool value)
        {
            if (animator.HasParameterOfType(parameterName, AnimatorControllerParameterType.Bool))
                animator.SetBool(parameterName, value);
            
        } 

        //update animator float
        public static void UpdateAnimatorFloat(Animator animator, string parameterName, float value)
        {
            if (animator.HasParameterOfType (parameterName, AnimatorControllerParameterType.Float))
            { animator.SetFloat(parameterName, value); }

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
    }
}
