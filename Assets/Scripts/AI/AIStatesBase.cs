namespace Assets.Scripts.AI
{
	public abstract class AIStatesBase<T>
	{
		public abstract T currentState { get; protected set; }

		public void ChangeState(T newState){
			currentState = newState;
		}
	}

}