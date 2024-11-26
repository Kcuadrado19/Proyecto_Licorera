using Microsoft.AspNetCore.Identity;

namespace Proyecto_Licorera_Corchos.web.Data.Entities
{
    public class IdentityUserToken
    {

        public class ApplicationUserToken : IdentityUserToken<string>
        {
            public virtual ApplicationUser User { get; set; }
        }

    }
}
