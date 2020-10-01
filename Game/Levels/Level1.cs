namespace ShitGame.Levels
{
	public static class Level_1
	{
		public static void Load()
		{
			Functions.SetPlayerSpawnPoint(0, 0);
			Functions.PlaceStaticObject(941, 958, ObjectType.Wall);
			Functions.PlaceStaticObject(-578, 727, ObjectType.Wall);
			Functions.PlaceStaticObject(-1001, -521, ObjectType.Wall);
			Functions.PlaceStaticObject(-632, -528, ObjectType.Wall);
			Functions.PlaceStaticObject(37, -549, ObjectType.Wall);
			Functions.PlaceStaticObject(289, -555, ObjectType.Wall);
			Functions.PlaceZombie(-1083, -1096, ZombieType.Regular);
			Functions.PlaceZombie(-851, -1199, ZombieType.Regular);
			Functions.PlaceZombie(-334, -1036, ZombieType.Regular);
			Functions.PlaceZombie(-14, -910, ZombieType.Regular);
			Functions.PlaceZombie(175, -1121, ZombieType.Regular);
			Functions.PlaceZombie(-65, -1129, ZombieType.Regular);
		}
	}
}