namespace PomtoApplication.ShareDto
{
    public class ResponseListDto<TDataList>
    {
        public string Mensagem { get; set; } = string.Empty;
        public bool IsSucess { get; set; }
        public int NumeroPagina { get; set; }
        public int TotalPagina { get; set; }
        public List<TDataList> Data { get; set; } = new();
    }
}