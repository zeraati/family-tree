using FamilyTree.Model.Enum.ResponseEnum;

namespace FamilyTree.Model
{
    public class ServiceResponseDTO
    {
        public ServiceResponseDTO(ResponseStatusEnum status,string message)
        {
            Status = status;
            Message = message;
        }

        public ResponseStatusEnum Status { get; set; }
        public string Message { get; set; }
        public string? ExceptionMessage { get; set; }

        public static ServiceResponseDTO CreatedSuccessfully = new(ResponseStatusEnum.Success,ResponseMessageEnum.CreatedSuccessfully.ToString());
        public static ServiceResponseDTO UpdatedSuccessfully = new(ResponseStatusEnum.Success,ResponseMessageEnum.UpdatedSuccessfully.ToString());
        public static ServiceResponseDTO DeletedSuccessfully = new(ResponseStatusEnum.Success,ResponseMessageEnum.DeletedSuccessfully.ToString());
        public static ServiceResponseDTO UploadSuccessfully = new(ResponseStatusEnum.Success,ResponseMessageEnum.UploadSuccessfully.ToString());
    }
    public class ServiceResponseDTO<T>
    {
        public ServiceResponseDTO(T data) => Data = data;
        public T Data { get; set; }
    }
}