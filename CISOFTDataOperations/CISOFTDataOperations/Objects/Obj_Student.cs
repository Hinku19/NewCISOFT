using System;
using System.Collections.Generic;
using System.Text;

namespace CISOFTDataOperations.Objects
{
    public class Obj_Student
    {

    }

    public class Students
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string PaternalSurname { get; set; }
        public string MaternalSurname { get; set; }
        public string Fullname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public int Phone1 { get; set; }
        public int Phone2 { get; set; }

        public Students(int studentID, string name, string paternalSurname, string maternalSurname, string fullname, DateTime birthdate, string email, int phone1, int phone2)
        {
            StudentID = studentID;
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
