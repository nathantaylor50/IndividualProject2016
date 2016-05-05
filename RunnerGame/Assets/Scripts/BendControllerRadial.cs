using UnityEngine;
using System.Collections;

[ExecuteInEditMode] //runs in edit mode
/// <summary>
/// Bend controller radial.
/// class for controlling the bending shader effects
/// </summary>
public class BendControllerRadial : MonoBehaviour 
{
	[SerializeField] bool b_bendOn = true;

	[SerializeField] Transform b_curveOrigin;
	[SerializeField] Transform b_referenceDirection;
	[SerializeField] float b_curvature = 0f;
	
	[Range(0.5f, 2f)]
	[SerializeField] float b_xScale = 1f;
	[Range(0.5f, 2f)]
	[SerializeField] float b_zScale = 1f;
	
	[SerializeField] float b_flatMargin = 0f;

	//strings into IDs
	private int b_curveOriginId;
	private int b_referenceDirectionId;
	private int b_curvatureId;
	private int b_scaleId;
	private int b_flatMarginId;

	private Vector3 b_scale = Vector3.zero;
	
	/// <summary>
	/// for every property in the shader that needs to change we convert to IDs
	/// </summary>
	void Start() 
	{
		b_curveOriginId = Shader.PropertyToID("_CurveOrigin");
		b_referenceDirectionId = Shader.PropertyToID("_ReferenceDirection");
		b_curvatureId = Shader.PropertyToID("_Curvature");
		b_scaleId = Shader.PropertyToID("_Scale");
		b_flatMarginId = Shader.PropertyToID("_FlatMargin");

		
		if (b_curveOrigin == null)
			SetCurveOrigin(false);
	}
	
	/// <summary>
	/// Update the shader values on every frame
	/// allows real time movement of the curve origin
	/// </summary>
	void Update() 
	{
		///turns the floating points into a vector which is more efficient for the shader
		b_scale.x = b_xScale;
		b_scale.z = b_zScale;


		if (b_bendOn)
			Shader.EnableKeyword("BEND_ON");
		else
			Shader.DisableKeyword("BEND_ON");

		Shader.SetGlobalVector(b_curveOriginId, b_curveOrigin.position);
		Shader.SetGlobalVector(b_referenceDirectionId, b_referenceDirection.forward);
		Shader.SetGlobalFloat(b_curvatureId, b_curvature * 0.00001f);
		Shader.SetGlobalVector(b_scaleId, b_scale);
		Shader.SetGlobalFloat(b_flatMarginId, b_flatMargin);
	}
	

	
	private void SetCurveOrigin(bool freeCamera)
	{

	}
	
	
	private void OnEnable()
	{
		
	}
	
	
	private void OnDisable()
	{
		Shader.SetGlobalVector(b_curveOriginId, Vector3.zero);
		Shader.SetGlobalFloat(b_curvatureId, 0);
	}
}
