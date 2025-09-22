namespace CTFPlatForm.Core.Other
{
    /// <summary>
    /// API结果类
    /// </summary>
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public object Result { get; set; }
        public string Msg { get; set; }
    }
}
