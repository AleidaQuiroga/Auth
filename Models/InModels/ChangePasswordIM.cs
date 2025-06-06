namespace Auth.Models.InModels
{
    public class ChangePasswordIM
    {
        public string strEmail { get; set; }
        public string strCurrentPassword { get; set; }
        public string strNewPassword { get; set; }
    }
}
