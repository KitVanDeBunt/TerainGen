using UnityEngine;
/// <summary>
/// Basic perlin.
/// </summary>
namespace GenFunction.Field
{
	public class BasicPerlin : BaseGenFunction
	{
		[SerializeField]
		private float mult = 10f;
		[Range(0.00001f,1.0f)]
		[SerializeField]
		private float perlinScale = 1.0f;

		public override float GetHeight(float x, float y, int mapX, int mapY){
			x = (float)x/(101f*perlinScale);
			y = (float)y/(101f*perlinScale);
			float tempHeight = UnityEngine.Mathf.PerlinNoise(x,y);
			if(useGen){
				return (tempHeight*mult);
			}else{
				return 0;
			}
		}
		
		public override float GetHeightScale(float x, float y, int mapX, int mapY){
			x = (float)x/((float)mapX*perlinScale);
			y = (float)y/((float)mapY*perlinScale);
			float tempHeight = UnityEngine.Mathf.PerlinNoise(x,y);
			if(useGen){
				return (tempHeight);
			}else{
				return 0;
			}
		}
	}
}

