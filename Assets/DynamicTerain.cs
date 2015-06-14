using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Generator;

public class DynamicTerain : MonoBehaviour {

	private class NewPart{
		
		public int xPart;
		public int yPart;
		public float dist;
		
		public NewPart(int xPart,int yPart, float dist){
			this.xPart = xPart;
			this.yPart = yPart;
			this.dist = dist;
		}
	}

	private class SortDist : IComparer<NewPart>
	{
		int IComparer<NewPart>.Compare(NewPart a, NewPart b) 
		{              
			if (a.dist > b.dist)
				return 1; 
			if (a.dist < b.dist)
				return -1; 
			else
				return 0;
		}
	}

	private List<TerainGen> terainList = new List<TerainGen>();

	[SerializeField]
	private TerainGen baseTerain;

	[SerializeField]
	private Transform traget;

	[SerializeField]
	private int terainSize = 101;

	[SerializeField]
	private int drawDist = 3;

	[SerializeField]
	private Gradient gizmoColorDistance;

	void OnDrawGizmos(){

		float xPos = traget.position.x;
		float zPos = traget.position.z;
		
		int intTerainStartX = Mathf.FloorToInt(xPos/(float)terainSize);
		int intTerainStartZ = Mathf.FloorToInt(zPos/(float)terainSize);

		for (int x = -drawDist; x < (drawDist+1); x++) {
			for (int z = -drawDist; z < (drawDist+1); z++) {
				float dist = Vector2.Distance(new Vector2((x*terainSize),(z*terainSize)),new Vector2(intTerainStartX,intTerainStartZ));
				float t = (dist/(float)drawDist)/(float)terainSize;
				// set color (gradiant distance)
				Gizmos.color = gizmoColorDistance.Evaluate(t);
				// draw terain rect
				DragGizmoRect(new Rect(intTerainStartX+x,intTerainStartZ+z	,terainSize,terainSize),80f-(0.1f*dist));
			}
		}
	}

	void DragGizmoRect(Rect rect,float height){
		Vector3 bottomLeft = 	new Vector3((rect.x*rect.width)					,height,(rect.y*rect.height));
		Vector3 bottomRight = 	new Vector3(((rect.x*rect.width)+rect.width)		,height,(rect.y*rect.height));
		Vector3 topRight = 		new Vector3(((rect.x*rect.width)+rect.width)		,height,((rect.y*rect.height)+rect.height));
		Vector3 topLeft = 		new Vector3((rect.x*rect.width)					,height,((rect.y*rect.height)+rect.height));

		Gizmos.DrawLine(bottomLeft,bottomRight);
		Gizmos.DrawLine(bottomRight,topRight);
		Gizmos.DrawLine(topRight,topLeft);
		Gizmos.DrawLine(topLeft,bottomLeft);
	}
	void Start () {
		InvokeRepeating("UpdateTerain",0.0f,1.0f);
	}

	void UpdateTerain () {
		float xPos = traget.position.x;
		float zPos = traget.position.z;

		int intTerainStartX = Mathf.FloorToInt(xPos/(float)terainSize);
		int intTerainStartY = Mathf.FloorToInt(zPos/(float)terainSize);

		int terainStartX = intTerainStartX*terainSize;
		int terainStartY = intTerainStartY*terainSize;
		StartCoroutine(UpdateParts(terainStartX,terainStartY));
	}

	IEnumerator UpdateParts(int terainStartX,int terainStartY){
		List<NewPart> newParts = new List<NewPart>();
		for (int x = -drawDist; x < (drawDist+1); x++) {
			for (int z = -drawDist; z < (drawDist+1); z++) {
				float dist = Vector2.Distance(new Vector2(terainStartX+((x*terainSize)-(terainSize*0.5f)),terainStartY+((z*terainSize)-(terainSize*0.5f))),new Vector2(traget.position.x,traget.position.z));
				
				newParts.Add(new NewPart(terainStartX+(x*(terainSize)),terainStartY+(z*(terainSize)),dist));
				//StartCoroutine(GenPart(terainStartX+(x*(terainSize)),terainStartY+(z*(terainSize))));
				//yield return null;
			}
		}
		// sort parts distance
		newParts.Sort(new SortDist());
		for (int i = 0; i < newParts.Count; i++) {
			// generate parts
			StartCoroutine(GenPart(newParts[i].xPart,newParts[i].yPart));
			yield return null;
		}
	}

	IEnumerator GenPart(int xPart, int yPart){
		bool alreadyExists = false;
		for (int i = 0; i < terainList.Count; i++) {
			if(terainList[i].StartX == xPart && terainList[i].StartY == yPart){
				alreadyExists = true;
			}
		}
		if(!alreadyExists){
			//Debug.Log("xPart: "+xPart+" yPart: "+yPart);

			TerainGen newTerain = ((GameObject)GameObject.Instantiate(baseTerain.gameObject,Vector3.zero,Quaternion.identity)).GetComponent<TerainGen>();
			
			
			newTerain.StartX = xPart;
			newTerain.StartY = yPart;
			newTerain.MapX = (terainSize+1);
			newTerain.MapY = (terainSize+1);
			newTerain.Generate();
			terainList.Add(newTerain);
			newTerain.transform.parent = transform;
			newTerain.gameObject.AddComponent<MeshCollider>();

			yield return null;
		}
	}
}























