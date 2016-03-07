using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NodeEditor;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace NodeEditor
{
	[Serializable]
	public class CardDeckManager
	{
		public static CardDeckManager instance;


		protected CardDeckManager ()
		{
		}

		public List<CardBase> cardList= new List<CardBase>();

		public void LoadDataFromFile(){
			/* load in cards data information??????*/
		}

	}
}

