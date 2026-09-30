using System;
using System.Collections.Generic;
using System.Text;

namespace VisionSolutionsDataOperations.Database
{
    public static class Constants
    {
        public const string ConfigFolder =
            @"C:\Program Files\VisionSolutions\Config\";

        // Se conservarán para una futura migración del sistema de cifrado.
        public const string EncryptionKey =
            "VisionSolutions2026SecureKey!!";

        public const string EncryptionIV =
            "VS2026InitVect!";
    }
}