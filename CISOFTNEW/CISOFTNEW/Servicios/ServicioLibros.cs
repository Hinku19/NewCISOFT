using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Text;
using Microsoft.Data.SqlClient;

namespace CISOFTNEW.Servicios
{
    //Libro de los cursos. Las existencias salen de la base de datos inventario de SOCOADA 10.10.15.2 (solo lectura)
    public class Libro
    {
        public int Id { get; set; }

        //Código del inventario (FC1, FC2, FC3, FFC4 ,PSSG.. hasta el momento son los 5 libros que se utilizan). Se captura al dar de alta y ya no cambia.
        [StringLength(30, ErrorMessage = "Máximo 30 caracteres.")]
        public string? Codigo { get; set; }

        [Required(ErrorMessage = "Escribe el nombre del libro.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string? Descripcion { get; set; }

        /// <summary>Existencias en el inventario (0 si el libro no aparece en él).</summary>
        public int Existencias { get; set; }

        /// <summary>Niveles activos que usan el libro, por ejemplo "1A, 1AKIDS, 1B".</summary>
        public string Niveles { get; set; } = "";

        public bool Activo { get; set; } = true;

        public Libro Clonar() => (Libro)MemberwiseClone();
    }

    /// <summary>
    /// Punto único de acceso a los libros para todas las pantallas.
    /// Lee y guarda con el SP dbo.NewCisoft_Books.
    /// </summary>
    public class ServicioLibros
    {
        private const string SP = "dbo.NewCisoft_Books";

        // Se carga una vez y se reutiliza en cada búsqueda (la búsqueda corre con cada tecla).
        private List<Libro>? _libros;

        public IReadOnlyList<string> Estados { get; } = new[] { "Activo", "Inactivo" };

        /// <param name="estado">"" = todos, "Activo" o "Inactivo".</param>
        public List<Libro> Buscar(string texto, string estado)
        {
            _libros ??= CargarDeBase();

            var q = Normalizar(texto.Trim());
            return _libros
                .Where(l => q.Length == 0
                         || Normalizar(l.Descripcion).Contains(q)
                         || Normalizar(l.Codigo).Contains(q)
                         || Normalizar(l.Niveles).Contains(q))
                .Where(l => estado.Length == 0 || l.Activo == (estado == "Activo"))
                .ToList();   // ya vienen ordenados del SP: primero los actuales
        }

        /// <summary>Obliga a volver a leer de la base (y del inventario) en la siguiente búsqueda.</summary>
        public void Recargar() => _libros = null;

        /// <summary>Guarda (alta o cambio). Regresa un mensaje de error, o null si se guardó.</summary>
        public string? Guardar(Libro libro)
        {
            string codigo = (libro.Codigo ?? "").Trim().ToUpperInvariant();
            string descripcion = (libro.Descripcion ?? "").Trim();

            try
            {
                if (libro.Id == 0)
                {
                    EjecutarSP(
                        new SqlParameter("@Option", SqlDbType.VarChar, 10) { Value = "INSERT" },
                        new SqlParameter("@BookCode", SqlDbType.VarChar, 30) { Value = codigo },
                        new SqlParameter("@BookDescription", SqlDbType.NVarChar, 100) { Value = descripcion },
                        new SqlParameter("@IsActive", SqlDbType.Bit) { Value = libro.Activo });
                }
                else
                {
                    // El código no se manda: no se puede cambiar.
                    EjecutarSP(
                        new SqlParameter("@Option", SqlDbType.VarChar, 10) { Value = "UPDATE" },
                        new SqlParameter("@BookId", SqlDbType.Int) { Value = libro.Id },
                        new SqlParameter("@BookDescription", SqlDbType.NVarChar, 100) { Value = descripcion },
                        new SqlParameter("@IsActive", SqlDbType.Bit) { Value = libro.Activo });
                }
            }
            catch (SqlException ex)
            {
                // Los RAISERROR del SP llegan aquí con su mensaje, por ejemplo
                // "Ya existe un libro con el código FC1."
                return ex.Message;
            }

            Recargar();
            return null;
        }

        private static List<Libro> CargarDeBase()
        {
            DataSet ds = EjecutarSP(
                new SqlParameter("@Option", SqlDbType.VarChar, 10) { Value = "LIST" });

            var lista = new List<Libro>();

            if (ds == null || ds.Tables.Count == 0)
                return lista;

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                lista.Add(new Libro
                {
                    Id = Convert.ToInt32(r["BookId"]),
                    Codigo = r["BookCode"] as string,                       // DBNull -> null (libros viejos)
                    Descripcion = Convert.ToString(r["BookDescription"])?.Trim(),
                    Existencias = Convert.ToInt32(r["Stock"]),
                    Niveles = Convert.ToString(r["LevelCodes"]) ?? "",
                    Activo = Convert.ToBoolean(r["IsActive"])
                });
            }

            return lista;
        }

        private static DataSet EjecutarSP(params SqlParameter[] parametros) =>
            CISOFTDataOperations.Database.DataOperations.ExecuteProcedure(
                CISOFTDataOperations.Database.Connection.NewCiSoft(),
                SP,
                parametros);

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