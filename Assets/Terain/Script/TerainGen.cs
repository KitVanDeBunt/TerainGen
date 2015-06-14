using UnityEngine;
using System.Collections.Generic;
using GenFunction.Field;


namespace Generator {
	[RequireComponent(typeof(MeshRenderer),typeof(MeshFilter))]
	public class TerainGen : BaseGenarator {

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

		[SerializeField]
		private float mDlo = 12f;

		//private float currentX = 0f;
		//private float currentY = 0f;

		//private float[] genDate;
		private float genDataTemp;

		[SerializeField]
		private bool debugPrint = false;

		
		public int StartY {
			get {
				return startY;
			}
			set {
				startY = value;
			}
		}
		
		public int StartX {
			get {
				return startX;
			}
			set {
				startX = value;
			}
		}
		public int MapY {
			get {
				return mapY;
			}
			set {
				mapY = value;
			}
		}

		public int MapX {
			get {
				return mapX;
			}
			set {
				mapX = value;
			}
		}


		public void Start(){
			//InvokeRepeating("Generate",0.2f,0.2f);
		}

		public override void Generate () {
			Mesh mesh = new Mesh();
			List<Vector3> verts = new List<Vector3>();
			List<Vector2> uvs = new List<Vector2>();
			List<int> tris = new List<int>();

			// generate vertices
			y = x = 0;
			for (y = startY; y < (mapY+startY); y++) {
				for (x = startX; x < (mapX+startX); x++) {
					genDataTemp = 0;
					for (i = 0; i < generators.Length; i++) {
						genDataTemp += generators[i].GetHeight(x,y,mapX,mapY);
					}
					verts.Add(new Vector3(x,genDataTemp,y));
					//Debug.Log( "\n:"+num+":("+x+","+y+")" );
				}
			}
			
			//generate triangles
			y = x = 0;
			for (y = 0; y < (mapY-1); y++) {
				for (x = 0; x < (mapX-1); x++) {
					int num = XYtoNum(x,y);
					
					tris.Add(num+mapX);
					tris.Add(num+1);
					tris.Add(num);

					tris.Add(num+mapX+1);
					tris.Add(num+1);
					tris.Add(num+mapX);
					//Debug.Log( "\n:"+num+":("+x+","+y+")" );
				}
			}
			
			//generate uvs
			y = x = 0;
			for (y = 0; y < mapY; y++) {
				if(debugPrint)Debug.Log(y+" - "+((float)y%mDlo));
				for (x = 0; x < mapX; x++) {
					int count = (int)((y*mDlo)+x);
					genDataTemp = 0;
					for (i = 0; i < generators.Length; i++) {
						genDataTemp += generators[i].GetHeight((x+startX),(y+startY),mapX,mapY);
					}
					
					//genDataTemp = verts[count].y;
					float mapNum = 0;
					if(genDataTemp>35f){
						uvs.Add(new Vector2((((float)x%2)/2f)+0.5f,(((float)y%2)/2f)+0.5f));
						mapNum = 3;
						//uvs.Add(new Vector2(1f,1f));
					}else if(genDataTemp>27f){
						uvs.Add(new Vector2((((float)x%2)/2f),(((float)y%2)/2f)+0.5f));
						mapNum = 2;
						//uvs.Add(new Vector2(1f,0.5f));
					}else if(genDataTemp>21f){
						uvs.Add(new Vector2((((float)x%2)/2f)+0.5f,((float)y%2)/2f));
						mapNum = 1;
						//uvs.Add(new Vector2(1f,0f));
					}else{
						uvs.Add(new Vector2(((float)x%2)/2f,((float)y%2)/2f));
						//uvs.Add(new Vector2((float)x%mDlo/2f,(float)y%mDlo)/2f);
					}
					//uvs.Add(new Vector2((float)x%2,(float)y%2));
				}
			}

			mesh.vertices = verts.ToArray();
			mesh.uv = uvs.ToArray();
			mesh.triangles = tris.ToArray();

			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
			mesh.Optimize();

			GetComponent<MeshFilter>().mesh = mesh;
		}

		protected float XYtoNum(float xn,float yn){
			return (xn+(yn*(float)mapX));
		}
		
		protected int XYtoNum(int xn,int yn){
			return (xn+(yn*mapX));
		}
	}
}
