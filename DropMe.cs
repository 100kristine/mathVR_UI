using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections; 
using System.Collections.Generic; 


namespace AssemblyCSharp {
public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image containerImage;
	public Image receivingImage;
	private Color normalColor;
	public Color highlightColor = Color.yellow;
	static Hashtable h = new Hashtable();
	public GameObject DropPanel;

	
	public void OnEnable ()
	{
		if (containerImage != null)
			normalColor = containerImage.color;
	}
	
	public void OnDrop(PointerEventData data)
	{
		containerImage.color = normalColor;
		
		if (receivingImage == null)
			return;
		Sprite dropSprite = GetDropSprite (data);
		if (receivingImage != null) {
			containerImage.color = highlightColor;
			if (dropSprite.name != null && SngletonStorage.blockType.Equals ("array")) {
				
				receivingImage.sprite = dropSprite;
			}
		}
	
	}

	public void OnPointerEnter(PointerEventData data)
	{
		if (containerImage == null)
			return;
		
		
		/*else {
			string prefabType = "A";
			switch(SngletonStorage.name) {
				case "A":
					prefabType = "A"; 
					break;
				case "AT":
					prefabType = "AT"; 
					break;
				case "VT":
					prefabType = "VT";
					break;
				case "M":
					prefabType = "M";
					break;
				}
			var prefab = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Textures and Sprites/MathVRUI/"+prefabType+".prefab", typeof(GameObject))) as GameObject;
			prefab.transform.parent = DropPanel.transform;
			prefab.transform.localScale = new Vector3 (1, 1, 1);
				
		}*/
		
		
		}


	public void OnPointerExit(PointerEventData data)
	{
		if (containerImage == null)
			return;
		
		containerImage.color = normalColor;
		
	}
	
	private Sprite GetDropSprite(PointerEventData data)
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
		return srcImage.sprite;
	}
}
}
