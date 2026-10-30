using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CISOFTNEW.Servicios
{
    /// <summary>Datos de un alumno. Las anotaciones [Required], [RegularExpression], etc.
    /// son las reglas de validación que usa el formulario automáticamente.</summary>
    public class Alumno
    {
        public string? Matricula { get; set; }
        public string? NoClienteStg { get; set; }

        [Required(ErrorMessage = "Escribe el primer nombre.")]
        public string? PrimerNombre { get; set; }

        public string? SegundoNombre { get; set; }

        [Required(ErrorMessage = "Escribe el apellido paterno.")]
        public string? ApellidoPaterno { get; set; }

        public string? ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "Selecciona una opción.")]
        public string? Sexo { get; set; }

        [Required(ErrorMessage = "Indica la fecha de nacimiento.")]
        public DateTime? FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Escribe la CURP.")]
        [RegularExpression(@"(?i)^[A-Z]{4}\d{6}[HM][A-Z]{5}[A-Z0-9]\d$", ErrorMessage = "La CURP no tiene un formato válido (18 caracteres).")]
        public string? Curp { get; set; }

        /// <summary>Nombre del tipo de sangre (p. ej. "O+"), tomado del catálogo de tipos de sangre.</summary>
        public string? TipoSangre { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres.")]
        public string? Alergias { get; set; }

        [Required(ErrorMessage = "Escribe el correo electrónico.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        public string? Email { get; set; }

        [RegularExpression(@"^[\d\s]{10,14}$", ErrorMessage = "Escribe 10 dígitos.")]
        public string? Telefono { get; set; }

        [RegularExpression(@"^[\d\s]{10,14}$", ErrorMessage = "Escribe 10 dígitos.")]
        public string? Celular { get; set; }

        [Required(ErrorMessage = "Escribe la calle.")]
        public string? Calle { get; set; }

        [Required(ErrorMessage = "Escribe el número exterior.")]
        public string? NoExterior { get; set; }

        public string? NoInterior { get; set; }

        [Required(ErrorMessage = "Escribe el código postal.")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "El código postal debe tener 5 dígitos.")]
        public string? CodigoPostal { get; set; }

        [Required(ErrorMessage = "Escribe la colonia.")]
        public string? Colonia { get; set; }

        [Required(ErrorMessage = "Escribe la ciudad.")]
        public string? Ciudad { get; set; }

        [Required(ErrorMessage = "Selecciona el tipo.")]
        public string? Tipo { get; set; }

        public string? Grupo { get; set; }

        /// <summary>false = dado de baja: no debe tomarse en cuenta en los demás procesos del sistema.</summary>
        public bool Activo { get; set; } = true;

        public int? BloodTypeId {get;set;}
        public int? AllergiesId {get;set;}


        // ---- Propiedades calculadas (no se guardan) ----
        public string NombreCompleto =>
            string.Join(" ", new[] { PrimerNombre, SegundoNombre, ApellidoPaterno, ApellidoMaterno }.Where(s => !string.IsNullOrWhiteSpace(s)));

        public string Iniciales =>
            $"{Primera(PrimerNombre)}{Primera(ApellidoPaterno)}".ToUpperInvariant();

        public Alumno Clonar() => (Alumno)MemberwiseClone();

        private static string Primera(string? s) => string.IsNullOrEmpty(s) ? "" : s[..1];
    }

  
      public class ServicioAlumnos
    {
        private const string SP_ALUMNOS = "NewCisoft_StudentData";

        private List<Alumno> _alumnos = new();

        // TODO: después se llenan desde los catálogos

        //*EL COMBO DE LOS TIPOS SE DESPLIEGA DE ESTA MANERA!!! 
        private Dictionary<string, int> _tiposPorNombre =new();
       public IReadOnlyList<string> Tipos => _tiposPorNombre.Keys.ToList();
        public IReadOnlyList<string> Grupos => _alumnos.Select(a =>a.Grupo)
            .Where(g=> !string.IsNullOrWhiteSpace(g))
            .Distinct()
            .OrderBy(g => g)
            .ToList()!;
        
                /// <summary>Carga el catálogo de tipos de alumno (SP: TYPES).</summary>
        private void CargarTipos()
        {
            DataSet ds = CISOFTDataOperations.Database.DataOperations.ExecuteProcedure(
                CISOFTDataOperations.Database.Connection.NewCiSoft(),
                SP_ALUMNOS,
                P("@Option", "TYPES"));

            _tiposPorNombre = new Dictionary<string, int>();

            if (ds == null || ds.Tables.Count == 0)
                return;

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                string? nombre = Texto(r, "StudentType");
                if (!string.IsNullOrEmpty(nombre) && !_tiposPorNombre.ContainsKey(nombre))
                    _tiposPorNombre.Add(nombre, Convert.ToInt32(r["StudentTypeId"]));
            }
        }

        /// <summary>Trae la lista general desde la base (SP: LIST).</summary>
        public void CargarAlumnos()
        {

            CargarTipos();

            DataSet ds = CISOFTDataOperations.Database.DataOperations.ExecuteProcedure(
                CISOFTDataOperations.Database.Connection.NewCiSoft(),
                SP_ALUMNOS,
                new SqlParameter("@Option", SqlDbType.VarChar, 10) { Value = "LIST" });

            _alumnos = new List<Alumno>();

            if (ds == null || ds.Tables.Count == 0)
                return;

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                _alumnos.Add(new Alumno
                {
                    Matricula       = Texto(r, "StudentId"),
                    PrimerNombre    = Texto(r, "FirstName"),
                    ApellidoPaterno = Texto(r, "PaternalSurname"),
                    ApellidoMaterno = Texto(r, "MaternalSurname"),
                    Curp            = Texto(r, "CURP"),
                    // Email           = Texto(r, "Email"),
                    Grupo = Texto(r, "GroupName"),
                    
                    Tipo            = Texto(r,"StudentType"),
                    Celular         = Texto(r, "PersonalPhone"),
                    Activo          = r["IsActive"] != DBNull.Value && Convert.ToBoolean(r["IsActive"])
                });
            }
        }

        /// <param name="estado">"" = todos, "Activo" o "Inactivo".</param>
        public List<Alumno> Buscar(string texto, string tipo, string grupo, string estado = "")
        {
            var q = Normalizar(texto.Trim());
            return _alumnos
                .Where(a => q.Length == 0 || new[] { a.NombreCompleto, a.Matricula, a.Curp, a.Email }
                                             .Any(c => Normalizar(c).Contains(q)))
                .Where(a => tipo.Length == 0 || a.Tipo == tipo)
                .Where(a => grupo.Length == 0 || a.Grupo == grupo)
                .Where(a => estado.Length == 0 || a.Activo == (estado == "Activo"))
                .OrderBy(a => a.ApellidoPaterno).ThenBy(a => a.PrimerNombre)
                .ToList();
        }

        /// <summary>Trae el detalle completo de un alumno (SP: SELECT).</summary>
        public Alumno? ObtenerPorMatricula(string matricula)
        {
            if (!int.TryParse(matricula, out int studentId))
                return null;

            DataSet ds = CISOFTDataOperations.Database.DataOperations.ExecuteProcedure(
                CISOFTDataOperations.Database.Connection.NewCiSoft(),
                SP_ALUMNOS,
                new SqlParameter("@Option", SqlDbType.VarChar, 10) { Value = "SELECT" },
                new SqlParameter("@StudentId", SqlDbType.Int) { Value = studentId });

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;

            DataRow r = ds.Tables[0].Rows[0];
            string sexo = (Texto(r, "Sex") ?? "").ToUpperInvariant();

            return new Alumno
            {
                Matricula       = Texto(r, "StudentId"),
                NoClienteStg    = Texto(r, "ClientId"),
                PrimerNombre    = Texto(r, "FirstName"),
                SegundoNombre   = Texto(r, "SecondName"),
                ApellidoPaterno = Texto(r, "PaternalSurname"),
                ApellidoMaterno = Texto(r, "MaternalSurname"),
                Sexo            = sexo == "M" ? "Masculino" : sexo == "F" ? "Femenino" : null,
                FechaNacimiento = r["BirthDate"] == DBNull.Value ? null : Convert.ToDateTime(r["BirthDate"]),
                Curp            = Texto(r, "CURP"),
                Email           = Texto(r, "Email"),
                Celular         = Texto(r, "PersonalPhone"),
                Telefono        = Texto(r, "EmergencyPhone"),
                Calle           = Texto(r, "Address"),
                NoExterior      = Texto(r, "ExtNumber"),
                NoInterior      = Texto(r, "IntNumber"),
                CodigoPostal    = Texto(r, "SatcCodigoPostal"),
                Colonia         = Texto(r, "Neighborhood"),
                Ciudad          = Texto(r, "City"),
                Tipo            = Texto(r, "StudentType"),
                Grupo           = Texto(r, "GroupName"),
                BloodTypeId     =Entero(r,"BloodTypeId"),
                AllergiesId = Entero(r, "AllergiesId"),
                Activo          = r["IsActive"] != DBNull.Value && Convert.ToBoolean(r["IsActive"])
            };
        }

        /// <summary>TEMPORAL: guarda solo en memoria. Se conecta al SP con INSERT / UPDATE.</summary>
              /// <summary>Alta o edición (SP: INSERT / UPDATE).
        /// Regresa un mensaje de error, o null si se guardó.</summary>
        public string? Guardar(Alumno a)
        {
            bool esNuevo = string.IsNullOrEmpty(a.Matricula);

            if (!_tiposPorNombre.TryGetValue(a.Tipo ?? "", out int tipoId))
                return "Selecciona una clasificación válida.";

            var parametros = new[]
            {
                P("@Option",           esNuevo ? "INSERT" : "UPDATE"),
                P("@StudentId",        esNuevo ? null : (object)int.Parse(a.Matricula!)),
                P("@FirstName",        Limpio(a.PrimerNombre)),
                P("@SecondName",       Limpio(a.SegundoNombre)),
                P("@PaternalSurname",  Limpio(a.ApellidoPaterno)),
                P("@MaternalSurname",  Limpio(a.ApellidoMaterno)),
                P("@BirthDate",        a.FechaNacimiento?.Date),
                P("@Sex",              a.Sexo == "Masculino" ? "M" : a.Sexo == "Femenino" ? "F" : null),
                P("@CURP",             Limpio(a.Curp)?.ToUpperInvariant()),
                P("@Email",            Limpio(a.Email)),
                P("@StudentTypeId",    tipoId),
                P("@PersonalPhone",    SoloDigitos(a.Celular)),
                P("@EmergencyPhone",   SoloDigitos(a.Telefono)),
                P("@Address",          Limpio(a.Calle)),
                P("@ExtNumber",        Limpio(a.NoExterior)),
                P("@IntNumber",        Limpio(a.NoInterior)),
                P("@Neighborhood",     Limpio(a.Colonia)),
                P("@SatcCodigoPostal", int.TryParse(a.CodigoPostal, out int cp) ? cp : null),
                P("@City",             Limpio(a.Ciudad)),
                P("@ClientId",         Limpio(a.NoClienteStg)),
                P("@AllergiesId",      a.AllergiesId),
                P("@BloodTypeId",      a.BloodTypeId),
                P("@IsActive",         a.Activo)
            };

            try
            {
                DataSet ds = CISOFTDataOperations.Database.DataOperations.ExecuteProcedure(
                    CISOFTDataOperations.Database.Connection.NewCiSoft(),
                    SP_ALUMNOS,
                    parametros);

                if (esNuevo)
                {
                    a.Matricula = Convert.ToString(ds.Tables[0].Rows[0]["StudentId"]);
                }
                else if (Convert.ToInt32(ds.Tables[0].Rows[0]["RowsAffected"]) == 0)
                {
                    return "No se encontró el alumno en la base de datos.";
                }

                return null;
            }
            catch (SqlException ex)
            {
                return "No se pudo guardar: " + ex.Message;
            }
        }
        /// <summary>Lee una columna como texto; NULL de SQL se vuelve null de C#.</summary>
        private static string? Texto(DataRow r, string columna) =>
            r[columna] == DBNull.Value ? null : Convert.ToString(r[columna])?.Trim();

                // Convierte null de C# en NULL de SQL
        private static SqlParameter P(string nombre, object? valor) =>
            new SqlParameter(nombre, valor ?? DBNull.Value);

        // Texto vacío o con solo espacios se guarda como NULL
        private static string? Limpio(string? s) =>
            string.IsNullOrWhiteSpace(s) ? null : s.Trim();

        // "662 123 4567" → 6621234567 (la columna es NUMERIC)
        private static decimal? SoloDigitos(string? s)
        {
            var digitos = new string((s ?? "").Where(char.IsDigit).ToArray());
            return digitos.Length == 0 ? null : decimal.Parse(digitos);
        }

        private static int? Entero(DataRow r, string columna) =>
            r[columna] == DBNull.Value ? null : Convert.ToInt32(r[columna]);

        private static string Normalizar(string? s)
        {
            var sb = new StringBuilder();
            foreach (var c in (s ?? "").Normalize(NormalizationForm.FormD))
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            return sb.ToString().ToLowerInvariant();
        }
  
  
        private static Alumno Nuevo(string mat, string nom1, string nom2, string pat, string mat2, string tipo, string grupo, string email, string cel) => new()
        {
            Matricula = mat, PrimerNombre = nom1, SegundoNombre = nom2, ApellidoPaterno = pat, ApellidoMaterno = mat2,
            Tipo = tipo, Grupo = grupo, Email = email, Celular = cel, Ciudad = "Caborca"
        };
    }
}
