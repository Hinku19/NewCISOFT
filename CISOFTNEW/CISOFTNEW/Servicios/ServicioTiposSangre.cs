using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Text;
using Microsoft.Data.SqlClient;

namespace CISOFTNEW.Servicios
{
    //Tipo sanguíneo (grupo ABO + factor Rh). Son 8 fijos: solo se edita la descripción y el estado.
    public class TipoSangre
    {
        public int Id { get; set; }

        public string BloodName{get; set; } = "";


        /// <summary>A, B, AB u O.</summary>
        public string Grupo { get; set; } = "";

        public bool RhPositivo { get; set; }

        [StringLength(250, ErrorMessage = "Máximo 250 caracteres.")]
        public string? Descripcion { get; set; }

        public bool Activo { get; set; } = true;

        // ---- Propiedades calculadas (no se guardan) ----
        public string Nombre => Grupo + (RhPositivo ? "+" : "−");

        public string FactorRh => RhPositivo ? "Positivo (+)" : "Negativo (−)";

        public TipoSangre Clonar() => (TipoSangre)MemberwiseClone();
    }

   
    // Punto único de acceso a los tipos de sangre para todas las pantallas.
    //Lee y guarda con el SP dbo.NewCisoft_BloodTypes.
    
    public class ServicioTiposSangre
    {
        private const string SP = "dbo.NewCisoft_BloodTypes";

        // Se carga una vez y se reutiliza en cada búsqueda (son 8 tipos de sangre distintos).
        private List<TipoSangre>? _tipos;

        /// <param name="estado">"" = todos, "Activo" o "Inactivo".</param>
        public List<TipoSangre> Buscar(string texto, string estado)
        {
            _tipos ??= CargarDeBase();

            var q = Normalizar(texto.Trim()).Replace("-", "−");
            return _tipos
                .Where(t => q.Length == 0
                         || Normalizar(t.Nombre).Contains(q)
                         || Normalizar($"{t.Grupo} {(t.RhPositivo ? "positivo" : "negativo")}").Contains(q)
                         || Normalizar(t.Descripcion).Contains(q))
                .Where(t => estado.Length == 0 || t.Activo == (estado == "Activo"))
                .ToList();
        }

        //Obliga a volver a leer de la base en la siguiente búsqueda.
        public void Recargar() => _tipos = null;

        //Guarda descripción y estado de un tipo existente (no se pueden crear nuevos)
        public void Guardar(TipoSangre tipo)
        {
            // No se manda @BloodName: el SP conserva el nombre actual.
            // Descripción vacía ("") = borrar la descripción en la base.
            CISOFTDataOperations.Database.DataOperations.ExecuteProcedure(
                CISOFTDataOperations.Database.Connection.NewCiSoft(),
                SP,
                new SqlParameter("@Option", SqlDbType.VarChar, 10) { Value = "UPDATE" },
                new SqlParameter("@BloodTypeId", SqlDbType.Int) { Value = tipo.Id },
              
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = tipo.Activo });

            Recargar();
        }

        private static List<TipoSangre> CargarDeBase()
        {
            DataSet ds = CISOFTDataOperations.Database.DataOperations.ExecuteProcedure(
                CISOFTDataOperations.Database.Connection.NewCiSoft(),
                SP,
                new SqlParameter("@Option", SqlDbType.VarChar, 10) { Value = "LIST" });

            var lista = new List<TipoSangre>();

            if (ds == null || ds.Tables.Count == 0)
                return lista;

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                // BloodName viene como "A+", "O-", "AB+", etc.
                string nombre = Convert.ToString(r["BloodNAme"])?.Trim() ?? "";

                lista.Add(new TipoSangre
                {
                    Id = Convert.ToInt32(r["BloodTypeId"]),
                    BloodName = nombre,
                    Grupo = nombre.TrimEnd('+', '-', '−').Trim(),
                    RhPositivo = nombre.EndsWith("+"),
                   // Descripcion = r["Description"] as string,   // DBNull -> null
                    Activo = Convert.ToBoolean(r["IsActive"])
                });
            }

            return lista;
        }

        private static string Normalizar(string? s)
        {
            var sb = new StringBuilder();
            foreach (var c in (s ?? "").Normalize(NormalizationForm.FormD))
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            return sb.ToString().ToLowerInvariant();
        }
    }
}