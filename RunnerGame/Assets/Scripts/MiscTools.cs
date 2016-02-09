using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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
    }
}
