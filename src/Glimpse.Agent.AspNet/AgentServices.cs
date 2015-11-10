using System.IO;
using Glimpse.Common.Initialization;
using Glimpse.Agent;
using Glimpse.Agent.Internal.Messaging;
using Glimpse.Agent.Configuration;
using Glimpse.Agent.Inspectors;
using Glimpse.Agent.Internal.Inspectors.Mvc;
using Glimpse.Initialization;
using System.Linq;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;
using Microsoft.Framework.OptionsModel;

namespace Glimpse
{
    public class AgentServices : IRegisterServices
    {
        public void RegisterServices(GlimpseServiceCollectionBuilder services)
        {
            services.AddOptions();

            RegisterPublisher(services);

            //
            // Broker
            //
            services.AddSingleton<IAgentBroker, DefaultAgentBroker>();

            //
            // Options
            //
            services.AddTransient<IConfigureOptions<GlimpseAgentOptions>, GlimpseAgentOptionsSetup>();
            services.AddSingleton<IRequestIgnorerUriProvider, DefaultRequestIgnorerUriProvider>();
            services.AddSingleton<IRequestIgnorerStatusCodeProvider, DefaultRequestIgnorerStatusCodeProvider>();
            services.AddSingleton<IRequestIgnorerContentTypeProvider, DefaultRequestIgnorerContentTypeProvider>();
            services.AddSingleton<IExtensionProvider<IRequestIgnorer>, DefaultExtensionProvider<IRequestIgnorer>>();
            services.AddSingleton<IExtensionProvider<IInspectorFunction>, DefaultExtensionProvider<IInspectorFunction>>();
            services.AddSingleton<IExtensionProvider<IInspector>, DefaultExtensionProvider<IInspector>>();
            services.AddSingleton<IExtensionProvider<IAgentStartup>, DefaultExtensionProvider<IAgentStartup>>();

            //
            // Messages.
            //
            services.AddSingleton<IMessageConverter, DefaultMessageConverter>();
            services.AddTransient<IMessagePayloadFormatter, DefaultMessagePayloadFormatter>();
            services.AddTransient<IMessageIndexProcessor, DefaultMessageIndexProcessor>();
            services.AddTransient<IMessageTypeProcessor, DefaultMessageTypeProcessor>();

            //
            // Common
            //
            services.AddTransient<IAgentStartupManager, DefaultAgentStartupManager>();
            services.AddTransient<IRequestIgnorerManager, DefaultRequestIgnorerManager>();
            services.AddTransient<IInspectorFunctionManager, DefaultInspectorFunctionManager>();
            services.AddTransient<IExceptionProcessor, ExceptionProcessor>();
            services.AddTransient<WebDiagnosticsInspector>();

            if (!services.Any(s => s.ServiceType == typeof(IResourceOptionsProvider)))
                services.AddSingleton<IResourceOptionsProvider, OptionsResourceOptionsProvider>();
        }

        private void RegisterPublisher(GlimpseServiceCollectionBuilder services)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(configurationBuilder.GetBasePath(), "glimpse.json");

            if (File.Exists(path))
            {
                var configuration = configurationBuilder.AddJsonFile("glimpse.json").Build();
                services.Configure<ResourceOptions>(configuration.GetSection("resources"));

                services.Replace(new ServiceDescriptor(typeof(IMessagePublisher), typeof(HttpMessagePublisher), ServiceLifetime.Transient));
            }

            // TODO: If I reach this line, than Glimpse has no way to send data from point A to B. Should we blow up?
        }
    }
}