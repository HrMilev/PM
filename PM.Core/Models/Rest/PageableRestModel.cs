namespace PM.Common.Models.Rest
{
    public class PageableRestModel
    {
        public int Page { get; set; } = 1;
        public int EntitiesPerPage { get; set; } = 10;
    }
}
