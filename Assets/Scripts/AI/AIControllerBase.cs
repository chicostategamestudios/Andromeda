using UnityEngine;

namespace Assets.Scripts.AI
{

	public abstract class AIControllerBase: AICustomComponentBase
	{
		protected override void Awake()
		{
			base.Awake ();

		}

		void InitializeCustomComponents()
		{
			AI.AICustomComponentBase[] components = GetComponents<AI.AICustomComponentBase> ();
			foreach (AI.AICustomComponentBase component in components) {
				component.Load (_parent.gameObject);
			}

		}

		public T GetCustomComponent<T>()
		{
			return GetComponent<T> ();
		}

	}
}