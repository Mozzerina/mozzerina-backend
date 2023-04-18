namespace Mozzerina.Data.DTO
{
    public class ResponseDto
    {
        public required string Name { get; set; }
        public List<object> Items { get; set; }
    }
}
