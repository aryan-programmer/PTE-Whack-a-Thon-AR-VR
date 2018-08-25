﻿//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//
using System.Collections.Generic;
using UnityEngine;

namespace HoloToolkit.MRDL.PeriodicTable
{
	public class ElementHandler : Singleton<ElementHandler>
	{
		List<ElementData> elements;
		Dictionary<string , Material> typeMaterials;

		[SerializeField] Element elementPrefab;

		[Header( "Materials" ),SerializeField]
		Material AlkaliMetal;
		[SerializeField]
		Material 
			AlkalineEarthMetal,
			TransitionMetal,
			Metalloid,
			DiatomicNonmetal,
			PolyatomicNonmetal,
			PostTransitionMetal,
			NobleGas,
			Actinide,
			Lanthanide;

		public Dictionary<string , Material> TypeMaterials => typeMaterials;

		void Start( )
		{
			typeMaterials = new Dictionary<string , Material>()
		{
			{ "alkali metal", AlkaliMetal },
			{ "alkaline earth metal", AlkalineEarthMetal },
			{ "transition metal", TransitionMetal },
			{ "metalloid", Metalloid },
			{ "diatomic nonmetal", DiatomicNonmetal },
			{ "polyatomic nonmetal", PolyatomicNonmetal },
			{ "post-transition metal", PostTransitionMetal },
			{ "noble gas", NobleGas },
			{ "actinide", Actinide },
			{ "lanthanide", Lanthanide },
		};

			// Parse the elements out of the json file
			TextAsset asset = Resources.Load<TextAsset>( "JSON/PeriodicTableJSON" );
			elements = ElementsData.FromJSON( asset.text ).elements;
		}

		public void SetRandomElement( Element elementObj ) =>
			elementObj.SetFromElementData(
				elements[ Random.Range( 0 , elements.Count ) ] );
	}
}