using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace CISOFTNEW.Servicios
{
    /// <summary>Datos de un alumno. Las anotaciones [Required], [RegularExpression], etc.
    /// son las reglas de validación que usa el formulario automáticamente.</summary>
    public class Alumno
    {
        public string? Matricula { get; set; }
        public string? NoClienteStg { get; set; }

        [Required(ErrorMessage = "Escribe el nombre.")]
        public string? Nombres { get; set; }

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

        [RegularExpression(@"(?i)^[A-ZÑ&]{3,4}\d{6}[A-Z0-9]{3}$", ErrorMessage = "El RFC no tiene un formato válido.")]
        public string? Rfc { get; set; }

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

        [Required(ErrorMessage = "Escribe el estado.")]
        public string? Estado { get; set; } = "Sonora";

        [Required(ErrorMessage = "Escribe el país.")]
        public string? Pais { get; set; } = "México";

        [Required(ErrorMessage = "Selecciona la clasificación.")]
        public string? Clasificacion { get; set; }

        public string? Grupo { get; set; }

        // ---- Propiedades calculadas (no se guardan) ----
        public string NombreCompleto =>
            string.Join(" ", new[] { Nombres, ApellidoPaterno, ApellidoMaterno }.Where(s => !string.IsNullOrWhiteSpace(s)));

        public string Iniciales =>
            $"{Primera(Nombres)}{Primera(ApellidoPaterno)}".ToUpperInvariant();

        public Alumno Clonar() => (Alumno)MemberwiseClone();

        private static string Primera(string? s) => string.IsNullOrEmpty(s) ? "" : s[..1];
    }

    /// <summary>
    /// Punto único de acceso a los datos de alumnos para todas las pantallas.
    /// HOY: usa una lista en memoria con datos de ejemplo.
    /// DESPUÉS: aquí conectarás CISOFTDataOperations (Obj_Student, etc.) sin tocar las pantallas.
    /// </summary>
    public class ServicioAlumnos
    {
        private readonly List<Alumno> _alumnos = DatosDeEjemplo();

        public IReadOnlyList<string> Clasificaciones { get; } = new[] { "Regular", "Becado", "Intercambio" };
        public IReadOnlyList<string> Grupos { get; } = new[] { "Grupo A", "Grupo B", "Grupo C" };

        public List<Alumno> Buscar(string texto, string clasificacion, string grupo)
        {
            var q = Normalizar(texto.Trim());
            return _alumnos
                .Where(a => q.Length == 0 || new[] { a.NombreCompleto, a.Matricula, a.Curp, a.Rfc, a.Email }
                                             .Any(c => Normalizar(c).Contains(q)))
                .Where(a => clasificacion.Length == 0 || a.Clasificacion == clasificacion)
                .Where(a => grupo.Length == 0 || a.Grupo == grupo)
                .OrderBy(a => a.ApellidoPaterno).ThenBy(a => a.Nombres)
                .ToList();
        }

        public Alumno? ObtenerPorMatricula(string matricula) =>
            _alumnos.FirstOrDefault(a => a.Matricula == matricula);

        public void Guardar(Alumno alumno)
        {
            alumno.Curp = alumno.Curp?.ToUpperInvariant();
            alumno.Rfc = alumno.Rfc?.ToUpperInvariant();

            if (string.IsNullOrEmpty(alumno.Matricula))
            {
                // TODO: la matrícula real debe generarla tu base de datos / DataOperations
                alumno.Matricula = $"A-{DateTime.Today:yy}-{_alumnos.Count + 1:0000}";
                _alumnos.Add(alumno);
                return;
            }

            int i = _alumnos.FindIndex(a => a.Matricula == alumno.Matricula);
            if (i >= 0) _alumnos[i] = alumno; else _alumnos.Add(alumno);
        }

        private static string Normalizar(string? s)
        {
            var sb = new StringBuilder();
            foreach (var c in (s ?? "").Normalize(NormalizationForm.FormD))
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            return sb.ToString().ToLowerInvariant();
        }

        private static List<Alumno> DatosDeEjemplo() => new()
        {
            Nuevo("A-26-0148", "Mariana", "López", "Ruiz", "Regular", "Grupo A", "mariana.lopez@correo.com", "662 123 4567"),
            Nuevo("A-26-0147", "José Luis", "Peralta", "Soto", "Becado", "Grupo B", "jl.peralta@correo.com", "637 555 0192"),
            Nuevo("A-26-0145", "Daniela", "Ochoa", "Villa", "Regular", "Grupo A", "dani.ochoa@correo.com", "631 208 3345"),
            Nuevo("A-26-0141", "Andrés", "Quiroz", "Maldonado", "Intercambio", "Grupo C", "andres.qm@correo.com", "662 874 1120"),
            Nuevo("A-26-0139", "Valeria", "Cota", "Encinas", "Regular", "Grupo B", "vale.cota@correo.com", "637 102 7788"),
            Nuevo("A-26-0132", "Luis Fernando", "Ríos", "", "Becado", "Grupo A", "lf.rios@correo.com", "662 331 9054"),
            Nuevo("A-26-0128", "Ximena", "Duarte", "Araiza", "Regular", "Grupo C", "ximena.da@correo.com", "631 440 2231"),
            Nuevo("A-26-0125", "Carlos", "Ibarra", "Leyva", "Regular", "Grupo B", "c.ibarra@correo.com", "662 290 6612"),
            Nuevo("A-26-0121", "Sofía", "Moreno", "Tapia", "Intercambio", "Grupo A", "sofia.mt@correo.com", "637 118 4409"),
        };

        private static Alumno Nuevo(string mat, string nom, string pat, string mat2, string clas, string grupo, string email, string cel) => new()
        {
            Matricula = mat, Nombres = nom, ApellidoPaterno = pat, ApellidoMaterno = mat2,
            Clasificacion = clas, Grupo = grupo, Email = email, Celular = cel, Ciudad = "Caborca"
        };
    }
}
