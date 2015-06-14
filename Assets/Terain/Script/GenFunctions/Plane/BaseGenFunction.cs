namespace GenFunction.Field
{
	public abstract class BaseGenFunction : UnityEngine.MonoBehaviour
	{
		[UnityEngine.SerializeField]
		protected bool useGen = true;

		public abstract float GetHeight(float x, float y, int mapX, int mapY);

		public abstract float GetHeightScale(float x, float y, int mapX, int mapY);
	}
}

