using System;
using System.Collections.Generic;
using System.Text;

namespace CISOFTDataOperations.Security
{
    public class UserSession
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public int CompanyId { get; set; }

        public int DivisionId { get; set; }

        public int SubsidiaryId { get; set; }

        public UserSession()
        {
            UserId = 0;
            UserName = string.Empty;
            CompanyId = 0;
            DivisionId = 0;
            SubsidiaryId = 0;
        }

        public UserSession(
            int userId,
            string userName,
            int companyId,
            int divisionId,
            int subsidiaryId)
        {
            UserId = userId;
            UserName = userName;
            CompanyId = companyId;
            DivisionId = divisionId;
            SubsidiaryId = subsidiaryId;
        }
    }
}
