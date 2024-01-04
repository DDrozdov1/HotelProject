namespace AppKursProject.ViewModels.Employees
{
    public enum SortState
    {
        NameAsc,
        NameDesc,
        PositionAsc,
        PositionDesc
    }

    public class EmployeeSortViewModel
    {
        public SortState NameSort { get; }
        public SortState PositionSort { get; }
        public SortState Current { get; }

        public EmployeeSortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            PositionSort = sortOrder == SortState.PositionAsc ? SortState.PositionDesc : SortState.PositionAsc;
            Current = sortOrder;
        }
    }
}