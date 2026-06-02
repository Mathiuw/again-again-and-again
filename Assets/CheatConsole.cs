using UnityEngine;
namespace MaiNull
{
	public class CheatConsole : MonoBehaviour
	{
		public static CheatConsole Instance;
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void RuntimeInit()
		{
#if DEVELOPMENT_BUILD || UNITY_EDITOR
			LoadConsole();
#endif
		}
		private static void LoadConsole()
		{
			if (Instance != null)
				return;

			var newConsole = new GameObject { name = "[Cheat Console]" };
			Instance = newConsole.AddComponent<CheatConsole>();
			DontDestroyOnLoad(newConsole);

#if USE_INPUT_SYSTEM
            EnhancedTouchSupport.Enable();
#endif
		}
	}
}
