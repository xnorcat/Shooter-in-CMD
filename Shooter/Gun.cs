/*
 * Created by SharpDevelop.
 * User: mrhitj
 * Date: 12/12/2023
 * Time: 10:27 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;

namespace Shooter
{
	/// <summary>
	/// Gun 
	/// </summary>
	
	public class Gun
	{
		public Bullet[] Magazine;
		public int Clip;
		public int MagazineIndex = 0;
		
		public Gun(string OwnerSprite, int Direction, int Clip = 3){
			this.Clip = Clip;
			Magazine = new Bullet[Clip];
			for(int i = 0; i < Clip; i++)
			{
				Magazine[i] = new Bullet(OwnerSprite, Direction);
			}
		}
		
		public bool isMagazineFull(){
			int Index = 0;
			bool isFull = true;
			while(Index < Magazine.Length){
				if(Magazine[Index].isShoot){
					isFull = false;
				}
				Index++;
			}
			return isFull;
		}
		
		public int BulletAmount(){
			int Amount = 0;
			for(int i = 0; i < Magazine.Length; i++){
				if(!Magazine[i].isShoot){
					Amount++;
				}
			}
			
			return Amount;
		}
		
		public void printMagazine(){
			for(int i = 0; i < Clip; i++){
				if(!Magazine[i].isShoot){
					Console.Write(Magazine[i].Sprite);
				}	
			}
			Console.Write("\n");
		}
		public void Shoot(BoxMap Box, int X, int Y){
			bool CheckX = (X == Box.x || X < 0);
			bool CheckY = (Y == Box.y || Y < 0);
			bool isObject = (Box.Objects.Contains(Box.Box[Y, X]));
			if(CheckX || CheckY || isObject){
				return;
			}
			
			Magazine[MagazineIndex].isShoot = true;
			Box.Box[Y, X] = Magazine[MagazineIndex].Sprite;
			Magazine[MagazineIndex].y = Y;
			Magazine[MagazineIndex].x = X;
			MagazineIndex = MagazineIndex + 1;
		}
	}
	
	/// <summary>
	/// Description of Bullet.
	/// </summary>
	public class Bullet
	{
		public int x;
		public int y;
		readonly public string Sprite = "|";
		public string OwnerSprite;
		private int Direction;
		public bool isShoot = false;
		public int Damage;
		
		public Bullet(string OwnerSprite, int Direction, int Damage = 5)
		{
			this.OwnerSprite = OwnerSprite;
			this.Direction = Direction;
			this.Damage = Damage;
		}
		
		public void Traveling(BoxMap Box)
		{
				if(isShoot){
					//For Direction, 1 is Down and -1 is Up.
					Box.Box[y, x] = Box.Air;
					y = y + Direction;
					
					if(isHit(Box)){
						isShoot = false;	
						//Get the id of object and give damage to it
						//Use current bullet xy to compare enemy xy
						//to compare multiple enemy, enemy needs to be in a list
						//and then compare them in a loop
						giveDamage(Box);
					}
					
					else{
						Box.Box[y, x] = Sprite;
					}
				}
		}
		
		private void giveDamage(BoxMap Box)
		{
			string collideWith = Box.Box[y, x];
			switch(collideWith)
			{
				case "A":
					Objects.player.Health -= Damage;
					break;
				case "Y":
					for(int i = 0; i < Objects.EnemyList.Length; i++){
						if(x == Objects.EnemyList[i].x && y == Objects.EnemyList[i].y){
							Objects.EnemyList[i].Health -= Damage;
							if(Objects.EnemyList[i].Health <= 0){
								Objects.EnemyList[i].isDead = true;
								Objects.EnemyList[i].iThinkisDead = true;
							}
						}
					}
					break;
			}
		}
		
		private bool isHit(BoxMap Box)
		{	
			if((y == Box.y || y < 0)){
				return true;
			}
			//y + (Direction * 1) reverses the direction 
			return (Box.Objects.Contains(Box.Box[y, x]));
		}
	}
}
