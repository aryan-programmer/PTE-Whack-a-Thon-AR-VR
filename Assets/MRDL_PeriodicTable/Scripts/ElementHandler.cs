﻿//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//
using System.Collections.Generic;
using UnityEngine;

namespace HoloToolkit.MRDL.PeriodicTable
{
	[System.Serializable]
	public class ElementData
	{
		public string name;
		public string category;
		public string spectral_img;
		public int xpos;
		public int ypos;
		public string named_by;
		public float density;
		public string color;
		public float molar_heat;
		public string symbol;
		public string discovered_by;
		public string appearance;
		public float atomic_mass;
		public float melt;
		public string number;
		public string source;
		public int period;
		public string phase;
		public string summary;
		public int boil;
	}

	[System.Serializable]
	class ElementsData
	{
		public List<ElementData> elements;

		public static ElementsData FromJSON( string json )
		{
			return JsonUtility.FromJson<ElementsData>( json );
		}
	}

	public class ElementHandler : Singleton<ElementHandler>
	{
		List<ElementData> elements;
		Dictionary<string , Material> typeMaterials;

		[SerializeField] Element elementPrefab;

		[Header( "Materials" )]
		[SerializeField]
		Material
			AlkaliMetal,
			AlkalineEarthMetal,
			TransitionMetal,
			Metalloid,
			DiatomicNonmetal,
			PolyatomicNonmetal,
			PostTransitionMetal,
			NobleGas,
			Actinide,
			Lanthanide;

		void Start( )
		{
			Debug.Log( "Creating arrangement" );

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
				elements[ Random.Range( 0 , elements.Count ) ] ,
				typeMaterials );

		public Element Instantiate( 
			Vector3 position , Quaternion rotation , Transform parent ) => 
			Instantiate( elementPrefab , position , rotation , parent );
	}
}