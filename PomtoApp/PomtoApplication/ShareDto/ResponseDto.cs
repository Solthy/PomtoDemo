namespace PomtoApplication.ShareDto
{
    public class ResponseDto<TData>
    {
        public TData? Data { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public bool IsSucess { get; set; }
    }
}