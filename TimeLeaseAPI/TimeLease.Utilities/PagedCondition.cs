namespace Rita.System
{
    /// <summary>
    /// 分页基类
    /// </summary>
    public class PagedCondition
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
    }
}
