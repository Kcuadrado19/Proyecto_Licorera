﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Proyecto_Licorera_Corchos.web.Data;

namespace Proyecto_Licorera_Corchos.web
{

    //método de extensión, es estatico
    //this - parametrización por referencia
    public static class CustomConfiguration
    {
        public static WebApplicationBuilder AddCustomBuilderConfiguration(this WebApplicationBuilder builder)
        {
            //aca se hacen las modificaciones al builder

            //data context

            builder.Services.AddDbContext<DataContext>(configuration =>
            {
                configuration.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
            });


            return builder;
        }
    }
}
