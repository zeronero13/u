using System;
using UnityEngine;
using UnityEditor;

[Serializable]
public class MyRect
{
	public float x;
	public float y;
	public float width;
	public float height;

	public MyRect (float x, float y, float width, float height)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	public MyRect (Rect rect){
		this.x = rect.x;
		this.y = rect.y;
		this.width = rect.width;
		this.height = rect.height;
	}
		
	public Rect getRect(){
		return new Rect (x, y, width, height);
	}

	public void setRect(Rect rect){
		this.x = rect.x;
		this.y = rect.y;
		this.width = rect.width;
		this.height = rect.height;		
	}

	public bool Contains(Vector2 vector){
		if( (x<vector.x) && (vector.x < x+width)){
			if( (y<vector.y) && (vector.y<y+height) ){
				return true;
			}
		}
		return false;
	}
}


