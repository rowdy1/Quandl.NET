namespace Quandl.NET.Model.Response
{
    public class TableMetadataResponse
    {
        public TableMetadataResponse(TableMetadata datatable)
        {
            Datatable = datatable;
        }

        public TableMetadata Datatable { get; }
    }
}