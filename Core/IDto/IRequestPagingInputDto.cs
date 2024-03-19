
namespace Finance.Core.IDto
{
    public interface IRequestPagingInputDto : IPagingInputDto
    {

        public long? StatusId { get; set; }
        public string? Number { get; set; }
    }
}