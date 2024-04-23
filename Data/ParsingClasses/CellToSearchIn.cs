namespace UmkConstructor.Data.ParsingClasses
{
    public enum CellDirection
    {
        AtRight, AtLeft, Below, Under
    }

    public class CellToSearchIn
    {
        public CellDirection CellDirection = CellDirection.AtRight;

        // на следующей строке/следующем столбце в указанном направлении
        public int Offset = 1;

        public CellToSearchIn() { }

        public CellToSearchIn(CellDirection cellDirection)
        {
            CellDirection = cellDirection;
        }

        public CellToSearchIn(int offset)
        {
            Offset = offset;
        }

        public CellToSearchIn(CellDirection cellDirection, int offset) 
        {
            CellDirection = cellDirection;
            Offset = offset;
        }
    }
}
