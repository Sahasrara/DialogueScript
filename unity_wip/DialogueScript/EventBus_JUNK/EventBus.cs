// using System.Collections.Concurrent;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;
// using UnityEngine.LowLevel;
// using UnityEngine.PlayerLoop;
//
// namespace DialogueScript
// {
//     public class EventBus
//     {
//         #region Private Variables
//         private static ConcurrentQueue<string> s_EventQueue;
//         private static HashSet<EventHandler> s_EventHandlers;
//         #endregion
//
//         #region Initialization
//         [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
//         private static void Initialize()
//         {
//             // Setup queues
//             s_EventQueue = new();
//             s_EventHandlers = new();
//
//             // Register self as custom player loop
//             PlayerLoopSystem defaultPlayerLoop = PlayerLoop.GetDefaultPlayerLoop();
//             PlayerLoopSystem[] originalSubsystems = defaultPlayerLoop.subSystemList;
//             PlayerLoopSystem[] newSubsystems = new PlayerLoopSystem[originalSubsystems.Length + 1];
//             for (int originalIndex = 0, newIndex = 0; newIndex < newSubsystems.Length; newIndex++, originalIndex++)
//             {
//                 if (originalSubsystems[originalIndex].type == typeof(Update))
//                 {
//                     // Insert "Update"
//                     newSubsystems[newIndex] = originalSubsystems[originalIndex];
//
//                     // Insert this after "Update"
//                     newSubsystems[newIndex++] = new PlayerLoopSystem
//                     {
//                         type = typeof(EventBus),
//                         updateDelegate = Tick,
//                     };
//                 }
//                 else
//                 {
//                     newSubsystems[newIndex] = originalSubsystems[originalIndex];
//                 }
//             }
//             defaultPlayerLoop.subSystemList = newSubsystems;
//             PlayerLoop.SetPlayerLoop(defaultPlayerLoop);
//         }
//         #endregion
//
//         #region Tick
//         private static void Tick()
//         {
//             for (EventHandler handler in s_EventHandlers)
//
//         }
//         #endregion
//
//         #region Public API
//         public delegate void EventHandler(string @event);
//         public static void Unsubscribe(EventHandler handler) => s_EventHandlers.Remove(handler);
//         public static void Subscribe(EventHandler handler) => s_EventHandlers.Add(handler);
//         public static void Raise(string @event) => s_EventQueue.Enqueue(@event);
//         #endregion
//     }
// }