// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections; 


namespace AssemblyCSharp {
	public class DropMeBox : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
	{
		public Image containerImage;
		public Image receivingImage;
		public GameObject DropPanel;

		private Color normalColor;
		public Color highlightColor = Color.yellow;
		static Hashtable h = new Hashtable();
		
		public void OnEnable ()
		{
			if (containerImage != null)
				normalColor = containerImage.color;
		}
		
		public void OnDrop(PointerEventData data)
		{
			containerImage.color = normalColor;
			print ("dropped");
			print (SngletonStorage.name);
			print (receivingImage);
			//if (receivingImage == null)
			//	return;
			if (!SngletonStorage.name.Equals ("empty")) {
				//string temp = SngletonStorage.name;
				
				string prefabType = "mPrefab";
				string parentType = SngletonStorage.blockType;
				
				switch (SngletonStorage.name) {
					
				case "plus":
					prefabType = "plusPrefab"; 
					break;
				case "subtract":
					prefabType = "minusPrefab"; 
					break;
				case "divide":
					prefabType = "dividePrefab";
					break;
				case "dot":
					prefabType = "dotPrefab";
					break;
				case "SF Generic_0":
					prefabType = "blankArrayPrimitive";
					break;
				default:
					prefabType = "plusPrefab";
					break;
				}
				if (SngletonStorage.blockType.Equals("primitive")){
					var left = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Textures and Sprites/MathVRUI/blankArrayPrimitive.prefab", typeof(GameObject))) as GameObject;
					left.transform.parent = DropPanel.transform;
					left.transform.localScale = new Vector3 (1, 1, 1);
					
					var prefab = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Textures and Sprites/MathVRUI/"+prefabType+".prefab", typeof(GameObject))) as GameObject;
					//var prefab = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Textures and Sprites/MathVRUI/"+prefabType+".prefab", typeof(GameObject))) as GameObject;
					prefab.transform.parent = DropPanel.transform;
					prefab.transform.localScale = new Vector3 (1, 1, 1);
					
					var right = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Textures and Sprites/MathVRUI/blankArrayPrimitive.prefab", typeof(GameObject))) as GameObject;
					right.transform.parent = DropPanel.transform;
					right.transform.localScale = new Vector3 (1, 1, 1);
					
				}
				else {
					if (SngletonStorage.name.Equals ("SF Generic_0")) {
						var prefab = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Textures and Sprites/MathVRUI/"+prefabType+".prefab", typeof(GameObject))) as GameObject;
						prefab.transform.parent = DropPanel.transform;
						prefab.transform.localScale = new Vector3 (1, 1, 1);
					}
					
				}
				
				SngletonStorage.name = "empty";	
				
			}
			
		}
		
		public void OnPointerEnter(PointerEventData data)
		{
			if (containerImage == null)
				return;
			
			Image droppedIm = GetDroppedImage(data);
			if (droppedIm != null)
				containerImage.color = highlightColor;
	
		}
		
		
		public void OnPointerExit(PointerEventData data)
		{
			if (containerImage == null)
				return;
			
			containerImage.color = normalColor;

		}
		
		private Image GetDroppedImage(PointerEventData data)
		{
			var originalObj = data.pointerDrag;
			if (originalObj == null)
				return null;
			
			var srcImage = originalObj.GetComponent<Image>();
			
			if (srcImage == null)
				return null;
			

			if (!h.ContainsKey (srcImage.name)) {
				h.Add (srcImage.name, SngletonStorage.name);
			} else {
				h[srcImage.name] =  SngletonStorage.name;
			}

			return srcImage;
		}
	}
}

