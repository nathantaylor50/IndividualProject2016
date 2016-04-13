using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace RunnerGame
{
	/// <summary>
	/// Event manager.
	/// </summary>
	public class EventManager
	{
		/// <summary>
		/// The event dictionary.
		/// </summary>
		private static Dictionary <string, UnityEvent> eventDictionary;

		/// <summary>
		/// Init this instance.
		/// </summary>
		private static void init()
		{
			if (eventDictionary == null) {
				eventDictionary = new Dictionary<string, UnityEvent> ();
			}
		}

		public static void StartListener (string eventName,  UnityAction listener)
		{
			init ();
			UnityEvent thisEvent = null;
			if (eventDictionary.TryGetValue (eventName, out thisEvent)) {
				thisEvent.AddListener (listener);
			} else {
				thisEvent = new UnityEvent ();
				thisEvent.AddListener (listener);
				eventDictionary.Add (eventName, thisEvent);
			}
		}

		public static void StopListener (string eventName, UnityAction listener)
		{
			init ();
			UnityEvent thisEvent = null;
			if (eventDictionary.TryGetValue (eventName, out thisEvent)) {
				thisEvent.RemoveListener (listener);
			}
		}

		public static void StartEvent (string eventName)
		{
			init ();
			UnityEvent thisEvent = null;
			if (eventDictionary.TryGetValue (eventName, out thisEvent))
			{
				thisEvent.Invoke();
			}
		}
	}
}

