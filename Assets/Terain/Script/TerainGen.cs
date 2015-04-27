using UnityEngine;
using System.Collections.Generic;
using Terain.Gen.Field;

[RequireComponent(typeof(MeshRenderer),typeof(MeshFilter))]
public class TerainGen : MonoBehaviour {

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

	private float currentX = 0f;
	private float currentY = 0f;

	//private float[] genDate;
	private float genDataTemp;

	[SerializeField]
	private bool debugPrint = false;

	[SerializeField]
	private Gen[] generators;

	public void Start(){
		//InvokeRepeating("Generate",0.2f,0.2f);
	}

	public void Generate () {
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
				uvs.Add(new Vector2((float)x%mDlo,(float)y%mDlo));
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

	private float XYtoNum(float xn,float yn){
		return (xn+(yn*(float)mapX));
	}
	
	private int XYtoNum(int xn,int yn){
		return (xn+(yn*mapX));
	}

}
