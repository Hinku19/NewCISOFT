using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CISOFTNEW.Servicios
{
    //Tipo de inscripción de un alumno (Plan familiar, Hijo de empleado, etc.)
    
    public class TipoEstudiante
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Escribe el nombre del tipo.")]
        [StringLength(60, ErrorMessage = "Máximo 60 caracteres.")]
        public string? Nombre { get; set; }

       //Precio en pesos (columna Pricing de la tabla)
        [Required(ErrorMessage = "Escribe el precio (0 si no tiene costo).")]
        [Range(0, 999999.99, ErrorMessage = "El precio debe estar entre $0 y $999,999.99.")]
        public decimal? Precio { get; set; } = 0;

        public bool Activo { get; set; } = true;

        public TipoEstudiante Clonar() => (TipoEstudiante)MemberwiseClone();
    }
    /// Lee y guarda con el SP NewCisoft_StudentTypes (LIST / INSERT / UPDATE).
  
    public class ServicioTiposEstudiante
    {

        private const string SP_TIPOS = "NewCisoft_StudentTypes";
        private List<TipoEstudiante> _tipos = new();

        /// <param name="estado">"" = todos, "Activo" o "Inactivo".</param>
        public List<TipoEstudiante> Buscar(string texto, string estado)
        {
            var q = Normalizar(texto.Trim());
            return _tipos
                .Where(t => q.Length == 0 || Normalizar(t.Nombre).Contains(q))
                .Where(t => estado.Length == 0 || t.Activo == (estado == "Activo"))
                .OrderBy(t => t.Nombre)
                .ToList();
        }

        /// <summary>Guarda (alta o cambio). Regresa un mensaje de error, o null si se guardó.</summary>
        public string? Guardar(TipoEstudiante tipo)
        {
            tipo.Nombre = tipo.Nombre?.Trim();

            bool repetido = _tipos.Any(t =>
                t.Id != tipo.Id && Normalizar(t.Nombre) == Normalizar(tipo.Nombre));

            if (repetido)
            {
                return $"Ya existe un tipo llamado \"{tipo.Nombre}\".";
            }

            bool esNuevo = tipo.Id == 0;

            try
            {
                DataSet ds = CISOFTDataOperations.Database.DataOperations.ExecuteProcedure(
                    CISOFTDataOperations.Database.Connection.NewCiSoft(),
                    SP_TIPOS,
                    P("@Option",        esNuevo ? "INSERT" : "UPDATE"),
                    P("@StudentTypeId", esNuevo ? null : (object)tipo.Id),
                    P("@Description",   tipo.Nombre),
                    P("@Pricing",       tipo.Precio ?? 0),
                    P("@IsActive",      tipo.Activo));

                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return "No se pudo guardar: la base de datos no regresó respuesta.";

                if (!esNuevo && Convert.ToInt32(ds.Tables[0].Rows[0]["RowsAffected"]) == 0)
                    return "No se encontró el tipo en la base de datos.";

                // Refresca la lista con lo que quedó en la base (incluye el Id nuevo)
                CargarTipos();
                return null;
            }
            catch (SqlException ex)
            {
                // 50000 = RAISERROR del SP (nombre repetido); el mensaje ya viene listo para mostrarse
                return ex.Number == 50000 ? ex.Message : "No se pudo guardar: " + ex.Message;
            }
        }

        private static string Normalizar(string? s)
        {
            var sb = new StringBuilder();
            foreach (var c in (s ?? "").Normalize(NormalizationForm.FormD))
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            return sb.ToString().ToLowerInvariant();
        }


        //*CARGAR LOS TIPOS DE ALUMNOS DESDE LA ABSE DE DATOS UTILIZANDO EL DATASET

        public void CargarTipos()
        {
            DataSet ds = CISOFTDataOperations.Database.DataOperations.ExecuteProcedure(
                CISOFTDataOperations.Database.Connection.NewCiSoft(),
                SP_TIPOS);

                _tipos = new List<TipoEstudiante>();

                if(ds == null || ds.Tables.Count == 0)
                return;

                foreach(DataRow r in ds.Tables[0].Rows)
            {
                _tipos.Add(new TipoEstudiante
                {
                    Id = Convert.ToInt32(r["StudentTypeId"]),
                    Nombre = Texto(r, "Description"),
                   
                    Precio = r["Pricing"] == DBNull.Value ? 0 : Convert.ToDecimal(r["Pricing"]),
                    Activo = r["IsActive"] != DBNull.Value && Convert.ToBoolean(r["IsActive"])
                });
            }
        }

        private static string? Texto (DataRow r, string columna) =>
            r[columna] == DBNull.Value ? null : Convert.ToString(r[columna])?.Trim();

        // Convierte null de C# en NULL de SQL
        private static SqlParameter P(string nombre, object? valor) =>
            new SqlParameter(nombre, valor ?? DBNull.Value);

    }
}
