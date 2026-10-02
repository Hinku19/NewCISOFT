using System;
using System.Collections.Generic;
using System.Text;

namespace CISOFTDataOperations.Objects
{
    internal class Obj_Teacher
    {
    }

    public class Teachers
    {
        public int TeacherID { get; set; }
        public string Name { get; set; }
        public string PaternalSurname { get; set; }
        public string MaternalSurname { get; set; }
        public string Fullname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public Teachers(int teacherID, string name, string paternalSurname, string maternalSurname, string fullname, DateTime birthdate, string email, string phone1, string phone2)
        {
            TeacherID = teacherID;
            Name = name;
            PaternalSurname = paternalSurname;
            MaternalSurname = maternalSurname;
            Fullname = fullname;
            Birthdate = birthdate;
            Email = email;
            Phone1 = phone1;
            Phone2 = phone2;
        }
    }
}
