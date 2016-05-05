using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RunnerGame
{
	[RequireComponent(typeof(Rect))]
	[RequireComponent(typeof(CanvasGroup))]
	/// <summary>
	/// detects press down, up and ongoing press for GUI buttons
	/// </summary>
	public class MobileButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		///possible actions
		public enum TouchActions { MainAction, Pause, Left, Right, Up, Down }
		///
		public TouchActions TouchBinding;
		///
		public bool SendDownAction = true;
		///
		public bool SendUpAction = true;
		///
		public bool SendOngoingAction = false;

		protected bool buttonPressed = false;
		protected string stringDownAction;
		protected string stringUpAction;
		protected string stringPressingAction;
	
		/// <summary>
		/// Start this instance.
		/// </summary>
		protected void Start()
		{
			//turn the canvas group inactive (invisible)
			CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
			canvasGroup.alpha = 0.0f;

			//set method for each possible binding
			switch (TouchBinding) {
			case TouchActions.MainAction:
				stringDownAction = "MainActionButtonDown";
				stringUpAction = "MainActionButtonUp";
				stringPressingAction = "MainActionButtonPressing";
				break;
			case TouchActions.Pause:
				stringDownAction = "PauseButtonDown";
				stringUpAction = "PauseButtonUp";
				stringPressingAction = "PauseButtonPressing";
				break;
			case TouchActions.Left:
				stringDownAction = "LeftButtonDown";
				stringUpAction = "LeftButtonUp";
				stringPressingAction = "LeftButtonPressing";
				break;
			case TouchActions.Right:
				stringDownAction = "RightButtonDown";
				stringUpAction = "RightButtonUp";
				stringPressingAction = "RightButtonPressing";
				break;
			case TouchActions.Up:
				stringDownAction = "UpButtonDown";
				stringUpAction = "UpButtonUp";
				stringPressingAction = "UpButtonPressing";
				break;
			case TouchActions.Down:
				stringDownAction = "DownButtonDown";
				stringUpAction = "DownButtonUp";
				stringPressingAction = "DownButtonPressing";
				break;
			}

			if (!SendDownAction) {
				stringDownAction = null;
			}
			if (!SendUpAction) {
				stringUpAction = null;
			}
			if (!SendOngoingAction) {
				stringPressingAction = null;
			}
		}
		/// <summary>
		/// on every frame chheck if buttons are pressed
		/// to detect continuous press then trigger the stringPressingAction method
		/// </summary>
		protected void Update()
		{	
			if (buttonPressed) { OnPointerPressing ();}

		}

		/// <summary>
		/// Raises the down pointer event.
		/// </summary>
		/// <param name="eventData">Event data.</param>
		public void OnPointerDown(PointerEventData eventData)
		{
			buttonPressed = true;
			if (stringDownAction != null) {
				InputManager.Instance.SendMessage (stringDownAction);
			}
		}

		/// <summary>
		/// Raises the up pointer event.
		/// </summary>
		/// <param name="eventData">Event data.</param>
		public void OnPointerUp (PointerEventData eventData)
		{
			buttonPressed = false;
			if (stringUpAction != null) {
				InputManager.Instance.SendMessage (stringUpAction);
			}
		}

		/// <summary>
		/// Raises the pressing pointer event.
		/// </summary>
		public void OnPointerPressing()
		{
			if (stringPressingAction != null) {
				InputManager.Instance.SendMessage (stringPressingAction);
			}
		}

	}
}
