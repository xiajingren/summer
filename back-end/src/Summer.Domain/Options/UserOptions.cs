namespace Summer.Domain.Options
{
    public class UserOptions
    {
        public int UserNameRequiredLength { get; set; } = 5;

        public int PasswordRequiredLength { get; set; } = 6;

        public bool PasswordRequireLowercase { get; set; } = true;

        public bool PasswordRequireUppercase { get; set; } = true;

        public bool PasswordRequireDigit { get; set; } = true;
    }
}