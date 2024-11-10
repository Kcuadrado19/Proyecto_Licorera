namespace Proyecto_Licorera_Corchos.web.Core.Pagination
{
    public class PaginationRequest
    {
        private int _page = 1; // lady: Página inicial predeterminada
        private int _recordsPerPage = 15; // lady: Registros por página predeterminados
        private int _maxRecordsPerPage = 50; // lady: Máximo de registros permitidos por página

        // lady: Filtro opcional para buscar o filtrar los datos
        public string? Filter { get; set; }

        // lady: Propiedad para la página actual con validación
        public int Page
        {
            get => _page;
            set => _page = value > 0 ? value : _page; // lady: Se asegura de que el valor de la página sea mayor que 0
        }

        // lady: Propiedad para los registros por página con validación
        public int RecordsPerPage
        {
            get => _recordsPerPage;
            set => _recordsPerPage = value <= _maxRecordsPerPage ? value : _maxRecordsPerPage; // lady: Limita los registros por página al máximo permitido
        }
    }
}
