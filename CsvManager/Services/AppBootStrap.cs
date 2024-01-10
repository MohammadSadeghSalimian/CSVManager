using System.Reflection;
using Autofac;
using CsvManager.ViewModels;
using CsvManager.Views.Ucs;
using CsvManager.Views.Windows;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using ReactiveUI;
using Splat.Autofac;

namespace CsvManager.Services
{
    public class AppBootStrap
    {

        private readonly ContainerBuilder _builder;
       
        public AppBootStrap()
        {
            _builder = new ContainerBuilder();
          
        }


        public void Setup()
        {

            RegisterServices();
            RegisterViewModels();
            RegisterViews();
            SetAutoFac();
        }


        private void RegisterServices()
        {
           
            _builder.RegisterType<MessageUnit>().As<IMessageUnit>().SingleInstance();
            _builder.RegisterType<KeyContainer>().As<IKeyContainer>().SingleInstance();
            _builder.RegisterType<Settings>().As<ISettings>().SingleInstance();
            _builder.RegisterType<DialogBuilder>().As<ICommonDialogBuilder>().SingleInstance();
            var configuration = MediatRConfigurationBuilder
                .Create(typeof(AppBootStrap).Assembly)
                .WithAllOpenGenericHandlerTypesRegistered() // Register all handlers by convention
                .WithRegistrationScope(RegistrationScope.Scoped) // Set the registration scope
                .Build();
            _builder.RegisterMediatR(configuration);


        }

        private void RegisterViews()
        {
            _builder.RegisterType<MainWindow>().As<IViewFor<MainVm>>();
            _builder.RegisterType<CsvMergerUc>().As<IViewFor<MergeVm>>();
        }

         

        private void RegisterViewModels()
        {
            _builder.RegisterType<MainVm>().AsSelf().SingleInstance();
            _builder.RegisterType<MergeVm>().AsSelf().SingleInstance();
          

        }

       

        private void SetAutoFac()
        {
            var autofacResolver = _builder.UseAutofacDependencyResolver();
            _builder.RegisterInstance(autofacResolver);
            autofacResolver.InitializeReactiveUI();
            var container = _builder.Build();
            container.Resolve<AutofacDependencyResolver>();
            autofacResolver.SetLifetimeScope(container);
        }
    }
    
}
