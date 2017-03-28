using UnityEngine;
using System.Collections;

public static class SceneRef{

	private static int LevelSelect = 1;

	public static int getLevelSelect
	{
		get { return LevelSelect; }
	}

	private static int MainMenu = 0;

	public static int getMainMenu
	{
		get { return MainMenu; }
	}

	private static int tutorial = 2;

	public static int getTutorial
	{
		get { return tutorial; }
	}

	private static int fire = 3;

	public static int getFireLevel
	{
		get { return fire; }
	}

	private static int ice = 4;

	public static int getIcelevel
	{
		get { return ice; }
	}

	private static int wind = 5;

	public static int getWindLevel
	{
		get { return wind; }
	}

	private static int earth = 6;

	public static int getEarthLevel
	{
		get { return earth; }
	}

	private static int final = 7;

	public static int getFinalLevel
	{
		get { return final; }
	}
}
