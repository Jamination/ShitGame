namespace ShitGame.Levels
{
	public static class Level_1
	{
		public static void Load()
		{
			Functions.SetPlayerSpawnPoint(0, 0);
			Functions.PlaceStaticObject(150, 266, ObjectType.Wall);
			Functions.PlaceStaticObject(191, 262, ObjectType.Wall);
			Functions.PlaceStaticObject(161, 262, ObjectType.Wall);
			Functions.PlaceStaticObject(52, 263, ObjectType.Wall);
			Functions.PlaceStaticObject(97, 264, ObjectType.Wall);
			Functions.PlaceStaticObject(206, 264, ObjectType.Wall);
			Functions.PlaceStaticObject(207, 265, ObjectType.Wall);
			Functions.PlaceStaticObject(125, 265, ObjectType.Wall);
			Functions.PlaceStaticObject(39, 269, ObjectType.Wall);
			Functions.PlaceStaticObject(112, 269, ObjectType.Wall);
			Functions.PlaceZombie(127, 211, ZombieType.Regular);
			Functions.PlaceZombie(105, 208, ZombieType.Regular);
			Functions.PlaceZombie(26, 208, ZombieType.Regular);
			Functions.PlaceZombie(58, 211, ZombieType.Regular);
			Functions.PlaceZombie(165, 204, ZombieType.Regular);
			Functions.PlaceZombie(142, 204, ZombieType.Regular);
			Functions.PlaceZombie(87, 204, ZombieType.Regular);
			Functions.PlaceZombie(13, 205, ZombieType.Regular);
			Functions.PlaceZombie(-162, -26, ZombieType.Regular);
			Functions.PlaceZombie(-96, -33, ZombieType.Regular);
			Functions.PlaceZombie(-51, -9, ZombieType.Regular);
			Functions.PlaceZombie(-58, 44, ZombieType.Regular);
			Functions.PlaceZombie(-135, 88, ZombieType.Regular);
			Functions.PlaceZombie(-240, 83, ZombieType.Regular);
			Functions.PlaceZombie(-285, -17, ZombieType.Regular);
			Functions.PlaceZombie(-195, -52, ZombieType.Regular);
			Functions.PlaceZombie(39, -85, ZombieType.Regular);
			Functions.PlaceZombie(-39, -144, ZombieType.Regular);
			Functions.PlaceZombie(-130, -112, ZombieType.Regular);
			Functions.PlaceZombie(140, -103, ZombieType.Regular);
			Functions.PlaceZombie(-392, 265, ZombieType.Regular);
			Functions.PlaceZombie(-319, 275, ZombieType.Regular);
		}
	}
}