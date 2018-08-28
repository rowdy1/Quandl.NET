namespace Quandl.NET.Model
{
    public class TableMeta
    {
        public TableMeta(string next_cursor_id)
        {
            NextCursorId = next_cursor_id;
        }

        public string NextCursorId { get; private set; }
    }
}