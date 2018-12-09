using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database.InGame
{
	public class GameInformation 
	{
		public int Id { get; set; }
    	public int Money { get; set; }
    	public int PlayerHp { get; set; }
    	public int PlayerSpeed { get; set; }
    	public int PlayerArmor { get; set; }
    	public int FortressHp { get; set; }

    	public GameInformation()
    	{  

    	}  
      
    	public GameInformation(int money, int playerHp, int playerSpeed, int playerArmor, int fortressHp)
    	{  
    		this.Money = money;  
    		this.PlayerHp = playerHp; 
    		this.PlayerSpeed = playerSpeed; 
    		this.PlayerArmor = playerArmor; 
    		this.FortressHp = fortressHp;
    	}  
	}
}

