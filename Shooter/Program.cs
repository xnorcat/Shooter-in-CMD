/*
 * Created by SharpDevelop.
 * User: mrhitj
 * Date: 12/7/2023
 * Time: 9:38 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Shooter
{
	public static class Objects
	{
		public static Player player = new Player(4, 15, -1, 3, 50);
		public static Enemy[] EnemyList = new Enemy[3];
		
	}
	class Program
	{
		public static void raiseException(string Message)
		{
			Console.Clear();
			Console.WriteLine(Message);
			Console.WriteLine("Press any key to Exit...");
			Console.ReadKey();
			Environment.Exit(0);
		}
		
		static void ClearBuffer()
		{
			while (Console.In.Peek() != -1) {
				Console.In.Read();
			}
		}
		
		public static void Main(string[] args)
		{
			bool Game = true;
			int FrameCount = 0;
			int enemyDefeated = 0;
			string Message = "You win!";
			ConsoleKeyInfo inputKey;

			for(int i = 0; i < Objects.EnemyList.Length; i++){
				Objects.EnemyList[i] = new Enemy(i+1, i+1, 5, 50);
			}
			var Map = new BoxMap(9, 18);
			
			Map.Init();
			Map.Place(Objects.player.Sprite, 
			          Objects.player.x, 
			          Objects.player.y);
			for(int i = 0; i < Objects.EnemyList.Length; i++){
				Map.Place(Objects.EnemyList[i].Sprite, 
				          Objects.EnemyList[i].x, 
				          Objects.EnemyList[i].y);
			}
			
			while(Game){
				Map.Display();
				Console.Write("Bullet : ");
				Objects.player.gun.printMagazine();
				Console.WriteLine("HP : " + Objects.player.Health);
				Console.WriteLine();
				
				for(int i = 0; i < Objects.EnemyList.Length; i++){
					if(!Objects.EnemyList[i].isDead)
						Console.WriteLine("Enemy" + i + " HP : " + Objects.EnemyList[i].Health);
				}
				Console.WriteLine(enemyDefeated);
				
				if(Console.KeyAvailable){
					inputKey = Console.ReadKey(true);
//					Console.WriteLine(inputKey.Key);
					
					Objects.player.Move(inputKey, Map);
					Objects.player.Shoot(inputKey, Map);
				}
				
				
				//Handle bullet travel
				if(FrameCount % 3 == 0){
					for(int i = 0; i < Objects.player.gun.Magazine.Length; i++)
					{
						Objects.player.gun.Magazine[i].Traveling(Map);
					}
					for(int i = 0; i < Objects.EnemyList.Length; i++){
						for (int j = 0; j < Objects.EnemyList[i].gun.Magazine.Length; j++) {
							Objects.EnemyList[i].gun.Magazine[j].Traveling(Map);
						}
					}
				}
				
				if((FrameCount % 15) == 0){
					for(int i = 0; i < Objects.EnemyList.Length; i++){
						Objects.EnemyList[i].Shoot(Map);
					}
				}
				
				if((FrameCount % 10) == 0){
					for(int i = 0; i < Objects.EnemyList.Length; i++){
						Objects.EnemyList[i].Move(Map);
					}
				}
				
				//Handling game win condition
				
				if(Objects.player.Health <= 0){
					//Game over
					Message = "Game Over!";
					break;
				}
				
				if(enemyDefeated == Objects.EnemyList.Length)
				{
					break;
				}
				
				for(int i = 0; i < Objects.EnemyList.Length; i++){
					if(Objects.EnemyList[i].isDead && Objects.EnemyList[i].iThinkisDead){
						enemyDefeated = enemyDefeated + 1;
						Objects.EnemyList[i].iThinkisDead = false;
					}
					
				}
				
				
				System.Threading.Thread.Sleep(10);
				
				FrameCount = (FrameCount == 60) ? 0 : FrameCount + 1;
				
				Console.Clear();
			}
			Console.WriteLine(Message);
			System.Threading.Thread.Sleep(2000);	
		}
		
	}
}