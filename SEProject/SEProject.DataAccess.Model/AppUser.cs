using Microsoft.AspNetCore.Identity;

namespace SEProject.DataAccess.Model
{
    public class AppUser : IdentityUser
    {
        public int? PoliceStationId { get; set; }
        public PoliceStation PoliceStation { get; set; }
    }
}
