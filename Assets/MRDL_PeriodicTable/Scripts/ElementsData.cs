//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//
using System.Collections.Generic;
using UnityEngine;

namespace HoloToolkit.MRDL.PeriodicTable
{
	[System.Serializable]
	class ElementsData
	{
		public List<ElementData> elements;

		public static ElementsData FromJSON( string json ) => JsonUtility.FromJson<ElementsData>( json );
	}
}