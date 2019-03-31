using Community.Domain.Abstract;
using Community.Domain.Concrete;
using Community.Domain.Entities;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Community.WebUI.Infrastructure
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            //        Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //        mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //    new Product { Name = "SimCity", Price = 1499 },
            //    new Product { Name = "TITANFALL", Price=2299 },
            //    new Product { Name = "Battlefield 4", Price=899.4 }
            //});
            // Bind<IProductRepository>().ToConstant(mock.Object);
            Bind<IUserRepository>().To<EFUserRepository>();
            Bind<IAddressRepository>().To<EFAddressRepository>();
            Bind<IDescriptionRepository>().To<EFDescriptionRepository>();
            Bind<IFrendRepository>().To<EFFrendRepository>();
            Bind<IGroupRepository>().To<EFGroupRepository>();
            Bind<IRoleRepository>().To<EFRoleRepository>();
            Bind<IUserFileRepository>().To<EFUserFileRepository>();

            Bind<IUserGroupRepository>().To<EFUserGroupRepository>();
            Bind<IUserSetingRepository>().To<EFUserSetingRepository>();
            Bind<IPostRepository>().To<EFPostRepository>();
            Bind<IUserInfo>().To<EFUserInfo>();
            Bind<IGenderRepository>().To<EFGenderRepository>();
            //EmailSettings emailSettings = new EmailSettings
            //{
            //    WriteAsFile = bool.Parse(ConfigurationManager
            //        .AppSettings["Email.WriteAsFile"] ?? "false")
            //};
            //Bind<IOrderProcessor>().To<EmailOrderProcessor>()
            //     .WithConstructorArgument("settings", emailSettings);
        }


    }
}