//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//
using UnityEngine;

namespace HoloToolkit.MRDL.PeriodicTable
{
	public class Element : MonoBehaviour
	{
		public TextMesh ElementNumber;
		public TextMesh ElementName;
		public TextMesh ElementNameDetail;

		public MeshRenderer BoxRenderer;
		public MeshRenderer PanelFront;

		ElementData data;

		Material material;

		public ElementData Data => data;

		/// <summary>
		/// Set the display data for this element based on the given data
		/// </summary>
		public void SetFromElementData( ElementData data )
		{
			this.data = data;

			ElementNumber.text = data.number;
			ElementName.text = data.symbol;
			ElementNameDetail.text = data.name;

			PanelFront.sharedMaterial = BoxRenderer.sharedMaterial = material = 
				ElementHandler.I.TypeMaterials[ data.category ];
		}
	}
}
