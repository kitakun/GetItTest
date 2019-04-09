namespace Voronov.GetItTestApp.Core.Utilities
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Reflection;

	public static class AssemblyFinder
	{
		private const string assemblyPrefix = "Voronov";

		public static void LoadAllAssembliesInDomain()
		{
			List<Assembly> loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
			string[] loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();

			string[] referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
			List<string> toLoad = referencedPaths
				.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)
				&& r.Contains(assemblyPrefix)
				&& !r.EndsWith(".Views.dll")).ToList();
			toLoad.ForEach(path =>
			{
				loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path)));
			});
		}

		/// <summary>
		/// Find all target classes agginable from T
		/// </summary>
		public static IEnumerable<T> FindAllInterfaces<T>()
		{
			var result = new List<T>();

			Assembly[] allAssemblyes = AppDomain.CurrentDomain.GetAssemblies();

			foreach (Assembly asm in allAssemblyes)
			{
				try
				{
					if (asm.ManifestModule.Name.StartsWith($"{assemblyPrefix}."))
					{
						foreach (Type type in asm.GetTypes())
						{
							if ((typeof(T)).IsAssignableFrom(type) && !type.IsAbstract)
							{
								result.Add((T)Activator.CreateInstance(type));
							}
						}
					}
				}
				catch (ReflectionTypeLoadException e)
				{
					var availableTypes = e.Types.Where(t => t != null);
					foreach (Type type in availableTypes)
					{
						if (typeof(T).IsAssignableFrom(type) && !type.IsAbstract)
						{
							result.Add((T)Activator.CreateInstance(type));
						}
					}
				}
			}

			return result;
		}
	}
}
