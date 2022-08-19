namespace examWPF.Domain.Commons.Response
{
    public class BaseResponse<TSource>
    {
        public TSource Data { get; set; }
        public ErrorResponse Error { get; set; } = new ErrorResponse(200, "Success");
    }
}