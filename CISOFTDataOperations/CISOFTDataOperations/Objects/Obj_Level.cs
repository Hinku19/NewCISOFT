using System;
using System.Collections.Generic;
using System.Text;

namespace CISOFTDataOperations.Objects
{
    //ÁQUI SE VA A ALMACENAR EL IDENTIFICADOR DEL NIVEL, 1A, 1B, 1C, 2A, 2B, 2C, 3A, 3B, 3C, 4A, 4B, 4C, 5A, 5B, 5C
    public class Obj_Level
    {
    }

    public class Levels
    {
        public int LevelID { get; set; }
        public string LevelDescription { get; set; }
        public Levels(int levelID, string levelDescription)
        {
            LevelID = levelID;
            LevelDescription = levelDescription;
        }
    }

}
