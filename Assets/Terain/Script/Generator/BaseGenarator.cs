using UnityEngine;
using System.Collections.Generic;
using GenFunction.Field;

namespace Generator {
	public abstract class BaseGenarator : MonoBehaviour {
		
		[SerializeField]
		protected BaseGenFunction[] generators;
		
		public abstract void Generate ();
	}
}
