using System;

namespace NodeEditor
{
	[Serializable]
	public class CardBase
	{
		public CardBase (string name)
		{
			this.name = name;
		}

		public string name{ set; get;}
		public int cost{ set; get;}


	}
}

