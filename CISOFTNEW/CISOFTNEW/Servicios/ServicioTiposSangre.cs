using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace CISOFTNEW.Servicios
{
    /// <summary>Tipo sanguíneo (grupo ABO + factor Rh). Son 8 fijos: solo se edita la descripción y el estado.</summary>
    public class TipoSangre
    {
        public int Id { get; set; }

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

    /// <summary>
    /// Punto único de acceso a los tipos de sangre para todas las pantallas.
    /// HOY: usa una lista en memoria con los 8 tipos.
    /// DESPUÉS: aquí se conecta CISOFTDataOperations sin tocar las pantallas.
    /// </summary>
    public class ServicioTiposSangre
    {
        private readonly List<TipoSangre> _tipos = DatosIniciales();

        /// <param name="estado">"" = todos, "Activo" o "Inactivo".</param>
        public List<TipoSangre> Buscar(string texto, string estado)
        {
            var q = Normalizar(texto.Trim()).Replace("-", "−");
            return _tipos
                .Where(t => q.Length == 0
                         || Normalizar(t.Nombre).Contains(q)
                         || Normalizar($"{t.Grupo} {(t.RhPositivo ? "positivo" : "negativo")}").Contains(q)
                         || Normalizar(t.Descripcion).Contains(q))
                .Where(t => estado.Length == 0 || t.Activo == (estado == "Activo"))
                .ToList();
        }

        /// <summary>Guarda los cambios de un tipo existente (no se pueden crear nuevos).</summary>
        public void Guardar(TipoSangre tipo)
        {
            tipo.Descripcion = tipo.Descripcion?.Trim();

            int i = _tipos.FindIndex(t => t.Id == tipo.Id);
            if (i >= 0) _tipos[i] = tipo;
        }

        private static string Normalizar(string? s)
        {
            var sb = new StringBuilder();
            foreach (var c in (s ?? "").Normalize(NormalizationForm.FormD))
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            return sb.ToString().ToLowerInvariant();
        }

        private static List<TipoSangre> DatosIniciales() => new()
        {
            Nuevo(1, "A", true, "Antígeno A en la superficie de los glóbulos rojos."),
            Nuevo(2, "A", false, "Antígeno A en la superficie de los glóbulos rojos."),
            Nuevo(3, "B", true, "Antígeno B en la superficie de los glóbulos rojos."),
            Nuevo(4, "B", false, "Antígeno B en la superficie de los glóbulos rojos."),
            Nuevo(5, "AB", true, "Antígenos A y B en la superficie de los glóbulos rojos."),
            Nuevo(6, "AB", false, "Antígenos A y B en la superficie de los glóbulos rojos."),
            Nuevo(7, "O", true, "Sin antígenos A ni B en la superficie de los glóbulos rojos."),
            Nuevo(8, "O", false, "Sin antígenos A ni B en la superficie de los glóbulos rojos."),
        };

        private static TipoSangre Nuevo(int id, string grupo, bool rhPositivo, string descripcion) => new()
        {
            Id = id, Grupo = grupo, RhPositivo = rhPositivo, Descripcion = descripcion
        };
    }
}
