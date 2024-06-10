using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Miro.Binder
{
	public class BindContainer
	{
		private static readonly Dictionary<Type, ISingleton> container = new();
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		public static void CreateBinderInstance()
		{
			Application.targetFrameRate = 160;
			
			var go = new GameObject("[Binder Group]");
			Object.DontDestroyOnLoad(go);
			
			var types = AppDomain.CurrentDomain.GetAssemblies()
			                              .SelectMany(x => x.GetTypes())
			                              .Where(x => x.FullName != null
			                                          && typeof(ISingleton).IsAssignableFrom(x)
			                                          && typeof(MonoBehaviour).IsAssignableFrom(x)
			                                          && !x.IsInterface
			                                          && !x.IsAbstract);

			foreach (var type in types)
				container.Add(type, go.AddComponent(type) as ISingleton);
		}

		public static T Get<T>() where T : ISingleton => (T)container[typeof(T)];
	}
} 