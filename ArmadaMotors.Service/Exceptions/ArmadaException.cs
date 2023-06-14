namespace ArmadaMotors.Service.Exceptions
{
    public class ArmadaException : Exception
    {
        public int Code { get; set; }
        public ArmadaException(int code = 500, string message = "Something went wrong")
            : base(message)
        {
            this.Code = code;
        }
    }
}
