using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace RunnerGame
{
	/// <summary>
	/// Event manager for handling various events
	/// invoke these methods to tell other classes the current event
	/// </summary>
	public class EventManager
	{
		/// <summary>
		/// The event dictionary. which represents a collection of strings 
		/// and events
		/// </summary>
		private static Dictionary <string, UnityEvent> eventDictionary;

		/// <summary>
		/// Initialize this instance.
		/// if not dictionary is present make a new dictionary
		/// </summary>
		private static void init()
		{
			if (eventDictionary == null) {
				eventDictionary = new Dictionary<string, UnityEvent> ();
			}
		}
		/// <summary>
		/// Starts the listener.
		/// </summary>
		/// <param name="eventName">Event name.</param>
		/// <param name="listener">Listener.</param>
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
		/// <summary>
		/// Stops the listener.
		/// </summary>
		/// <param name="eventName">Event name.</param>
		/// <param name="listener">Listener.</param>
		public static void StopListener (string eventName, UnityAction listener)
		{
			init ();
			UnityEvent thisEvent = null;
			if (eventDictionary.TryGetValue (eventName, out thisEvent)) {
				thisEvent.RemoveListener (listener);
			}
		}
		/// <summary>
		/// Starts the event.
		/// </summary>
		/// <param name="eventName">Event name.</param>
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

