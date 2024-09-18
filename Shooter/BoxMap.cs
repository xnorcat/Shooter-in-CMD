/*
 * Created by SharpDevelop.
 * User: mrhitj
 * Date: 12/7/2023
 * Time: 9:58 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Shooter
{
	/// <summary>
	/// Make a box for the map
	/// </summary>
	sealed public class BoxMap
	{
		public int x;
		public int y;
		public string[,] Box;
		readonly public string Air = ".";
		readonly public  string Border = "+";
		public string[] Objects = {"A", "Y", "+"};
		
		public BoxMap(int x = 5, int y = 10)
		{
			this.x = x;
			this.y = y;
			Box = new string[y,x];
		}
		
		public void Init()
		{
			for(int i = 0; i < y; i++){
				for(int j = 0; j < x; j++){
					Box[i, j] = Air;
				}
			}
			AddBorder();
		}
		
		private void AddBorder()
		{
			for(int i = 0; i < y; i++){
				for(int j = 0; j < x; j++){
					if(i == 0 || i == (y-1)){
						Box[i, j] = Border;
					}
					if(j != 0 && j != (x-1)){
						//Continue
					}
					else{
						Box[i, j] = Border;
					}
				}
			}
		}
		
		public void Display()
		{
			for(int i = 0;i < y; i++){
				for(int j = 0;j < x; j++){
					Console.Write(Box[i, j] + " ");
				}
				Console.Write("\n");
			}
		}
		
		public void Place(string Sprite, int EntityX, int EntityY)
		{
			bool CheckX = (EntityX > (x - 1) || EntityX < 0);
			bool CheckY = (EntityY > (y - 1) || EntityY < 0);
			if(!CheckX && !CheckY){
				Box[EntityY, EntityX] = Sprite;
				return;
			}
			Program.raiseException("Coordinate out of map bound.");
		}
	
	}
}
