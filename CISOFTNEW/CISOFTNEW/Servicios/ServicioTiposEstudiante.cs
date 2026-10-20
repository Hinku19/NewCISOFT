using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace CISOFTNEW.Servicios
{
    /// <summary>Tipo de inscripción de un alumno (Plan familiar, Hijo de empleado, etc.).
    /// Las anotaciones son las reglas de validación que usa el formulario automáticamente.</summary>
    public class TipoEstudiante
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Escribe el nombre del tipo.")]
        [StringLength(60, ErrorMessage = "Máximo 60 caracteres.")]
        public string? Nombre { get; set; }

        [StringLength(250, ErrorMessage = "Máximo 250 caracteres.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Escribe el descuento (0 si no tiene).")]
        [Range(0, 100, ErrorMessage = "El descuento debe estar entre 0 y 100.")]
        public decimal? Descuento { get; set; } = 0;

        public bool Activo { get; set; } = true;

        public TipoEstudiante Clonar() => (TipoEstudiante)MemberwiseClone();
    }

    /// <summary>
    /// Punto único de acceso a los tipos de estudiante para todas las pantallas.
    /// HOY: usa una lista en memoria con datos de ejemplo.
    /// DESPUÉS: aquí se conecta CISOFTDataOperations sin tocar las pantallas.
    /// </summary>
    public class ServicioTiposEstudiante
    {
        private readonly List<TipoEstudiante> _tipos = DatosDeEjemplo();

        /// <param name="estado">"" = todos, "Activo" o "Inactivo".</param>
        public List<TipoEstudiante> Buscar(string texto, string estado)
        {
            var q = Normalizar(texto.Trim());
            return _tipos
                .Where(t => q.Length == 0 || Normalizar(t.Nombre).Contains(q) || Normalizar(t.Descripcion).Contains(q))
                .Where(t => estado.Length == 0 || t.Activo == (estado == "Activo"))
                .OrderBy(t => t.Nombre)
                .ToList();
        }

        /// <summary>Guarda (alta o cambio). Regresa un mensaje de error, o null si se guardó.</summary>
        public string? Guardar(TipoEstudiante tipo)
        {
            tipo.Nombre = tipo.Nombre?.Trim();
            tipo.Descripcion = tipo.Descripcion?.Trim();

            bool repetido = _tipos.Any(t =>
                t.Id != tipo.Id && Normalizar(t.Nombre) == Normalizar(tipo.Nombre));

            if (repetido)
            {
                return $"Ya existe un tipo llamado \"{tipo.Nombre}\".";
            }

            if (tipo.Id == 0)
            {
                // TODO: el Id real debe generarlo la base de datos
                tipo.Id = _tipos.Count == 0 ? 1 : _tipos.Max(t => t.Id) + 1;
                _tipos.Add(tipo);
                return null;
            }

            int i = _tipos.FindIndex(t => t.Id == tipo.Id);
            if (i >= 0) _tipos[i] = tipo; else _tipos.Add(tipo);
            return null;
        }

        private static string Normalizar(string? s)
        {
            var sb = new StringBuilder();
            foreach (var c in (s ?? "").Normalize(NormalizationForm.FormD))
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            return sb.ToString().ToLowerInvariant();
        }

        private static List<TipoEstudiante> DatosDeEjemplo() => new()
        {
            Nuevo(1, "Regular", "Alumno inscrito de forma individual, sin convenio ni descuento.", 0, true),
            Nuevo(2,"Foráneo","Alumno Foráneo", 5,true ),
            Nuevo(3,"Hijo de empleado","Alumno que es hijo o pariente de empleado", 10,true ),
            Nuevo(4,"Empleado","Empleado activo de SOCOADA", 50,true ),
            Nuevo(5,"Descuento especial","Alumno con descuento especial", 10,true ),
            Nuevo(6,"Paquete Familiar","Alumno incluido en paquete familiar", 10,true ),

        };

        private static TipoEstudiante Nuevo(int id, string nombre, string descripcion, decimal descuento, bool activo) => new()
        {
            Id = id, Nombre = nombre, Descripcion = descripcion, Descuento = descuento, Activo = activo
        };
    }
}
