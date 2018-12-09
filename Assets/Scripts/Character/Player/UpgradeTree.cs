using Database.InGame;
using Database.Login;
using MySql.Data.MySqlClient;
using UnityEngine;

namespace Character.Player
{
    public class UpgradeTree:MonoBehaviour
    {
        public int ExtraHpLevel;
        public int ExtraAttackLevel;
        public int ExtraArmorLevel;
        
        public int ExtraCastleHpLevel;
        public int ExtraBulletSpecialLevel;

        public static UpgradeTree PlayerArchive;

        private void Awake()
        {
            if (PlayerArchive == null)
            {
                PlayerArchive = this;
                this.init();
            }
            else
            {
                Destroy(gameObject);
            }
           				
        }

        private void init(){
            
            GameInformation gameinfo = new GameInformation();
            
            MySqlConnection conn = GetConnection(); 
            MySqlCommand cmd = new MySqlCommand("select * from gameinformation where userId ='"+LoginService.userId+"';", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            // 逐行读取数据
            while (reader.Read())
            {
                gameinfo.Money = reader.GetInt32("money");
                gameinfo.PlayerHp = reader.GetInt32("playerHp");
                gameinfo.PlayerSpeed = reader.GetInt32("playerSpeed");
                gameinfo.PlayerArmor = reader.GetInt32("playerArmor");
                gameinfo.FortressHp  = reader.GetInt32("fortressHp");
            }

            ExtraHpLevel = gameinfo.PlayerHp;
            ExtraAttackLevel = gameinfo.PlayerArmor;
            ExtraArmorLevel = 0;
            ExtraCastleHpLevel = gameinfo.FortressHp;
            ExtraBulletSpecialLevel = gameinfo.PlayerSpeed;
            reader.Close();
        }


        /*public int ExtraHpLevel
        {
            get { return _extraHpLevel; }
            set { _extraHpLevel = value; }
        }

        public int ExtraAttackLevel
        {
            get { return _extraAttackLevel; }
            set { _extraAttackLevel = value; }
        }

        public int ExtraArmorLevel
        {
            get { return _extraArmorLevel; }
            set { _extraArmorLevel = value; }
        }

        public int ExtraCastleHpLevel
        {
            get { return _extraCastleHpLevel; }
            set { _extraCastleHpLevel = value; }
        }

        public int ExtraBulletSpecialLevel
        {
            get { return _extraBulletSpecialLevel; }
            set { _extraBulletSpecialLevel = value; }
        }*/
        
        public MySqlConnection GetConnection()  
        {  
            MySqlConnection conn = new MySqlConnection("Server=bj-cdb-rhhshji8.sql.tencentcdb.com;Port=63254;UserId=root;Password=LIzhen123;Database=shj");  
            conn.Open();  
            return conn;
        }  
    }
}