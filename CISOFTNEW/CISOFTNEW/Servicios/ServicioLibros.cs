using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace CISOFTNEW.Servicios
{
    /// <summary>Libro del material de los cursos.
    /// Las anotaciones son las reglas de validación que usa el formulario automáticamente.</summary>
    public class Libro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Escribe el título.")]
        [StringLength(120, ErrorMessage = "Máximo 120 caracteres.")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "Escribe el autor o la editorial.")]
        [StringLength(120, ErrorMessage = "Máximo 120 caracteres.")]
        public string? Autor { get; set; }

        [RegularExpression(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d\-]+$", ErrorMessage = "El ISBN debe tener 10 o 13 dígitos (puede llevar guiones).")]
        public string? Isbn { get; set; }

        [Required(ErrorMessage = "Selecciona el nivel.")]
        public string? Nivel { get; set; }

        [Required(ErrorMessage = "Escribe las existencias (0 si no hay).")]
        [Range(0, 9999, ErrorMessage = "Escribe un número entre 0 y 9999.")]
        public int? Existencias { get; set; } = 0;

        [Required(ErrorMessage = "Escribe la existencia mínima.")]
        [Range(0, 9999, ErrorMessage = "Escribe un número entre 0 y 9999.")]
        public int? ExistenciaMinima { get; set; } = 5;

        [StringLength(40, ErrorMessage = "Máximo 40 caracteres.")]
        public string? Ubicacion { get; set; }

        /// <summary>Imagen de la portada como "data URL" (data:image/jpeg;base64,...). Null = sin portada.</summary>
        public string? Portada { get; set; }

        public bool Activo { get; set; } = true;

        // ---- Propiedades calculadas (no se guardan) ----

        /// <summary>Disponible, Bajo inventario, Agotado o Inactivo (se calcula con las existencias).</summary>
        public string Estado =>
            !Activo ? "Inactivo"
            : Existencias is null or <= 0 ? "Agotado"
            : Existencias <= ExistenciaMinima ? "Bajo inventario"
            : "Disponible";

        public Libro Clonar() => (Libro)MemberwiseClone();
    }

    /// <summary>
    /// Punto único de acceso a los libros para todas las pantallas.
    /// HOY: usa una lista en memoria con datos de ejemplo.
    /// DESPUÉS: aquí se conecta CISOFTDataOperations sin tocar las pantallas.
    /// </summary>
    public class ServicioLibros
    {
        private readonly List<Libro> _libros = DatosDeEjemplo();

        // TODO: cuando exista el catálogo de Niveles, esta lista debe salir de ahí
        public IReadOnlyList<string> Niveles { get; } = new[] { "Básico 1", "Básico 2", "Intermedio", "Avanzado" };

        public IReadOnlyList<string> Estados { get; } = new[] { "Disponible", "Bajo inventario", "Agotado", "Inactivo" };

        /// <param name="nivel">"" = todos.</param>
        /// <param name="estado">"" = todos, o uno de <see cref="Estados"/>.</param>
        public List<Libro> Buscar(string texto, string nivel, string estado)
        {
            var q = Normalizar(texto.Trim());
            return _libros
                .Where(l => q.Length == 0 || new[] { l.Titulo, l.Autor, l.Isbn }.Any(c => Normalizar(c).Contains(q)))
                .Where(l => nivel.Length == 0 || l.Nivel == nivel)
                .Where(l => estado.Length == 0 || l.Estado == estado)
                .OrderBy(l => Niveles.ToList().IndexOf(l.Nivel ?? ""))
                .ThenBy(l => l.Titulo)
                .ToList();
        }

        /// <summary>Guarda (alta o cambio). Regresa un mensaje de error, o null si se guardó.</summary>
        public string? Guardar(Libro libro)
        {
            libro.Titulo = libro.Titulo?.Trim();
            libro.Autor = libro.Autor?.Trim();
            libro.Isbn = string.IsNullOrWhiteSpace(libro.Isbn) ? null : libro.Isbn.Trim();
            libro.Ubicacion = libro.Ubicacion?.Trim();

            if (libro.Isbn is not null)
            {
                bool repetido = _libros.Any(l =>
                    l.Id != libro.Id && SoloDigitos(l.Isbn) == SoloDigitos(libro.Isbn));

                if (repetido)
                {
                    return $"Ya existe un libro con el ISBN {libro.Isbn}.";
                }
            }

            if (libro.Id == 0)
            {
                // TODO: el Id real debe generarlo la base de datos
                libro.Id = _libros.Count == 0 ? 1 : _libros.Max(l => l.Id) + 1;
                _libros.Add(libro);
                return null;
            }

            int i = _libros.FindIndex(l => l.Id == libro.Id);
            if (i >= 0) _libros[i] = libro; else _libros.Add(libro);
            return null;
        }

        private static string SoloDigitos(string? s) => new string((s ?? "").Where(char.IsDigit).ToArray());

        private static string Normalizar(string? s)
        {
            var sb = new StringBuilder();
            foreach (var c in (s ?? "").Normalize(NormalizationForm.FormD))
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            return sb.ToString().ToLowerInvariant();
        }

        private static List<Libro> DatosDeEjemplo() => new()
        {
            Nuevo(1, "English File Beginner Student Book", "Christina Latham-Koenig", "978-0194030412", "Básico 1", 24, "Estante A-1"),
            Nuevo(2, "English File Elementary Student Book", "Oxford University Press", "978-0194030504", "Básico 2", 18, "Estante A-2"),
            Nuevo(3, "Vocabulary in Use Elementary", "Michael McCarthy, Felicity O'Dell", "978-1107480537", "Básico 2", 21, "Estante A-3"),
            Nuevo(4, "English Grammar in Use Fourth Edition", "Raymond Murphy", "978-1108586627", "Intermedio", 12, "Estante B-1"),
            Nuevo(5, "English File Intermediate Student Book", "Christina Latham-Koenig", "978-0194030580", "Intermedio", 15, "Estante B-2"),
            Nuevo(6, "TOEFL Preparation Guide 7th Edition", "Educational Testing Service", "978-1268682472", "Avanzado", 4, "Estante C-1"),
            Nuevo(7, "Advanced Grammar in Use", "Martin Hewings", "978-1108921890", "Avanzado", 3, "Estante C-2"),
        };

        private static Libro Nuevo(int id, string titulo, string autor, string isbn, string nivel, int existencias, string ubicacion) => new()
        {
            Id = id, Titulo = titulo, Autor = autor, Isbn = isbn, Nivel = nivel,
            Existencias = existencias, ExistenciaMinima = 5, Ubicacion = ubicacion
        };
    }
}
