namespace Terain.Gen.Field
{
	public abstract class Gen : UnityEngine.MonoBehaviour
	{
		[UnityEngine.SerializeField]
		protected bool useGen = true;

		public abstract float GetHeight(float x, float y, int mapX, int mapY);
	}
}

