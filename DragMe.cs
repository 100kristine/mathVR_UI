using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using testInterface.storage;


namespace AssemblyCSharp {

[RequireComponent(typeof(Image))]
public class DragMe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public bool dragOnSurfaces = true;
	
	private GameObject m_DraggingIcon;
	private RectTransform m_DraggingPlane;

	static string name;

	public void OnBeginDrag(PointerEventData eventData)
	{
		var canvas = FindInParents<Canvas>(gameObject);
		if (canvas == null)
			return;

		// We have clicked something that can be dragged.
		// What we want to do is create an icon for this.
		m_DraggingIcon = new GameObject("icon");

		m_DraggingIcon.transform.SetParent (canvas.transform, false);
		m_DraggingIcon.transform.SetAsLastSibling();
		
		var image = m_DraggingIcon.AddComponent<Image>();

		// The icon will be under the cursor.
		// We want it to be ignored by the event system.
		CanvasGroup group = m_DraggingIcon.AddComponent<CanvasGroup>();
		group.blocksRaycasts = false;

		image.sprite = GetComponent<Image>().sprite;
		image.SetNativeSize();


		string iconName = GetComponent<Image> ().sprite.name; // ie plus

			SngletonStorage.name = iconName;
			switch (SngletonStorage.name) {
			case "plus":
				SngletonStorage.blockType = "primitive";
				break;
			case "subtract":
				SngletonStorage.blockType = "primitive";
				break;
			case "divide":
				SngletonStorage.blockType = "primitive";
				break;
			case "dot":
				SngletonStorage.blockType = "primitive";
				break;
			case "A":
				SngletonStorage.blockType = "array";
				break;
			case "AT":
				SngletonStorage.blockType = "array";
				break;
			case "V":
				SngletonStorage.blockType = "array";
				break;
			case "VT":
				SngletonStorage.blockType = "array";
				break;
			case "M":
				SngletonStorage.blockType = "array";
				break;
			case "SF Generic_0":
				SngletonStorage.blockType = "array";
				break;
			default:
				SngletonStorage.blockType = "array";
				break;
			}
		if (dragOnSurfaces)
			m_DraggingPlane = transform as RectTransform;
		else
			m_DraggingPlane = canvas.transform as RectTransform;
		
		SetDraggedPosition(eventData);
	}

	public void OnDrag(PointerEventData data)
	{
		if (m_DraggingIcon != null)
			SetDraggedPosition(data);


	}

	private void SetDraggedPosition(PointerEventData data)
	{
		if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
			m_DraggingPlane = data.pointerEnter.transform as RectTransform;

		var rt = m_DraggingIcon.GetComponent<RectTransform>();
		Vector3 globalMousePos;

		m_DraggingIcon.GetComponent<RectTransform> ();

		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
		{
			rt.position = globalMousePos;
			rt.rotation = m_DraggingPlane.rotation;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (m_DraggingIcon != null)
			Destroy(m_DraggingIcon);

	}

	static public T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null) return null;
		var comp = go.GetComponent<T>();

		if (comp != null)
			return comp;
		
		Transform t = go.transform.parent;
		while (t != null && comp == null)
		{
			comp = t.gameObject.GetComponent<T>();
			t = t.parent;
		}
		return comp;
	}
}
}