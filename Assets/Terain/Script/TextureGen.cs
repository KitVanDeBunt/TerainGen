using UnityEngine;
using System.Collections.Generic;
using System.IO;

using GenFunction.Field;

namespace Generator {
	public class TextureGen : BaseGenarator {
		
		[SerializeField]
		private int startX;
		[SerializeField]
		private int startY;
		
		//counters
		private int x;
		private int y;
		private int i;
		
		[SerializeField]
		private int mapX = 100;
		[SerializeField]
		private int mapY = 100;
		
		//private float currentX = 0f;
		//private float currentY = 0f;
		
		//private float[] genDate;
		private float genDataTemp;
		
		[SerializeField]
		private bool debugPrint = false;

		private Texture2D tempTexture;

		[SerializeField]
		private string TextureName = "Generated_Texture";

		private int texWidth = 1024;
		private int texHeight = 1024;

		[SerializeField]
		private Gradient heightGradiant;
		
		//[SerializeField]
		//private GradientAlphaKey gradiantAlphaKey;
		
		//[SerializeField]
		//private GradientColorKey gradiantColorKey;

		public override void Generate () {
			
			// generate vertices
			/*y = x = 0;
			for (y = startY; y < (mapY+startY); y++) {
				for (x = startX; x < (mapX+startX); x++) {
					genDataTemp = 0;
					for (i = 0; i < generators.Length; i++) {
						genDataTemp += generators[i].GetHeight(x,y,mapX,mapY);
					}
					verts.Add(new Vector3(x,genDataTemp,y));
					//Debug.Log( "\n:"+num+":("+x+","+y+")" );
				}
			}*/


			float color;
			tempTexture = new Texture2D(texWidth, texHeight, TextureFormat.RGBA32, false);
			for (x = 0; x < texWidth; x++) {
				for (y = 0; y < texWidth; y++) {
					color = 0f;
					for (i = 0; i < generators.Length; i++) {
						color += generators[i].GetHeightScale(x,y,mapX,mapY);
					}
					color = (color/generators.Length);
					tempTexture.SetPixel(x,y,heightGradiant.Evaluate(color));
				}
			}
			tempTexture.Apply();
			
			byte[] bytes = tempTexture.EncodeToPNG();
			if(Application.isPlaying){
				Destroy(tempTexture);
			}else{
				DestroyImmediate(tempTexture);
			}

			
			File.WriteAllBytes(Application.dataPath + "/Terain/TextureGenOutput/"+TextureName+".png", bytes);
		}
		
		protected float XYtoNum(float xn,float yn){
			return (xn+(yn*(float)mapX));
		}
		
		protected int XYtoNum(int xn,int yn){
			return (xn+(yn*mapX));
		}
	}
}
