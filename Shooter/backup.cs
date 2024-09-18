///*
// * Created by SharpDevelop.
// * User: mrhitj
// * Date: 1/15/2024
// * Time: 1:08 AM
// * 
// * To change this template use Tools | Options | Coding | Edit Standard Headers.
// */
//
///*
// * Created by SharpDevelop.
// * User: mrhitj
// * Date: 12/7/2023
// * Time: 9:38 AM
// * 
// * To change this template use Tools | Options | Coding | Edit Standard Headers.
// */
//using System;
//
//namespace Shooter
//{
//	public static class Object
//	{
//		public static var player = new Player(4, 15);
//	}
//	class Program
//	{
//		public static void raiseException(string Message)
//		{
//			Console.Clear();
//			Console.WriteLine(Message);
//			Console.WriteLine("Press any key to Exit...");
//			Console.ReadKey();
//			Environment.Exit(0);
//		}
//		
//		static void ClearBuffer()
//		{
//			while (Console.In.Peek() != -1) {
//				Console.In.Read();
//			}
//		}
//		
//		public static void Main(string[] args)
//		{
//			bool Game = true;
//			int FrameCount = 0;
//			ConsoleKeyInfo inputKey;
//			
//			var Map = new BoxMap(9, 18);
//			var player = new Player(4, 15);
//			
//			Enemy[] EnemyList = new Enemy[2];
//			for(int i = 0; i < EnemyList.Length; i++){
//				EnemyList[i] = new Enemy(i+1, i+1);
//			}
//			
//			Map.Init();
//			Map.Place(player.Sprite, player.x, player.y);
//			for(int i = 0; i < EnemyList.Length; i++){
//				Map.Place(EnemyList[i].Sprite, EnemyList[i].x, EnemyList[i].y);
//			}
//			
//			while(Game){
//				Map.Display();
//				Console.Write("Bullet : ");
//				player.gun.printMagazine();
//				
//				if(Console.KeyAvailable){
//					inputKey = Console.ReadKey(true);
////					Console.WriteLine(inputKey.Key);
//					
//					player.Move(inputKey, Map);
//					player.Shoot(inputKey, Map);
//				}
//				
//				
//				//Handle bullet travel
//				if(FrameCount % 5 == 0){
//					for(int i = 0; i < player.gun.Magazine.Length; i++)
//					{
//						player.gun.Magazine[i].Traveling(Map);
//					}
//					for(int i = 0; i < EnemyList.Length; i++){
//						for (int j = 0; j < EnemyList[i].gun.Magazine.Length; j++) {
//							EnemyList[i].gun.Magazine[j].Traveling(Map);
//						}
//					}
//				}
//				
//				if((FrameCount % 15) == 0){
//					for(int i = 0; i < EnemyList.Length; i++){
//						EnemyList[i].Shoot(Map);
//					}
//				}
//				
//				if((FrameCount % 10) == 0){
//					for(int i = 0; i < EnemyList.Length; i++){
//						EnemyList[i].Move(Map);
//					}
//				}
//				
//				if(player.Health == 0){
//					//Game over
//					break;
//				}
//				
//				System.Threading.Thread.Sleep(5);
//				
//				FrameCount = (FrameCount == 60) ? 0 : FrameCount + 1;
//				
//				Console.Clear();
//			}
//			
//		}
//		
//	}
//}