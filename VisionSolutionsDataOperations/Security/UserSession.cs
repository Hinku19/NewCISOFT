using System;
using System.Collections.Generic;
using System.Text;

namespace VisionSolutionsDataOperations.Security
{
    public class UserSession
    {
        public int UserId { get; set; }

        public int CompanyId { get; set; }

        public int DivisionId { get; set; }

        public int SubsidiaryId { get; set; }

        public UserSession()
        {
            UserId = 0;
            CompanyId = 0;
            DivisionId = 0;
            SubsidiaryId = 0;
        }

        public UserSession(
            int userId,
            int companyId,
            int divisionId,
            int subsidiaryId)
        {
            UserId = userId;
            CompanyId = companyId;
            DivisionId = divisionId;
            SubsidiaryId = subsidiaryId;
        }
    }
}
