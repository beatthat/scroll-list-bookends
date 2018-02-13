using UnityEngine;
using BeatThat;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BeatThat
{
	/// <summary>
	/// For something like a horizonal scroll list of cards,
	/// you frequently want book-end/bumper objects (visible or invisible)
	/// that prevent the first and last items from scrolling past center position.
	/// </summary>
	public class ScrollListBookEnds : Subcontroller<IHasItemAddedGoEvent>
	{
		public Transform m_before;
		public Transform m_after;

		override protected void BindSubcontroller()
		{
			EnforceBookEndPositions();
			Bind(this.controller.itemAddedGO, this.itemAddedAction);
		}

		private void EnforceBookEndPositions()
		{
			m_before.SetSiblingIndex(0);
			m_after.SetSiblingIndex(m_after.transform.parent.childCount - 1);

			LayoutRebuilder.ForceRebuildLayoutImmediate(m_after.transform.parent as RectTransform);
		}

		private void OnItemAdded(GameObject item)
		{
			EnforceBookEndPositions();
		}
		private UnityAction<GameObject> itemAddedAction { get { return m_itemAddedAction?? (m_itemAddedAction = this.OnItemAdded); } }
		private UnityAction<GameObject> m_itemAddedAction;
	}
}