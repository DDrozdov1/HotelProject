namespace AppKursProject.ViewModels.Customers
{
    public enum SortState
    {
        FullNameAsc, FullNameDesc,
        PassportDataAsc, PassportDataDesc
    }

    public class CustomerSortViewModel
    {
        public SortState FullNameSort { get; }
        public SortState PassportDataSort { get; }
        public SortState Current { get; }

        public CustomerSortViewModel(SortState sortOrder)
        {
            FullNameSort = sortOrder == SortState.FullNameAsc ? SortState.FullNameDesc : SortState.FullNameAsc;
            PassportDataSort = sortOrder == SortState.PassportDataAsc ? SortState.PassportDataDesc : SortState.PassportDataAsc;
            Current = sortOrder;
        }
    }
}
