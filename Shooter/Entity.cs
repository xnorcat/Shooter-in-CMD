/*
 * Created by SharpDevelop.
 * User: mrhitj
 * Date: 12/7/2023
 * Time: 1:10 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;

namespace Shooter
{
	/// <summary>
	/// Description of Player.
	/// </summary>
	public class Player
	{
		public int x;
		public int y;
		readonly public string Sprite = "A";
		public int Health;
		public Gun gun;
		
		public Player(int x, int y, int ShootDirection = -1,int Clip = 3, int Health = 30)
		{
			this.x = x;
			this.y = y;
			this.Health = Health;
			gun = new Gun(Sprite, ShootDirection, Clip);
			
		}
		
		public void Move(ConsoleKeyInfo key, BoxMap Box)
		{
			Box.Box[y,x] = Box.Air;
		
			switch(key.Key)
			{
				case ConsoleKey.UpArrow:
					y--;
					y = (y == -1 || Box.Objects.Contains(Box.Box[y, x])) ? y + 1 : y;
					break;
				case ConsoleKey.DownArrow:
					y++;
					y = (y == Box.y || Box.Objects.Contains(Box.Box[y, x])) ? y - 1 : y;
					break;
				case ConsoleKey.LeftArrow:
					x--;
					x = (x == -1 || Box.Objects.Contains(Box.Box[y, x])) ? x + 1 : x;
					break;
				case ConsoleKey.RightArrow:
					x++;
					x = (x == Box.x || Box.Objects.Contains(Box.Box[y, x])) ? x - 1 : x;
					break;
				default:
					break;
			}
		
			Box.Box[y,x] = Sprite;
		}
		
		public void Shoot(ConsoleKeyInfo key, BoxMap Box)
		{
			switch(key.Key){
					
				case ConsoleKey.Z:
					//Single Shot
					gun.MagazineIndex = (gun.MagazineIndex == gun.Clip) ? 0 : gun.MagazineIndex;
					
					if(!gun.Magazine[gun.MagazineIndex].isShoot){
						gun.Shoot(Box, x, y-1);
					}
					break;
					
				case ConsoleKey.X:
					//Fire 3 bulleta at once
					if(gun.BulletAmount() >= 3){
						gun.MagazineIndex = 0;
						gun.Shoot(Box, x-1, y-1);
						gun.Shoot(Box, x, y-1);
						gun.Shoot(Box, x+1, y-1);
					}
					break;
				default:
					//do nothing la
					break;
			}
		}
		
		
	}
	
	//I don't know how to use inherited class on argument e.g. Place(Enemy enemy),
	//where enemy was inherited from Player class
	public class Enemy
	{
		public int x;
		public int y;
		readonly public string Sprite = "Y";
		public int Health;
		public Gun gun;
		private int Direction = 1;
		public bool isDead = false; //The heavy is dead?!
		//for counting how many enemy defeat because i dont know the efficient way
		//or this is the best thing i can come up for now
		public bool iThinkisDead = false; 
		
		public Enemy(int x, int y, int Clip = 5, int Health = 50)
		{
			this.x = x;
			this.y = y;
			this.Health = Health;
			gun = new Gun(Sprite, 1, Clip);
		}
		
		public void Move(BoxMap Box)
		{
			Box.Box[y, x] = Box.Air;
			if(isDead){
				return;
			}
			//For Direction, 1 is Right and -1 is Left
			
			x = x + Direction;
			x = (x == Box.x || x < 0) ? x + (Direction * -1) : x;
			
			bool isObject = Box.Objects.Contains(Box.Box[y, x]);
			
			if((isObject && Direction == 1)|| x == Box.x - 1){
				x = (isObject) ? x - 1 : x;
				Direction = -1;
			}
			
			else if((isObject && Direction == -1) || x == 0){
				x = (isObject) ? x + 1 : x;
				Direction = 1;
			}
			
			Box.Box[y, x] = Sprite;
		}
		
		public void Shoot(BoxMap Map)
		{
			if(isDead){
				return;
			}
			gun.MagazineIndex = (gun.MagazineIndex == gun.Clip) ? 0 : gun.MagazineIndex;
					
			if(!gun.Magazine[gun.MagazineIndex].isShoot){
				gun.Shoot(Map, x, y+1);
			}
		}
	}
	
}
