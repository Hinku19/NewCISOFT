using System;
using System.Collections.Generic;
using System.Text;

namespace CISOFTDataOperations.Objects
{
    //IDENTIFICADOR DE GRUPO, CONFORMADO POR IDSTAGE, IDLEVEL, IDINCREMENTAL
    public class Obj_Group
    {

    }

    public class Groups
    {
        public int GroupID { get; set; }
        public int StageID { get; set; }
        public int LevelID { get; set; }
        public int IncrementalID { get; set; }
        public Groups(int groupID, int stageID, int levelID, int incrementalID)
        {
            GroupID = groupID;
            StageID = stageID;
            LevelID = levelID;
            IncrementalID = incrementalID;
        }
    }
}
