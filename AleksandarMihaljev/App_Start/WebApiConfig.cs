using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AleksandarMihaljev.Interface;
using AleksandarMihaljev.Models;
using AleksandarMihaljev.Repository;
using AutoMapper;
using Microsoft.Owin.Security.OAuth;

using Newtonsoft.Json.Serialization;
using TemplateZaTest.Resolver;
using Unity;
using Unity.Lifetime;

namespace AleksandarMihaljev
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var cors = new EnableCorsAttribute("*", "*", "*");

            config.EnableCors(cors);
            config.EnableSystemDiagnosticsTracing();

            var container = new UnityContainer();
            container.RegisterType<IBusRepository, BusRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPassengerRepository, PassengerRepository>(new HierarchicalLifetimeManager());


            config.DependencyResolver = new UnityResolver(container);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Passenger, PassengerDto>();
            });


        }
    }
}
